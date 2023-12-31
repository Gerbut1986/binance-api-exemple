#property strict

#ifdef __MQL5__

#include <Trade/PositionInfo.mqh>
#include <Arrays/ArrayLong.mqh>

struct WindowsRect
{
   int left;
   int top;
   int right;
   int bottom;
};

#import "user32.dll"
   int GetParent(int hWnd);
   int SendMessageA(int hWnd, int msg, int wParam, int lParam);
   void SetCursorPos(int x, int y);
   void mouse_event(uint dwFlags,int dx, int dy, int dwData, int dwExtraInfo);
   int GetWindowRect(int hWnd, WindowsRect &rect);
   int SetForegroundWindow(int handle);
   int ShowWindow(int handle, int nCmdShow);
   int IsIconic(int handle);
   int GetAncestor(int handle, int type);
   int PostMessageA(int handle, int msg, int wparam, int lparam);
   int FindWindowExW(int hwndParent, int hwndChildAfter, string lpszClass, string plszWindow);
   int SetWindowTextW(int hWnd, string lpString);
   int SetFocus(int hWnd);
   int GetWindowTextA(int hWnd, char &data[], int nMaxCount);
   int SendMessageA(int hWnd, int msg, int wParam, WindowsRect& rt);
#import

#define WM_MDIACTIVATE 546
#define MOUSEEVENTF_ABSOLUTE 0x8000
#define MOUSEEVENTF_LEFTDOWN 0x0002
#define MOUSEEVENTF_LEFTUP 0x0004
#define MOUSEEVENTF_MOVE 0x0001
#define WM_CHAR 0x0102
#define WM_KEYDOWN 0x0100
#define WM_KEYUP 0x0101
#define VK_RETURN 0x0D

#define LVM_FIRST 0x1000 
#define LVM_GETITEMCOUNT (LVM_FIRST + 4)
#define LVM_GETITEMRECT (LVM_FIRST + 14)
#define LVM_ENSUREVISIBLE (LVM_FIRST + 19)

enum ClickerLanguage
{
   ClickerLanguageEnglish,
   ClickerLanguageRussian
};

