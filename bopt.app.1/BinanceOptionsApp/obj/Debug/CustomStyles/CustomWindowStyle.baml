   M S B A M L   `   `   `  ����N  JWesternpipsPrivate7, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:BinanceOptionsApp.CustomStylesBinanceOptionsApp.CustomStyles  5      6    0BinanceOptionsApp.CustomStyles.CustomWindowStyleR NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source   cwMinimizeButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName  cwCloseButton  cwWindowStyle'  !System.Windows.Shell.WindowChrome   NonClientFrameEdges   GlassFrameThickness   ResizeBorderThickness   CaptionHeight �� Path �� RelativeSource �� Mode �� AncestorType ��Name �� Padding J� SnapsToDevicePixels �� BitmapScalingMode&   System.Windows.Media.TextOptions   TextRenderingMode   TextFormattingMode  cwWindowTitleBackground  cwWindowBorderBrush � ColumnDefinitions ��Name �� Mode ��Name ��Name   IsHitTestVisibleInChrome9   3BinanceOptionsApp.CustomStyles.CustomWindowSettings3  -System.Windows.Media.Effects.DropShadowEffect   Color    Opacity!   
BlurRadius"   ShadowDepth#   	Direction$ �� 	Resources% �� Triggers& �� Binding' �� Value  cwWindowStatusForeground $  cwWindowStatusForegroundInactive  cwWindowBorderBrushInactive   5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      3cs,clr-namespace:BinanceOptionsApp.CustomStyles  5        5      �� 5   
   $ Resources.xaml?�6   %m  &	        &	 �    &	 �    7  7  7  7   7  7  7  7  7  �� 5      " ��6%   .�5   
   ��  � �� 5      �� 5      " ��6"   .R�5      � .+�5      �� $ 
PART_Image��6T   $��../res/minimize.png��6    $��26��6=   $��26��6H   6j   5       5      P� 5       �@J�IsMouseOver	 . . �5      �� $
 
PART_Image��6%    � ��$ ../res/minimize2.png��6O   5      P� 5       � ��	 . . �5      �� $
 
PART_Image��6%    �@J�Opacity$	 0.7Q�6P   5      5      5      5      �� 5      " ��6"   .�5   
   ��  � �� 5       �� 5!      " ��6"   .R�5"      � .+�5#      �� $ 
PART_Image��6Q   $��../res/close.png��6    $��26��6:   $��26��6E   6g   5$       5%      P� 5&       �@J�IsMouseOver	 . . �5'      �� $
 
PART_Image��6%    � ��$ ../res/close2.png��6O   5(      P� 5)       � ��	 . . �5*      �� $
 
PART_Image��6%    �@J�Opacity$	 0.7Q�6P   5+      5-      5/      50      �� 52      " �6"   .�53   
   ��  �@�AllowsTransparency . 6/   �� 54   
    �@�WindowStyle$
 None=�6(   �� 55   
    � ��
 ���� 6'   �� 56   
    �@�
ResizeMode$ CanResizeWithGrip=�6'   �� 57   
    �@J�SnapsToDevicePixels . 6?   �� 58   
    �@ WindowChrome 59        5:      $
 None=�5;      $ 0q�5<      $ 7q�5=       5>      �� $+ %(cs:CustomWindowSettings.TitleHeight)� �� $ FindAncestor=�$ WindowL�5A      -   5        �� 5C   
    � �� 5D      �� 5E      " �6"   .R�5F      �� $ PART_Container��6   $ 7 7 7 5q�65    . $ NearestNeighbor=�5G   8   ._�5H      � $ 	ClearType=�6   $ Display=�6I   8��  .+�5J      �� $��../Res/header.png��6$   $	��376��6?   $��63��6K   $	��Top=�5K   $   $
��Left=�6<   $��0,1,0,0q�6W   6j   �� 5L      $ PART_Border��6%   $
��Auto��6:   $
��Auto��6G   ���#97e2f98�� $��1q�5M   X   $ 0q�6l   ._�5N   "   ]� $��Stretch=�6,   
������ $��Stretch=�6c   .+�5O   &   �� $ TitleBar��6-   $	��Top=�6?   8�� $��0q�5P   )   ._�5Q   *   � ��6/   �� $+ %(cs:CustomWindowSettings.TitleHeight)� �� $ FindAncestor=�$ WindowL� 5R   .   �� 5S   2   $��36 �6C   �� 5T   2   �� 5U   2   $ MinButtonColumn��6C   $��28 �6\   �� 5V   2   $ MaxButtonColumn��6C   $��0 �6\   �� 5W   2   $��28 �6C   �� 5X   2   $��2 �6C   .+�5Z   .   �� $
 Icon��64   $��0��6B   $��Right=�5\   1   $��4,2,0,2q�6M   $��32��5]   1   $��32��6<   ��5[   1   �� $
 Icon�$ OneWay=� �� *TemplatedParent+5]   <   �� 5`   .   $ Caption��68   $��1��6I   $
