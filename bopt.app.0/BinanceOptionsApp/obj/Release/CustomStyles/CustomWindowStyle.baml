   M S B A M L   `   `   `  ���� N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  6    0BinanceOptionsApp.CustomStyles.CustomWindowStyleR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   cwMinimizeButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName  cwCloseButton  cwWindowStyle'  !System.Windows.Shell.WindowChrome   NonClientFrameEdges   GlassFrameThickness   ResizeBorderThickness   CaptionHeight �� Path �� RelativeSource �� Mode �� AncestorType ��Name �� Padding J� SnapsToDevicePixels �� BitmapScalingMode&   System.Windows.Media.TextOptions   TextRenderingMode   TextFormattingMode  cwWindowTitleBackground  cwWindowBorderBrush � ColumnDefinitions ��Name �� Mode ��Name ��Name   IsHitTestVisibleInChrome9   3BinanceOptionsApp.CustomStyles.CustomWindowSettings3  -System.Windows.Media.Effects.DropShadowEffect   Color    Opacity!   
BlurRadius"   ShadowDepth#   	Direction$ �� 	Resources% �� Triggers& �� Binding' �� Value  cwWindowStatusForeground $  cwWindowStatusForegroundInactive  cwWindowBorderBrushInactive   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     3cs,clr-namespace:BinanceOptionsApp.CustomStyles    �� $ Resources.xaml?�%�  &	        &	     &	 2    7  7  7  7   7  7  7  7  7  �� " ��.���  � �� �� " ��.R�� .+��� $ 
PART_Image��$��../res/minimize.png��$��26��$��26�� P�  �@J�IsMouseOver	 . . ��� $
 
PART_Image�� � ��$ ../res/minimize2.png��P�  � ��	 . . ��� $
 
PART_Image�� �@J�Opacity$	 0.7Q��� " ��.���  � �� �� " ��.R�� .+��� $ 
PART_Image��$��../res/close.png��$��26��$��26�� P�  �@J�IsMouseOver	 . . ��� $
 
PART_Image�� � ��$ ../res/close2.png��P�  � ��	 . . ��� $
 
PART_Image�� �@J�Opacity$	 0.7Q��� " �.���  �@�AllowsTransparency . ��  �@�WindowStyle$
 None=���  � ��
 ���� ��  �@�
ResizeMode$ CanResizeWithGrip=���  �@J�SnapsToDevicePixels . ��  �@ WindowChrome   $
 None=�$ 0q�$ 7q� �� $+ %(cs:CustomWindowSettings.TitleHeight)� �� $ FindAncestor=�$ WindowL�-   ��  � �� �� " �.R��� $ PART_Container��$ 7 7 7 5q� . $ NearestNeighbor=�._�� $ 	ClearType=�$ Display=�8��  .+��� $��../Res/header.png��$	��376��$��63��$	��Top=�$
��Left=�$��0,1,0,0q��� $ PART_Border��$
��Auto��$
��Auto�����#97e2f98�� $��1q�$ 0q�._�]� $��Stretch=�
������ $��Stretch=�.+��� $ TitleBar��$	��Top=�8�� $��0q�._�� ���� $+ %(cs:CustomWindowSettings.TitleHeight)� �� $ FindAncestor=�$ WindowL� �� $��36 ��� �� $ MinButtonColumn��$��28 ��� $ MaxButtonColumn��$��0 ��� $��28 ��� $��2 �.+��� $
 Icon��$��0��$��Right=�$��4,2,0,2q�$��32��$��32������ $
 Icon�$ OneWay=� �� *TemplatedParent+�� $ Caption��$��1��$
��Left=�$��Center=�$��4,2,0,0q�$��Arial)�$��16(����� $ Title�$ OneWay=� �� *TemplatedParent+�� $ 	MinButton��-    . $��2��$	��Top=�$��0,3,0,0q�$��26��$��26��|�.  8�� �� $ 	MaxButton��-   $��3�� . $	��Top=�$��0,3,0,0q�$��26��$��26��|�.  �� $ CloseButton��-    . $��4��$	��Top=�$��0,3,0,0q�$��26��$��26��|�.  8�� ��  P�  �@ StandardIcon	 .  . ��� $

 Icon�� � y�$ 	Collapsed=�P�  �@ StandardCaption	 .  . ��� $
 Caption�� � y�$ 	Collapsed=�P�  �@ AllowMaximize	 .  . ��� $
 	MaxButton�� � y�$ 	Collapsed=��� $
 MaxButtonColumn�� � ��$ 0 �P�  �@ AllowMinimize	 .  . ��� $
 	MinButton�� � y�$ 	Collapsed=��� $
 MinButtonColumn�� � ��$ 0 �P�  �@ UseCustomBackground	 . . ��� $
 TitleBar�� �� $0 *(cs:CustomWindowSettings.CustomBackground)� �� $ FindAncestor=�$ WindowL� � ��P�  �@ UseImageBackground	 . . ��� $
 TitleBar�� � ��
 ���� �� $
 PART_Border�� �� $/ )(cs:CustomWindowSettings.ImageBackground)� �� $ FindAncestor=�$ WindowL� � ��P�  �@ UseTitleColor	 . . ��� $
 Caption�� �� $* $(cs:CustomWindowSettings.TitleColor)� �� $ FindAncestor=�$ WindowL� � ��P�  �@�WindowState$	 Normal=�. ��� $
 PART_Border�� �@J�Effect   $ black��$	  0.5Q�$! 7Q�$" 2Q�$	# 315Q�$ ���� '��       " ��% �� & �� *
IsActive+ �� $ WindowL�' True.a���  � ��8  ��  � ��8  �� & �� *
IsActive+ �� $ WindowL�	' False.a���  � ��8  ��  � ��8  