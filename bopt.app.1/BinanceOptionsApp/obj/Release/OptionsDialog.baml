   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  O,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  %    BinanceOptionsApp.OptionsDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9ctrl(clr-namespace:BinanceOptionsApp.Controls     ;cs,clr-namespace:BinanceOptionsApp.CustomStyles     =controls(clr-namespace:BinanceOptionsApp.Controls     -   $	��520��$	��460��  � WindowStartupLocation$  CenterOwner=� � 
ResizeMode$ NoResize=�
�������$��14(�.��� 
������� � 	Resources  �� MergedDictionaries �� Source �� 
TargetType   EditorTextBox �� BasedOn  LightCheckBox/  )BinanceOptionsApp.Controls.IntegerTextBox  myGreenThinButton  StandardGroupBox��  �� $&  CustomStyles/GroupBoxStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?��� $&  CustomStyles/CheckBoxStyles.xaml?��� $$ CustomStyles/ButtonStyles.xaml?�%�   '��       7   '��      7  '       7   '�� -     7  '�� <     7  �� " ��8   �� " ��8   �� "  8   �� " ��8   �� " ��8    � RowDefinitions �� �� $��48 �.+���  �� Orientation$ Vertical=�$��5q�.+���   locSmtpServer#���  �� ���� *Smtp.Server+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� Mode$
 TwoWay=���   locSmtpPort#���       Value �� *	Smtp.Port+$
 TwoWay=�$	 PropertyChanged=���   locSmtpSender#���  �� ���� *Smtp.Sender+$
 TwoWay=�$	 PropertyChanged=���   locSmtpPassword#���  �� ���� *Smtp.Password+$
 TwoWay=�$	 PropertyChanged=��� 
�������$	��0,3q� d� 	IsChecked �� *
Smtp.SSL+$
 TwoWay=�$	 PropertyChanged=�.��
SMTP SSL�� $��0,10,0,0q�  locSmtpRecipients#���  �� ���� *Smtp.Recipients+$
 TwoWay=�$	 PropertyChanged=��� $��0,15,0,0q�
������� �� *Clicker.UseClickerForOpen+$
 TwoWay=�$	 PropertyChanged=�.��Use Clicker For Open�� $��0,0,0,0q�
������� �� *Clicker.UseClickerForClose+$
 TwoWay=�$	 PropertyChanged=�.��Use Clicker For Close�  � ColumnDefinitions �� �� �� .+��� $	��Buy��
��� � �{��� *	Started+ 		 bToBi �� 	Converter# [	 .���� $��20,10q�.+��� -    ��Name$ buSetBuyCoord��.��Set�� -   $ buShowBuyCoord��.��Show�� -   $ buClickBuyCoord��.��Click�� $
��Sell��
���  ��$��1��{��� *	Started+# [	 .���� $��20,10q�.+��� -   $ buSetSellCoord��.��Set�� -   $ buShowSellCoord��.��Show�� -   $ buClickSellCoord��.��Click�� $��Close��
����ح�$��2��{��� *	Started+# [	 .���� $��20,10q�.+��� -   $ buSetCloseCoord��.��Set�� -	   $ buShowCloseCoord��.��Show�� -
   $ buClickCloseCoord��.��Click�� $ 
Horizontal=�$��1��$��Center=�$��Center=�.+��� -   $ buAccept�� 
 myBlueButton#��[
 $��80�� �� 	IsDefault .   locDlgAccept#���  �� -   $ buCancel��  myRedButton#��[ $��80��$��5,0,0,0q� �� IsCancel .   locDlgCancel#���  �� -   $ buTest��$��1��#��[
 $��80��  
locDlgTest#���  $��Center=�$
��Left=�$��5q