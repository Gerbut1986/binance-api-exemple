   M S B A M L   `   `   `  ����N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  5   	   O,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  5   	   "    BinanceOptionsApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:BinanceOptionsApp     5   	   ;cs,clr-namespace:BinanceOptionsApp.CustomStyles     5   	   -   5          � WindowStyle$
  None=�5	       � AllowsTransparency .  � Title$ Binance Options App��5
   	   $	��725��6%   $
��1184��65    � WindowStartupLocation$ CenterScreen=�6E   
������� � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $( "CustomStyles/TabControlStyles.xaml?�6%   6S   �� 5      $% CustomStyles/TextBoxStyles.xaml?�6%   6P   %    5      .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      $��8 �6   6)   �� 5      $��21 �6   6*   .+�5   
   �� -   5         ��Name$ buHome��5   O   $��32��5      $��32��6   $	��Top=�6)   $��Right=�5      $��
0,-31,52,0q�6-   	 � Cursor$
	 Hand��6A   '  !System.Windows.Shell.WindowChrome
   IsHitTestVisibleInChrome
 . 5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��	0,-32,3,0q�6.   $��Right=�6A   .+�5!      �� $��16(�6   $��	Underline{�6&   ���#b6d800$��Trader Name:��6W   6m   �� 5"      -   5         ��Name$ txtUsername��5"   &   $��16(�6   
�������$��5,0,0,0q�6N   6a   �� 5#      -   5        $ 	txtUpdate��5#   &   $��16(�6   ���#b6d800$��15,0,0,0q�6N   6b   5$      �� 5%   
   -   5        $ tbSerialNumber��5&      $��	0,-12,3,0q�5%      $��Right=�6'   $	��Top=�6C   $��9(�6[   ���#aaeSerial number:#5(      �� 5*   
   -   5        $��
tabDynamic��5*      
���   ���6:   ��  5+         	TabHeader �� DataType �� RelativeSource �� AncestorType �� Path �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers �� Binding �� Mode �� Value F�Name �� StrokeStartLineCap �� StrokeEndLineCap �� Stretch �� Triggers P� Property P� Value  �� 
TargetName! �Name" � ColumnDefinitions  bToV# �� 	Converter 	 bToVi$ ��Name% J� Effect3  -System.Windows.Media.Effects.DropShadowEffect&   
BlurRadius'   Color(   Opacity��%�  &	        7  7  7  �� 5,      $ TabItem��61   .R�5-      ]� .+�5.      �� $ 	btnDelete��6�   -   5        $��1��5.   !   $��15��61   $��15��6=   $��Center=�6H   $��Center=�6e   $��Right=�5/   !   $��5,0,0,0q�68   $��0q�6I   ��50   !   ��  �� " ��$ .���51      �� 52   "   " ��6(   .�53   &   ��  � ��
 ���� 6C   �� 54   &    �@�Cursor$
 Hand��6?   �� 55   &    � |� .  6B   �� 56   &    � �� 57   *   �� 58   .   " ��6>   .R�59   2   � #��z��67    5:   6   ���� 5;   :   'F�       " F�6@   .�5<   >   ��  � �� �#fff6f    5=   >   �� 5>   B    6N   �� *
IsSelected+ �� $ TabItemL�$ FindAncestor=� True6�   .a�5?   F   ��  � �� �#0234a66q   5@   C   5B   ;   5C   7   .+�5D   6   F� $ 
ButtonPath��6;   $��2q�5E   A   +����        �@B @B �    @B �@B     	$��2��5G   A   $
 Flat=�5H   A   $
 Flat=�5I   A   $ Uniform=�5J   A   $��Center=�5K   A   $��Center=�5L   A   5M   7   5N   3    5O   2   P� 5P   6    � {� .  . �5Q   :   ��  � y�$ Hidden=�6W   5R   7   P� 5S   6    �@J�IsMouseOver . . �5T   :   ��  � �� �	#a0ffffff6W   �� 5U   :   $  
ButtonPath��6A    � �� �#0234a66k   5V   7   P� 5W   6    � �� . . �5X   :   �� $  
ButtonPath��6A    � ��$ 2.5,2.5,1.5,1.5q�6k   5Y   7   5[   /   5]   '   5^   #   5`      � 5a      $! 
headerGrid��6:   -   5        $��Center=�5a      " 5d      �� 5e   "   $��16 �63   6@   �� 5f   "   65   .+�5h      �� $��18��6$   $��18��6/   $��Center=�6;   y�6V   ��  �� " ��$ DataContext.FeederOk�8#   6�   �� 5i      $��18��6$   $��18��6/   $��Center=�6;   y�6V   ��  �� " ��$ DataContext.FeederOk�8#  6�   �� 5j      $��1��6(   $��5,0,5,0q�6�   $��Center=�6�   ��68   ��  �� " ��$ DataContext.Title�6�   �� 5k      $$ 
editHeader��5l   o   -	   5        $��1��5l   !   ���#ddd���#c6d820
����  �y�5k   &   ��  �� " ��$ DataContext.IsInEditMode�8#  .��5m   `   % 5n   "     5o   &   $& 5Q�67   $' Green��6F   $	( 0.5Q�6T   6d   .��5p   2   5q      5r      5s      5t      5u      5v      �� 5x   
   $��1��6   $��Stretch=�6!   $��Stretch=�6?   �� 5y   
   $��2��6   $��Stretch=�6!   $��Stretch=�6?   �� 5z   
   
�������$��2��6&   $��Center=�63   $��14(�6N   $��Right=�6\   $	��5,0q�6x   Sergey Riehl © 2023-20245|      �� 5~   
   -
   5        $ buClose��5�      $��X��5~      $��32��6   $��32��6(   $	��Top=�64   $��Right=�5      $��0,5,10,0q�6-   $
	 Hand��6?   
 . 5�   O   �� 5�   
   -   5        $ 
buFullScrn��5�      $��[]��5�      $��32��6   $��32��6)   $	��Top=�65   $��Right=�5�      $��0,5,46,0q�6)   $
	 Hand��6;   
 . 5�   :   �� 5�   
   -   5        $ 
buZvernuty��5�      $��_��5�      $��32��6   $��32��6(   $	��Top=�64   $��Right=�5�      $��0,5,82,0q�6)   $
	 Hand��6;   
 . 5�   N   5�      5�      