   M S B A M L   `   `   `  ����@  <MLGui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MLGuiMLGui  5          MLGui.ClassificationPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:MLGui     5      .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      $
��.01* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��.01* �6    � RowDefinitions 5   
   �� 5      $	��.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��.5* �6   .+�5   
   �� $��Create Model��6   $��1��6(   $��2��68   $��Center=�6L   $��Center=�6i   $��25(�6�   �� 5   
   $��Choose Text Column��6   $��1��6.   $��1��6>   $��Center=�6K   $��20(�6h   $	��Top=�6v   $��0,10,0,0q�6�   �� 5   
   -   5        $��TextColumnNameSelector��5      $��1��61   $	��200��6>   $��30��6J   $��1��6V   $��0,20,0,0q�6f   $��Center=�6x   �� 5!   
   $��Choose Label Column��6   $��2��6/   $��1��6?   $��Center=�6L   $��20(�6i   $	��Top=�6w   $��0,10,0,0q�6�   �� 5"   
   -   5        $��LabelColumnNameSelector��5"      $��1��62   $	��200��6?   $��30��6K   $��2��6W   $��0,20,0,0q�6g   $��Center=�6y   �� 5$   
   $��
Text Input��6   $��1��6&   $��2��66   $	��Top=�6C   $��Center=�6[   $��0,10,0,0q�6x   $��18(�6�   �� 5%   
   -   5         ��Name$ 	TextInput��5%      $��2��6%   $��1��62    �� TextAlignment$ Center=�6X   $	��150��6o   $��Center=�6{   $��30��6�   $��0,20,0,0q�6�   $��Center=�6�   ��6B   �� *Text+6�   �� 5'   
   $��2��6   $��2��6$   $	��Top=�61   $
��Left=�6I   $��	10,10,0,0q�6d   $��18(�6w   �� 5(      $�Output: ��6    �� 5)      �6   �� *Output+5*      �� 5,   
   -   5         ��Name$ CreateModelButton��5,      $��Create Model��6,   $��3��6C   $��1��6P   $��Center=�6`   $��Center=�6}   $��2��6�   $	��200��6�   $��60��6�   ��6�   �� *ContinueClicked+5-      5.      