   M S B A M L   `   `   `  ����H  DOdyssey, Version=1.0.7908.6419, Culture=neutral, PublicKeyToken=null7 clr-namespace:Silverwiz.ExpanderSilverwiz.Expander  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   animTime#    Silverwiz.Expander.OdcSummary  �� 
TargetType �� Property �� Value �� 
TargetType*   $Silverwiz.Expander.OdcExpanderHeader  Name    HeaderBackground    HeaderBorderThickness   	CanExpand    CornerRadius	   CornerRadius
    
FocusImage   
FocusImage    Image   Image    NotificationCount   NotificationCount    ShowEllipse   ShowEllipse J� SnapsToDevicePixels d� 	IsChecked �� RelativeSource �� Path+   %Silverwiz.Expander.AnimationDecorator   Duration    OpacityAnimation   OpacityAnimation   
IsExpanded ��Name �� Padding � LayoutTransform �� ScaleY �� Triggers P� 
SourceName  P� Property! P� Value" �� 
TargetName# ]� 
Conditions$ �� 
SourceName% �� Property& �� Value' :� RoutedEvent( �� TargetProperty) g� DecelerationRatio* g� Duration+ �� 
TargetName	, W� To�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      *local clr-namespace:Silverwiz.Expander  5      %�	  &	        '        7   7   7   E�5      0:0:0.4D�6)   �� 5      "    6   .�5   
   ��  � �� �	#408080806'   �� 5   
    � �� �	#604040406(   �� 5   
    �@  HeaderBackground �	#40A0A0A06-   �� 5	   
   ! �@  MouseOverHeaderBackground �	#60FFFFFF66   �� 5
   
   ! �@  MouseOverHeaderForeground �	#F000000066   �� 5   
    �@  HeaderBorderThickness$ 1q�62   �� 5   
    �@  CornerRadius$ 0��6)   �� 5   
    �@  ShowEllipse . 6(   �� 5   
    � ��$ 8q�6$   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� ._�5      �� .+�5        $ header��66   $��0��5   6   #��z 5   6   #��z��5   6   #��z 5   6    . #��z��5   6   #��z��5   6   #	 z 5   6   # z
 5   6   # z 5   6   # z 5!   6   # z 5"   6    .  5   6   ��  �� *TemplatedParent+$ 
IsExpanded�5#   6     5$      $��1��67   #��z��5%   7   #��z��5&   7   #��z��5'   7   8   # z 5+   7    .  5)   7   ��  �� *TemplatedParent+$ 
IsExpanded�._�5-   "   �� $ panel��6)   $��1��5.   )   #��z��5/   )   #��z��50   )   $��0q�51   )   # z��52   )   ._�53   &   �� $��Bottom=�67   #��z��54   7   #��z��55   7   57   #   58      59       5:      �� 5;      
scaleT��$ 1Q�6=   5=       5>      P� 5?      $ header��6"     �@J�IsMouseOver! . . �5@      �� $" header��6%    6O   ��  �� *TemplatedParent+$ MouseOverHeaderForeground� � ���� 5A      $" header��6%    6O   ��  �� *TemplatedParent+$ MouseOverHeaderBackground� � ��5B      ]� 5C      # 5D      �� 5E   "   $$ header��6,   % � ��& . 6U   �� 5F   "   % �@  HasPressedBackground& . 6L   ./�5H      �� $" header��6%    6O   ��  �� *TemplatedParent+$ PressedHeaderBackground� � ��5I      :� 5J      $%' local:OdcSummary.MaximizedEvent��6'   [�5K      �� 5L   "   .��5M   &   �� $( ScaleY�61   .��5N   *   W� $	) 0.8Q�6:   8*  $+ scaleT��5P   :   $, 1X�5Q   :   5R   '   5S   #   5U      :� 5V      $%' local:OdcSummary.MinimizedEvent��6'   [�5W      �� 5X   "   .��5Y   &   �� $( ScaleY�61   .��5Z   *   W� $	) 0.8Q�6:   8*  $+ scaleT��5\   :   $, 0X�5]   :   5^   '   5_   #   5a      5c      5e      5f      5h      