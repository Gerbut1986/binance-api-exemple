   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BinanceOptionsAppBinanceOptionsApp  O,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  "    BinanceOptionsApp.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:BinanceOptionsApp     ;cs,clr-namespace:BinanceOptionsApp.CustomStyles     -     � Title$  Westernpips Private 7��$	��725��$
��1184�� � WindowStartupLocation$ CenterScreen=�
������� � 	Resources  �� MergedDictionaries �� Source��  �� $( "CustomStyles/TabControlStyles.xaml?��� $% CustomStyles/TextBoxStyles.xaml?�%    .���  � RowDefinitions �� �� $��8 ��� $��21 �.+��� -    ��Name$ buHome��$��32��$��32��$	��Top=�$��Right=�$��
0,-63,52,0q� � Cursor$
 Hand��'  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome . �� -   $ 	buOptions��$��32��$��32��$	��Top=�$��Right=�$��
0,-63,95,0q�$
 Hand�� . �� 	 �� Orientation$	 
Horizontal=�$��	0,-32,3,0q�$��Right=�.+��� $��16(�$��	Underline{����#b6d800$��Trader Name:���� -   
 ��Name$
 txtUsername��$��16(�
�������$��5,0,0,0q��� -   $
 	txtUpdate��$��16(����#b6d800$��15,0,0,0q��� -   $
 tbSerialNumber��$��	0,-12,3,0q�$��Right=�$	��Top=�$��9(����#aaeSerial number:#�� -   $��
tabDynamic������     	TabHeader �� DataType �� RelativeSource �� AncestorType �� Path �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers �� Binding �� Mode �� Value F�Name �� StrokeStartLineCap �� StrokeEndLineCap �� Stretch �� Triggers P� Property P� Value �� 
TargetName �Name  � ColumnDefinitions  bToV! �� 	Converter 	 bToVi" ��Name# J� Effect3  -System.Windows.Media.Effects.DropShadowEffect$   
BlurRadius%   Color&   Opacity��%�  &	        7  7  7  �� $ TabItem��.R�]� .+��� $ 	btnDelete��-   $��1��$��15��$��15��$��Center=�$��Center=�$��Right=�$��5,0,0,0q�$��0q�����  �� " ��$ .����� " ��.���  � ��
 ���� ��  �@�Cursor$
 Hand����  � |� .  ��  � �� �� " ��.R�� #��z�� ���� 'F�       " F�.���  � �� �#fff ��  �� *
IsSelected+ �� $ TabItemL�$ FindAncestor=� True.a���  � �� �#0234a6.+�F� $ 
ButtonPath��$��2q�+����        �@B @B �    @B �@B     	$��2��$
 Flat=�$
 Flat=�$ Uniform=�$��Center=�$��Center=� P�  � {� .  . ���  � y�$ Hidden=�P�  �@J�IsMouseOver . . ���  � �� �	#a0ffffff�� $ 
ButtonPath�� � �� �#0234a6P�  � �� . . ��� $ 
ButtonPath�� � ��$ 2.5,2.5,1.5,1.5q�� $ 
headerGrid��-	   $��Center=�  �� $��16 ��� .+��� $��Res/green-orb.png��$��18��$��18��$��Center=�y���  �� " ��$ DataContext.FeederOk�8!   �� $��Res/red-orb.png��$��18��$��18��$��Center=�y���  �� " ��$ DataContext.FeederOk�8!  �� $��1��$��5,0,5,0q�$��Center=�����  �� " ��$ DataContext.Title��� $" 
editHeader��-
   $��1�����#ddd���#c6d820
����  �y���  �� " ��$ DataContext.IsInEditMode�8!  .��#   $$ 5Q�$% Green��$	& 0.5Q�.���� $��1��$��Stretch=�$��Stretch=��� $��2��$��Stretch=�$��Stretch=��� $��2��$��Center=�$��14(�$��Right=�$	��5,0q�Copyright © 2023-2024�� $��2��$��9��$	��502��$��Center=�$
��Left=�$	��5,0q�$��Res/status.png���� -   $ buFullScrn_Copy��$��X��$��32��$��32��$	��Top=�$��Right=�$��
0,-63,10,0q�$
 Hand�� . 