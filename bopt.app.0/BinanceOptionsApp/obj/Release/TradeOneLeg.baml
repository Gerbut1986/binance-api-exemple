   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  N JVisualMarketsEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngineVisualMarketsEngine C&clr-namespace:BinanceOptionsApp.ModelsBinanceOptionsApp.Models  #    BinanceOptionsApp.TradeOneLeg   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:BinanceOptionsApp     =controls(clr-namespace:BinanceOptionsApp.Controls     Mvm>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngine     9models&clr-namespace:BinanceOptionsApp.Models     .���   � 	Resources   �� MergedDictionaries �� Source �� 
TargetType   LightTextBox �� BasedOn  LightTextBlock/  )BinanceOptionsApp.Controls.IntegerTextBox.  (BinanceOptionsApp.Controls.DoubleTextBox/  )BinanceOptionsApp.Controls.DecimalTextBox  LightComboBox  LightScrollBar  LightCheckBox��  �� $% CustomStyles/ListBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/GroupBoxStyles.xaml?��� $&  CustomStyles/ComboBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $' !CustomStyles/ScrollBarStyles.xaml?�%�   '��       7   '��      7  '       7   '  -     7   '  <     7   '�� K     7  '�� Z     7  '�� i     7  �� " ��8   �� " ��8   �� "  8   �� "  8   �� "  8   �� " ��8   �� " ��8   �� " ��8    � RowDefinitions �� $��9* ��� $��3* � � ColumnDefinitions �� $��1* ��� $��2* �.+��  �� �� .+��  �� �� .+���   locTabFastTitle#���  $��5q�$��0��  myLeftGroupBox#��[   cwWindowTitleBackground#��[ {��� *	Started+ 	 bToBi �� 	Converter# [ .���  	 cwGrayBackground#��[	 .+�'   !BinanceOptionsApp.ProviderControl  -     Name$
 fast��$��Center=�$��5q���  
 locTabSlowTitle#��� 
 $��5q�$��1��#��[ #��[ {��� *	Started+# [ .��� #��[	 .+�  -   $
 slow��$��Center=�$��5q�� $��1��.+���   locTabDataFeedAnalyze#���  $��5q�#��[ #��[ .��� #��[	 	 � IsSharedSizeScope	 .  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+�� $��1��$	��5,2q� �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto �
 z� SharedSizeGroup$	
 GG1���� $��3 ��� �� $��3 ��� .+��� $��Center=�$��Center=�$��0��$��2��  
locTabFast#���  �� $��Center=�$��Center=�$��0��$��4��  
locTabSlow#���  �� $��Center=�$��Right=�$��1��$��0��  	locTabBid#���  �� $��Center=�$��Right=�$��2��$��0��  	locTabAsk#���  �� $��Center=�$��Right=�$��3��$��0��$��Spread:���� $��Center=�$��Right=�$��4��$��0��  
locTabTime#���  �� $��Center=�$��Right=�$��5��$��0��$
��Δτ���� $��Center=�$��Right=�$��6��$��0��  	locTabLot#���  �� $��Center=�$��Right=�$��7��$��0��  locTabBalance#���  ��  �� 
IsReadOnly .  �� TextAlignment$ Center=�$��1��$��1��$��2��$��3,1,0,1q�.����`�   	priceConv `� 	Converter# [ .1���  �� Path$
 Fast��� $ Fast.Bid�.���� $��Center=� . $ Center=�$��1��$��4��.����`� # [ .1��� $
 Slow��� $ Slow.Bid�.����  . $ Center=�$��2��$��1��$��2��$��3,1,0,1q�.����`� # [ .1��� $
 Fast��� $ Fast.Ask�.���� $��Center=� . $ Center=�$��2��$��4��.����`� # [ .1��� $
 Slow��� $ Slow.Ask�.����  . $ Center=�$��3��$��1��$��2��$��3,1,0,1q����� *Fast.ViewSpread+�� $��Center=� . $ Center=�$��3��$��4������ *Slow.ViewSpread+��  . $ Center=�$��4��$��1��$��2��$��3,1,0,1q����� *	Fast.Time+ 
 dtToTS# [ �� $��Center=� . $ Center=�$��4��$��4������ *	Slow.Time+# [ ��  . $ Center=�$��5��$��1��$��2��$��3,1,0,1q�.����`�   tickSpeedConv# [ .1��� $" Fast.AverageTimeBetweenTicks��� $ Fast.MaxTPS�.���� $��Center=� . $ Center=�$��5��$��4��.����`� # [ .1��� $" Slow.AverageTimeBetweenTicks��� $ Slow.MaxTPS�.����  . $ Center=�$��6��$��1��$��2��$��3,1,0,1q����� *Fast.Volume+  volConv# [ �� $��Center=� . $ Center=�$��6��$��4������ *Slow.Volume+# [ ��  . $ Center=�$��7��$��1��$��2��$��3,0,0,0q�$��2������ *Fast.Balance+  balConv# [ �� $��Center=� . $ Center=�$��7��$��4������ *Slow.Balance+# [ � $��2��$	��5,0q� �� $
