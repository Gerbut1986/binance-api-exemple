   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  N JVisualMarketsEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngineVisualMarketsEngine C&clr-namespace:BinanceOptionsApp.ModelsBinanceOptionsApp.Models  (    "BinanceOptionsApp.TradeOneLegHedge   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:BinanceOptionsApp     =controls(clr-namespace:BinanceOptionsApp.Controls     Mvm>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngine     9models&clr-namespace:BinanceOptionsApp.Models     .���   � 	Resources   �� MergedDictionaries �� Source �� 
TargetType   LightTextBox �� BasedOn  LightTextBlock/  )BinanceOptionsApp.Controls.IntegerTextBox.  (BinanceOptionsApp.Controls.DoubleTextBox/  )BinanceOptionsApp.Controls.DecimalTextBox  LightComboBox  LightScrollBar  LightCheckBox��  �� $% CustomStyles/ListBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/GroupBoxStyles.xaml?��� $&  CustomStyles/ComboBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $' !CustomStyles/ScrollBarStyles.xaml?�%�   '��       7   '��      7  '       7   '  -     7   '  <     7   '�� K     7  '�� Z     7  '�� i     7  �� " ��8   �� " ��8   �� "  8   �� "  8   �� "  8   �� " ��8   �� " ��8   �� " ��8    � RowDefinitions �� $��9* ��� $��3* � � ColumnDefinitions �� $��1* ��� $��2* �.+��  �� �� .+�� $��0�� �� �� �� .+���   locTabFastTitle#���  $��5q�$��0��  myLeftGroupBox#��[   cwWindowTitleBackground#��[ {��� *	Started+ 	 bToBi �� 	Converter# [ .���  	 cwGrayBackground#��[	 .+�'   !BinanceOptionsApp.ProviderControl  -     Name$
 fast��$��5q�$��Center=���  
 locTabSlow1Title#��� 
 $��5q�$��1��#��[ #��[ {��� *	Started+# [ .��� #��[	 .+�  -   $ slow1��$��5q�$��Center=���   locTabSlow2Title#���  $��5q�$��2��#��[ #��[ {��� *	Started+# [ .��� #��[	 .+�  -   $ slow2��$��5q�$��Center=�� $��1��.+���   locTabDataFeedAnalyze#���  $��5q�#��[ #��[ .��� #��[	 	 � IsSharedSizeScope	 .  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� .+�� $��0��$	��5,2q� �� $
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
 GG2���� $��3 ��� �� $��3 ��� �� $��3 ��� .+��� $��Center=�$��Center=�$��0��$��2��  
locTabFast#���  �� $��Center=�$��Center=�$��0��$��4��  locTabSlow1#���  �� $��Center=�$��Center=�$��0��$��6��  locTabSlow2#���  �� $��Center=�$��Right=�$��1��$��0��  	locTabBid#���  �� $��Center=�$��Right=�$��2��$��0��  	locTabAsk#���  �� $��Center=�$��Right=�$��3��$��0��$��Spread:���� $��Center=�$��Right=�$��4��$��0��  
locTabTime#���  �� $��Center=�$��Right=�$��5��$��0��$
��Δτ���� $��Center=�$��Right=�$��6��$��0��  	locTabLot#���  �� $��Center=�$��Right=�$��7��$��0��  locTabBalance#���  �� $��Center=� �� 
IsReadOnly .  �� TextAlignment$ Center=�$��1��$��2��.����`�   	priceConv `� 	Converter# [ .1���  �� Path$
 Fast��� $ Fast.Bid�.���� $��Center=� . $ Center=�$��1��$��4��.����`� # [ .1��� $
 Slow��� $ Slow.Bid�.���� $��Center=� . $ Center=�$��1��$��6��.����`� # [ .1��� $ Slow2��� $ 	Slow2.Bid�.���� $��Center=� . $ Center=�$��2��$��2��.����`� # [ .1��� $
 Fast��� $ Fast.Ask�.���� $��Center=� . $ Center=�$��2��$��4��.����`� # [ .1��� $
 Slow��� $ Slow.Ask�.���� $��Center=� . $ Center=�$��2��$��6��.����`� # [ .1��� $ Slow2��� $ 	Slow2.Ask�.���� $��Center=� . $ Center=�$��3��$��2������ *Fast.ViewSpread+�� $��Center=� . $ Center=�$��3��$��4������ *Slow.ViewSpread+�� $��Center=� . $ Center=�$��3��$��6������ *Slow2.ViewSpread+�� $��Center=� . $ Center=�$��4��$��2������ *	Fast.Time+ 
 dtToTS# [ �� $��Center=� . $ Center=�$��4��$��4������ *	Slow.Time+# [ �� $��Center=� . $ Center=�$��4��$��6������ *
