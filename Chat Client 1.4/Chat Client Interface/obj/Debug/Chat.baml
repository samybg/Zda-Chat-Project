   M S B A M L   `   `   `  ����  Chat Client Interface     Chat_Client_Interface.Chat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  SecondWindow��5   	    � Title$
 Chat��5   	   $	��364��6   $	��637��6#   .��5      �  � ColumnDefinitions 5   
   �� 5      .+�5	   
   � $
��Left=�6   $	��333��6*   $	��Top=�67   $	��629��6O   ���	#FF61DA1B.+�5
      �� -   5         ��Name$ chatbox��5
      $
��Left=�6'   $��73��6B   $��	0,260,0,0q�6N    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��504��6�   
���   �$��1q�6�   
�������$��Type Here...��6�   6J  �� 5      -   5        $ 
chatWindow��5      $
��Left=�6*   $	��255��6E   $
 Wrap=�6R   $	��Top=�6f   $	��504��6~   $��
Connected!��6�    �� IsReadOnlyCaretVisible . $��0,-1,0,0q�6�   �� 5      -   5        $
 eKey��5      $
��Left=�6$   $��23��6?   $��509,300,0,0q�6K   $
 Wrap=�6`   $��Key lengh must be 8��6t   $	��Top=�6�   $	��120��6�   6�   �� 5      -   5         ��Name$ 
encription��5      $��	Encripted��6+   $
��Left=�6?   $��530,263,-16,0q�6Z   $	��Top=�6q   $	��115��6�   6�   �� 5      -   5         ��Name$ keyError��5      $
��Left=�6*   $��530,279,0,0q�6E   $
��Wrap=�6Z   $	��Top=�6n   $��78��6�   $��21��6�   �� 5      -   5         ��Name$ listbox��5      $
��Left=�6'   $	��255��6B   $��
508,-1,0,0q�6O   $	��Top=�6c   $	��121��6{   5      5      5      