��Auto ��� $
��Auto � �� $
��Auto �$	
 GG1���� $��3 ��� �� $��3 ��� .+��� $��Center=�$��Center=�$��0��$��2��  	locTabBuy#���  �� $��Center=�$��Center=�$��0��$��4��  
locTabSell#���  �� $��Center=�$��Right=�$��1��$��0��  	locTabGap#���  ��  . $ Center=�$��1��$��1��$��2��$��3,0,0,0q����� *GapBuy+  gapConv# [ ��  . $ Center=�$��1��$��3��$��2��$��3,0,0,0q����� *	GapSell+# [ � $��0��$��1�� �� �� .+���   locTabTrading#���  $��0��$��5q�#��[ #��[ .��� #��[	  �� �� �� �� .+���   locTabProcess#���  $	��5,3q�$	��5,3q�  LightGroupBox#��[ .����  �� Orientation$ Vertical=�.+��� $ Vertical=�{��� *	Started+# [ .+��� $ 
Horizontal=�.+���   locTabLogCheck#���   d� 	IsChecked �� *Log+�� $��5,0,0,0q�   locTabChartCheck#���    �� *	AllowView+�� $��5,0,0,0q� ! locTabSaveCheck#��� !  �� *	SaveTicks+�� $��5,0,0,0q� " locTabEMailCheck#��� "  �� *
AllowEMail+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� # locTabSlippage#��� #   $��1��$��0��   Value �� *
Slippage+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��0��$��2�� $ locTabPoint#��� $   $��1��$��2��   FractionalPart$ 8��   Value �� *Point+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� % locTabMagic#��� %   $��1��$��0�� �� *Magic+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� & locTabComission#��� &   $��1��$��2�� �� *Open.Comission+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ' locTabSleep#��� '   $��1��$��0�� �� *	SleepMs+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� ( locTabRiskDeposit#��� (   $��1��$��2��$ 2�� �� *Open.RiskDeposit+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ) locTabSignalMode#��� )   $��1��$��0�� �� *Open.SignalMode+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��NTrades:��  $��1��$��2�� �� *Open.NTrades+$ PropertyChanged=�$ TwoWay=�� $
��Auto��$
��0,10q�.+��� -    ��Name$ buStart�� * myBlueButton#��[* $��80�� + locStart#��� + $��Center=�y��� *	Started+ 	, bToVi# [, �� -   $ buStop�� - myRedButton#��[-  . locStop#��� . $��	49,0,49,0q�y��� *	Started+ / bToV# [/ �� -   $ buLoad�� 0 myLoadButton#��[0 {��� *	Started+# [ �� -   $ buSave�� 1 mySaveButton#��[1 {��� *	Started+# [ ��  2 locTabInput#��� 2 $��0,3,5,3q�$	��5,3q�$��1��#��[ {��� *	Started+# [ .���� $ Vertical=�.+��  �� $��3* ��� $��3* �.+���  3 locTabAutoShift#��� 3 $��Center=� �� *Open.AvtoShiftBid+  $��1�� �� *Open.AvtoShiftPeriod+$ TwoWay=�$ PropertyChanged=��  �� $
��Auto ��� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��Center=�$
��Left=� 4 locTabShiftBid#��� 4   $��1��$��0��$ 5�� �� *Open.ShiftBid+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��Center=�$
��Left=� 5 locTabShiftAsk#��� 5   $��1��$��2��$ 5�� �� *Open.ShiftAsk+$ PropertyChanged=�$ TwoWay=��� $��2�� 6 locTabAutoSettings#��� 6  �� *Open.AvtoSettings+�� $��2��$��2�� 7 locTabAverageSpread#��� 7  �� *Open.UseAverageSpread+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��
MinSpread:���� $��0��$��2�� 8 locTabMaxSpread#��� 8   $��1��$��0�� �� *Open.MinSpread+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MaxSpread+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� 9 locTabMinSpreadSlow#��� 9   $��1��$��0��$ 2�� �� *Open.MinSpreadSlow+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� : locTabMaxSpreadSlow#��� :   $��1��$��2��$ 2�� �� *Open.MaxSpreadSlow+$ PropertyChanged=�$ TwoWay=�� -    �Name$ workTimeGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ; locTabStartTime#��� ; �� $��1��$��0������ *Open.StartTime+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� < locTabStopTime#��� < �� $��1��$��2������ *Open.EndTime+$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��2��#��[ ���� $ 
Horizontal=�.+��� $��Center=�$��Center=� = 
locTabOpen#��� =  �� *	AllowOpen+�� -    ��Name$ tbOpenOrderType��$��	Underline{�$��15,0,0,0q� � Cursor$
 Hand������ *Open.OrderTypeString+.���� $ Vertical=�.+��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��#���    $��1��$��0��$ 8�� �� *