��Left=�5a   1   $��Center=�6L   $��4,2,0,0q�6g   $��Arial)�5b   1   $��16(�6D   ��5c   1   �� $ Title�$ OneWay=� �� *TemplatedParent+�� 5e   .   $ 	MinButton��65   -   5         . $��2��5f   1   $	��Top=�6A   $��0,3,0,0q�5g   1   $��26��5h   1   $��26��6<   |�.  8�� 5m   /   �� 5n   .   $ 	MaxButton��6E   -   5        $��3��5n   5    . $	��Top=�5o   1   $��0,3,0,0q�5p   1   $��26��5q   1   $��26��5r   1   |�.  5v   /   �� 5w   .   $ CloseButton��5y   1   -   5         . $��4��5x   1   $	��Top=�5z   1   $��0,3,0,0q�5{   1   $��26��5|   1   $��26��5}   1   |�.  8�� 5�   /   5�   +   5�   '   �� 5�   &   5�   #   5�      5�      5�       5�      P� 5�       �@ StandardIcon	 .  . �5�      �� $

 Icon��6%    � y�$ 	Collapsed=�6M   6a   5�      P� 5�       �@ StandardCaption	 .  . �5�      �� $
 Caption��6%    � y�$ 	Collapsed=�6P   6d   5�      P� 5�       �@ AllowMaximize	 .  . �5�      �� $
 	MaxButton��6%    � y�$ 	Collapsed=�6R   6f   �� 5�      $
 MaxButtonColumn��6%    � ��$ 0 �6S   6_   5�      P� 5�       �@ AllowMinimize	 .  . �5�      �� $
 	MinButton��6%    � y�$ 	Collapsed=�6R   6f   �� 5�      $
 MinButtonColumn��6%    � ��$ 0 �6S   6_   5�      P� 5�       �@ UseCustomBackground	 . . �5�      �� $
 TitleBar��6%    6Q   �� $0 *(cs:CustomWindowSettings.CustomBackground)� �� $ FindAncestor=�$ WindowL� � ��6�   5�      P� 5�       �@ UseImageBackground	 . . �5�      �� $
 TitleBar��6%    � ��
 ���� 6g   �� 5�      $
 PART_Border��6%    6T   �� $/ )(cs:CustomWindowSettings.ImageBackground)� �� $ FindAncestor=�$ WindowL� � ��6�   5�      P� 5�       �@ UseTitleColor	 . . �5�      �� $
 Caption��6%    6P   �� $* $(cs:CustomWindowSettings.TitleColor)� �� $ FindAncestor=�$ WindowL� � ��6�   5�      P� 5�       �@�WindowState$	 Normal=�69   . �5�      �� $
 PART_Border��6%    �@J�Effect 5�   "     5�   &   $ black��5�   )   $	  0.5Q�5�   )   $! 7Q�5�   )   $" 2Q�5�   )   $	# 315Q�5�   )   5�      5�      5�      5�      $ 5�   
   ���� 5�      '��       " ��6   % 5�      �� 5�      & 6"   �� *
IsActive+ �� $ WindowL�' True6t   .a�5�      ��  � ��8  67   �� 5�       � ��8  67   5�      �� 5�      & 6"   �� *
IsActive+ �� $ WindowL�	' False6t   .a�5�      ��  � ��8  67   �� 5�       � ��8  67   5�      5�      5�      5�      5�      