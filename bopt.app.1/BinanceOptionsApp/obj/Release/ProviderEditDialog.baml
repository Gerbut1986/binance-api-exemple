   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  O,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  *    $BinanceOptionsApp.ProviderEditDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:BinanceOptionsApp     ;cs,clr-namespace:BinanceOptionsApp.CustomStyles     =controls(clr-namespace:BinanceOptionsApp.Controls     -   $	��250��$	��540��  � WindowStartupLocation$  CenterOwner=�   cwWindowStyle#��[   � 
ResizeMode$ NoResize=�
������� � Title$ Edit Provider��$��14(�.��� 
������� � 	Resources  �� MergedDictionaries �� Source �� 
TargetType  EditorTextBox �� BasedOn  LightCheckBox/  )BinanceOptionsApp.Controls.IntegerTextBox  myGreenThinButton  StandardGroupBox��  �� $&  CustomStyles/GroupBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $$ CustomStyles/ButtonStyles.xaml?�%�   '��       7  '��      7  '       7  '�� -     7  '�� <     7  �� " ��8   �� " ��8   �� "  8   �� " ��8   �� " ��8    � RowDefinitions �� �� $��48 �.+��� $��Center=�	 �� Orientation$	 Vertical=�$��5q�.+�'   !BinanceOptionsApp.ProviderControl  -   
  Name$
 providerControl���� $	 
Horizontal=�$��0,20,0,0q�.+��� $��Center=�$��80��
Bid Color:�� -   $��5,0,0,0q�
����ح�$��1q�$��80��$��24�� � Cursor$
 Hand������  �� Color �� *
BidColor+�� $��15,0,0,0q�$��Center=�$��80��
Bid Width:  $��80��$��Center=�$��5,0,0,0q�   Value �� *
BidWidth+�� $��Center=�$	��120��$��15,0,0,0q����� *
BidWidth+����  �� *
BidColor+�� $	 
Horizontal=�$��0,5,0,0q�.+��� $��Center=�$��80��
Ask Color:�� -   $��5,0,0,0q�
����ح�$��1q�$��80��$��24��$
 Hand������  �� *
AskColor+�� $��15,0,0,0q�$��Center=�$��80��
Ask Width:  $��80��$��Center=�$��5,0,0,0q� �� *
AskWidth+�� $��Center=�$	��120��$��15,0,0,0q����� *
AskWidth+����  �� *
AskColor+�� $	 
Horizontal=�$��1��$��Center=�$��Center=�.+��� -    ��Name$ buAccept��  myBlueButton#��[ $��80�� �� 	IsDefault .   locDlgAccept#���  �� -   $ buCancel��  myRedButton#��[ $��80��$��5,0,0,0q� �� IsCancel .   locDlgCancel#���  