Open.Lot+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� > locTabRiskPercent#��� >   $��1��$��2��$ 2�� �� *Open.RiskPercent+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ? locTabMinLevelOpen#��� ? $��2���� $��0��$��2�� @ 	locMinTPS#��� @   $��1��$��0��$ 2�� �� *Open.MinimumLevel+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MinimumTPS+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� A locTabMinGapForOpen#��� A �� $��0��$��2�� B locTabMaxGapForOpen#��� B   $��1��$��0��$ 2�� �� *Open.MinGapForOpen+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MaxGapForOpen+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� C locTabOpenTimer#��� C �� $��0��$��2�� D locTabFreezeTime#��� D   $��1��$��0�� �� *Open.OpenTimerMs+$ PropertyChanged=�$ TwoWay=�  -	     Name$ tbFreezeTime��$��1��$��2�� �� *Open.FreezeTime+$ PropertyChanged=�$ TwoWay=�� -
   $ minFastGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� E locTabMinFastCoef#��� E   $��1��$��0��$ 2�� �� *Open.MinFastCoef+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� F locTabFastCoefPeriod#��� F   $��1��$��2�� �� *Open.FastCoefPeriod+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��MinGapFast:��  $��1��$��0��$ 2�� �� *Open.MinGapFast+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��GapFastTicks��  $��1��$��2�� �� *Open.GapFastTicks+$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��3��#��[ ���� $ 
Horizontal=�.+��� $��Center=� G locTabClose#��� G �� -   $ tbCloseOrderType��$��	Underline{�$��15,0,0,0q�$
 Hand������ *Close.OrderTypeString+.���� $ Vertical=�.+���  H locTabMinLevelClose#��� H   $ 2�� �� *Close.MinimumLevelClose+$ PropertyChanged=�$ TwoWay=�� -   $ gridMinOrderTime�� �� ��  �� �� $��3 ��� .+��� $��0��$��0�� I locTabMinOrderTime#��� I �� $��0��$��2�� J locTabCloseTimer#��� J   $��1��$��0�� �� *Open.MinOrderTimeSec+$ PropertyChanged=�$ TwoWay=�  $��1��$��2�� �� *Close.CloseTimerSec+$ PropertyChanged=�$ TwoWay=��  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� K locTabFixTP#��� K �� $��0��$��2�� L locTabFixSL#��� L   $��1��$��0��$ 2�� �� *Close.FixTP+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixSL+$ PropertyChanged=�$ TwoWay=���  M locTabFixTrailing#��� M  �� *Close.Trailing+�  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� N locTabFixTrailStart#��� N �� $��0��$��2�� O locTabFixTrailStop#��� O   $��1��$��0��$ 2�� �� *Close.FixTrailStart+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixTrailStop+$ PropertyChanged=�$ TwoWay=���  P locTabChart#��� P $��1��$��5q�#��[ ���#ef8c07.��$  VisualMarketsEngine.ChartGroup  -     Name$ 
chartGroup��#��[ 
�������$��0,10,0,0q���  Q locTabStrategyLog#��� Q $��1��$��2��$��5q�#��[  R cwTabStripBackground#��[R .���� $
��Auto=�$
��Auto=�#��[ .���� -   $ logBlock��$
��Wrap=�$��Courier)�$��5q��� -   $ logClear�� S myLogCloseButton#��[S $��22��$��5,5,0,0q�$��1��$	��Top=�$
��Left=�.��X