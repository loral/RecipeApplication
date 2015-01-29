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
using System.Data;
using System.Xml;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {
        public XmlDocument doc = new XmlDocument();

        public AddRecipeWindow(XmlDocument StartFile)
        {
            InitializeComponent();

            if (StartFile != null)
            {
                doc = StartFile;
            }
        }

        private void AddRecipeWindowLoaded(object sender, RoutedEventArgs e)
        {
            PopulateComboBoxs();
        }

        private void PopulateComboBoxs()
        {
            // Get unit list
            List<string> unitList = new List<string>();
            UnitsOfMeasure unitClass = new UnitsOfMeasure();
            unitList = unitClass.GetList();

            // Get ingredient list
            List<string> ingredientList = new List<string>();
            XmlNodeList XMLingredientList = doc.SelectNodes("//Ingredient");

            foreach (XmlNode ingredient in XMLingredientList)
            {
                ingredientList.Add(ingredient.InnerXml);
            }

            ingredientList.Sort();

            // Populate combo boxs
            foreach (ComboBox cb in FindVisualChildren<ComboBox>(addRecipeWindow))
            {
                if (cb.Name.Contains("cb_ingredientUnit"))
                {
                    cb.ItemsSource = unitList;
                }
                else if (cb.Name.Contains("cb_ingredientName"))
                {
                    cb.ItemsSource = ingredientList;
                }
            }
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

        private void saveRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            //Save recipe
            Recipe newRecipe = new Recipe();

            foreach (TextBox tb in FindVisualChildren<TextBox>(addRecipeWindow))
            {
                if (tb.Text.ToString().Trim() != "" && tb.Tag.ToString() == "Name")
                {
                    newRecipe.name = tb.Text;
                }
                else if (tb.Text.ToString().Trim() != "" && tb.Tag.ToString() == "PrepTime")
                {
                    newRecipe.prepTime = tb.Text;
                }
                else if (tb.Text.ToString().Trim() != "" && tb.Tag.ToString() == "CookTime")
                {
                    newRecipe.cookTime = tb.Text;
                }
                else if (tb.Text.ToString().Trim() != "" && tb.Tag.ToString() == "Yeild")
                {
                    newRecipe.yeild = tb.Text;
                }
                else if (tb.Text.ToString().Trim() != "" && tb.Tag.ToString() == "Direction")
                {
                    newRecipe.directions.Add(tb.Text);
                }
            }

            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(addRecipeWindow))
            {
                if (comboBox.Text.ToString().Trim() != "" && comboBox.Tag.ToString() == "Ingredient")
                {
                    //Save Ingredients
                }
                else if (comboBox.Text.ToString().Trim() != "" && comboBox.Tag.ToString() == "Rating")
                {
                    newRecipe.rating = Convert.ToDouble(comboBox.SelectedValue);
                }
            }

            foreach (CheckBox cb in FindVisualChildren<CheckBox>(addRecipeWindow))
            {
                if (cb.IsChecked == true && cb.Tag.ToString() == "MealType")
                {
                    newRecipe.mealTypes.Add((MealType)Enum.Parse(typeof(MealType), cb.Name.ToString()));
                }
                else if (cb.IsChecked == true && cb.Tag.ToString() == "RecipeType")
                {
                    newRecipe.recipeTypes.Add((RecipeType)Enum.Parse(typeof(RecipeType), cb.Name.ToString()));
                }
                else if (cb.IsChecked == true && cb.Tag.ToString() == "Categorie")
                {
                    newRecipe.categories.Add((Category)Enum.Parse(typeof(Category), cb.Name.ToString()));
                }
            }

            // FractionToDouble(tb_ingredientQuantity.Text)
            // Update recipe book
            // Possibly update ingredient list
            MessageBox.Show("You clicked 'Save'");
            this.Close();
        }

        private void cancelRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        double FractionToDouble(string fraction)
        {
            double result;

            if (double.TryParse(fraction, out result))
            {
                return result;
            }

            string[] split = fraction.Split(new char[] { ' ', '/' });

            if (split.Length == 2 || split.Length == 3)
            {
                int a, b;

                if (int.TryParse(split[0], out a) && int.TryParse(split[1], out b))
                {
                    if (split.Length == 2)
                    {
                        return (double)a / b;
                    }

                    int c;

                    if (int.TryParse(split[2], out c))
                    {
                        return a + (double)b / c;
                    }
                }
            }

            throw new FormatException("Not a valid fraction.");
        }

    }
}
