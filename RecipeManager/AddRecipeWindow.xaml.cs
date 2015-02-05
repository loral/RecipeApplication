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
        public XmlDocument recipeBook = new XmlDocument();
        public List<string> ingredientList = new List<string>();
        public Recipe newRecipe;
        public string recipeXML;
        public XmlNodeList XMLingredientList;

        public AddRecipeWindow(XmlDocument StartFile)
        {
            InitializeComponent();

            // If not opened from an existing file create one else load the existing one.
            if (StartFile.InnerXml == "")
            {
                string newFile = @"<?xml version=""1.0"" encoding=""utf-8""?><RecipeManager><RecipeBook><Recipes></Recipes></RecipeBook><IngredientList></IngredientList><Menues></Menues><Links></Links></RecipeManager>";
                recipeBook.LoadXml(newFile);
            }
            else if (StartFile != null)
            {
                recipeBook = StartFile;
            }

            // Create hot keys
            try
            {
                RoutedCommand saveRecipe = new RoutedCommand();
                saveRecipe.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveRecipe, saveRecipe_btn_Click));

                RoutedCommand cancel = new RoutedCommand();
                cancel.InputGestures.Add(new KeyGesture(Key.Q, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(cancel, cancelRecipe_btn_Click));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
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
            XMLingredientList = recipeBook.SelectNodes("//Ingredient");
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

        private void saveRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            // Get new recipe info from window
            newRecipe = GetRecipe();

            // Create new recipe xml
            recipeXML = GetRecipeXML(newRecipe);

            // Add new recipe to recipe book file
            XmlDocument recipeDoc = new XmlDocument();
            recipeDoc.LoadXml(recipeXML);
            XmlNode recipeNode = recipeDoc.DocumentElement;
            recipeBook.SelectNodes("//RecipeManager/RecipeBook/Recipes")[0].AppendChild(recipeBook.ImportNode(recipeNode, true));

            // Add new ingredients to recipe book file
            foreach (Ingredient ingredient in newRecipe.ingredients)
            {
                if (!ingredientList.Contains(ingredient.Name.ToString()))
                {
                    XmlDocument ingredientDoc = new XmlDocument();
                    ingredientDoc.LoadXml("<Ingredient>" + ingredient.Name.ToString() + "</Ingredient>");
                    XmlNode ingredientNode = ingredientDoc.DocumentElement;

                    recipeBook.SelectNodes("//RecipeManager/IngredientList")[0].AppendChild(recipeBook.ImportNode(ingredientNode, true));
                }
            }

            // Save updated file to disk
            try
            {
                var myObject = this.Owner as MainWindow;

                if (Application.Current.Properties["StartFile"] != null && Application.Current.Properties["StartFile"].ToString() != null && Application.Current.Properties["StartFile"].ToString() != "No filename given")
                {
                    string fileName = Application.Current.Properties["StartFile"].ToString();
                    recipeBook.Save(fileName);
                }
                else
                {
                    SaveAs(this, null);
                }
                MessageBox.Show("Saved", "Saved!");
                myObject.reLoadFile();
                this.Close();
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
                string filename = dlg.FileName;
                recipeBook.Save(filename);
                Application.Current.Properties["StartFile"] = filename;
            }
            else
            {
                throw new Exception("No save file path selected!");
            }
        }

        private Recipe GetRecipe()
        {
            Recipe newRecipe = new Recipe();
            int ingredientCount = 1;

            foreach (TextBox tb in FindVisualChildren<TextBox>(addRecipeWindow))
            {
                if (tb.Text.ToString().Trim() != "" && tb.Tag != null)
                {
                    if (tb.Tag.ToString() == "Name")
                    {
                        newRecipe.name = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "PrepTime")
                    {
                        newRecipe.prepTime = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "CookTime")
                    {
                        newRecipe.cookTime = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "Yeild")
                    {
                        newRecipe.yeild = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "Direction")
                    {
                        newRecipe.directions.Add(tb.Text);
                    }
                }
            }

            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(addRecipeWindow))
            {
                if (comboBox.Text.ToString().Trim() != "")
                {
                    if (comboBox.Tag.ToString() == "Ingredient" && comboBox.Name.ToString().Contains("cb_ingredientName"))
                    {
                        try
                        {
                            Ingredient ingredient = new Ingredient();
                            string name = "cb_ingredientName" + ingredientCount;
                            string quantity = "tb_ingredientQuantity" + ingredientCount;
                            string unit = "cb_ingredientUnit" + ingredientCount;
                            ComboBox nameControl = (ComboBox)addRecipeWindow.FindName(name);
                            TextBox quantityControl = (TextBox)addRecipeWindow.FindName(quantity);
                            ComboBox unitControl = (ComboBox)addRecipeWindow.FindName(unit);

                            if (comboBox.Name != nameControl.Name)
                            {
                                throw new Exception("Mismatch ingredient control error");
                            }

                            ingredient.Name = nameControl.Text;
                            ingredient.Quanity = quantityControl.Text;
                            ingredient.Unit = unitControl.Text;
                            newRecipe.ingredients.Add(ingredient);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }

                        ingredientCount++;
                    }
                    else if (comboBox.Tag.ToString() == "Rating")
                    {
                        newRecipe.rating = Convert.ToDouble(comboBox.Text.ToString());
                    }
                }
            }

            foreach (CheckBox cb in FindVisualChildren<CheckBox>(addRecipeWindow))
            {
                if (cb.IsChecked == true)
                {
                    if (cb.Tag.ToString() == "MealType")
                    {
                        newRecipe.mealTypes.Add((MealType)Enum.Parse(typeof(MealType), cb.Name.ToString()));
                    }
                    else if (cb.Tag.ToString() == "RecipeType")
                    {
                        newRecipe.recipeTypes.Add((RecipeType)Enum.Parse(typeof(RecipeType), cb.Name.ToString()));
                    }
                    else if (cb.Tag.ToString() == "Categorie")
                    {
                        newRecipe.categories.Add((Category)Enum.Parse(typeof(Category), cb.Name.ToString()));
                    }
                }
            }

            return newRecipe;
        }

        private string GetRecipeXML(Recipe newRecipe)
        {
            // Build recipe xml
            string recipeToAddXML = @"<Recipe><Name>{name}</Name><Rating>{rating}</Rating><PrepTime>{prepTime}</PrepTime><CookTime>{cookTime}</CookTime><Yeild>{yeild}</Yeild><MealTypes>{mealTypes}</MealTypes><RecipeTypes>{recipeType}</RecipeTypes><RecipeIngredients>{recipeIngredient}</RecipeIngredients><Directions>{direction}</Directions><Categories>{category}</Categories></Recipe>";

            recipeToAddXML = recipeToAddXML.Replace("{name}", newRecipe.name);
            recipeToAddXML = recipeToAddXML.Replace("{rating}", newRecipe.rating.ToString());
            recipeToAddXML = recipeToAddXML.Replace("{prepTime}", newRecipe.prepTime);
            recipeToAddXML = recipeToAddXML.Replace("{cookTime}", newRecipe.cookTime);
            recipeToAddXML = recipeToAddXML.Replace("{yeild}", newRecipe.yeild);

            string mealTypeXML = "";
            string recipeTypeXML = "";
            string ingredientXML = "";
            string directionXML = "";
            string categorieXML = "";

            foreach (MealType mealType in newRecipe.mealTypes)
            {
                mealTypeXML = mealTypeXML + ("<MealType>" + mealType + "</MealType>");
            }

            recipeToAddXML = recipeToAddXML.Replace("{mealTypes}", mealTypeXML);

            foreach (RecipeType recipeType in newRecipe.recipeTypes)
            {
                recipeTypeXML = recipeTypeXML + ("<RecipeType>" + recipeType + "</RecipeType>");
            }

            recipeToAddXML = recipeToAddXML.Replace("{recipeType}", recipeTypeXML);

            foreach (Ingredient ingredient in newRecipe.ingredients)
            {
                ingredientXML = ingredientXML + ("<RecipeIngredient><IngredientName>" + ingredient.Name + "</IngredientName><IngredientQuantity>" + ingredient.Quanity + "</IngredientQuantity><IngredientUnit>" + ingredient.Unit + "</IngredientUnit></RecipeIngredient>");
            }

            recipeToAddXML = recipeToAddXML.Replace("{recipeIngredient}", ingredientXML);

            foreach (string direction in newRecipe.directions)
            {
                directionXML = directionXML + ("<Direction>" + direction + "</Direction>");
            }

            recipeToAddXML = recipeToAddXML.Replace("{direction}", directionXML);

            foreach (Category category in newRecipe.categories)
            {
                categorieXML = categorieXML + ("<Category>" + category + "</Category>");
            }

            recipeToAddXML = recipeToAddXML.Replace("{category}", categorieXML);

            return recipeToAddXML;
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

    }
}
