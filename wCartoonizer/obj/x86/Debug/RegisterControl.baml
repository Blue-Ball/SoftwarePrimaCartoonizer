   M S B A M L   `   `   `  ����  PrimaCartoonizer&     PrimaCartoonizer.RegisterControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      -   5        $��386.071��5      $��552.912��60     � 	Resources  5         TwitterTextBoxStyle �� 
TargetType-  'System.Windows.Input.KeyboardNavigation �� Property �� Value �� 
TargetType � 	Resources  Key g� 	BeginTime �� 
TargetName �� TargetProperty	 O� KeyTime
 O� Value R� KeyTime  System.Windows.Visibility   Visible R� Value   	Collapsed ��Name �� CornerRadius J� IsHitTestVisible J� Opacity ��Name
 � Tag ��Name �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName P� Property P� Value N� ExitActions ��Name  
FocusedOff N� EnterActions  	FocusedOn  
DisabledOn  DisabledOff  GlassButton  � RowDefinitions! �� RelativeTransform" �� ScaleX# �� ScaleY$ �� AngleX% �� AngleY& �� Angle' T� X( T� Y) � Offset* � Color+ �� 
StartPoint, �� EndPoint  	Timeline1  	Timeline2��%j  &	        7  7  7  7  &	 A    7  7  �� 5	   
   " ��6,   .�5
      ��  �@ TabNavigation$
 None=�6A   �� 5       �@�FocusVisualStyle 61   Z� �� 5       �@J�	AllowDrop . 6*   �� 5       � ��
 ���� 6+   �� 5       � ��$ Stretch=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ Trebuchet MS)�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 4q�6(   �� 5       � ��$ 2q�60   �� 5       � ��
 �   �6+   �� 5       � �� �	#FFAAAAAA6,   �� 5       � �� 5      �� 5      " ��6&    5      ���� 5      	FocusedOn .��5   "   U� $ 00:00:00X�6@   $ FocusVisualElement��5   @   $ (UIElement.Opacity)�5   @   .\�5   &   �� $	 00:00:00.1000000��6;   $
 1Q�6V   5    #   5!      �� 5"      
FocusedOff .��5#   "   U� $ 00:00:00X�6@   $ FocusVisualElement��5$   @   $ (UIElement.Opacity)�5%   @   .\�5&   &   �� $	 00:00:00.3000000��6;   $
 0Q�6V   5'   #   5(      �� 5)      
DisabledOn .��5*   "   T� $ 00:00:00X�6@   $ DisabledVisualElement��5+   @   $ (UIElement.Visibility)�5,   @   ..�5-   &   i� $ 00:00:00.1000000��6=   # Z 6X   5.   #   5/      �� 50      DisabledOff .��51   "   T� $ 00:00:00X�6@   $ DisabledVisualElement��52   @   $ (UIElement.Visibility)�53   @   ..�54   &   i� $ 00:00:00.1000000��6=   # Z 6X   55   #   56      57      .R�58      � .+�59      �� $ 
BorderBase��6%   $��3q�5:   %   
������ #��z��5<   %   #��z��5=   %   $ 3��5>   %   �� 5?      $ FocusVisualElement��6%   $��0q�5@   %   
������ ���	#52ECF5FD#��z��5C   %   $ 7��5D   %    .  $ 0Q�5F   %   ._�5G   "   �� $ InnerFocusVisualElement��6)   
������ ���	#72E3F1FC#��z��5J   )   $ 5��5K   )    .  $ 1Q�5M   )   ._�5N   &   �� $ InnerFocusVisualElement2��6-   
������ ���	#92D9ECFA#��z��5Q   -   $ 3��5R   -    .  $ 1Q�5T   -   ._�5U   *   �� $ InnerFocusVisualElement3��61   
������ ���	#A27DBEF1#��z��5X   1   $ 3��5Y   1    .  $ 1Q�5[   1   5\   '   5]   #   5^      �� 5_      $ 
TextPrompt��6$   $��1��5`   $   $��3q�5a   $   #��z 5b   $   |�.  
���iii�$y�	Collapsed=�5e   $   �� 5f      $ PART_ContentHost��6+   $��3q�5g   +   #��z��5h   +   �� 5i      $ DisabledVisualElement��6%   $��3q�5j   %   ���	#A5FFFFFF���	#59C0C0C0#��z��5m   %   $ 3��5n   %    .  $y�	Collapsed=�5p   %   �� 5q      $ ReadOnlyVisualElement��6%   $��3q�5r   %   ���	#66FFFFFF$
 2.75��5t   %   $y�	Collapsed=�5u   %   5v       5w      ]� 5x       5y   "   �� 5z   &    �@J�	IsFocused .  6E   �� 5{   &    � ��$  ��6@   /�5}   "   �� 5~   &   $ 
