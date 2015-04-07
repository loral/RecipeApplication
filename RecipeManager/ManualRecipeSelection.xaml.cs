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

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for ManualRecipeSelection.xaml
    /// </summary>
    public partial class ManualRecipeSelection : Window
    {
        public List<Recipe> possibleRecipes;
        public Recipe chosenRecipe;

        public ManualRecipeSelection(List<Recipe> _recipes)
        {
            InitializeComponent();
            possibleRecipes = _recipes;        
        }

        private void ManualRecipeSelectionLoaded(object sender, RoutedEventArgs e)
        {
            RecipeListView.ItemsSource = possibleRecipes;
        }

        private void ChooseSelected(object sender, RoutedEventArgs e)
        {
            foreach(Recipe recipe in RecipeListView.SelectedItems)
            {
                chosenRecipe = recipe;
            }
            this.Close();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show(chosenRecipe.name);
        }
    }
}
