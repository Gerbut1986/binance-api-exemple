#property strict

#ifdef __MQL4__
#import "user32.dll"
	int GetAncestor(int, int);
	int GetDlgItem(int, int);
 	int SetDlgItemTextA(int, int, string);
	int SetDlgItemTextW(int, int, string);
	int SendDlgItemMessageA(int, int, int, int, int);
	int SendDlgItemMessageW(int, int, int, int, int);
	int GetClientRect(int, int &lpRect[]);
	int GetLastActivePopup(int);
 	int FindWindowExA(int, int, string, string);
 	int FindWindowExW(int, int, string, string);
	int GetParent(int);
	int SendMessageA(int, int, int, int);
	int SendMessageW(int, int, int, int);
	int PostMessageA(int, int, int, int); 
	int PostMessageW(int, int, int, int); 	
   int GetWindowTextA(int hWnd, char &data[], int nMaxCount);
	int GetWindow(int, int);
	int IsWindow(int);
	int IsWindowVisible(int);
	int IsWindowEnabled(int);
#import
#import "kernel32.dll"
	int lstrcat(string, string);
#import
 

class MT4Control
{
private:
   int VK_HOME() { return 0x24; }
   int VK_DOWN() { return 0x28; }
   int VK_RIGHT() { return 0x27; }
   int BM_GETCHECK() { return 0x00F0; }
   int BM_CLICK() { return 0x00F5; }
   int CB_SELECTSTRING() { return 0x014D; }
   int CB_SETCURSEL() { return 0x014E; }
   int CBN_SELCHANGE() { return 1; }
   int WM_CHAR() { return 0x0102; }
   int WM_CLOSE() { return 0x0010; }
   int WM_COMMAND() { return 0x0111; }
   int WM_KEYDOWN() { return 0x0100; }
   int WM_LBUTTONUP() { return 0x0202; }
   int WM_LBUTTONDOWN() { return 0x0201; }
   int WM_LBUTTONDBLCLK() { return 0x0203; }
   int GW_CHILD() { return 5; }
   string GV_BUSY() { return "_mt4_control_"; }

