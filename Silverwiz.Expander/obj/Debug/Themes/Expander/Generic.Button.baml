   M S B A M L   `   `   `  ����H  DOdyssey, Version=1.0.7908.6419, Culture=neutral, PublicKeyToken=null7 clr-namespace:Silverwiz.ExpanderSilverwiz.Expander  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   animTime"    Silverwiz.Expander.OdcButton  �� 
TargetType �� Property �� Value �� 
TargetType*   $Silverwiz.Expander.OdcExpanderHeader  Name J� SnapsToDevicePixels    HeaderBorderThickness    HeaderBackground    ShowEllipse	   ShowEllipse
    CornerRadius   CornerRadius    
IsSelected   
IsSelected    Image   Image    NotificationCount   NotificationCount    
FocusImage   
FocusImage   	CanExpand d� 	IsChecked �� RelativeSource �� Path �� Triggers P� 
SourceName P� Property P� Value �� 
TargetName ]� 
Conditions �� 
SourceName �� Property  �� Value! :� RoutedEvent" �� TargetProperty	# W� To$ g� Duration% �� 
TargetName& g� DecelerationRatio�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    *local clr-namespace:Silverwiz.Expander  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %�  &	        '        7   E�5      0:0:0.4D�6)   �� 5      "    6   .�5   
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
    � �� 5      �� 5      "   6"   .R�5      �� ._�5      �� .+�5        $ header��66    . #��z 5   1   #��z��5   1   #��z��5   1   #��z 5   1   #��z��5   1   #	 z 5   1   # z
 5   1   # z 5   1   # z 5   1   # z 5   1   # z 5    1    .   5   1   ��  �� *TemplatedParent+$ 
IsExpanded�5!   1   5#      5$       5%      P� 5&      $ header��6F    �@J�IsMouseOver . . �5'      �� $ header��6�    6;   ��  �� *TemplatedParent+$ MouseOverHeaderForeground� � ��6�   �� 5(      $ header��6�    6;   ��  �� *TemplatedParent+$ MouseOverHeaderBackground� � ��6�   5)      ]� 5*       5+      �� 5,   "   $ header��6N    � ��  . �� 5-   "    �@  HasPressedBackground  . 6L   ./�5/      �� $ header��6�    6;   ��  �� *TemplatedParent+$ PressedHeaderBackground� � ��6�   50      :� 51      $$! local:OdcButton.MinimizedEvent��6'   [�52      �� 53   "   .��54   &   �� $" ScaleY�61   .��55   *   W� $# 0X�6;   8$   $% scaleT��6g   $	& 0.8Q�6�   56   '   57   #   59      5;      5=      5>      5@      