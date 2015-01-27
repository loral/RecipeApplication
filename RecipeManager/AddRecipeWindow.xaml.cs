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
            LoadUnits();
            LoadIngredientNames();
        }

        private void LoadUnits()
        {
            List<string> unitList = new List<string>();
            UnitsOfMeasure unitClass = new UnitsOfMeasure();
            unitList = unitClass.GetList();

            foreach (ComboBox cb in FindVisualChildren<ComboBox>(addRecipeWindow))
            {
                if (cb.Name.Contains("cb_ingredientUnit"))
                {
                    cb.ItemsSource = unitList;
                }
            }
        }

        private void LoadIngredientNames()
        {
            List<string> ingredientList = new List<string>();
            XmlNodeList XMLingredientList = doc.SelectNodes("//Ingredient");

            foreach (XmlNode ingredient in XMLingredientList)
            {
                ingredientList.Add(ingredient.InnerXml);
            }

            ingredientList.Sort();

            foreach(ComboBox cb in FindVisualChildren<ComboBox>(addRecipeWindow))
            {
                if (cb.Name.Contains("cb_ingredientName"))
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
