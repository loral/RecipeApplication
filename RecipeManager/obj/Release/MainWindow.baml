   M S B A M L   `   `   `  ����   RecipeManager    RecipeManager.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��RecipeManager��  � Title$  Recipe Manager��$	��650��$
��1150��$	��605��$	��800�� � 	Resources    AndalusFont �� Property �� Value  SimplicityFont��%{   &	        &	 2     �� .���  � ��$ ./CustomFonts/#Andalus)��� .���  � ��$ ./CustomFonts/#Simplicity)�.���  � RowDefinitions �� $��25 ��� .+�� $��0��.+�o�  o� 
IsMainMenu . .@�m� $��_File��$��SemiBold"�.@�m� -   $��_Add Recipe...��$��Normal"�m� -   $��_Edit Recipe...��$��Normal"�m� -   $��_Create Menu...��$��Normal"��� m� -   $��_Open...��$��Normal"��� m� -   $��_Save��$��Normal"�m� -   $��_Save As...��$��Normal"��� m� -   $��Print��$��Normal"��� m� -	   $��_Exit��$��Normal"�� $��1�� � ColumnDefinitions �� -
    �Name$ 
LeftColumn��$
��Auto ��� -   $ MiddleColumn��$	��300 �$	��500���� -   $ RightColumn��$��* �$	��550��.+�9� -   $��FilterExpander��$�� ��$��0�� 9� ExpandDirection$ Right=�	 9� 
IsExpanded	 .  ��� $��0��
������  �� $	��235 � �� $��0 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��35 ��� $��55 ��� $	��275 ��� $��35 �.+�� $��0��$��1��.+��� $
��Left=�$��Center=�$
��Name��$��SemiBold"��� -   $��
nameFilter��|�. $
��Left=�$��Center=�$��70,0,0,0q�$��20��$	��160��� $��0��$��2��.+��� $��Rating��$��Center=�$��SemiBold"��� -   $��ratingLowFilter��$
��Left=�$��Center=�$��70,0,0,0q�$��20��$��50���� $
��Left=�$��Center=�$��	123,0,0,0q�$��to���� -   $��ratingHighFilter��$
��Left=�$��Center=�$��	147,0,0,0q�$��20��$��50��� $��0��$��3��.+��� $��Center=�$��	Prep Time��$��SemiBold"�
��������� -   $��prepLowFilter��$
��Left=�$��Center=�$��70,0,0,0q�$��20��$��50��{�.  �� $
��Left=�$��Center=�$��	123,0,0,0q�$��to��
��������� -   $��prepHighFilter��$
��Left=�$��Center=�$��	147,0,0,0q�$��20��$��50��{�.  � $��0��$��4��.+��� $��Center=�$��	Cook Time��$��SemiBold"�
��������� -   $��cookLowFilter��$
��Left=�$��Center=�$��70,0,0,0q�$��20��$��50��{�.  �� $
��Left=�$��Center=�$��	123,0,0,0q�$��to��
��������� -   $��cookHighFilter��$
��Left=�$��Center=�$��	147,0,0,0q�$��20��$��50��{�.  � $��0��$��5��.+��� $��Center=�$��
Ingredient��$��SemiBold"��� -   $��ingredientFilter��$
��Left=�$��Center=�$��70,0,0,0q�$��20��$	��160��� $��0��$��6��.+��� $��0,1,0,0q�
���   ��� $��Center=�$
��Type��$��SemiBold"��� -   $��MainDish��$
��Main��

 � Tag$
 
RecipeType��$
��Left=�$��Center=�$��70,0,0,0q��� -   $
��Side��$
��Side��$
 
RecipeType��$
��Left=�$��Center=�$��	130,0,0,0q�� $��0��$��7�� �� �� .+��� $��0,1,0,0q�
���   ��� $��0��$��Center=�$
��Meal��$��SemiBold"��� -   $��	Breakfast��$��1��$��	Breakfast��$
 MealType��$
��Left=�$��Center=�$��15,0,0,0q��� -   $��Lunch��$��1��$��Lunch��$
 MealType��$
��Left=�$��Center=�$��90,0,0,0q��� -   $��Dinner��$��1��$��Dinner��$
 MealType��$
��Left=�$��Center=�$��	150,0,0,0q�� $��0��$��8��$��0,0,0,3q� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��0,1,0,0q�
���   �$��12���� $��0��$��Category��$��Center=�$��0,-5,0,0q�$��SemiBold"��� -   $��AppetizerSnack��$��1��$
 	Categorie��$��Appetizer/Snack��$
��Left=�$��Center=�$��5,0,0,0q��� -   $��BeanRiceGrain��$��1��$
 	Categorie��$��Bean/Rice/Grain��$
��Left=�$��Center=�$��	125,0,0,0q��� -   $��Beverage��$��2��$
 	Categorie��$��Beverage��$
��Left=�$��Center=�$��5,0,0,0q��� -   $��Bread��$��2��$
 	Categorie��$��Bread��$
��Left=�$��Center=�$��	125,0,0,0q��� -   $��CakeFrosting��$��3��$
 	Categorie��$��Cake/Frosting��$
��Left=�$��Center=�$��5,0,0,0q��� -    $��Candy��$��3��$
 	Categorie��$��Candy��$
��Left=�$��Center=�$��	125,0,0,0q��� -!   $��CanningFreezing��$��4��$
 	Categorie��$��Canning/Freezing��$
��Left=�$��Center=�$��5,0,0,0q��� -"   $��	CheeseEgg��$��4��$
 	Categorie��$��
Cheese/Egg��$
��Left=�$��Center=�$��	125,0,0,0q��� -#   $��	CookieBar��$��5��$
 	Categorie��$��
Cookie/Bar��$
��Left=�$��Center=�$��5,0,0,0q��� -$   $��Dessert��$��5��$
 	Categorie��$��Dessert��$
��Left=�$��Center=�$��	125,0,0,0q��� -%   $
��Fish��$��6��$
 	Categorie��$
��Fish��$
��Left=�$��Center=�$��5,0,0,0q��� -&   $��Grill��$��6��$
 	Categorie��$��Grill��$
��Left=�$��Center=�$��	125,0,0,0q��� -'   $
��Meat��$��7��$
 	Categorie��$
��Meat��$
��Left=�$��Center=�$��5,0,0,0q��� -(   $��Pasta��$��7��$
 	Categorie��$��Pasta��$
��Left=�$��Center=�$��	125,0,0,0q��� -)   $��PieTart��$��8��$
 	Categorie��$��Pie/Tart��$
��Left=�$��Center=�$��5,0,0,0q��� -*   $��Poultry��$��8��$
 	Categorie��$��Poultry��$
��Left=�$��Center=�$��	125,0,0,0q��� -+   $��SaladDressing��$��9��$
 	Categorie��$��Salad/Dressing��$
��Left=�$��Center=�$��5,0,0,0q��� -,   $��SauceRelish��$��9��$
 	Categorie��$��Sauce/Relish��$
��Left=�$��Center=�$��	125,0,0,0q��� --   $��
SlowCooker��$��10��$
 	Categorie��$��Slow Cooker��$
��Left=�$��Center=�$��5,0,0,0q��� -.   $��SoupStew��$��10��$
 	Categorie��$��	Soup/Stew��$
��Left=�$��Center=�$��	125,0,0,0q��� -/   $��VegetablesFruit��$��11��$
 	Categorie��$��Vegetables/Fruit��$
��Left=�$��Center=�$��5,0,0,0q�� $��0��$��9��.+��� -0   $��ClearFilter��$��Clear��$��50��$��20��$��Right=�$	��Top=�$��0,5,10,5q�� $��1��.+��� -1   $��RecipeNameBorder��$��3,0,0,0q�
��������� -2   $��RecipeListView��$��14(� �� DisplayMemberPath$
 Name��$��5,5,5,5q�
������ ����  �� Path$ Recipes� �� SelectedValue �� $ SelectedRecipe� � ContextMenu �� .@�m� -3   $��Create Menu��m� -4   $��Add New Recipe��m� -5   $��Edit Selected Recipe��m� -6   $��Print Selected Rercipe���� $��1��$��Right=�$��Stretch=�$��3��
��������� $��2��$	��535��.��� -7   $��SelectedRecipeView��$��2��$��
10,10,10,5q�.+�� $��0�� �� $��7* ��� $	��13* ��� $	��80* �.+��� $��0��$��1q���Z� ._�� $��0��.+��� -8   $��SelectedRecipeHeader��$��0,5,0,5q�#���   �� TextAlignment$ Center=�$��25(��� $��1��
���  ��$��1q� �� CornerRadius$ 5��$��3,3,3,3q�._�� $��1��$��2,2,2,2q� �� $��97���� �� $	��105���� �� $��70���� .+��� -9   $��	prepLabel��$��0��$��Center=�$��Right=�$��0,0,0,0q�$��
Prep Time:��
���  ��#���  $��20(��� -:   $��
prepTxtblk��$��1��$��-5,0,0,0q�#���   $
��Left=�$��Center=�$��17(���Z� �� -;   $��cookTimeLabel��$��2��$��Center=�$��Right=�$��0,0,0,0q�$��
Cook Time:��
���  ��#���  $��20(��� -<   $��cookTimeTxtblk��$��3��$��-5,0,0,0q�#���   $
��Left=�$��Center=�$��17(���Z� �� -=   $��
yeildLabel��$��4��$��Center=�$��Right=�$��0,0,0,0q�$��Serves:��
���  ��#���  $��20(��� ->   $��yeildTxtblk��$��5��$��-5,0,0,0q�#���   $
��Left=�$��Center=�$��17(���Z� � -?   $��SelectedRecipeIngDir��$��2�� �� $	��40* �$	��200���� $	��60* �$	��310��.+��� $��0��
���  ��$��1q�$��3,3,3,3q�$ 5��._�� $��0��$��2,2,2,2q� �� -@   $ _ingredientLabel��$��7* ��� $	��90* �.+��� $��0��$��1q���Z� ._��� -A   $��ingredientsLabel��$��0��$��5,0,0,0q�$��Ingredients:��$��Center=�
���  ��#���  $��20(��� -B   $��ingredientsTxtblk��$��1��$��
10,0,10,10q�#���   $
��Wrap=�$��17(��� $��1��$
��Left=�$��Stretch=�$��3��
������ �� $��1��
���  ��$��1q�$��3,3,3,3q�$ 5��._�� $��1��$��2,2,2,2q� �� -C   $ _directionsLabel��$��7* ��� $	��90* �.+��� $��0��$��1q���Z� ._��� -D   $��directionsLabel��$��0��$��5,0,0,0q�$��Directions:��$��Center=�
���  ��#���  $��20(��� -E   $��directionsTxtblk��$��1��$��
10,0,10,10q�#���   $
��Wrap=�$��17(