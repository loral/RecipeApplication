using System.Windows;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Application Logic

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Any stuff that needs to be set or loaded after the window is created needs to go here.
        }

        private void AddRecipe(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();
            addRecipeWindow.Show();
        }

        private void ViewRecipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'View Recipe'");
        }

        private void CreateMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Create Menu'");
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Open'");
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Save'");
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Save As'");
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Would you like to save changes?", "Save?", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                RoutedEventArgs args = new RoutedEventArgs();
                Save(this, args);
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
