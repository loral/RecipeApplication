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
using System.Xml;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for EditRecipeWindow.xaml
    /// </summary>
    public partial class EditRecipeWindow : Window
    {
        public XmlDocument recipeBook = new XmlDocument();
        public List<string> ingredientList = new List<string>();
        public Recipe newRecipe;
        public Recipe _editRecipe;
        public string recipeXML;
        public XmlNodeList XMLingredientList;

        public EditRecipeWindow(XmlDocument StartFile, Recipe editRecipe)
        {
            _editRecipe = editRecipe;
            InitializeComponent();

            if (StartFile != null)
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

        private void EditRecipeWindowLoaded(object sender, RoutedEventArgs e)
        {
            PopulateWindow();
            tb_name.Focusable = true;
            Keyboard.Focus(tb_name);
        }

        private void PopulateWindow()
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
            foreach (ComboBox cb in FindVisualChildren<ComboBox>(editRecipeWindow))
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

            tb_name.Text = _editRecipe.Name;
            cb_rating.Text = _editRecipe.rating.ToString();
            tb_prepTime.Text = _editRecipe.PrepTime;
            tb_cookTime.Text = _editRecipe.CookTime;
            tb_yeild.Text = _editRecipe.Yeild;

            foreach (MealType mealType in _editRecipe.mealTypes)
            {
                if (mealType == MealType.Breakfast)
                    Breakfast.IsChecked = true;
                else if (mealType == MealType.Lunch)
                    Lunch.IsChecked = true;
                else if (mealType == MealType.Dinner)
                    Dinner.IsChecked = true;
            }

            foreach (RecipeType recipeType in _editRecipe.recipeTypes)
            {
                if (recipeType == RecipeType.MainDish)
                    MainDish.IsChecked = true;
                else if (recipeType == RecipeType.Side)
                    Side.IsChecked = true;
            }

            int counter = 1;
            foreach (Ingredient ingredient in _editRecipe.ingredients)
            {
                foreach (TextBox tb in FindVisualChildren<TextBox>(editRecipeWindow))
                {
                    if ((tb.Tag != null) && (tb.Tag.ToString() == "Ingredient") && (tb.Name == ("tb_ingredientQuantity" + counter.ToString())))
                    {
                        tb.Text = ingredient.Quanity;
                        counter = counter + 1;
                        break;
                    }
                }
            }

            counter = 1;
            foreach (Ingredient ingredient in _editRecipe.ingredients)
            {
                foreach (ComboBox cb in FindVisualChildren<ComboBox>(editRecipeWindow))
                {
                    if ((cb.Tag != null) && (cb.Tag.ToString() == "Ingredient") && (cb.Name == ("cb_ingredientUnit" + counter.ToString())))
                    {
                        cb.Text = ingredient.Unit;
                        counter = counter + 1;
                        break;
                    }
                }
            }

            counter = 1;
            foreach (Ingredient ingredient in _editRecipe.ingredients)
            {
                foreach (ComboBox cb in FindVisualChildren<ComboBox>(editRecipeWindow))
                {
                    if ((cb.Tag != null) && (cb.Tag.ToString() == "Ingredient") && (cb.Name == ("cb_ingredientName" + counter.ToString())))
                    {
                        cb.Text = ingredient.Name;
                        counter = counter + 1;
                        break;
                    }
                }
            }

            counter = 1;
            foreach (string direction in _editRecipe.directions)
            {
                foreach (TextBox tb in FindVisualChildren<TextBox>(editRecipeWindow))
                {
                    if ((tb.Tag != null) && (tb.Tag.ToString() == "Direction") && (tb.Name == ("tb_direction" + counter.ToString())))
                    {
                        tb.Text = direction;
                        counter = counter + 1;
                        break;
                    }
                }
            }

            foreach (Category category in _editRecipe.categories)
            {
                if (category == Category.AppetizerSnack)
                    AppetizerSnack.IsChecked = true;
                else if (category == Category.BeanRiceGrain)
                    BeanRiceGrain.IsChecked = true;
                else if (category == Category.Beverage)
                    Beverage.IsChecked = true;
                else if (category == Category.Bread)
                    Bread.IsChecked = true;
                else if (category == Category.CakeFrosting)
                    CakeFrosting.IsChecked = true;
                else if (category == Category.Candy)
                    Candy.IsChecked = true;
                else if (category == Category.CanningFreezing)
                    CanningFreezing.IsChecked = true;
                else if (category == Category.CheeseEgg)
                    CheeseEgg.IsChecked = true;
                else if (category == Category.CookieBar)
                    CookieBar.IsChecked = true;
                else if (category == Category.Dessert)
                    Dessert.IsChecked = true;
                else if (category == Category.Fish)
                    Fish.IsChecked = true;
                else if (category == Category.Grill)
                    Grill.IsChecked = true;
                else if (category == Category.Meat)
                    Meat.IsChecked = true;
                else if (category == Category.Pasta)
                    Pasta.IsChecked = true;
                else if (category == Category.PieTart)
                    PieTart.IsChecked = true;
                else if (category == Category.Poultry)
                    Poultry.IsChecked = true;
                else if (category == Category.SaladDressing)
                    SaladDressing.IsChecked = true;
                else if (category == Category.SauceRelish)
                    SauceRelish.IsChecked = true;
                else if (category == Category.SlowCooker)
                    SlowCooker.IsChecked = true;
                else if (category == Category.SoupStew)
                    SoupStew.IsChecked = true;
                else if (category == Category.VegetablesFruit)
                    VegetablesFruit.IsChecked = true;
            }

        }

        private void saveRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            // Get new recipe info from window
            try
            {
                newRecipe = GetRecipe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save canceled." + System.Environment.NewLine + ex.ToString());
                return;
            }

            // Create new recipe xml
            recipeXML = GetRecipeXML(newRecipe);

            // Add new recipe to recipe book file
            XmlDocument recipeDoc = new XmlDocument();
            recipeDoc.LoadXml(recipeXML);
            XmlNode newRecipeNode = recipeDoc.DocumentElement;

            //string escapedName = _editRecipe.name;

            string nodeName = "//RecipeManager/RecipeBook/Recipes/Recipe[./Name='" + _editRecipe.Name + "']";
            XmlNode oldRecipeNode = recipeBook.SelectSingleNode(nodeName);

            oldRecipeNode.ParentNode.RemoveChild(oldRecipeNode);
            recipeBook.SelectNodes("//RecipeManager/RecipeBook/Recipes")[0].AppendChild(recipeBook.ImportNode(newRecipeNode, true));

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

                string fileName = Application.Current.Properties["StartFile"].ToString();
                recipeBook.Save(fileName);

                MessageBox.Show("Saved", "Saved!");
                myObject.reLoadFile();
                myObject.Focus();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        private Recipe GetRecipe()
        {
            Recipe newRecipe = new Recipe();
            int ingredientCount = 1;

            XmlNodeList _recipeNames = recipeBook.SelectNodes("//RecipeManager/RecipeBook/Recipes/Recipe");

            foreach (TextBox tb in FindVisualChildren<TextBox>(editRecipeWindow))
            {
                if (tb.Text.ToString().Trim() != "" && tb.Tag != null)
                {
                    if (tb.Tag.ToString() == "Name")
                    {
                        // Duplicate name check
                        foreach (XmlNode _recipe in _recipeNames)
                        {
                            if (tb.Text == _recipe["Name"].InnerText)
                            {
                                //Possible duplicate name.
                            }
                        }
                        newRecipe.Name = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "PrepTime")
                    {
                        newRecipe.PrepTime = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "CookTime")
                    {
                        newRecipe.CookTime = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "Yeild")
                    {
                        newRecipe.Yeild = tb.Text;
                    }
                    else if (tb.Tag.ToString() == "Direction")
                    {
                        newRecipe.directions.Add(tb.Text);
                    }
                }
            }

            if (String.IsNullOrEmpty(newRecipe.Name))
            {
                MessageBox.Show("Pleae enter a recipe name.");
                throw new Exception("Invalid recipe name.");
            }

            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(editRecipeWindow))
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
                            ComboBox nameControl = (ComboBox)editRecipeWindow.FindName(name);
                            TextBox quantityControl = (TextBox)editRecipeWindow.FindName(quantity);
                            ComboBox unitControl = (ComboBox)editRecipeWindow.FindName(unit);

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
                        try
                        {
                            newRecipe.rating = Convert.ToDouble(comboBox.Text.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Pleae enter a valid numerical number for a rating." + System.Environment.NewLine + ex.ToString());
                            throw new Exception("Invalid rating");
                        }

                    }
                }
            }

            foreach (CheckBox cb in FindVisualChildren<CheckBox>(editRecipeWindow))
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

            recipeToAddXML = recipeToAddXML.Replace("{name}", XmlConvert.EncodeLocalName(newRecipe.Name));
            recipeToAddXML = recipeToAddXML.Replace("{rating}", newRecipe.rating.ToString());
            recipeToAddXML = recipeToAddXML.Replace("{prepTime}", XmlConvert.EncodeLocalName(newRecipe.PrepTime));
            recipeToAddXML = recipeToAddXML.Replace("{cookTime}", XmlConvert.EncodeLocalName(newRecipe.CookTime));
            recipeToAddXML = recipeToAddXML.Replace("{yeild}", XmlConvert.EncodeLocalName(newRecipe.Yeild));

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

        private void deleteRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete " + _editRecipe.Name + "?", "Delete?", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                string nodeName = "//RecipeManager/RecipeBook/Recipes/Recipe[./Name='" + _editRecipe.Name + "']";
                XmlNode oldRecipeNode = recipeBook.SelectSingleNode(nodeName);

                oldRecipeNode.ParentNode.RemoveChild(oldRecipeNode);

                // Save updated file to disk
                try
                {
                    var myObject = this.Owner as MainWindow;

                    string fileName = Application.Current.Properties["StartFile"].ToString();
                    recipeBook.Save(fileName);

                    MessageBox.Show("Deleted", "Deleted!");
                    myObject.reLoadFile();
                    myObject.Focus();

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }
    }
}
