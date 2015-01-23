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
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Save'");
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            // Configure save file dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "RecipeManager"; // Default file name
            dlg.DefaultExt = ".rmn"; // Default file extension
            dlg.Filter = "Text documents (.rmn)|*.rmn"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                string filename = dlg.FileName;
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
