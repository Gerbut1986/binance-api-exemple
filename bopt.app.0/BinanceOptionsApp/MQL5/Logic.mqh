#property strict

#define NAME "WesternpipsPrivate7"
#define COPYRIGHT "Copyright © 2007-2019 Westernpips.com All Rights Reserved."
#define LINK "https://westernpips.com"
#define VERSION "7.5"
#define ICON "icon.ico"

input string ChannelName="";
input uint ExpertSleepMs=0;
input bool UseClicker=true;

#include "Connector.mqh"
#ifdef __MQL5__
#include "ClickerMT5.mqh"
#include <Trade/Trade.mqh>
CPositionInfo pinfo;
CTrade trade;
Clicker clicker;
#endif
#ifdef __MQL4__
#include "ClickerMT4.mqh"
MT4Control clicker(10);
#endif

string _symbol;
int _digits;
string channelName;
mtsPipeMessageClient client;
bool connected;
string prefix;
datetime lastViewTime;
int sendMs;
int sendSleepMs;

struct TOrder
{
   bool buy;
   long ticket;
   double openprice;
   datetime opentime;
   double lot;
   double pnl;
   int magic;
};
TOrder orders[];

int _OnInit()
{
   EventSetMillisecondTimer(ExpertSleepMs>0 ? ExpertSleepMs : 1);
   _symbol=Symbol();
   _digits=Digits();
   channelName = StringLen(ChannelName)>0 ? ChannelName : _symbol;
   client.initialize("myfreedom_"+channelName);
   connected=false;
   prefix="myfreedom_";
   lastViewTime=0;
   sendMs=250;
   sendSleepMs=10;
   return INIT_SUCCEEDED;
}
void _OnDeinit(const int reason)
{
   EventKillTimer();
   ObjectsDeleteAll(0,prefix);
}
void _OnTick(bool _timer)
{
   double bid=0;
   double ask=0;
   if (_timer)
   {
      #ifdef __MQL4__
      RefreshRates();
      #endif
   }
   bid=SymbolInfoDouble(Symbol(),SYMBOL_BID);
   ask=SymbolInfoDouble(Symbol(),SYMBOL_ASK);
   
   int n=scanOrders();
   
   mtsVEncoder encoder;
   encoder.addInt(1);//version
   encoder.addInt(1);//command quotes[1]
   encoder.addDouble(bid);
   encoder.addDouble(ask);
   encoder.addUInt64((ulong)TimeCurrent());
   encoder.addInt(n);
   for (int i=0;i<n;i++)
   {
      encoder.addInt(orders[i].buy ? 1 : 0);
      encoder.addInt64(orders[i].ticket);
      encoder.addDouble(orders[i].openprice);
      encoder.addUInt64((ulong)orders[i].opentime);
      encoder.addDouble(orders[i].lot);
      encoder.addDouble(orders[i].pnl);
      encoder.addInt(orders[i].magic);
   }
   double balance=AccountInfoDouble(ACCOUNT_BALANCE);
   double equity=AccountInfoDouble(ACCOUNT_EQUITY);
   encoder.addDouble(balance);
   encoder.addDouble(equity);
   
   if (client.send(encoder.data,sendMs,sendSleepMs))
   {
      mtsVDecoder decoder(client.response);
      int ver=decoder.readInt();
      if (ver>0)
      {
         connected=true;
         int cmd=decoder.readInt();
         if (cmd==1 || cmd==2) // buy,sell
         {
            double lot=decoder.readDouble();
            int magic=decoder.readInt();
            int slippage=decoder.readInt();
            double openPrice=0;
            int execution=0;
            long ticket=sendOrder(bid,ask,cmd==1,lot,magic,slippage,openPrice,execution);
            mtsVEncoder answer;
            answer.addInt(1);//version
            answer.addInt(2);//command open result
            answer.addInt64(ticket);
            answer.addDouble(openPrice);
            answer.addInt(execution);
            client.send(answer.data,sendMs,sendSleepMs);
         }
         else
         if (cmd==3) // close
         {
            long ticket=decoder.readInt64();
            int slippage=decoder.readInt();
            int execution=0;
            double closePrice=closeOrder(bid,ask,ticket,slippage,execution);
            mtsVEncoder answer;
            answer.addInt(1);//version
            answer.addInt(3);//command close result
            answer.addDouble(closePrice);
            answer.addInt(execution);
            client.send(answer.data,sendMs,sendSleepMs);            
         }
         else
         if (cmd==4) // modify
         {
            long ticket=decoder.readInt64();
            double slPrice=decoder.readDouble();
            double tpPrice=decoder.readDouble();
            int execution=0;
            modifyOrder(ticket,slPrice,tpPrice,execution);
            mtsVEncoder answer;
            answer.addInt(1);//version
            answer.addInt(4);//command modify result
            answer.addInt(execution);
            client.send(answer.data,sendMs,sendSleepMs);                        
            
         }
      }
      else
      {
         connected=false;
      }
   }
   else
   {
      connected=false;
   }
   datetime utcnow=TimeGMT();
   if ((utcnow-lastViewTime)>=1)
   {
      lastViewTime=utcnow;
      drawCommentRectangle(5);
      drawComment(1,"Westernpips Private 7 Connector",C'0x00,0x1c,0x38',"");
      drawComment(3,connected ? channelName+" connected" : channelName+" disconnected", connected ? C'0x00,0x6d,0xc5' : C'0xff,0x3d,0x00',"");
      drawComment(3,channelName,clrWhite,"top");
      drawComment(4,TimeToString(utcnow,TIME_MINUTES|TIME_SECONDS),C'0x00,0x1c,0x38',"");
   }
}
void _OnChartEvent(const int id,const long &lparam,const double &dparam,const string &sparam)
{
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
int measure_ticks_dif(uint _ticks_end, uint _ticks_start)
{
   if (_ticks_end>=_ticks_start)
   {
      return (int)(_ticks_end-_ticks_start);
   }
   else
   {
      return (int)(_ticks_end+(0xffffffff-_ticks_start));
   }
}
long sendOrder(double _bid, double _ask, bool _buy,double _lot,int _magic,int _slippage,double &_openedAt, int &_execution)
{
   _openedAt=0;
   _execution=0;
   
   uint time0=GetTickCount();
   if (!UseClicker)
   {
      #ifdef __MQL4__
      long ticket=(long)OrderSend(Symbol(),_buy ? OP_BUY : OP_SELL,_lot,_buy ? _ask : _bid, _slippage,0,0,NULL,_magic);
      if (ticket>=0)
      {
         if (OrderSelect((int)ticket,SELECT_BY_TICKET))
         {
            _openedAt=OrderOpenPrice();
      #endif
      #ifdef __MQL5__
      trade.SetDeviationInPoints(_slippage);
      trade.SetExpertMagicNumber(_magic);
      if (trade.PositionOpen(Symbol(),_buy ? ORDER_TYPE_BUY : ORDER_TYPE_SELL,_lot,_buy ? _ask : _bid,0,0))
      {
         long ticket=(long)trade.ResultOrder();
         if (pinfo.SelectByTicket(ticket))
         {
            _openedAt=pinfo.PriceOpen();
      #endif
            _execution=measure_ticks_dif(GetTickCount(),time0);
            return ticket;
         }
      }
   }
   else
   {
#ifdef __MQL4__
      long ticket=(long)clicker.orderSend(Symbol(),_buy ? OP_BUY : OP_SELL,_lot,_slippage,0,0,NULL);
      if (ticket>=0)
      {
         if (OrderSelect((int)ticket,SELECT_BY_TICKET))
         {
            _openedAt=OrderOpenPrice();
            _execution=measure_ticks_dif(GetTickCount(),time0);
            return ticket;
         }
      }
#endif
#ifdef __MQL5__
      long ticket=-1;
      if (_buy) clicker.clickBuy(Symbol(),_lot); else clicker.clickSell(Symbol(),_lot);
      if (pinfo.Select(Symbol()))
      {
         ticket=(long)pinfo.Ticket();
         _openedAt=pinfo.PriceOpen();
         _execution=measure_ticks_dif(GetTickCount(),time0);
         return ticket;
      }
#endif
   }
   return -1;
}
double closeOrder(double _bid, double _ask,long _ticket, int _slippage, int &_execution)
{
   uint time0=GetTickCount();
   double result=0.0;
   if (!UseClicker)
   {
      #ifdef __MQL4__
      if (OrderSelect((int)_ticket,SELECT_BY_TICKET))
      {
         if (OrderClose((int)_ticket,OrderLots(),OrderType()==OP_BUY ? _bid : _ask, _slippage))
         {
            if (OrderSelect((int)_ticket,SELECT_BY_TICKET,MODE_HISTORY))
            {
               result=OrderClosePrice();         
            }
         }
      }
      #endif
      #ifdef __MQL5__
      if (pinfo.SelectByTicket(_ticket))
      {
         trade.SetDeviationInPoints(_slippage);
         if (trade.PositionClose(pinfo.Ticket()))
         {
            result=trade.ResultPrice();
         }
      }
      #endif
   }
   else
   {
      #ifdef __MQL4__
      if (OrderSelect((int)_ticket,SELECT_BY_TICKET))
      {
         if (clicker.orderClose((int)_ticket,OrderLots(),_slippage))
         {
            if (OrderSelect((int)_ticket,SELECT_BY_TICKET,MODE_HISTORY))
            {
               result=OrderClosePrice();         
            }
         }
      }
      #endif
      #ifdef __MQL5__
      if (pinfo.SelectByTicket(_ticket))
      {
         clicker.clickClose(_ticket);
         result=0;         
      }
      #endif
   }
   _execution=measure_ticks_dif(GetTickCount(),time0);
   return result;
}
void modifyOrder(long _ticket, double slPrice, double tpPrice, int &_execution)
{
   slPrice=NormalizeDouble(slPrice,Digits());
   tpPrice=NormalizeDouble(tpPrice,Digits());
   uint time0=GetTickCount();
   #ifdef __MQL4__
   if (OrderModify((int)_ticket,0,slPrice,tpPrice,0))
   {
   }
   #endif
   #ifdef __MQL5__
   if (trade.PositionModify(pinfo.Ticket(),slPrice,tpPrice))
   {
   }
   #endif
   _execution=measure_ticks_dif(GetTickCount(),time0);
}
int scanOrders()
{
   int pos=0;
   ArrayResize(orders,0);
   
   #ifdef __MQL4__
   int n=OrdersTotal();
   for (int i=0;i<n;i++)
   {
      if (!OrderSelect(i,SELECT_BY_POS)) continue;
      if (OrderType()>1) continue;
      if (OrderSymbol()==_symbol)
      {
         ArrayResize(orders,pos+1);
         orders[pos].buy=OrderType()==OP_BUY;
         orders[pos].ticket=(long)OrderTicket();
         orders[pos].openprice=OrderOpenPrice();
         orders[pos].opentime=OrderOpenTime();
         orders[pos].lot=OrderLots();
         orders[pos].pnl=OrderProfit()+OrderSwap()+OrderCommission();
         orders[pos].magic=OrderMagicNumber();
         pos++;
      }
   }
   #endif
   #ifdef __MQL5__
   int n=PositionsTotal();
   for (int i=0;i<n;i++)
   {
      if (!pinfo.SelectByIndex(i)) continue;
      if (pinfo.Symbol()==_symbol)
      {
         ArrayResize(orders,pos+1);
         orders[pos].buy=pinfo.PositionType()==POSITION_TYPE_BUY;
         orders[pos].ticket=(long)pinfo.Ticket();
         orders[pos].openprice=pinfo.PriceOpen();
         orders[pos].opentime=pinfo.Time();
         orders[pos].lot=pinfo.Volume();
         orders[pos].pnl=pinfo.Profit()+pinfo.Swap()+pinfo.Commission();
         orders[pos].magic=(int)pinfo.Magic();
         pos++;
      }
   }
   #endif
   return pos;
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
void drawComment(int _line,string _text, color _clr, string _extraId)
{
   int lineSize=18;
   int fontSize=10;
   int x=10;
   int y=20+_line*lineSize;
   string textName=prefix+"_cmt"+IntegerToString(_line)+"_"+_extraId;
   if (ObjectFind(0,textName)!=0)
   {
      ObjectCreate(0,textName,OBJ_LABEL,0,0,0);
      ObjectSetInteger(0,textName,OBJPROP_XDISTANCE,x);
      ObjectSetInteger(0,textName,OBJPROP_YDISTANCE,y);
      ObjectSetInteger(0,textName,OBJPROP_CORNER,CORNER_LEFT_UPPER);
      ObjectSetInteger(0,textName,OBJPROP_BACK,0);
      ObjectSetInteger(0,textName,OBJPROP_SELECTABLE,0);
      ObjectSetInteger(0,textName,OBJPROP_SELECTED,0);
      ObjectSetInteger(0,textName,OBJPROP_HIDDEN,1);
      ObjectSetString(0,textName,OBJPROP_FONT,"Tahoma");
      ObjectSetInteger(0,textName,OBJPROP_FONTSIZE,fontSize);
   }
   ObjectSetInteger(0,textName,OBJPROP_COLOR,_clr);
   ObjectSetString(0,textName,OBJPROP_TEXT,_text);
}
void drawCommentRectangle(int _lines)
{
   string textName=prefix+"_bkg";
   if (ObjectFind(0,textName)!=0)
   {
      ObjectCreate(0,textName,OBJ_RECTANGLE_LABEL,0,0,0);
      ObjectSetInteger(0,textName,OBJPROP_YDISTANCE,15+20);
      ObjectSetInteger(0,textName,OBJPROP_XSIZE,200);
      ObjectSetInteger(0,textName,OBJPROP_CORNER,CORNER_LEFT_UPPER);
      ObjectSetInteger(0,textName,OBJPROP_BGCOLOR,C'0xb6,0xd8,0x00');
      ObjectSetInteger(0,textName,OBJPROP_BORDER_COLOR,C'0xb6,0xd8,0x00');
      ObjectSetInteger(0,textName,OBJPROP_COLOR,C'0xb6,0xd8,0x00');
      ObjectSetInteger(0,textName,OBJPROP_BORDER_TYPE,BORDER_FLAT);
      ObjectSetInteger(0,textName,OBJPROP_WIDTH,1);
      ObjectSetInteger(0,textName,OBJPROP_BACK,0);
      ObjectSetInteger(0,textName,OBJPROP_SELECTABLE,0);
      ObjectSetInteger(0,textName,OBJPROP_SELECTED,0);
      ObjectSetInteger(0,textName,OBJPROP_HIDDEN,1);
   }
   ObjectSetInteger(0,textName,OBJPROP_XDISTANCE,_lines==0 ? -500 : 5);
   ObjectSetInteger(0,textName,OBJPROP_YSIZE,_lines*20);
}