using RecipeManager.Properties;
using System;
using System.Collections.Generic;
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
        public List<string> ingredients;

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

            // Create a list of possible reciepies that are both dinner and main dish
            foreach (Recipe recipe in recipeBook)
            {
                if (recipe.mealTypes.Contains(MealType.Dinner) && recipe.recipeTypes.Contains(RecipeType.MainDish))
                    recipeBookCopy.Add(Clone.DeepClone<Recipe>(recipe));
            }

            // Shuffle the list
            PartialShuffle<Recipe>(recipeBookCopy, Convert.ToInt32(cb_meals.Text), rnd);

            // Trim list to selected meal length
            if(recipeBookCopy.Count > Convert.ToInt32(cb_meals.Text))
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
                    }
                }
            }

            ingredients.Sort();

            RecipeListView.ItemsSource = recipeBookCopy;
            IngredientListView.ItemsSource = ingredients;
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

            // ***Need to write the code to populate body with recipes and ingredients***

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
            // Todo
        }

        private void ManualReplace(object sender, RoutedEventArgs e)
        {
            // Todo
        }

        private void RemoveIngredients(object sender, RoutedEventArgs e)
        {
            // Todo
        }
    }
}
