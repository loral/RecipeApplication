   M S B A M L   `   `   `  ����  RecipeManager)    #RecipeManager.ManualRecipeSelection   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Manual Recipe Selection��5   	   $	��300��6)   $	��300��66   $	��400��6F   $	��400��6R   .��5      �  � RowDefinitions 5   
   �� 5	      $��* �6   �� 5
      $��37 �6   .+�5   
   � $��0��6   .+�5      �� -   5        $��RecipeListView��5       �� DisplayMemberPath$
 Name��6-   $��14(�6F   $��5,5,5,5q�6T   
������  �� SelectionMode$ Single=�6    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Use selected recipe��5   #   6@   5      5      5      � 5   
   $��1��6   .+�5      �� -   5        $��
btn_select��5   j   $��Right=�6   $��Bottom=�61   $��0,0,89,7q�6L   $��22��6^   $��Select��6|   $��75��6�   6�   �� 5      -   5        $��
btn_cancel��5   i   $��Right=�6   $��Bottom=�61   $��0,0,7,7q�6L   $��22��6]   $��Cancel��6{   $��75��6�   6�   5      5       5!      