   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WesternpipsPrivate7WesternpipsPrivate7  K*clr-namespace:WesternpipsPrivate7.ControlsWesternpipsPrivate7.Controls  N JVisualMarketsEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngineVisualMarketsEngine G(clr-namespace:WesternpipsPrivate7.ModelsWesternpipsPrivate7.Models  )    #WesternpipsPrivate7.TradeTwoLegLock   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3local!clr-namespace:WesternpipsPrivate7     ?controls*clr-namespace:WesternpipsPrivate7.Controls     Mvm>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngine     ;models(clr-namespace:WesternpipsPrivate7.Models     .���   � 	Resources   �� MergedDictionaries �� Source �� 
TargetType   LightTextBox �� BasedOn  LightTextBlock1  +WesternpipsPrivate7.Controls.IntegerTextBox0  *WesternpipsPrivate7.Controls.DoubleTextBox1  +WesternpipsPrivate7.Controls.DecimalTextBox  LightComboBox  LightScrollBar  LightCheckBox��  �� $% CustomStyles/ListBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/GroupBoxStyles.xaml?��� $&  CustomStyles/ComboBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $' !CustomStyles/ScrollBarStyles.xaml?�%�   '��       7   '��      7  '       7   '  -     7   '  <     7   '�� K     7  '�� Z     7  '�� i     7  �� " ��8   �� " ��8   �� "  8   �� "  8   �� "  8   �� " ��8   �� " ��8   �� " ��8    � RowDefinitions �� $��9* ��� $��3* � � ColumnDefinitions �� $��1* ��� $��2* �.+��  �� �� .+�� $��0�� �� �� �� .+���   locTabFastTitle#���  $��5q�$��0��  myLeftGroupBox#��[   cwWindowTitleBackground#��[ {��� *	Started+ 	 bToBi �� 	Converter# [ .���  	 cwGrayBackground#��[	 .+�)   #WesternpipsPrivate7.ProviderControl  -     Name$
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
locTabGap1#���  �� $��Center=�$��Right=�$��2��$��0��  
locTabGap2#���  �� $��Center=� . $ Center=�$��1��$��2������ *GapBuy+  gapConv# [ �� $��Center=� . $ Center=�$��1��$��4������ *	GapSell+# [ �� $��Center=� . $ Center=�$��2��$��2������ *	GapBuy2+# [ �� $��Center=� . $ Center=�$��2��$��4������ *
GapSell2+# [ � $��0��$��1�� �� �� .+���   locTabTrading#���  $��0��$��5q�#��[ #��[ .��� #��[	  �� �� �� �� .+���    locTabProcess#���   $	��5,3q�$	��5,3q� ! LightGroupBox#��[! .����  �� Orientation$ Vertical=�.+��� $ Vertical=�{��� *	Started+# [ .+��� $ 
Horizontal=�.+���  " locTabLogCheck#��� "  d� 	IsChecked �� *Log+�� $��5,0,0,0q� # locTabChartCheck#��� #  �� *	AllowView+�� $��5,0,0,0q� $ locTabSaveCheck#��� $  �� *	SaveTicks+�� $��5,0,0,0q� % locTabEMailCheck#��� %  �� *
AllowEMail+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� & locTabSlippage#��� &   $��1��$��0��   Value �� *
Slippage+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��0��$��2�� ' locTabPoint#��� '   $��1��$��2��   FractionalPart$ 8��   Value �� *Point+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� ( locTabMagic#��� (   $��1��$��0�� �� *Magic+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� ) locTabComission#��� )   $��1��$��2�� �� *Open.Comission+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� * locTabSleep#��� *   $��1��$��0�� �� *	SleepMs+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� + locTabRiskDeposit#��� +   $��1��$��2��$ 2�� �� *Open.RiskDeposit+$ PropertyChanged=�$ TwoWay=���  , locTabSignalMode#��� ,    �� *Open.SignalMode+$ PropertyChanged=�$ TwoWay=�� $
��Auto��$
��0,10q�.+��� -    ��Name$ buStart�� - myBlueButton#��[- $��80�� . locStart#��� . y��� *	Started+ 	/ bToVi# [/ �� -   $ buStop�� 0 myRedButton#��[0 $��80�� 1 locStop#��� 1 y��� *	Started+ 2 bToV# [2 �� -   $ buLoad�� 3 myLoadButton#��[3 {��� *	Started+# [ �� -   $ buSave�� 4 mySaveButton#��[4 {��� *	Started+# [ ��  5 locTabInput#��� 5 $��0,3,5,3q�$	��5,3q�#��[! $��1��{��� *	Started+# [ .���� $ Vertical=�.+���  �� DisplayMemberPath$ Description�� �� SelectedValuePath$ value������  �� Source /   )WesternpipsPrivate7.EnumLangToItemsSource  *M� *models:LockType++ �� SelectedValue �� *Open.LockType+$ TwoWay=�$ PropertyChanged=��  �� $��3* ��� $��3* �.+���  6 locTabAutoShift#��� 6 $��Center=� �� *Open.AvtoShiftBid+  $��1�� �� *Open.AvtoShiftPeriod+$ TwoWay=�$ PropertyChanged=��  �� $
��Auto ��� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��Center=�$
��Left=� 7 locTabShiftBid#��� 7   $��1��$��0��$ 5�� �� *Open.ShiftBid+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��Center=�$
��Left=� 8 locTabShiftAsk#��� 8   $��1��$��2��$ 5�� �� *Open.ShiftAsk+$ PropertyChanged=�$ TwoWay=��� $��2�� 9 locTabAutoSettings#��� 9 $��16��$	��Top=� �� *Open.AvtoSettings+�� $��2��$��2�� : locTabAverageSpread#��� : $��16��$	��Top=� �� *Open.UseAverageSpread+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��
MinSpread:���� $��0��$��2�� ; locTabMaxSpread#��� ;   $��1��$��0�� �� *Open.MinSpread+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MaxSpread+$ PropertyChanged=�$ TwoWay=�� -    �Name$ workTimeGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� < locTabStartTime#��� < �� $��1��$��0������ *Open.StartTime+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� = locTabStopTime#��� = �� $��1��$��2������ *Open.EndTime+$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��2��#��[! ���� $ 
Horizontal=�.+��� $��Center=�$��Center=� > 
locTabOpen#��� >  �� *	AllowOpen+�� -	    ��Name$ tbOpenOrderType��$��	Underline{�$��15,0,0,0q� � Cursor$
 Hand������ *Open.OrderTypeString+.���� $ Vertical=�.+��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��$��Center=�$
��Left=� ? 
locTabLot1#��� ?   $��1��$��0��$ 8�� �� *
Open.Lot+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��$��Center=�$
��Left=� @ 
locTabLot2#��� @   $��1��$��2��$ 8�� �� *Open.LotSlow+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� A locTabMinLevelOpen#��� A $��2���� $��0��$��2�� B 	locMinTPS#��� B   $��1��$��0��$ 2�� �� *Open.MinimumLevel+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Open.MinimumTPS+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0�� C locTabMinOrderTime#��� C   $��1��$��0�� �� *Open.MinOrderTimeSec+$ PropertyChanged=�$ TwoWay=��� $��0��$��2�� D locTabMinOrderPoint#��� D   $��1��$��2�� �� *Open.MinOrderPoint+$ PropertyChanged=�$ TwoWay=���  E locTabMaxGapForOpen#��� E   $ 2�� �� *Open.MaxGapForOpen+$ PropertyChanged=�$ TwoWay=��� $ 
Horizontal=�$��Center=�$��0,2,0,0q�.+���  F locStopMode#��� F $��Center=� �� *Open.StopMode+  $ Center=�$��35��$	��2,0q� �� *Open.StopModePoints+��  G locStopModePtIn#��� G $��Center=�  $ Center=�$��50��$	��2,0q� �� *Open.StopModeMs+��  H locStopModeMs#��� H $��Center=��� $��0,3,5,3q�$	��5,3q�$��3��#��[! ���� $ 
Horizontal=�.+��� $��Center=� I locTabClose#��� I �� -
   $ tbCloseOrderType��$��	Underline{�$��15,0,0,0q�$
 Hand������ *Close.OrderTypeString+.���� $ Vertical=�.+���  J locTabMinLevelClose#��� J   $ 2�� �� *Close.MinimumLevelClose+$ PropertyChanged=�$ TwoWay=�� -   $ gridMinOrderTime�� �� ��  �� �� $��3 ��� .+��� $��0��$��0��#��� C �� $��0��$��2�� K locTabCloseTimer#��� K   $��1��$��0�� �� *Close.MinOrderTimeSec2Leg+$ PropertyChanged=�$ TwoWay=�  $��1��$��2�� �� *Close.CloseTimerSec+$ PropertyChanged=�$ TwoWay=��  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� L locTabFixTP#��� L �� $��0��$��2�� M locTabFixSL#��� M   $��1��$��0��$ 2�� �� *Close.FixTP+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixSL+$ PropertyChanged=�$ TwoWay=���  N locTabFixTrailing#��� N  �� *Close.Trailing+�  �� ��  �� �� $��3 ��� .+��� $��0��$��0�� O locTabFixTrailStart#��� O �� $��0��$��2�� P locTabFixTrailStop#��� P   $��1��$��0��$ 2�� �� *Close.FixTrailStart+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��$ 2�� �� *Close.FixTrailStop+$ PropertyChanged=�$ TwoWay=���  Q locTabChart#��� Q $��1��$��5q�#��[ ���#ef8c07.��$  VisualMarketsEngine.ChartGroup  -     Name$ 
chartGroup��#��[ 
���������  R locTabStrategyLog#��� R $��1��$��2��$��5q�#��[  S cwTabStripBackground#��[S .���� $
��Auto=�$
��Auto=�#��[ .���� -   $ logBlock��$
��Wrap=�$��Courier)�$��5q��� -   $ logClear�� T myLogCloseButton#��[T $��22��$��5,5,0,0q�$��1��$	��Top=�$
��Left=�.��X