class Clicker
{
private:
   string toolBoxName;
   WindowsRect getWindowRect(long _handle)
   {
      WindowsRect res;
      res.left=0;
      res.top=0;
      res.bottom=0;
      res.right=0;
      GetWindowRect((int)_handle,res);
      return res;
   }
   string getWindowText(long _handle)
   {
      char txt[256];
      ArrayInitialize(txt,0);
      GetWindowTextA((int)_handle,txt,20);
      return CharArrayToString(txt,0,WHOLE_ARRAY,CP_UTF8);
   }
   void mouseclick(int x, int y, bool doubleclick)
   {
      SetCursorPos(x,y);
      int n = doubleclick ? 2 : 1;
      for (int i = 0; i < n; i++)
      {
         mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
         mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
      }  
   }
   void bringMainWindowToTop(long _handle)
   {
      while (true)
      {
         int parent = GetParent((int)_handle);
         if (parent!=0) _handle=(long)parent;
         else break;
      }
      if (_handle!=0)
      {
         if (IsIconic((int)_handle)!=0)
         {
            ShowWindow((int)_handle, 9);
         }
         SetForegroundWindow((int)_handle);   
      }
   }
   void showOneClickTrading(long _chartId)
   {
      ChartSetInteger(_chartId,CHART_SHOW_ONE_CLICK,1);
      ChartRedraw(_chartId);
      Sleep(0);
      ChartRedraw(_chartId);
   }
   void openNewChart(string __symbol)
   {
      long chartId = ChartOpen(__symbol,(ENUM_TIMEFRAMES)PERIOD_M1);
      showOneClickTrading(chartId);
   }
   long findChartTabHandle(string __symbol, bool _showOneClick)
   {
      long chartId = ChartFirst();
      while (chartId>=0)
      {
         string chartSymbol=ChartSymbol(chartId);
         if (chartSymbol==__symbol)
         {
            if (_showOneClick) showOneClickTrading(chartId);
            return ChartGetInteger(chartId,CHART_WINDOW_HANDLE);
         }
         chartId=ChartNext(chartId);
      }
      return -1;
   }
   void showChartTab(long _handle)
   {
      int hParent = GetParent((int)_handle);
      int hMdi = GetParent(hParent);
      SendMessageA(hMdi,WM_MDIACTIVATE,hParent,0);
   }
   void setLots(long _handle, double _lots)
   {
      int hEdit = FindWindowExW((int)_handle,0,"Edit",NULL);
      string lots=DoubleToString(_lots,2);
      
      Sleep(250);
      WindowsRect rt=getWindowRect(_handle);
      mouseclick(rt.left+105,rt.top+35,false);
      
      string s="";
      while (s!="0.00" && !IsStopped())
      {
         SetFocus(hEdit);
         SendMessageA(hEdit,WM_CHAR,0x30,0);
         Sleep(15);
         s=getWindowText(hEdit);
      }
      if (IsStopped()) return;
      int len=StringLen(lots);
      for (int i=0;i<len;i++)
      {
         SetFocus(hEdit);
         SendMessageA(hEdit,WM_CHAR,lots[i],0);
         Sleep(15);
      }
      SendMessageA(hEdit,WM_KEYDOWN,VK_RETURN,0);
      SendMessageA(hEdit,WM_KEYUP,VK_RETURN,0);
   }
   int getListItemsCount(int _list)
   {
      return SendMessageA(_list, LVM_GETITEMCOUNT, 0, 0);
   }
   void listViewEnsureVisible(int _list, int _iItem)
   {
      SendMessageA(_list,LVM_ENSUREVISIBLE,_iItem,0);
   }
   WindowsRect listViewGetItemRect(int _list,int _iItem)
   {
      WindowsRect res;
      res.left=0;
      res.top=0;
      res.right=0;
      res.bottom=0;
      SendMessageA(_list,LVM_GETITEMRECT,_iItem,res);
      return res;
   }
   int getToolboxWindow()
   {
      int hMain=(int)ChartGetInteger(0,CHART_WINDOW_HANDLE);
      while (hMain!=0)
      {
         int hParent=GetParent(hMain);
         if (hParent==0) break;
         hMain=hParent;
      }
      int hToolbox=FindWindowExW(hMain,0,"AfxControlBar140su",toolBoxName);
      return hToolbox;
   }
   int getTradesList()
   {
      int hBox = getToolboxWindow();
      
      WindowsRect rt = getWindowRect(hBox);
      mouseclick(rt.left+45,rt.bottom-10,false);

      hBox=FindWindowExW(hBox,0,NULL,toolBoxName);
      
      rt=getWindowRect(hBox);
      mouseclick(rt.left+75,rt.top+15,false);
      mouseclick(rt.left+200,rt.top+15,false);
      
      int hList=FindWindowExW(hBox,0,"SysListView32",NULL);
      hList=FindWindowExW(hBox,hList,"SysListView32",NULL);
      return hList;
   }
   long activateChart(string __symbol)
   {
      SymbolSelect(__symbol,true);
      long handle = findChartTabHandle(__symbol,true);
      if (handle==-1)
      {
         openNewChart(__symbol);
         handle=findChartTabHandle(__symbol,true);
      }
      //bringMainWindowToTop(handle);
      showChartTab(handle);
      return handle;
   }
   void clickSell(long _handle, double _lots=-1)
   {
      if (_handle<=0) return;
      //bringMainWindowToTop(_handle);
      if (_lots>0) setLots(_handle,_lots);
      WindowsRect rt = getWindowRect(_handle);
      mouseclick(rt.left+60,rt.top+60,false);
   }
   void clickBuy(long _handle, double _lots=-1)
   {
      if (_handle<=0) return;
      //bringMainWindowToTop(_handle);
      if (_lots>0) setLots(_handle,_lots);
      WindowsRect rt = getWindowRect(_handle);
      mouseclick(rt.left+140,rt.top+60,false);
   }
   
public:
   Clicker()
   {
      initialize(ClickerLanguageEnglish);
   }
   void initialize(ClickerLanguage _lang)
   {
      if (_lang==ClickerLanguageEnglish) initialize("Toolbox");
      if (_lang==ClickerLanguageRussian) initialize("Инструменты"); 
   }
   void initialize(string _toolboxName)
   {
      toolBoxName=_toolboxName;
   }
   void setLots(string __symbol, double _lots)
   {
      long handle=activateChart(__symbol);
      setLots(handle,_lots);
   }
   void clickClose(long _ticket)
   {
      CArrayLong tickets;
      CPositionInfo pos;
      int n=PositionsTotal();
      for (int i=0;i<n;i++)
      {
         if (pos.SelectByIndex(i))
         {
            tickets.Add(pos.Ticket());
         }
      }
      tickets.Sort();
      int lineIndex=tickets.Search(_ticket);
      if (lineIndex<0) return;

      int hList=getTradesList();
      int linesCount=getListItemsCount(hList);
      
      if (linesCount==(n+1) && linesCount==(tickets.Total()+1))
      {
         listViewEnsureVisible(hList,lineIndex);
         WindowsRect rt=listViewGetItemRect(hList,lineIndex);
         
         WindowsRect rtList=getWindowRect(hList);
         rt.left+=rtList.left;
         rt.right+=rtList.left;
         rt.top+=rtList.top;
         rt.bottom+=rtList.top;
         
         mouseclick(rt.right-8,rt.top+8,false);
      }
   }
   void clickBuy(string __symbol, double _lots=-1)
   {
      long handle=activateChart(__symbol);
      clickBuy(handle,_lots);
   }
   void clickSell(string __symbol, double _lots=-1)
   {
      long handle=activateChart(__symbol);
      clickSell(handle,_lots);
   }
};
#endif