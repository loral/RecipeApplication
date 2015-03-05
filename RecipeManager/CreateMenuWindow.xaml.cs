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

        }

        private void createMenu_btn_Click(object sender, RoutedEventArgs e)
        {
            string recipeOutput = "";

            foreach (Recipe _recipe in recipeBook)
            {
                recipeOutput += _recipe.name + System.Environment.NewLine;
            }
            output.Text = recipeOutput;
        }

        private void emailMenu_btn_Click(object sender, RoutedEventArgs e)
        {
            SendEmail();
        }

        public void SendEmail()
        {
            string to;

            if (!string.IsNullOrEmpty(email_txtbx.Text))
                to = email_txtbx.Text;
            else
            {
                MessageBox.Show("Please enter a valid email.", "Invalid Email Address");
                return;
            }

            string from = "loral@loralgodfrey.com";
            string subject = "Using the new SMTP client.";
            string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
            MailMessage message = new MailMessage(from, to, subject, body);
            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            // Set up google
            SmtpClient client = new SmtpClient("mail.loralgodfrey.com", 2626);
            client.EnableSsl = false;
            client.Timeout = 60000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("loral@loralgodfrey.com", "Gibscam7!");

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                output.Text = "";
                output.Text = ex.ToString();
            }
        }
    }
}
