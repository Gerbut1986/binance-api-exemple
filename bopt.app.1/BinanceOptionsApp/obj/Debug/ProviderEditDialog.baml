   M S B A M L   `   `   `  ����N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  5   	   O,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  5	   	   G(clr-namespace:BinanceOptionsApp.ControlsBinanceOptionsApp.Controls  5   	   *    $BinanceOptionsApp.ProviderEditDialog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:BinanceOptionsApp     5   	   ;cs,clr-namespace:BinanceOptionsApp.CustomStyles     5	   	   =controls(clr-namespace:BinanceOptionsApp.Controls     5   	   -   5        $	��250��5   	   $	��540��6     � WindowStartupLocation$  CenterOwner=�6"      cwWindowStyle#��[  5
   	    � 
ResizeMode$ NoResize=�5   	   
������� � Title$ Edit Provider��5      $��14(�5   	   .��5      � 
������� � 	Resources 5   
    �� MergedDictionaries �� Source �� 
TargetType  EditorTextBox �� BasedOn  LightCheckBox/  )BinanceOptionsApp.Controls.IntegerTextBox  myGreenThinButton  StandardGroupBox�� 5       5      �� 5      $&  CustomStyles/GroupBoxStyles.xaml?�6)   6U   �� 5      $% CustomStyles/TextBoxStyles.xaml?�6)   6T   �� 5      $&  CustomStyles/CheckBoxStyles.xaml?�6)   6U   �� 5      $$ CustomStyles/ButtonStyles.xaml?�6)   6S   %�   '��       7  '�� "     7  '  D     7  '�� f     7  '�� �     7  �� 5      " ��6   8   6X   �� 5      " ��6   8   6Y   �� 5      "  6   8   6h   �� 5      " ��6   8   6[   �� 5      " ��6   8   6\   5       � RowDefinitions 5    
   �� 5!      6   �� 5"      $��48 �6   6*   .+�5$   
   �� $��Center=�6   	 �� Orientation$	 Vertical=�60   $��5q�6G   .+�5%      '   !BinanceOptionsApp.ProviderControl  -   5        
  Name$
 providerControl��5%   $   �� 5&      $	 
Horizontal=�6   $��0,20,0,0q�62   .+�5'      �� $��Center=�6   $��80��67   
Bid Color:6Q   �� 5(      -   5        $��5,0,0,0q�5(      
����ح�$��1q�6B   $��80��6V   $��24��6a    � Cursor$
 Hand��6�   ��5)      �� 5*       �� Color 6*   �� *
BidColor+5,      �� 5-      $��15,0,0,0q�6   $��Center=�6.   $��80��6I   
Bid Width:6c     5.      $��80��6E   $��Center=�6S   $��5,0,0,0q�6n      Value 6*   �� *
BidWidth+6�   �� 5/      $��Center=�6   $	��120��67   $��15,0,0,0q�6b   ��6F   �� *
BidWidth+��50      �� 51       6*   �� *
BidColor+53      54      �� 55      $	 
Horizontal=�6   $��0,5,0,0q�62   .+�56      �� $��Center=�6   $��80��67   
Ask Color:6Q   �� 57      -   5        $��5,0,0,0q�57      
����ح�$��1q�6B   $��80��6V   $��24��6a   $
 Hand��6�   ��58      �� 59       6*   �� *
AskColor+5;      �� 5<      $��15,0,0,0q�6   $��Center=�6.   $��80��6I   
Ask Width:6c     5=      $��80��6E   $��Center=�6S   $��5,0,0,0q�6n    6*   �� *
AskWidth+6�   �� 5>      $��Center=�6   $	��120��67   $��15,0,0,0q�6b   ��6F   �� *
AskWidth+��5?      �� 5@       6*   �� *
AskColor+5B      5C      5D      �� 5E   
   $	 
Horizontal=�6   $��1��6.   $��Center=�6;   $��Center=�6V   .+�5F      �� -   5         ��Name$ buAccept��5F        myBlueButton#��[ 6'   $��80��6M    �� 	IsDefault .   locDlgAccept#���  6�   6�   �� 5G      -   5        $ buCancel��5G        myRedButton#��[ 6'   $��80��6L   $��5,0,0,0q�6Z    �� IsCancel .   locDlgCancel#���  6�   6�   5H      5I      5J      