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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            MessageBox.Show("You clicked 'Add Recipe'");
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
            MessageBox.Show("You clicked 'Exit'");
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
