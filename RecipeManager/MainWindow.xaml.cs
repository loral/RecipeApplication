﻿using System;
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
using System.Xml;
using System.IO;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XmlDocument doc = new XmlDocument();
        public string fileName = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            if (Application.Current.Properties["StartFile"] != null)
            {
                fileName = Application.Current.Properties["StartFile"].ToString();
                if (fileName != null && fileName != "No filename given")
                {
                    LoadFileData(fileName);
                }
            }

            // Create hot keys
            try
            {
                RoutedCommand addRecipe = new RoutedCommand();
                addRecipe.InputGestures.Add(new KeyGesture(Key.A, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(addRecipe, AddRecipe));

                RoutedCommand saveFile = new RoutedCommand();
                saveFile.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(saveFile, Save));

                RoutedCommand openFile = new RoutedCommand();
                openFile.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
                CommandBindings.Add(new CommandBinding(openFile, Open));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        #region Application Logic

        private void LoadFileData(string filePath)
        {  
            doc.Load(filePath);
            MessageBox.Show(doc.InnerXml);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Any stuff that needs to be set or loaded after the window is created needs to go here.
        }

        private void AddRecipe(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow(doc);
            addRecipeWindow.Owner = this;
            addRecipeWindow.Show();
        }

        public void reLoadFile()
        {
            try
            {
                doc.Load(Application.Current.Properties["StartFile"].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ViewRecipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'View Recipe'");
        }

        private void EditRecipe(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Edit Recipe'");
        }

        private void CreateMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked 'Create Menu'");
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".rmn"; // Default file extension
            dlg.Filter = "Recipe Manager documents (.rmn)|*.rmn"; // Filter files by extension 

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results 
            if (result == true)
            {
                // Open document 
                fileName = dlg.FileName;
                LoadFileData(fileName);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // Save updated file to disk
            try
            {
                if (Application.Current.Properties["StartFile"] != null && Application.Current.Properties["StartFile"].ToString() != null && Application.Current.Properties["StartFile"].ToString() != "No filename given")
                {
                    doc.Save(fileName);
                    MessageBox.Show("Saved", "Saved!");
                }
                else
                {
                    SaveAs(this, null);
                }
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
            dlg.Filter = "Xml documents (.rmn)|*.rmn"; // Filter files by extension 

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results 
            if (result == true)
            {
                // Save document 
                fileName = dlg.FileName;
                doc.Save(fileName);
                MessageBox.Show("Saved", "Saved!");
            }
            else
            {
                MessageBox.Show("No save file path selected!","Error");
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
