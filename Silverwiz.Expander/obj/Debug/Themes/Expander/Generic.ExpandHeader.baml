   M S B A M L   `   `   `  ����H  DOdyssey, Version=1.0.7908.6419, Culture=neutral, PublicKeyToken=null7 clr-namespace:Silverwiz.ExpanderSilverwiz.Expander  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 !  BooleanToVisibilityConverter1*    $Silverwiz.Expander.OdcExpanderHeader  �� 
TargetType �� Property �� Value �� 
TargetType    CornerRadius �� CornerRadius � ColumnDefinitions �� Orientation ��Name	 �� Left

 �� Top J� SnapsToDevicePixels    Image    
FocusImage �� RelativeSource �� Path �� 	Converter �� 
StartPoint �� EndPoint � Offset � Color    NotificationCount ��Name J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Color �Name ��Name J� Opacity �� Triggers P� Property  P� Value! �� 
TargetName" ]� 
Conditions# �� Property$ �� Value% �� Angle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      *local clr-namespace:Silverwiz.Expander  5      %�  &	        '        7   7   �� 5      6#   �� 5      "    6   .�5   
   ��  �@  CollapseGeometry= �� �  � � 	=  	= ��pr  � � �  �D � 	=  z ��pr  �D 	6-   �� 5	   
    �@  CornerRadius$ 8,8,0,0��6)   �� 5
   
    � �� �#8080806(   �� 5   
    � ��$	 0,1q�6,   �� 5   
    � ��
 �   �6'   �� 5   
    � ��$ 30��6#   �� 5   
    � ��$ 18(�6%   �� 5   
    � �� �	#608080806'   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� #��z��6   #��z��5      #��z��5      # z 5      ._�5      �  5      �� 5   "   $��* �63   �� 5   "   $��40 �63   .+�5      �� $��6,0,6,0q�6)   $��Center=�5   )   $ 
Horizontal=�5   )   .+�5   "   �� $��30��6)   $��30��5    )   $��Center=�5!   )   $��Center=�5"   )   .+�5#   &   �� $ image��6,   $	 2��5$   ,   $
 2��5%   ,   $��26��5&   ,   $��26��5'   ,   $��Center=�5(   ,    . #��z 5*   ,   $
��Fill=�5+   ,   �� 5,   &   $ 
focusImage��6,   $	 2��5-   ,   $
 2��5.   ,   $��26��5/   ,   $��26��50   ,   $��Center=�51   ,    . #��z 53   ,   $
��Fill=�54   ,   $y�	Collapsed=�55   ,   � 57   &   $	 5��6+   $
 5��58   +   $��21��59   +   $��21��5:   +   y�5;   +   ��  �� *TemplatedParent+$ HasNotificationCount�8   .+�5>   *   A� $��21��62   $��21��5?   2   
�������$��1��5A   2   ��5B   .   �� 5C   2   $ 0.5,0&�6F   $ 0.5,1&�6Y   .��5D   6   � $ 0Q�6C   $ 	#FFD66E5A��6N   � 5E   6   $ 1Q�6C   $ 	#FFA43F42��6N   5F   3   5H   +   �� 5I   *   $��Center=�64   $	��Top=�5J   4   $��14(�5K   4   $��Normal"�5L   4   
�������#��z 5N   4   5O   '   5P   #   �� 5R   "   $ content��63   $��0��5S   3   $��4,0,0,0q�5T   3   $��Center=�5U   3   #��z��5V   3   #��z��5W   3   #��z��5X   3   #��z��5Y   3   #��z��5Z   3    5[   &     5\   *   
effect��$ 5Q�5]   ;   $ 2Q�5^   ;   $ White��5_   ;   5b   #   5c      � 5d      $ circleButton��6#   $��1��5e   #   $��0,0,4,0q�5f   #   $��Center=�5g   #   .+�5h   "   �� $
 path��6)   $��12��5i   )   $��12��5j   )   $��0q�5k   )   y�5l   )   ��  �� *TemplatedParent+$ 	CanExpand�8  .+�5o   &   �� $��12��6,   $	 0.6Q�67   $T��N/PrimaCartoonizer;component/Images/sidebar_button_big_indicator-contracted.png��6E   5p   #   5q      5r      5s       5t      P� 5u       �@  
IsSelected  . . �5v      �� $! image��6%    � y�$ 	Collapsed=�6N   �� 5w      $! 
focusImage��6%    � y�$ Visible=�6S   5x      ]� 5y      " 5z      �� 5{   "   # �@  
IsExpanded$ . 6B   �� 5|   "   # �@  HasExpandGeometry$ .  6I   ./�5~      �� $
! path��6%    �@�LayoutTransform 5   "   �� 5�   &   $% 90Q�66   5�      5�      5�      5�      5�      5�      