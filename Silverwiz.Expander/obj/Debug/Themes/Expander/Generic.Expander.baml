   M S B A M L   `   `   `  ����H  DOdyssey, Version=1.0.7908.6419, Culture=neutral, PublicKeyToken=null7 clr-namespace:Silverwiz.ExpanderSilverwiz.Expander  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   animTime$    Silverwiz.Expander.OdcExpander  �� 
TargetType �� Property �� Value �� 
TargetType*   $Silverwiz.Expander.OdcExpanderHeader  Name    HeaderBackground    HeaderBorderThickness   	CanExpand    CornerRadius	   CornerRadius
    
FocusImage   
FocusImage    Image   Image    
IsExpanded   
IsExpanded    
IsSelected   
IsSelected    NotificationCount   NotificationCount    ShowEllipse   ShowEllipse J� SnapsToDevicePixels d� 	IsChecked �� RelativeSource �� Path+   %Silverwiz.Expander.AnimationDecorator   Duration   
IsExpanded ��Name �� Padding    ExpandAreaHeight � LayoutTransform  �� ScaleY! �� Triggers" P� 
SourceName# P� Property$ P� Value% �� 
TargetName& ]� 
Conditions' �� 
SourceName( �� Property) �� Value* :� RoutedEvent+ �� TargetProperty, g� DecelerationRatio- g� Duration. �� 
TargetName	/ W� To�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      *local clr-namespace:Silverwiz.Expander  5      %�  &	        '        7   7   E�5      0:0:0.4D�6)   �� 5      "    6   .�5   
   ��  � �� �	#408080806'   �� 5   
    � �� �#7A728C6(   �� 5   
    �@  HeaderBackground �	#40A0A0A06-   �� 5	   
   ! �@  MouseOverHeaderBackground �	#60FFFFFF66   �� 5
   
   ! �@  MouseOverHeaderForeground �	#F000000066   �� 5   
    �@  HeaderBorderThickness$	 0,1q�62   �� 5   
    �@  CornerRadius$ 0��6)   �� 5   
    �@  ShowEllipse . 6(   �� 5   
    � ��$ 4q�6$   �� 5   
    � ��$ 0,-1,0,0q�6#   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� ._�5      �� .+�5        $ header��66   #��z 5   6   #��z��5   6   #��z 5   6    . #��z��5   6   #��z��5   6   #	 z 5   6   # z
 5   6   # z 5   6   # z 5!   6   # z 5"   6   # z 5#   6   # z 5$   6    .  5   6   ��  �� *TemplatedParent+$ 
IsExpanded�5%   6     5'      $��1��67   #��z��5(   7   #��z��5)   7   #��z��5*   7   8    .  5,   7   ��  �� *TemplatedParent+$ 
IsExpanded�._�5/   "   �� $ panel��6)   $��1��50   )   #��z��51   )   #��z��52   )   $��0q�53   )   # z��54   )   ._�55   &   �� #��z 67   $��Bottom=�56   7   #��z��57   7   #��z��58   7   5:   #   5;      5<       5=      �� 5>      
scaleT��$  1Q�6=   5@      ! 5A      P� 5B      $" header��6"   # �@J�IsMouseOver$ . . �5C      �� $% header��6%    6O   ��  �� *TemplatedParent+$ MouseOverHeaderForeground� � ���� 5D      $% header��6%    6O   ��  �� *TemplatedParent+$ MouseOverHeaderBackground� � ��5E      ]� 5F      & 5G      �� 5H   "   $' header��6,   ( � ��) . 6U   �� 5I   "   ( �@  HasPressedBackground) . 6L   ./�5K      �� $% header��6%    6O   ��  �� *TemplatedParent+$ PressedHeaderBackground� � ��5L      :� 5M      $&*  local:OdcExpander.MinimizedEvent��6'   [�5N      �� 5O   "   .��5P   &   �� $+ ScaleY�61   .��5Q   *   W� $	, 0.8Q�6:   8-  $. scaleT��5S   :   $/ 0X�5T   :   5U   '   5V   #   5X      5Z      5\      5]      5_      