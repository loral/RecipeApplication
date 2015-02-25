using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Diagnostics;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlNodeList XMLingredientList;
        public XmlDocument doc = new XmlDocument();
        public string fileName = string.Empty;
        CultureInfo culture = new CultureInfo("en-US");
        public IngredientView ingredientView;

        public MainWindow()
        {
            InitializeComponent();

            if (Application.Current.Properties["StartFile"] != null)
            {
                fileName = Application.Current.Properties["StartFile"].ToString();
                if (fileName != null && fileName != "No filename given")
                {
                    LoadFileData(fileName);
                }
            }

            DataContext = new MainWindowViewModel(doc);
            ingredientView = new IngredientView(doc);

            // Create hot keys
            try
            {
                RoutedCommand hideFilter = new RoutedCommand();
                hideFilter.InputGestures.Add(new KeyGesture(Key.H, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(hideFilter, HideFilter));

                RoutedCommand addRecipe = new RoutedCommand();
                addRecipe.InputGestures.Add(new KeyGesture(Key.N, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(addRecipe, AddRecipe));

                RoutedCommand saveFile = new RoutedCommand();
                saveFile.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveFile, Save));

                RoutedCommand openFile = new RoutedCommand();
                openFile.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openFile, Open));

                RoutedCommand print = new RoutedCommand();
                print.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(print, Print));

                RoutedCommand edit = new RoutedCommand();
                edit.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(edit, EditRecipe));

                RoutedCommand menu = new RoutedCommand();
                menu.InputGestures.Add(new KeyGesture(Key.M, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(menu, CreateMenu));

                RoutedCommand exit = new RoutedCommand();
                exit.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(exit, Exit));

                RoutedCommand zoomIn = new RoutedCommand();
                zoomIn.InputGestures.Add(new KeyGesture(Key.NumPad1, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomIn, ZoomIn));

                RoutedCommand zoomInNum = new RoutedCommand();
                zoomInNum.InputGestures.Add(new KeyGesture(Key.D1, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomInNum, ZoomIn));

                RoutedCommand zoomOut = new RoutedCommand();
                zoomOut.InputGestures.Add(new KeyGesture(Key.NumPad2, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomOut, ZoomOut));

                RoutedCommand zoomOutNum = new RoutedCommand();
                zoomOutNum.InputGestures.Add(new KeyGesture(Key.D2, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomOutNum, ZoomOut));

                RoutedCommand zoomNormal = new RoutedCommand();
                zoomNormal.InputGestures.Add(new KeyGesture(Key.NumPad0, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomNormal, ZoomNormal));

                RoutedCommand zoomNormalNum = new RoutedCommand();
                zoomNormalNum.InputGestures.Add(new KeyGesture(Key.D0, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(zoomNormalNum, ZoomNormal));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        #region Application Logic

        private void LoadFileData(string filePath)
        {
            doc.Load(filePath);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipeListView.ItemsSource).Filter = UserFilter;
            Keyboard.Focus(nameFilter);

            // Populate ingredient combo box
            foreach (ComboBox cb in FindVisualChildren<ComboBox>(RecipeManager))
            {
                if (cb.Name.Contains("ingredientFilter"))
                {
                    cb.ItemsSource = ingredientView.Ingredients;
                }
            }

            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0) 
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }
        }

        private void AddRecipe(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(doc);
            addRecipeWindow.Owner = this;
            addRecipeWindow.Show();
        }

        private void RecipeFilter_OnCriteriaChange(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipeListView.ItemsSource).Refresh();
            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }
        }

        private void RecipeFilter_OnCriteriaChange(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipeListView.ItemsSource).Refresh();
            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }
        }

        private bool UserFilter(object item)
        {
            // Create variables
            var recipe = (Recipe)item;

            List<Category> _categoryList = new List<Category>();
            List<MealType> _mealTypeList = new List<MealType>();
            List<RecipeType> _recipeTypeList = new List<RecipeType>();

            bool ratingGreaterThan, ratingLessThan, ingred, name, categories, meal, recipeType;

            // Get checkbox filds
            foreach (CheckBox cb in FindVisualChildren<CheckBox>(RecipeManager))
            {
                if (cb.IsChecked == true && cb.Tag != null)
                {
                    if (cb.Tag.ToString() == "Categorie")
                    {
                        _categoryList.Add((Category)Enum.Parse(typeof(Category), cb.Name.ToString()));
                    }
                    else if (cb.Tag.ToString() == "MealType")
                    {
                        _mealTypeList.Add((MealType)Enum.Parse(typeof(MealType), cb.Name.ToString()));
                    }
                    else if (cb.Tag.ToString() == "RecipeType")
                    {
                        _recipeTypeList.Add((RecipeType)Enum.Parse(typeof(RecipeType), cb.Name.ToString()));
                    }
                }
            }

            // Run filter tests
            // Rating
            if (String.IsNullOrEmpty(ratingLowFilter.Text.Trim()))
                ratingGreaterThan = true;
            else if (ratingLowFilter.Text.Trim() == ".")
                ratingGreaterThan = true;
            else
                ratingGreaterThan = (recipe.rating >= Convert.ToDouble(ratingLowFilter.Text));
            if (String.IsNullOrEmpty(ratingHighFilter.Text.Trim()))
                ratingLessThan = true;
            else if (ratingHighFilter.Text.Trim() == ".")
                ratingLessThan = true;
            else
                ratingLessThan = (recipe.rating <= Convert.ToDouble(ratingHighFilter.Text) || String.IsNullOrEmpty(recipe.rating.ToString()));
            // Ingredient
            if (String.IsNullOrEmpty(ingredientFilter.Text))
                ingred = true;
            else
                ingred = (recipe.ingredients.Exists(junk => junk.Name.IndexOf(ingredientFilter.Text, StringComparison.OrdinalIgnoreCase) > -1));
            // Name
            if (String.IsNullOrEmpty(nameFilter.Text))
                name = true;
            else
            {
                //name = (recipe.name.StartsWith(nameFilter.Text, StringComparison.OrdinalIgnoreCase)); // Starts with 
                name = (culture.CompareInfo.IndexOf(recipe.name, nameFilter.Text, CompareOptions.IgnoreCase) > -1); // Contains
            }

            categories = (ListContainsAll.ContainsAllItems(recipe.categories, _categoryList));
            meal = (ListContainsAll.ContainsAllItems(recipe.mealTypes, _mealTypeList));
            recipeType = (ListContainsAll.ContainsAllItems(recipe.recipeTypes, _recipeTypeList));

            // Return results
            return (ratingGreaterThan && ratingLessThan && ingred && name && categories && meal && recipeType);
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public void reLoadFile()
        {
            try
            {
                fileName = Application.Current.Properties["StartFile"].ToString();
                doc.Load(fileName);
                DataContext = new MainWindowViewModel(doc);
                CollectionViewSource.GetDefaultView(RecipeListView.ItemsSource).Filter = UserFilter;
                ingredientView = new IngredientView(doc);

                // Populate ingredient combo box
                foreach (ComboBox cb in FindVisualChildren<ComboBox>(RecipeManager))
                {
                    if (cb.Name.Contains("ingredientFilter"))
                    {
                        cb.ItemsSource = ingredientView.Ingredients;
                    }
                }

                if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
                {
                    RecipeListView.SelectedItem = RecipeListView.Items[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnSelectedRecipeChange(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListView.SelectedValue == null)
            {
                SelectedRecipeHeader.Text = "";
                prepTxtblk.Content = "";
                cookTimeTxtblk.Content = "";
                yeildTxtblk.Content = "";
                ingredientsTxtblk.Text = "";
                directionsTxtblk.Text = "";
                return;
            }

            Recipe _selectedRecipe = (Recipe)RecipeListView.SelectedValue;
            PopulateSelectedRecipeDisplayed(_selectedRecipe);
        }

        private void HideFilter(object sender, ExecutedRoutedEventArgs e)
        {
            if (LeftColumn.ActualWidth > 1)
            {
                LeftColumn.Width = new GridLength(0,GridUnitType.Pixel);
            }
            else
            {
                LeftColumn.Width = new GridLength(240, GridUnitType.Pixel);
            }
        }

        private void PopulateSelectedRecipeDisplayed(Recipe _selectedRecipe)
        {
            SelectedRecipeHeader.Text = "";
            prepTxtblk.Content = "";
            cookTimeTxtblk.Content = "";
            yeildTxtblk.Content = "";
            ingredientsTxtblk.Text = "";
            directionsTxtblk.Text = "";
            int directionNum = 1;

            if (_selectedRecipe.rating > 0)
            {
                SelectedRecipeHeader.Inlines.Add(new Run(_selectedRecipe.name + " (" + _selectedRecipe.rating.ToString() + ")"));
            }
            else
            {
                SelectedRecipeHeader.Inlines.Add(new Run(_selectedRecipe.name));
            }

            prepTxtblk.Content = _selectedRecipe.prepTime;

            cookTimeTxtblk.Content = _selectedRecipe.cookTime;

            yeildTxtblk.Content = _selectedRecipe.yeild;

            if (_selectedRecipe.ingredients.Count > 0)
            {
                ingredientsTxtblk.Inlines.Add(new Bold(new Run("• ")));
                ingredientsTxtblk.Inlines.Add(_selectedRecipe.ingredients[0].Quanity + " " + _selectedRecipe.ingredients[0].Unit + " " + _selectedRecipe.ingredients[0].Name);
            }

            foreach (Ingredient ingredient in _selectedRecipe.ingredients.Skip(1))
            {
                ingredientsTxtblk.Inlines.Add(System.Environment.NewLine);
                ingredientsTxtblk.Inlines.Add(new Bold(new Run("• ")));
                ingredientsTxtblk.Inlines.Add(ingredient.Quanity + " " + ingredient.Unit + " " + ingredient.Name);
            }

            if (_selectedRecipe.directions.Count > 0)
            {
                directionsTxtblk.Inlines.Add(new Bold(new Run(directionNum++ + ". ")));
                directionsTxtblk.Inlines.Add(_selectedRecipe.directions[0]);
            }

            foreach (string direction in _selectedRecipe.directions.Skip(1))
            {
                directionsTxtblk.Inlines.Add(System.Environment.NewLine + System.Environment.NewLine);
                directionsTxtblk.Inlines.Add(new Bold(new Run(directionNum++ + ". ")));
                directionsTxtblk.Inlines.Add(direction);  
            }

        }

        private void EditRecipe(object sender, RoutedEventArgs e)
        {
            EditRecipeWindow editRecipeWindow = new EditRecipeWindow(doc, (Recipe)RecipeListView.SelectedItem);
            editRecipeWindow.Owner = this;
            editRecipeWindow.Show();
        }

        private void ZoomIn(object sender, ExecutedRoutedEventArgs e)
        {
            var headerFontSize = SelectedRecipeHeader.FontSize + 1;
            var labelFontSize = prepLabel.FontSize + 1;
            var infoFontSize = prepTxtblk.FontSize + 1;

            if (labelFontSize > 45)
                return;

            SelectedRecipeHeader.FontSize = headerFontSize;
            prepLabel.FontSize = labelFontSize;
            cookTimeLabel.FontSize = labelFontSize;
            yeildLabel.FontSize = labelFontSize;
            ingredientsLabel.FontSize = labelFontSize;
            directionsLabel.FontSize = labelFontSize;

            prepTxtblk.FontSize = infoFontSize;
            cookTimeTxtblk.FontSize = infoFontSize;
            yeildTxtblk.FontSize = infoFontSize;
            ingredientsTxtblk.FontSize = infoFontSize;
            directionsTxtblk.FontSize = infoFontSize;

            SetRecipeDisplayMinWidth();
        }

        private void ZoomOut(object sender, ExecutedRoutedEventArgs e)
        {
            var headerFontSize = SelectedRecipeHeader.FontSize - 1;
            var labelFontSize = prepLabel.FontSize - 1;
            var infoFontSize = prepTxtblk.FontSize - 1;

            if (headerFontSize < 2 || labelFontSize < 2 || infoFontSize < 2)
                return;

            SelectedRecipeHeader.FontSize = headerFontSize;
            prepLabel.FontSize = labelFontSize;
            cookTimeLabel.FontSize = labelFontSize;
            yeildLabel.FontSize = labelFontSize;
            ingredientsLabel.FontSize = labelFontSize;
            directionsLabel.FontSize = labelFontSize;

            prepTxtblk.FontSize = infoFontSize;
            cookTimeTxtblk.FontSize = infoFontSize;
            yeildTxtblk.FontSize = infoFontSize;
            ingredientsTxtblk.FontSize = infoFontSize;
            directionsTxtblk.FontSize = infoFontSize;

            SetRecipeDisplayMinWidth();
        }

        private void ZoomNormal(object sender, ExecutedRoutedEventArgs e)
        {
            SelectedRecipeHeader.FontSize = 25;
            prepLabel.FontSize = 20;
            cookTimeLabel.FontSize = 20;
            yeildLabel.FontSize = 20;
            ingredientsLabel.FontSize = 20;
            directionsLabel.FontSize = 20;
            prepTxtblk.FontSize = 17;
            cookTimeTxtblk.FontSize = 17;
            yeildTxtblk.FontSize = 17;
            ingredientsTxtblk.FontSize = 17;
            directionsTxtblk.FontSize = 17;

            SetRecipeDisplayMinWidth();
        }

        private void SetRecipeDisplayMinWidth()
        {
            int fontSize = Convert.ToInt32(prepLabel.FontSize);

            if (fontSize < 20)
            { 
                RightColumn.MinWidth = 550;
                return;
            }

            switch (fontSize)
            {
                case 20:
                case 21:
                    RightColumn.MinWidth = 550;
                    break;
                case 22:
                    RightColumn.MinWidth = 652;
                    break;
                case 23:
                    RightColumn.MinWidth = 707;
                    break;
                case 24:
                    RightColumn.MinWidth = 730;
                    break;
                case 25:
                    RightColumn.MinWidth = 762;
                    break;
                case 26:
                    RightColumn.MinWidth = 782;
                    break;
                case 27:
                    RightColumn.MinWidth = 807;
                    break;
                case 28:
                    RightColumn.MinWidth = 834;
                    break;
                case 29:
                    RightColumn.MinWidth = 866;
                    break;
                case 30:
                    RightColumn.MinWidth = 886;
                    break;
                case 31:
                    RightColumn.MinWidth = 914;
                    break;
                case 32:
                    RightColumn.MinWidth = 942;
                    break;
                case 33:
                    RightColumn.MinWidth = 966;
                    break;
                case 34:
                    RightColumn.MinWidth = 989;
                    break;
                case 35:
                    RightColumn.MinWidth = 1019;
                    break;
                case 36:
                    RightColumn.MinWidth = 1044;
                    break;
                case 37:
                    RightColumn.MinWidth = 1068;
                    break;
                case 38:
                    RightColumn.MinWidth = 1095;
                    break;
                case 39:
                    RightColumn.MinWidth = 1120;
                    break;
                case 40:
                    RightColumn.MinWidth = 1145;
                    break;
                case 41:
                    RightColumn.MinWidth = 1171;
                    break;
                case 42:
                    RightColumn.MinWidth = 1200;
                    break;
                case 43:
                    RightColumn.MinWidth = 1220;
                    break;
                case 44:
                    RightColumn.MinWidth = 1250;
                    break;
                default:
                    RightColumn.MinWidth = 1275;
                    break;
            }
        }

        private void CreateMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Create Menu'");
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".rmn"; // Default file extension
            dlg.Filter = "Recipe Manager (.rmn)|*.rmn"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                fileName = dlg.FileName;
                Application.Current.Properties["StartFile"] = fileName;
                reLoadFile();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // Save updated file to disk
            try
            {
                if (Application.Current.Properties["StartFile"] != null && Application.Current.Properties["StartFile"].ToString() != null && Application.Current.Properties["StartFile"].ToString() != "No filename given")
                {
                    doc.Save(Application.Current.Properties["StartFile"].ToString());
                    MessageBox.Show("Saved", "Saved!");
                }
                else
                {
                    MessageBox.Show("No file to save. Please add a recipe to save first");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["StartFile"] == null)
            {
                MessageBox.Show("No file to save. Please add a recipe to save first");
                return;
            }
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "RecipeManager"; // Default file name
            dlg.DefaultExt = ".rmn"; // Default file extension
            dlg.Filter = "recipe manager (.rmn)|*.rmn"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                fileName = dlg.FileName;
                Application.Current.Properties["StartFile"] = fileName;
                doc.Save(fileName);
                MessageBox.Show("Saved", "Saved!");
            }
            else
            {
                MessageBox.Show("No save file path selected!", "Error");
            }
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            // Configure printer dialog box
            System.Windows.Controls.PrintDialog dlg = new System.Windows.Controls.PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.UserPageRangeEnabled = true;

            // Show print file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process print file dialog box results 
            if (result == true)
            {
                // Print document
                //http://programming-pages.com/2012/06/12/printing-in-wpf/
                MessageBox.Show("You clicked print");
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Do you want to save changes to this document before the application closes? Click Yes to save and close, No to close without saving, or Cancel to not close.", "Save?", MessageBoxButton.YesNoCancel);
            if (dialogResult == MessageBoxResult.Yes)
            {
                RoutedEventArgs args = new RoutedEventArgs();
                Save(this, args);
            }
            else if (dialogResult == MessageBoxResult.Cancel)
            {
                return;
            }
            Application.Current.Shutdown();
        }

        private void Tools(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Tools'");
        }

        private void Help(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Help'");
        }

        #endregion
    }
}
