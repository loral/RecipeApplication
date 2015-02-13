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
            MessageBox.Show(doc.InnerXml);
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
        }

        private void RecipeFilter_OnCriteriaChange(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(RecipeListView.ItemsSource).Refresh();
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
            if (String.IsNullOrEmpty(ratingLowFilter.Text))
                ratingGreaterThan = true;
            else
                ratingGreaterThan = (recipe.rating >= Convert.ToDouble(ratingLowFilter.Text));
            if (String.IsNullOrEmpty(ratingHighFilter.Text))
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
                SelectedRevipeView.Text = "";
                return;
            }
                
            Recipe _selectedRecipe = (Recipe)RecipeListView.SelectedValue;
            PopulateSelectedRecipeDisplayed(_selectedRecipe);
        }

        private void PopulateSelectedRecipeDisplayed(Recipe _selectedRecipe)
        {
            SelectedRevipeView.Text = "";

            string _recipeToDisplay = "Name: " + _selectedRecipe.name;
            _recipeToDisplay += System.Environment.NewLine + "Rating: " + _selectedRecipe.rating;
            _recipeToDisplay += System.Environment.NewLine + "Prep Time: " + _selectedRecipe.prepTime;
            _recipeToDisplay += System.Environment.NewLine + "Cook Time: " + _selectedRecipe.cookTime;
            _recipeToDisplay += System.Environment.NewLine + "Yeild: " + _selectedRecipe.yeild;

            _recipeToDisplay += System.Environment.NewLine + "Meal Types: ";

            foreach(MealType mealType in _selectedRecipe.mealTypes)
            {
                _recipeToDisplay += mealType + ", ";
            }

            _recipeToDisplay += System.Environment.NewLine + "Recipe Types: ";

            foreach (RecipeType recipeType in _selectedRecipe.recipeTypes)
            {
                _recipeToDisplay += recipeType + ", ";
            }

            _recipeToDisplay += System.Environment.NewLine + "Categories: ";

            foreach (Category category in _selectedRecipe.categories)
            {
                _recipeToDisplay += category + ", ";
            }

            _recipeToDisplay += System.Environment.NewLine + System.Environment.NewLine + "Ingredients: ";

            foreach(Ingredient ingredient in _selectedRecipe.ingredients)
            {
                _recipeToDisplay += System.Environment.NewLine + "• " + ingredient.Quanity + " " + ingredient.Unit + " " + ingredient.Name;
            }

            _recipeToDisplay += System.Environment.NewLine + System.Environment.NewLine + "Directions: ";

            foreach (string direction in _selectedRecipe.directions)
            {
                _recipeToDisplay += System.Environment.NewLine + "• " + direction;
            }

            SelectedRevipeView.Text = _recipeToDisplay;
        }

        private void EditRecipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Edit Recipe'");
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
            dlg.Filter = "Recipe Manager documents (.rmn)|*.rmn"; // Filter files by extension 

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
                    SaveAs(this, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
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