Slow2.Time+# [ �� $��Center=� . $ Center=�$��5��$��2��.����`�   tickSpeedConv# [ .1��� $" Fast.AverageTimeBetweenTicks��� $ Fast.MaxTPS�.���� $��Center=� . $ Center=�$��5��$��4��.����`� # [ .1��� $" Slow.AverageTimeBetweenTicks��� $ Slow.MaxTPS�.���� $��Center=� . $ Center=�$��5��$��6��.����`� # [ .1��� $# Slow2.AverageTimeBetweenTicks��� $ Slow2.MaxTPS�.���� $��Center=� . $ Center=�$��6��$��2������ *Fast.Volume+  volConv# [ �� $��Center=� . $ Center=�$��6��$��4������ *Slow.Volume+# [ �� $��Center=� . $ Center=�$��6��$��6������ *Slow2.Volume+# [ �� $��Center=� . $ Center=�$��7��$��2������ *Fast.Balance+  balConv# [ �� $��Center=� . $ Center=�$��7��$��4������ *Slow.Balance+# [ �� $��Center=� . $ Center=�$��7��$��6������ *Slow2.Balance+# [ � $��1��$	��5,2q� �� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto �$	
 GG2���� $��3 ��� �� $��3 ��� .+��� $��Center=�$��Center=�$��0��$��2��  	locTabBuy#���  �� $��Center=�$��Center=�$��0��$��4��  
locTabSell#���  �� $��Center=�$��Right=�$��1��$��0��  
locTabGap1#���  �� $��Center=�$��Right=�$��2��$��0��$��H-Gap���� $��Center=� . $ Center=�$��1��$��2������ *GapBuy+  gapConv# [ �� $��Center=� . $ Center=�$��1��$��4������ *	GapSell+# [ �� $��Center=� . $ Center=�$��2��$��2������ *	GapBuy2+# [ �� $��Center=� . $ Center=�$��2��$��4������ *
GapSell2+# [ � $��0��$��1�� �� �� .+���   locTabTrading#���  $��0��$��5q�#��[ #��[ .��� #��[	  �� �� �� �� .+���   locTabProcess#���  $	��5,3q�$	��5,3q�   LightGroupBox#��[  .����  �� Orientation$ Vertical=�.+��� $ Vertical=�{��� *	Started+# [ .+��� $ 
Horizontal=�.+���  ! locTabLogCheck#��� !  d� 	IsChecked �� *Log+�� $��5,0,0,0q� " locTabChartCheck#��� "  �� *	AllowView+�� $��5,0,0,0q� # locTabSaveCheck#��� #  �� *	SaveTicks+�� $��5,0,0,0q� $ locTabEMailCheck#��� $  �� *
AllowEMail+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� % locTabSlippage#��� %   $��1��$��0��   Value �� *
Slippage+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��0��$��2�� & locTabPoint#��� &   $��1��$��2��   FractionalPart$ 8��   Value �� *Point+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ' locTabMagic#��� '   $��1��$��0�� �� *Magic+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� ( locTabComission#��� (   $��1��$��2�� �� *Open.Comission+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ) locTabSleep#��� )   $��1��$��0�� �� *	SleepMs+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� * locTabRiskDeposit#��� *   $��1��$��2��$ 2�� �� *Open.RiskDeposit+$ PropertyChanged=�$ TwoWay=���  + locTabSignalMode#��� +    �� *Open.SignalMode+$ PropertyChanged=�$ TwoWay=�� $
��Auto��$
��0,10q�.+��� -    ��Name$ buStart�� , myBlueButton#��[, $��80�� - locStart#��� - y��� *	Started+ 	. bToVi# [. �� -   $ buStop�� / myRedButton#��[/ $��80�� 0 locStop#��� 0 y��� *	Started+ 1 bToV# [1 �� -   $ buLoad�� 2 myLoadButton#��[2 {��� *	Started+# [ �� -   $ buSave�� 3 mySaveButton#��[3 {��� *	Started+# [ ��  4 locTabInput#��� 4 $��0,3,5,3q�$	��5,3q�#��[  $��1��{��� *	Started+# [ .���� $ Vertical=�.+��  �� $��3* ��� $��3* �.+���  5 locTabAutoShift#��� 5 $��Center=� �� *Open.AvtoShiftBid+  $��1�� �� *Open.AvtoShiftPeriod+$ TwoWay=�$ PropertyChanged=��  �� $
��Auto ��� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��Center=�$
��Left=� 6 locTabShiftBid#��� 6   $��1��$��0��$ 5�� �� *Open.ShiftBid+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��Center=�$
��Left=� 7 locTabShiftAsk#��� 7   $��1��$��2��$ 5�� �� *Open.ShiftAsk+$ PropertyChanged=�$ TwoWay=��� $��2�� 8 locTabAutoSettings#��� 8 $��16��$	��Top=� �� *Open.AvtoSettings+�� $��2��$��2�� 9 locTabAverageSpread#��� 9 $��16��$	��Top=� �� *Open.UseAverageSpread+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��
MinSpread:���� $��0��$��2�� : locTabMaxSpread#��� :   $��1��$��0�� �� *Open.MinSpread+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MaxSpread+$ PropertyChanged=�$ TwoWay=�� -    �Name$ workTimeGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ; locTabStartTime#��� ; �� $��1��$��0������ *Open.StartTime+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� < locTabStopTime#��� < �� $��1��$��2������ *Open.EndTime+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��
H.Slip(pt)��  $ 1��$��1��$��0������ *#!Open.MinimumHedgeSlippageInPoints+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��H.Level��  $ 1��$��1��$��2������ *Open.MinimumHedgeLevel+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��H.Time(sec)��  $ 1��$��1��$��0������ *Open.HTimeSeconds+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��H.Trades(max)��  $��1��$��2������ *Open.HMaxTrades+$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��2��#��[  ���� $ 
Horizontal=�.+��� $��Center=�$��Center=� = 
locTabOpen#��� =  �� *	AllowOpen+�� -	    ��Name$ tbOpenOrderType��$��	Underline{�$��15,0,0,0q� � Cursor$
 Hand������ *Open.OrderTypeString+.���� $ Vertical=�.+��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��Center=�$