   int sleepMs;
   int handle_mt;
   int handle_order;
   int handle_panel;
   int lv;
   int sh;
public:
   MT4Control(int _sleepMs)
   {
      sleepMs=_sleepMs;
      handle_mt=0;
      handle_order=0;
      handle_panel=0;
      lv=0;
      sh=0;
	   handle_mt = GetAncestor(WindowHandle(Symbol(),Period()), 2);
	   lv = GetDlgItem(GetDlgItem(GetDlgItem(handle_mt,0xE81E),0x51), 0x81C1);
	   sh = GetWindow(lv, GW_CHILD());
   }
private:
   int posByTicket(int _ticket)
   {
	   int r[], o[];
	   ArrayResize(r,0);
	   ArrayResize(o,0);
      int t=OrdersTotal();
	   for(int i11=0;i11<t;i11++)
	   {
		   if(OrderSelect(i11,SELECT_BY_POS,MODE_TRADES))
		   {
   		   if(OrderType()==OP_BUY||OrderType()==OP_SELL)
   		   {
   			   int l=ArraySize(r);
   			   ArrayResize(r,l+1);
   			   r[l]=OrderTicket();
   		   }
   		   else
   		   {
   			   int l=ArraySize(o);
   			   ArrayResize(o,l+1);
   			   o[l]=OrderTicket();
   		   }
         }
	   }
	   {
   	   int l=ArraySize(r);
   	   if ( l > 0 ) ArraySort(r);
   	   for(int i0=0;i0<l;i0++)
   	   {
   		   if(r[i0]==_ticket)return(i0);
   	   }
	   }
	   {
   	   int l=ArraySize(o);
   	   if ( l > 0 ) ArraySort(o);
   	   for(int i1=0;i1<l;i1++)
   	   {
   		   if(o[i1]==_ticket)return(i1+1+ArraySize(r));
   	   }
	   }
	   return -1;
   }
   bool orderSelect(int _ticket)
   {
   	if (_ticket==0) return(true);
   	int Rect[4], l=50;
   	int p=posByTicket(_ticket);
   	if (p!=-1)
   	{
   		GetClientRect(sh, Rect);
   		PostMessageW(sh, WM_LBUTTONDOWN(), 0, Rect[2]-l);
   		PostMessageW(sh, WM_LBUTTONUP(), 0, Rect[2]-l);
   		PostMessageW(sh, WM_LBUTTONDOWN(), 0, l);
   		PostMessageW(sh, WM_LBUTTONUP(), 0, l);
   		PostMessageW(lv, WM_KEYDOWN(), VK_HOME(), 0);
   		for(int i2=0;i2<p;i2++)
   		{
   			PostMessageW(lv, WM_KEYDOWN(), VK_DOWN(), 0);
   		}
   		return(true);
   	}
   	return(false);
   }
   bool findPanel()
   {
   	handle_panel = 0;
   	for ( int try = 0; try < 100 && !IsStopped(); try ++ )
   	{
   		handle_panel = FindWindowExW( handle_order, handle_panel, "#32770", "" );
   		if ( IsWindowVisible( handle_panel ) != 0 ) return true;
   		Sleep(1);
   	}
   	return false;
   }
   string getWindowText(int _handle)
   {
      char txt[256];
      ArrayInitialize(txt,0);
      GetWindowTextA((int)_handle,txt,20);
      return CharArrayToString(txt,0,WHOLE_ARRAY,CP_UTF8);
   }
   bool ticketInTitle(int _handle, int _ticket)
   {
   	if (_ticket == 0 ) return(true);
		string wtext=getWindowText(_handle);
		if ( StringFind( getWindowText(_handle), "#"+IntegerToString(_ticket) ) != -1 ) return( true );
		PostMessageW( handle_order, WM_CLOSE(), 0, 0 );
		Sleep(10);
   	return(false);
   }
   bool openDialog(int _ticket, int _cmd)
   {
   	if (isTradeContextBusy())
   	{
   		return(false);
   	}
   	if(!orderSelect(_ticket)) return(false);
   
   	PostMessageW(handle_mt, WM_COMMAND(), _cmd, 0);
   
   	if ( !findOrder() ) return(false);
   	if ( !findPanel() ) return(false);
   	if ( !ticketInTitle( handle_order, _ticket ) )
   	{
   		if ( !ticketInTitle( handle_order, _ticket ) ) return(false);
   	}
   
   	GlobalVariableSet(GV_BUSY(), handle_order);
   	return(true);
   }
   bool findOrder()
   {
   	handle_order = handle_mt;
   	for ( int try = 0; try < 100 && !IsStopped(); try ++ )
   	{
   		handle_order = GetLastActivePopup( handle_mt );
   		if ( handle_order != handle_mt && handle_order != 0 ) return(true);
   		Sleep(1);
   	}
   	return(false);
   }
   bool sendMessageCB(int _w, int _i3, int _m, int _a, int _button)
   {
   	int h = GetDlgItem(_w, _i3);
   	SendMessageW(h, _m, _a, _button);
   	return(SendMessageW(_w, WM_COMMAND(), (CBN_SELCHANGE()<<16)+_i3, h)!=0);
   }
   bool validOrder(int _ticket)
   {
   	if ( OrderSelect( _ticket, SELECT_BY_TICKET, MODE_TRADES ) ) return(true);
   	return(false);
   }
   string getDlgItemText(int _w, int _i1)
   {
   	int h = GetDlgItem(_w, _i1);
   	return(getWindowText(h));
   }
   void charPaste(int _h, string _s)
   {
   	int i4,l=StringLen(_s),q;
   	for(i4=0;i4<l;i4++)
   	{
   		string c=StringSubstr(_s,i4,1);
   		if(c==".") q=46; else q=48+StrToInteger(c);
   		PostMessageW(_h, WM_CHAR(), q, 0);
   		Sleep(sleepMs/10);
   	}
   }
   bool setPrice(int _w, int _d, string _s)
   {
   	int h=GetDlgItem(_w, _d);
   	if ( !IsWindowEnabled(h) ) return(false);
   	PostMessageW(h, WM_LBUTTONDBLCLK(), 0, 0);
   	charPaste( h, _s );
   	return(true);
   }
   void setSlippage(int _v)
   {
   	int h=GetDlgItem(handle_panel, 0x453);
   	if(!IsWindowEnabled(h))return;
   	bool e=SendMessageW(h, BM_GETCHECK(), 0, 0);
   	if((_v&&!e)||(!_v&&e))
   	{
   		PostMessageW(h, WM_LBUTTONDOWN(), 0, 0);
   		PostMessageW(h, WM_LBUTTONUP(), 0, 0);
   	}
   	SetDlgItemTextW(handle_panel, 0x450, IntegerToString(_v));
   }
   int getResult(int _button)
   {
   	Sleep(sleepMs);
   	SendDlgItemMessageW(handle_panel, _button, BM_CLICK(), 0, 0);
   	string sr="";
   	int r = 0;
   
      datetime startWait=TimeGMT();
   	while (!IsStopped())
   	{
   		findPanel();
   		sr=getDlgItemText(handle_panel,0x532);
   		if (StringLen(sr)>0) 
   		{
   		   if (sr[0]=='#') break;
   		}
   		if ((TimeGMT()-startWait)>=10) break;
   		Sleep(sleepMs);
   	}
   	if (StringLen(sr)>0)
   	{
   	   if (StringSubstr(sr, 0, 1)=="#")
   	   {
      		r = StrToInteger(StringSubstr(sr, 1, StringFind(sr, " ")-1));
   		}
      }
      
   	Sleep(sleepMs);
   	PostMessageW(handle_order, WM_CLOSE(), 0, 0);
   	if ( GlobalVariableCheck(GV_BUSY()) ) GlobalVariableDel(GV_BUSY());
   	return(r);
   }
public:
   bool isTradeContextBusy()
   {
	   if (IsTradeContextBusy()) return true;
	   if (!GlobalVariableCheck(GV_BUSY()) ) return(false);
	   int h = int(GlobalVariableGet(GV_BUSY()));
	   return( IsWindowVisible(h) !=0 && GetParent(h) == handle_mt );
   }
   int orderSend (string __symbol, int _cmd, double _volume, int _slippage, double _stoploss, double _takeprofit, string _comment)
   {
      if (isTradeContextBusy()) return -1;
   	int digits		= int (MarketInfo( __symbol, MODE_DIGITS ));
   	int lot_digits	= int (NormalizeDouble(MathCeil(MathAbs(MathLog(MarketInfo( __symbol, MODE_LOTSTEP ))/MathLog(10.0))),0)); 
   	if ( !openDialog( 0, 35431 ) ) return(-1);
   	Sleep(sleepMs);
   	sendMessageCB(handle_order, 0x53E, CB_SELECTSTRING(), -1, lstrcat(__symbol,""));
   	Sleep(sleepMs);
   	SetDlgItemTextW(handle_order, 0x568, DoubleToStr( _volume, lot_digits ) );
      Sleep(sleepMs);
   	if (_stoploss>0) Sleep(sleepMs);
   	string sl = DoubleToStr(_stoploss, digits );
		setPrice(handle_order, 0x51B, sl );
   	if ( _takeprofit > 0 ) Sleep(sleepMs);
   	string tp = DoubleToStr( _takeprofit, digits );
		setPrice( handle_order, 0x55A, tp );
		if (_comment==NULL) _comment="";
   	if ( _comment != "" ) Sleep(sleepMs);
   	SetDlgItemTextW( handle_order, 0x425, _comment );
   
   	int button=-1;
   	if (_cmd < 2)
   	{
   		if(_cmd==OP_SELL)
   			button = 0x508;
   		else
   			button = 0x40C;
   		Sleep(sleepMs);
   		setSlippage(_slippage );
   	}
   	return(getResult(button));
   }
   bool orderClose(int _ticket, double _lots, int _slippage) 
   {
      if (isTradeContextBusy()) return false;
   	if ( !validOrder( _ticket ) ) return(false);
   	if ( OrderType() != OP_BUY && OrderType() != OP_SELL )
   	{
   		return(false);
   	}
   	int digits		= int(MarketInfo( OrderSymbol(), MODE_DIGITS ));
   	int lot_digits	= int(NormalizeDouble(MathCeil(MathAbs(MathLog(MarketInfo( OrderSymbol(), MODE_LOTSTEP ))/MathLog(10.0))),0)); 
   	if ( !openDialog( _ticket, 35451 ) ) return(false);
   	Sleep( sleepMs);
   	SetDlgItemTextW( handle_order, 0x568, DoubleToStr( _lots, lot_digits ) );
   	Sleep( sleepMs);
   	setSlippage(_slippage);
   	return(getResult(0x411)==_ticket);
   }
};
#endif