   M S B A M L   `   `   `  ����@  <MLGui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MLGuiMLGui  5          MLGui.ModelCreationPage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:MLGui     5      .��5	      � 
�������  � ColumnDefinitions  5
   
   �� 5      $
��.01* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��.01* �6    � RowDefinitions 5   
   �� 5      $	��.5* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $	��.5* �6   .+�5   
   �� $��Create Model��6   $��1��6(   $��2��68   $��Center=�6L   $��Center=�6i   $��25(�6�   �� 5   
   $.��(Choose Independent Columns (Categorical)��6   $��1��6D   $��1��6T   $��Center=�6a   $��13(�6~   $	��Top=�6�   $��0,10,0,0q�6�   �� 5   
   -   5        $*��$IndependentCategoricalColumnSelector��5      $��1��6?   $	��200��6L   $��30��6X   $��1��6d   $��0,20,0,0q�6t   $��Center=�6�   ��5      �� 5       .R�5!      �� -   5         d� 	IsChecked 5!      �� *	Checked+��6=   �� *	Content+6Y   5"      5$      �� 5*   
   $-��'Choose Independent Columns (Continuous)��6   $��2��6C   $��1��6S   $��Center=�6`   $��13(�6}   $	��Top=�6�   $��0,10,0,0q�6�   �� 5+   
   -   5        $)��#IndependentColumnContinuousSelector��5+      $��1��6>   $	��200��6K   $��30��6W   $��2��6c   $��0,20,0,0q�6s   $��Center=�6�   ��5,      �� 5-      .R�5.      �� -   5         5.      �� *	Checked+��6=   �� *	Content+6Y   5/      51      �� 53   
   $��Choose Dependent Column��6   $��1��63   $��2��6C   $��Center=�6P   $��18(�6m   $	��Top=�6{   $��0,10,0,0q�6�   �� 54   
   -   5        $��DependentColumnSelector��54      $��2��62   $	��200��6?   $��30��6K   $��1��6W   $��0,20,0,0q�6g   $��Center=�6y    �� SelectedItem 6�   �� *DependentColumn+�� 56   
   $��Cycles��6   $��2��6"   $��2��62   $	��Top=�6?   $��Center=�6W   $��0,10,0,0q�6t   $��18(�6�   �� 57   
   -   5         ��Name$ CyclesInput��57      $��2��6'   $��2��64    �� TextAlignment$ Center=�6\   $��50��6s   $��Center=�6~   $��30��6�   $��0,20,0,0q�6�   $��Center=�6�   ��6D   �� *Cycles+6�   �� 59   
   $��Plot?��6   $��Center=�6!   $	��Top=�6>   $��25(�6V   $��3��6d   $��1��6q   $��10q�6�   �� 5:   
   $��Center=�6.   $��Bottom=�6K   $��3��6f   $��1��6s   $��20q�6�    6   �� *Plot+6�   �� 5<   
   $��2��6   $��Center=�6!   $��	Add Point��6_   $��Center=�6s   $��25(�6�   $��3��6�   ��6>   �� *PredictPoint+6�   �� 5>   
   -   5         ��Name$ CreateModelButton��5>      $��Create Model��6,   $��4��6C   $��1��6P   $��Center=�6`   $��Center=�6}   $��2��6�   $	��200��6�   $��60��6�   ��6�   �� *CreateModel+5@      5A      