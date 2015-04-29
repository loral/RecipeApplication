using RecipeManager.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Interaction logic for CreateMenuWindow.xaml
    /// </summary>
    public partial class CreateMenuWindow : Window
    {

        public List<Recipe> recipeBook;
        public List<Recipe> recipeBookCopy;
        public List<Recipe> randReplaceRecipeBookCopy;
        public List<Recipe> manualReplaceRecipeBookCopy;

        public List<string> ingredients { get; set; }

        public List<StringValue> _Ingredients { get; set; }

        public CreateMenuWindow(List<Recipe> _recipes)
        {
            InitializeComponent();
            recipeBook = _recipes;
        }

        private void cancelRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CreateMenuWindowLoaded(object sender, RoutedEventArgs e)
        {

        }

        private void saveMenu_btn_Click(object sender, RoutedEventArgs e)
        {
            // Do
        }

        private void createMenu_btn_Click(object sender, RoutedEventArgs e)
        {
            // Make sure a valid number of meals is selected
            if (string.IsNullOrEmpty(cb_meals.Text))
            {
                MessageBox.Show("Please select the number of meals you would like to create a munu for.");
                return;
            }

            string recipeOutput = String.Empty;
            Random rnd = new Random();
            recipeBookCopy = new List<Recipe>();
            ingredients = new List<string>();
            _Ingredients = new List<StringValue>();

            // Create a list of possible reciepies that are both dinner and main dish
            foreach (Recipe recipe in recipeBook)
            {
                if (recipe.mealTypes.Contains(MealType.Dinner) && recipe.recipeTypes.Contains(RecipeType.MainDish))
                    recipeBookCopy.Add(Clone.DeepClone<Recipe>(recipe));
            }

            // Shuffle the list
            PartialShuffle<Recipe>(recipeBookCopy, Convert.ToInt32(cb_meals.Text), rnd);

            // Trim list to selected meal length
            if (recipeBookCopy.Count > Convert.ToInt32(cb_meals.Text))
            {
                recipeBookCopy.RemoveRange(Convert.ToInt32(cb_meals.Text), (recipeBookCopy.Count - Convert.ToInt32(cb_meals.Text)));
            }

            // Grab ingredients
            foreach (Recipe recipe in recipeBookCopy)
            {
                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    if (!ingredients.Contains(ingredient.Name))
                    {
                        ingredients.Add(ingredient.Name);
                        StringValue newIngredient = new StringValue(ingredient.Name);
                        _Ingredients.Add(newIngredient);
                    }
                }
            }

            ingredients.Sort();
            _Ingredients = _Ingredients.OrderBy(i => i.Value).ToList();

            RecipeListView.ItemsSource = recipeBookCopy;
            IngredientListView.ItemsSource = ingredients;
            IngredientGridView.ItemsSource = _Ingredients;

            // Make sure a recipe is selected
            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }
        }

        public IList<T> PartialShuffle<T>(IList<T> source, int count, Random random)
        {
            for (int i = 0; i < count && i < source.Count; i++)
            {
                // Pick a random element out of the remaining elements,
                // and swap it into place.
                int index = i + random.Next(source.Count - i);
                T tmp = source[index];
                source[index] = source[i];
                source[i] = tmp;
            }

            return source;
        }

        private void emailMenu_btn_Click(object sender, RoutedEventArgs e)
        {
            SendEmail();
        }

        public void SendEmail()
        {
            string to;
            List<string> ingredientList = new List<string>();

            if (!string.IsNullOrEmpty(email_txtbx.Text))
                to = email_txtbx.Text;
            else
            {
                MessageBox.Show("Please enter a valid email.", "Invalid Email Address");
                return;
            }

            string from = Settings.Default.from_email;
            string subject = "Recipe Manager Menu";

            string body = string.Empty;

            // Build the body text
            body += "Recipes:" + System.Environment.NewLine;
            foreach (Recipe recipe in recipeBookCopy)
            {
                body += recipe.Name + System.Environment.NewLine;
            }

            body += System.Environment.NewLine + "Ingredients:" + System.Environment.NewLine;

            foreach (string ingredient in ingredients)
            {
                body += ingredient + System.Environment.NewLine;
            }

            MailMessage message = new MailMessage(from, to, subject, body);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            // Set up client
            SmtpClient client = new SmtpClient(Settings.Default.smtp_client_host, Settings.Default.smtp_client_port);
            client.EnableSsl = false;
            client.Timeout = Settings.Default.timeout;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Settings.Default.client_username, Settings.Default.client_password);

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            MessageBox.Show("Email sent!");
        }

        private void RandomReplace(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            randReplaceRecipeBookCopy = new List<Recipe>();

            // Get list of recipe names currently on the menu
            List<string> menuRecipes = new List<string>();

            foreach (Recipe recipe in recipeBookCopy)
            {
                menuRecipes.Add(recipe.Name);
            }

            // Create a copy of the recipe book to use for finding a substitue recipe (recipes not currently on the menu and that are dinner and main dish)
            foreach (Recipe recipe in recipeBook)
            {
                if (recipe.mealTypes.Contains(MealType.Dinner) && recipe.recipeTypes.Contains(RecipeType.MainDish) && !menuRecipes.Contains(recipe.Name))
                    randReplaceRecipeBookCopy.Add(Clone.DeepClone<Recipe>(recipe));
            }

            // Shuffle the copied list
            PartialShuffle<Recipe>(randReplaceRecipeBookCopy, randReplaceRecipeBookCopy.Count, rnd);

            // Grab the fist recipe from the shuffled list and swap it with the selected recipe
            if (randReplaceRecipeBookCopy.Count > 0)
            {
                foreach (Recipe _recipe in RecipeListView.SelectedItems)
                {
                    if (recipeBookCopy.IndexOf(_recipe) > -1)
                    {
                        recipeBookCopy[recipeBookCopy.IndexOf(_recipe)] = randReplaceRecipeBookCopy[0];
                    }
                }


                // Update recipe list view
                ICollectionView view = CollectionViewSource.GetDefaultView(recipeBookCopy);
                view.Refresh();

                // Update ingredient list view
                ingredients.Clear();

                foreach (Recipe recipe in recipeBookCopy)
                {
                    foreach (Ingredient ingredient in recipe.ingredients)
                    {
                        if (!ingredients.Contains(ingredient.Name))
                        {
                            ingredients.Add(ingredient.Name);
                        }
                    }
                }

                ingredients.Sort();

                ICollectionView ingredView = CollectionViewSource.GetDefaultView(ingredients);
                ingredView.Refresh();

            }

            else
                MessageBox.Show("There are not enough recipes in your recipe book to replace the selected recipe.");

            // Make sure a recipe is selected
            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }

        }

        private void ManualReplace(object sender, RoutedEventArgs e)
        {
            manualReplaceRecipeBookCopy = new List<Recipe>();
            Recipe manualReplaceRecipe = new Recipe();

            // Get list of recipe names currently on the menu
            List<string> menuRecipes = new List<string>();

            foreach (Recipe recipe in recipeBookCopy)
            {
                menuRecipes.Add(recipe.Name);
            }

            // Create a copy of the recipe book to use for finding a substitue recipe (recipes not currently on the menu and that are dinner and main dish)
            foreach (Recipe recipe in recipeBook)
            {
                if (recipe.mealTypes.Contains(MealType.Dinner) && recipe.recipeTypes.Contains(RecipeType.MainDish) && !menuRecipes.Contains(recipe.Name))
                    manualReplaceRecipeBookCopy.Add(Clone.DeepClone<Recipe>(recipe));
            }

            // From the manualReplaceRecipeBookCopy list have the submitter choose one and replace the selected recipe with it
            if (manualReplaceRecipeBookCopy.Count > 0)
            {

                // Get user selected recipe
                ManualRecipeSelection manualRecipeSelection = new ManualRecipeSelection(manualReplaceRecipeBookCopy);
                manualRecipeSelection.Owner = this;

                if (manualRecipeSelection.ShowDialog() == false)
                {
                    manualReplaceRecipe = manualRecipeSelection.chosenRecipe;
                    if (manualReplaceRecipe == null)
                        return;
                }

                // Replace selected recipe with the user selected recipe
                foreach (Recipe _recipe in RecipeListView.SelectedItems)
                {
                    if (recipeBookCopy.IndexOf(_recipe) > -1)
                    {
                        recipeBookCopy[recipeBookCopy.IndexOf(_recipe)] = manualReplaceRecipe;
                    }
                }

                // Update recipe list view
                ICollectionView view = CollectionViewSource.GetDefaultView(recipeBookCopy);
                view.Refresh();

                // Update ingredient list view
                ingredients.Clear();

                foreach (Recipe recipe in recipeBookCopy)
                {
                    foreach (Ingredient ingredient in recipe.ingredients)
                    {
                        if (!ingredients.Contains(ingredient.Name))
                        {
                            ingredients.Add(ingredient.Name);
                        }
                    }
                }

                ingredients.Sort();

                ICollectionView ingredView = CollectionViewSource.GetDefaultView(ingredients);
                ingredView.Refresh();
            }

            else
                MessageBox.Show("There are not enough recipes in your recipe book to replace the selected recipe.");

            // Make sure a recipe is selected
            if (RecipeListView.SelectedIndex == -1 && RecipeListView.Items.Count > 0)
            {
                RecipeListView.SelectedItem = RecipeListView.Items[0];
            }
        }

        private void RemoveIngredients(object sender, RoutedEventArgs e)
        {
            foreach (string _ingredient in IngredientListView.SelectedItems)
            {
                ingredients.Remove(_ingredient);
            }

            ICollectionView view = CollectionViewSource.GetDefaultView(ingredients);
            view.Refresh();
        }

        private void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var chk = (CheckBox)sender;
            var row = VisualTreeHelpers.FindAncestor<DataGridRow>(chk);
            var newValue = !chk.IsChecked.GetValueOrDefault();

            row.IsSelected = newValue;
            chk.IsChecked = newValue;

            // Mark event as handled so that the default 
            // DataGridPreviewMouseDown doesn't handle the event
            e.Handled = true;
        }

        private void TestDataGrid_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var chk = VisualTreeHelpers.FindAncestor<CheckBox>((DependencyObject)e.OriginalSource, "TestCheckBox");

            if (chk == null)
                e.Handled = true;
        }
    }
}