��Left=� > 
locTabLot1#��� >   $��1��$��0��$ 8�� �� *
Open.Lot+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��Center=�$
��Left=� ? 
locTabLot2#��� ?   $��1��$��2��$ 8�� �� *Open.LotSlow+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� @ locTabMinLevelOpen#��� @ $��2���� $��0��$��2�� A 	locMinTPS#��� A   $��1��$��0��$ 2�� �� *Open.MinimumLevel+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MinimumTPS+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� B locTabMinGapForOpen#��� B �� $��0��$��2�� C locTabMaxGapForOpen#��� C   $��1��$��0��$ 2�� �� *Open.MinGapForOpen+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MaxGapForOpen+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� D locTabOpenTimer#��� D �� $��0��$��2�� E locTabFreezeTime#��� E   $��1��$��0�� �� *Open.OpenTimerMs+$ PropertyChanged=�$ TwoWay=�  -
     Name$ tbFreezeTime��$��1��$��2�� �� *Open.FreezeTime+$ PropertyChanged=�$ TwoWay=�� -   $ minFastGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� F locTabMinFastCoef#��� F   $��1��$��0��$ 2�� �� *Open.MinFastCoef+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� G locTabFastCoefPeriod#��� G   $��1��$��2�� �� *Open.FastCoefPeriod+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��MinGapFast:��  $��1��$��0��$ 2�� �� *Open.MinGapFast+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��GapFastTicks��  $��1��$��2�� �� *Open.GapFastTicks+$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��3��#��[  ���� $ 
Horizontal=�.+��� $��Center=� H locTabClose#��� H �� -   $ tbCloseOrderType��$��	Underline{�$��15,0,0,0q�$
 Hand������ *Close.OrderTypeString+.���� $ Vertical=�.+���  I locTabMinLevelClose#��� I   $ 2�� �� *Close.MinimumLevelClose+$ PropertyChanged=�$ TwoWay=�� -   $ gridMinOrderTime�� �� ��  �� �� $��3 ��� .+��� $��0��$��0�� J locTabMinOrderTime#��� J �� $��0��$��2�� K locTabCloseTimer#��� K   $��1��$��0�� �� *Close.MinOrderTimeSec2Leg+$ PropertyChanged=�$ TwoWay=�  $��1��$��2�� �� *Close.CloseTimerSec+$ PropertyChanged=�$ TwoWay=��  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� L locTabFixTP#��� L �� $��0��$��2�� M locTabFixSL#��� M   $��1��$��0��$ 2�� �� *Close.FixTP+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixSL+$ PropertyChanged=�$ TwoWay=���  N locTabFixTrailing#��� N  �� *Close.Trailing+�  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� O locTabFixTrailStart#��� O �� $��0��$��2�� P locTabFixTrailStop#��� P   $��1��$��0��$ 2�� �� *Close.FixTrailStart+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixTrailStop+$ PropertyChanged=�$ TwoWay=��  �� ��  �� �� $��3 ��� .+��� $��0��$��0��$��H.Profit($)���� $��0��$��2��$��	H.Stop($)��  $��1��$��0��$ 2�� �� *Close.HProfit+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.HStop+$ PropertyChanged=�$ TwoWay=���  Q locTabChart#��� Q $��1��$��5q�#��[ ���#ef8c07.��$  VisualMarketsEngine.ChartGroup  -     Name$ 
chartGroup��#��[ 
���������  R locTabStrategyLog#��� R $��1��$��2��$��5q�#��[  S cwTabStripBackground#��[S .���� $
��Auto=�$
��Auto=�#��[ .���� -   $ logBlock��$
��Wrap=�$��Courier)�$��5q��� -   $ logClear�� T myLogCloseButton#��[T $��22��$��5,5,0,0q�$��1��$	��Top=�$
��Left=�.��X