TextPrompt��6-    � y�$ Visible=�6[   5�      P� 5�       �@J�	IsFocused .  5�   "   �� 5�   &   $  FocusedOff_BeginStoryboard��66   8��  6Z    5�   "   �� 5�   &   $ FocusedOn_BeginStoryboard��66   8�� 6Y   5�      P� 5�       � {� .   5�   "   �� 5�   &   $  DisabledOn_BeginStoryboard��66   8�� 6Z   . �5�   "   ��  � ��
 �����6?   5�      P� 5�       � {� .  5�   "   �� 5�   &   8�� 66   . �5�   "   ��  � �� �	#FF0000006?   5�      5�      5�      5�      �� 5�   
   " ��6.    5�      ���� 5�      	Timeline1 .��5�      U� $ 00:00:00X�64   $
 glow��5�   4   $ (UIElement.Opacity)�5�   4   .\�5�      �� $	 00:00:00.3000000��6/   $
 1Q�6J   5�      5�      �� 5�      	Timeline2 .��5�      U� $ 00:00:00X�64   $
 glow��5�   4   $ (UIElement.Opacity)�5�   4   .\�5�      �� $	 00:00:00.3000000��6/   $
 0Q�6J   5�      5�      5�      .R�5�      �� ���	#ffffffff$��1,1,1,1q�5�      $ 4,4,4,4��5�      ._�5�      �� $ border��6   ���	#BD0F3E91���	#33333333$��1,1,1,1q�5�      $ 4,4,4,4��5�      ._�5�      �   5�      �� 5�      $��0.507* �6,   �� 5�      $��0.493* �6,   .+�5�      �� $
 glow��6!   $��2��5�   !   $
��Auto��5�   !   $��Stretch=�5�   !   $ 4,4,4,4��5�   !   $ 0Q�5�   !   ��5�      
� 5�   "   ! 5�   &   V� 5�   *   .~�5�   .   �� $" 1.702Q�6=   $# 2.243Q�6L   �� 5�   .   $$ 0Q�6<   $% 0Q�6G   �� 5�   .   $& 0Q�6>   T� 5�   .   $' -0.368Q�6A   $( -0.152Q�6L   5�   +   .��5�   &   � $) 0Q�63   $* 	#B28DBDFF��6>   � 5�   &   $) 1Q�63   $* 	#008DBDFF��6>   5�   #   5�      �� 5�      $��2��6+   $
��Auto��5�   +   $��Center=�5�   +   $��Center=�5�   +   �� 5�      $ shine��6!   $
��Auto��5�   !   $��0,0,0,0q�5�   !   $��Stretch=�5�   !   $ 4,4,0,0��5�   !   ��5�      �� 5�   "   $+ 0.494,0.028&�66   $, 0.494,0.889&�6O   .��5�   &   � $) 0Q�63   $* 	#99FFFFFF��6>   � 5�   &   $) 1Q�63   $* 	#33FFFFFF��6>   5�   #   5�      5�      5�      5�       5�      P� 5�       � �� . . �5�      �� $ shine��6    �@J�Opacity$	 0.4Q�6C   �� 5�      $ border��6    � �� �	#BD0F3E916G   �� 5�      $
 glow��6    � y�$ Hidden=�6E   5�      P� 5�       �@J�IsMouseOver .  5�      �� 5�      8��  6*    5�      �� 5�      $ Timeline2_BeginStoryboard��6*   8�� 6M   5�      5�      5�      .��5�      � $��-8,0,22,-102q�6   .+�5�   
   �� $��20,20,20,-3q�6   .+�5�      �� -   5        $��lblAppStatus��5�      $��6��5�      $��3q�5�      $
��Left=�5�      $��Center=�5�      $��
Calisto MT)�5�      $��14(�5�      
���   �$,��&Thank you for evaluating our software.��5      - J� Effect- 5     3  -System.Windows.Media.Effects.DropShadowEffect  5     .   
BlurRadius$. 4Q�6'   /   	Direction$	/ 330Q�5  '   0   Opacity$	0 0.5Q�5  '   1   ShadowDepth$1 4Q�5  '   2   Color$2 Black��5  '   5     �� 5	     $��1��6   $��1��5
     $��4��5     $��3q�5     $
��Left=�5     $��Center=�5     $��
Calisto MT)�5     $��14(�5     
���   �$I��CIf you want to use the full version of the software, you can get it��5     - 5       5     $. 4Q�6'   $	/ 330Q�5  '   $	0 0.5Q�5  '   $1 4Q�5  '   $2 Black��5  '   5     �� 5     $��2��6   $��2��5     $��1��5     $��4��5     $��27��5     $��3q�5      $
��Left=�5!     $��Center=�5"     $��
Calisto MT)�5#     $��14(�5$     
���   �$=��7directly with the Registration Key from the link below:��5&     - 5'       5(     $. 4Q�6'   $	/ 330Q�5)  '   $	0 0.5Q�5*  '   $1 4Q�5+  '   $2 Black��5,  '   5.     �� 5/     -   5        $��btnLink��5/     $��4��50     $��1��51     $��4��52     $��3q�53     $3��-Click here to get your Product Key online >>>��55     3 � Cursor$
3 Hand��56     ���	#FF0033DC��58     �� 59     " ��6&   .R�5:     �� $��
Calisto MT)�6$   $��16(�5;  $   $��	Underline{�5<  $   - 5=       5>  "   $. 4Q�63   $	/ 330Q�5?  3   $	0 0.5Q�5@  3   $1 4Q�5A  3   $2 Black��5B  3   �� 5D     5E     5F     ��5H     �� 5I     " ��6   .�5J     ��  � ��
 �����67   4 �� Triggers4 5K     P� 5L      �@J�IsMouseOver . . �5M  "   ��  � ��
 ��  �6?   5N     5P     5R     �� 5T     $��4��6   $��1��5U     $��4��5V     $	��366��5W     $
��3,10q�5X     $
��Left=�5Y     $��Center=�5Z     $��
Calisto MT)�5[     $��16(�5\     
���   �$��Registration Key:��5^     - 5_       5`     $. 4Q�6'   $	/ 330Q�5a  '   $	0 0.5Q�5b  '   $1 4Q�5c  '   $2 Black��5d  '   5f     �� 5h     -   5        $��textBox1��5h     $��35��5i     $��10q�5j     $��18(�5k     �� 5l     $	��366��5m     $��	3,10,0,10q�5n     $
��Left=�5o     $��Center=�5p     $��
Calisto MT)�5q     $��16(�5r     
���   �- 5u       5v     $. 4Q�6'   $	/ 330Q�5w     $	0 0.5Q�5x     $1 4Q�5y     $2 Black��5z     �� 5{  %   $�	Your Name��6)   �� 6<   $�:��6@   6L   �� 5|     -   5        5 ��Name$5 textBox1_Copy��5|     $��35��5}     $��9,10,265,10q�5~     $��18(�5     �� 5�     $��48��6   $��20q�5�     $��Bottom=�5�     $��RightToLeft=�5�     6 �� Orientation$6 
Horizontal=�5�     .+�5�     �� -   5        $��7��5�     $��2��5�     $��3��5�     $	��170��5�     $��27,4,7,4q�5�     ���	#FFFFFFFF#���  5�     .��5�     �� $	��160��6!   $��Center=�5�  !   $��Center=�5�  !   $6 
Horizontal=�5�  !   .+�5�     �� $��5,0,50,0q�6$   $��Center=�5�  $   $��14(�5�  $   $��Register my version��5�  $   $
��Wrap=�5�  $   5�     5�     �� 5�     -   5        $��7��5�     $��4��5�     $��4q�5�     ���	#FFFFFFFF#���  5�     .��5�     �� $��70��6!   $��Center=�5�  !   $��Center=�5�  !   $6 
Horizontal=�5�  !   .+�5�     �� $��5,0,50,0q�6$   $��Center=�5�  $   $��14(�5�  $   $��
Free trial��5�  $   $
��Wrap=�5�  $   5�     5�     5�     5�     5�     5�     