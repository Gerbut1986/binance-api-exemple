   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  N JVisualMarketsEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngineVisualMarketsEngine '    !BinanceOptionsApp.TradeVisualizer   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     =controls(clr-namespace:BinanceOptionsApp.Controls     Mvm>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngine     .��� .+��� -     ��Name$
  tabs��.@��� -    ��Name$ tiDiagnostics��$��Diagnostics��.��� 
������� � RowDefinitions �� $��2* ��� $��24 ��� $��* �.+�0   *BinanceOptionsApp.Controls.DiagnosticsGrid  -     Name$ diagnosticsControl��$��0��� $��1��.+��� ���	#3f5555ff�� $��Center=�$��Center=�$��16(�
����ح�LOG�� $
��Auto=�$��2��$
��Auto=�.���� -    ��Name$ 
logControl��$
��Wrap=�$��Courier)�$��5q��� -   $ tiLiveChart��$��
Live Chart��$y�	Collapsed=�.��$  VisualMarketsEngine.ChartGroup  -     Name$ vmLive��
�������
��������� -   $ tiBacktestChart��$��Backtest Chart��$y�	Collapsed=�.��  -   $ 
vmBacktest��
�������
��������� -	   $ tiLiveOrders��$��Live Orders��$y�	Collapsed=�.��&   System.Windows.Controls.DataGrid  -
     Name$ dgLiveOrders��   AutoGenerateColumns .     
IsReadOnly . 	   CanUserAddRows	 .  
   CanUserDeleteRows
 .     CanUserReorderColumns .     CanUserResizeRows .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Side��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Side+  $ Volume�� �� *Volume+  $ 
Open Price�� �� *	OpenPrice+  $ 	Open Time�� �� *
OpenTime+ �� StringFormat$ {0:HH:mm:ss.fff}��  $ Open Execution Time(ms)�� �� *OpenExecutionTimeMs+  $ Open Slippage(pt)�� �� *OpenSlippagePt+  $ Close Price�� �� *
ClosePrice+  $ 
Close Time�� �� *	CloseTime+$ {0:HH:mm:ss.fff}��  $ Close Execution Time(ms)�� �� *CloseExecutionTimeMs+  $ Close Slippage(pt)�� �� *CloseSlippagePt+  $ 
Profit(pt)�� �� *
ProfitPt+�� -   $ tiBacktestOrders��$��Backtest Orders��$y�	Collapsed=�.��  -   $ dgBacktestOrders�� .   . 	 .  
 .   .   .     $
 Side�� �� *Side+  $ Volume�� �� *Volume+  $ 
Open Price�� �� *	OpenPrice+  $ 	Open Time�� �� *
OpenTime+$ {0:HH:mm:ss.fff}��  $ Close Price�� �� *
ClosePrice+  $ 
Close Time�� �� *	CloseTime+$ {0:HH:mm:ss.fff}��  $ 
Profit(pt)�� �� *
ProfitPt+�� -   $ tiDebug��$��Debug��$y�	Collapsed=�.��  -   $ dgDebug�� .   . 	 .  
 .   .   .     EnableRowVirtualization . !   EnableColumnVirtualization .  %� IsVirtualizing . 