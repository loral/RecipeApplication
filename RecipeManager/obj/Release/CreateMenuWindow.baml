   M S B A M L   `   `   `  ����   RecipeManager$    RecipeManager.CreateMenuWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Create Menu��$	��450��$	��600��$	��300��$	��400��.���  � RowDefinitions �� $��45 �$��45���� $��* �$	��100���� $��37 �$��37��.+�� $��0��.+��� $��Meals��$��Center=�$��7,0,0,0q��� -   $��cb_meals��$��0��$��0��$��45��$
��Left=�$��Center=�$��50,0,0,0q�$��23�� �� 
IsEditable .   �� MaxDropDownHeight$	 250��.@��� .��1�� .��2�� .��3�� .��4�� .��5�� .��6�� .��7�� .��8�� .��9�� .��10�� .��11�� .��12�� .��13�� .��14�� .��15�� .��16�� .��17�� .��18�� .��19�� .��20�� .��21�� .��22�� .��23�� .��24�� .��25�� .��26�� .��27�� .��28�� .��29�� .��30�� .��31�� -   $��btn_createMenu��$��0��$
��Left=�$��Center=�$��	105,0,0,0q�$��22��$��Create Menu��$��75���� -   $��email_txtbx��$��0��$��22��$	��175��$��Right=�$��Center=�$��0,0,10,0q�.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 6sys&clr-namespace:System;assembly=mscorlib      �� 
TargetType" �� �� 	Resources    CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch	 !� Visual��%^   &	        !� $
 Left=�$ Center=�$
 None=�	 �� $��address@email.com��
�������
 �� Triggers
 P�  P� Property � �� �� Empty P� Value# Z . ���  �� Property � �� �� Value# [  P�  � �� Z� . ���  � ��# [  P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.��� $��1�� � ColumnDefinitions �� $��* �$	��200���� $��* �$	��200��.+��� $��0,1,0,1q�
���   �$��2��$��5,0,5,0q��� -   $��RecipeListView��$��0�� �� DisplayMemberPath$
 Name��$��14(�$��5,5,5,5q�
������  �� SelectionMode$ Single=� � ContextMenu �� .@�m� -   $&�� Randomly Replace Selected Recipe��m� -   $&�� Manually Replace Selected Recipe���� $��1��$
��Left=�$��Stretch=�$��2��
�������&   System.Windows.Controls.DataGrid  -   $��IngredientGridView��$��1�� %� IsVirtualizing .     HeadersVisibility$
 None=�   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * $��14(�   SelectionMode$ Extended=�
������ $��2,1,1,1q�   CanUserAddRows .   �� -	   $��RemoveSelectedMenu��.@�m� -
   $#��Remove unselected ingredients��   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Width$ 25    CellTemplate �� .R���  ��Name$ TestCheckBox��-   $��Center=�$��Center=� d� 	IsChecked$ FalseY��� -   $��IngredientListView��$��1��$��14(�$��5,5,5,5q�$y�Hidden=�
������  �� .@�m� -   $!��Remove Selected Ingredients��� $��3��.+��� -   $��btn_emailRecipe��$��0��$��Right=�$��Bottom=�$��0,0,89,7q�$��22��$��
Email Menu��$��75���� -   $��btn_cancelRecipe��$��0��$��Right=�$��Bottom=�$��0,0,7,7q�$��22��$��Cancel��$��75��