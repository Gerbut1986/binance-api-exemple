   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  C&clr-namespace:BinanceOptionsApp.ModelsBinanceOptionsApp.Models  N JVisualMarketsEngine, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullV>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngineVisualMarketsEngine +    %BinanceOptionsApp.TradeMultiLegSpread   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:BinanceOptionsApp     =controls(clr-namespace:BinanceOptionsApp.Controls     9models&clr-namespace:BinanceOptionsApp.Models     Mvm>clr-namespace:VisualMarketsEngine;assembly=VisualMarketsEngine     .���   � 	Resources   �� MergedDictionaries �� Source��  �� $% CustomStyles/ListBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/GroupBoxStyles.xaml?��� $&  CustomStyles/ComboBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $' !CustomStyles/ScrollBarStyles.xaml?�%     � RowDefinitions �� $
��4.5* ��� $
��4.5* ��� $��3* � � ColumnDefinitions �� $��1* ��� $��2* �.+��  �� .+���    locTabControl#���   $��5q�  myLeftGroupBox#��[   cwWindowTitleBackground#��[ {��� *	Started+ 	 bToBi �� 	Converter# [ .���   cwGrayBackground#��[ .+��� $��Center=�$��5q�.+�'   !BinanceOptionsApp.ProviderControl  -     Name$
 fast��� $
��0,10q� �� �� $��5 ��� .+��� -     myBlueButton#��[ $	��130��.��
Add Broker�� -    ��Name$ removeBroker��  myRedButton#��[ $��2��$	��130��$��5,0,0,0q�.��Remove Broker� $��0��$��1��.+���   locTabTrading#���  $��0��$��5q�#��[ #��[ .��� #��[  �� �� �� �� .+���   locTabProcess#���  $	��5,3q�$	��5,3q� 	 LightGroupBox#��[	 .����  �� Orientation$ Vertical=�.+��� $ Vertical=�{��� *	Started+# [ .+��� $ 
Horizontal=�.+���  
 locTabLogCheck#��� 
 	 d� 	IsChecked	 �� *Log+�  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��  locTabSlippage#���  /  )BinanceOptionsApp.Controls.IntegerTextBox  $��1��$��0��
   Value
 �� *
Slippage+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� $��0��$��2��  locTabPoint#���  /  )BinanceOptionsApp.Controls.DecimalTextBox  $��1��$��2��   FractionalPart$ 8��   Value �� *Point+$ PropertyChanged=�$ TwoWay=��  �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��  locTabMagic#���    $��1��$��0��
 �� *Magic+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��  locTabComission#���    $��1��$��2��
 �� *	Comission+$ PropertyChanged=�$ TwoWay=���   locTabSleep#���    
 �� *	SleepMs+$ PropertyChanged=�$ TwoWay=�� $
��Auto��$
��0,10q�.+��� -   $ buStart��#��[ $��80��  locStart#���  y��� *	Started+ 	 bToVi# [ �� -   $ buStop��#��[ $��80��  locStop#���  y��� *	Started+  bToV# [ �� -   $ buLoad��  myLoadButton#��[ {��� *	Started+# [ �� -   $ buSave��  mySaveButton#��[ {��� *	Started+# [ �� $��0,3,5,3q�$	��5,3q�$��1��#��[	 ���� $ 
Horizontal=�.+��� $��Center=�$��Center=�  
locTabOpen#���  	 �� *	AllowOpen+�� -    ��Name$ tbOpenOrderType��$��	Underline{�$��15,0,0,0q� � Cursor$
 Hand������ *Open.OrderTypeString+.���� $ Vertical=�.+���   locTabMinLevelOpen#���    $ 2�� �� *Open.MinimumLevel+$ PropertyChanged=�$ TwoWay=�� -	    �Name$ workTimeGrid�� �� $
��Auto ��� $
��Auto � �� �� $��3 ��� .+��� $��0��$��0��  locTabStartTime#���  �� $��1��$��0������ *Open.StartTime+$ PropertyChanged=�$ TwoWay=��� $��0��$��2��  locTabStopTime#���  �� $��1��$��2������ *Open.EndTime+$ PropertyChanged=�$ TwoWay=��� -
   $ openLotText��  	locTabLot#���    -     Name$ openLotValue��$ 8�� ��  �� ElementName$
 list�� �� Path$&  SelectedValue.MultiLegOpenVolume�$ PropertyChanged=�$ TwoWay=��� $��0,3,5,3q�$	��5,3q�$��2��#��[	 ���� $ 
Horizontal=�.+��� $��Center=�  locTabClose#���  �� -   $ tbCloseOrderType��$��	Underline{�$��15,0,0,0q�$
 Hand������ *Close.OrderTypeString+.���� $ Vertical=�.+���   locTabMinLevelClose#���    $ 2�� �� *Close.MinimumLevelClose+$ PropertyChanged=�$ TwoWay=���   locTabFixTP#���    $ 2�� �� *Close.FixTP+$ PropertyChanged=�$ TwoWay=���   locTabFixSL#���    $ 2�� �� *Close.FixSL+$ PropertyChanged=�$ TwoWay=�� -   $ gridMinOrderTime�� �� ��  �� �� $��3 ��� .+��� $��0��$��0��  locTabMinOrderTime#���  �� $��0��$��2��   locTabCloseTimer#���     $��1��$��0��
 �� *Open.MinOrderTimeSec+$ PropertyChanged=�$ TwoWay=�  $��1��$��2��
 �� *Close.CloseTimerSec+$ PropertyChanged=�$ TwoWay=���  ! locTabBrokers#��� ! $��1��$��2��$��3q�#��[ #��[ .���� -    ��Name$
 list��#��[ $��0q�$��Disabled=�
����������� *	Providers+���� .R��  � Orientation$ 
Horizontal=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Center=���  � �� ��  �� 
TargetType" ��.R��� $��_Border�� �� Padding$ 3q� J� SnapsToDevicePixels . ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,0&����  � Offset$ 0Q�  � Color$  #ff6e02��� $	 0.5Q�$  #ffff00��� $ 1Q�$  #ff6e02��._��� ! �� Triggers! P� " P� Property" �@��
IsSelected# P� Value# . . ��� $ �� 
TargetName$$ _Border�� � ��$ 3q��� $$ _Border�� �@��Padding$ 0q����� .R��� $��1q�$	��200������ $ .� 
" minmax# [" ���� $	 0,0&�$	 1,0&���� $ 0Q�$  #ff6e02��� $	 0.5Q�$  #ffff00��� $ 1Q�$  #ff6e02��._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Stretch=�% �� TextAlignment$% Center=�$
��Bold"�$��5q����� *Name+�� $ 
Horizontal=�$��Center=�$��1��$��3q�.+��� ���� *Prefix+�� ���� *Symbol+�� ���� *	Postfix+��  # locMultiOpenVolume#��� # $��5,0,5,0q��� ���� *MultiLegOpenVolume+ $ volConv# [$ � $��2�� �� �� $��10 ��� .+��� $��0��$��Right=����#ffec00��`�  % 	priceConv& `� 	Converter#& [% .1��� $ .��� $	 Bid��� $��2��$
��Left=����#97e2f9��`� #& [% .1��� $ .��� $	 Ask��� $��3��$ 
Horizontal=�$��Center=�.+��� ���� *Volume+# [$ �� $��5,0,0,0q� & locTabBrokerLot#��� & �� $��4��$ 
Horizontal=�$
��Left=�.+���  ' 	locTabGap#��� ' �� $��5,0,0,0q����� *MultiLegGap+# [$ �� $��4��$ 
Horizontal=�$��Right=�.+��� ���� *MultiLegProfit+# [$ �� $��5,0,0,0q� ( locTabBrokerPoint#��� ( ��  ) locTabStrategyLog#��� ) $��2��$��2��$��5q�#��[  * cwTabStripBackground#��[* .���� $
��Auto=�$
��Auto=�#��[ .���� -   $ logBlock��$
��Wrap=�$��Courier)�$��5q��� -   $ logClear�� + myLogCloseButton#��[+ $��22��$��5,5,0,0q�$��2��$	��Top=�$
��Left=�.��X