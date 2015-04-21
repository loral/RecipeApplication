using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace RecipeManager
{
    public class MainWindowViewModel
    {
        public List<Recipe> Recipes { get; set; }

        public Recipe SelectedRecipe { get; set; }

        public MainWindowViewModel(XmlDocument doc)
        {

            XmlNodeList _recipes = doc.SelectNodes("//RecipeManager/RecipeBook/Recipes/Recipe");

            Recipes = new List<Recipe>();

            foreach (XmlNode _recipe in _recipes)
            {
                List<MealType> _mealList = new List<MealType>();
                List<RecipeType> _recipeList = new List<RecipeType>();
                List<Ingredient> _ingredientList = new List<Ingredient>();
                List<Direction> _directionsList = new List<Direction>();
                List<Category> _categoryList = new List<Category>();
                Double? _doubleRating;

                var _name = _recipe["Name"].InnerText;
                var _rating = _recipe["Rating"].InnerText;

                if (_rating != null && _rating != "")
                {
                    try
                    {
                        _doubleRating = Convert.ToDouble(_rating);
                    }
                    catch (Exception)
                    {
                        _doubleRating = null;
                        MessageBox.Show("Non null on convertable rating encountered.");
                    }
                }
                else
                {
                    _doubleRating = null;
                }

                var _prep = _recipe["PrepTime"].InnerText;
                var _cook = _recipe["CookTime"].InnerText;
                var _yeild = _recipe["Yeild"].InnerText;

                var _meal = _recipe["MealTypes"].ChildNodes;
                var _recipeTypes = _recipe["RecipeTypes"].ChildNodes;
                var _ingredients = _recipe["RecipeIngredients"].ChildNodes;
                var _directions = _recipe["Directions"].ChildNodes;
                var _categories = _recipe["Categories"].ChildNodes;

                foreach (XmlNode _mealType in _meal)
                {
                    _mealList.Add((MealType)Enum.Parse(typeof(MealType), _mealType.InnerText));
                }
                foreach (XmlNode _recipeType in _recipeTypes)
                {
                    _recipeList.Add((RecipeType)Enum.Parse(typeof(RecipeType), _recipeType.InnerText));
                }
                foreach (XmlNode _ingredient in _ingredients)
                {
                    var _ingName = _ingredient["IngredientName"].InnerText;
                    var _ingQuantity = _ingredient["IngredientQuantity"].InnerText;
                    var _ingUnit = _ingredient["IngredientUnit"].InnerText;

                    Ingredient newIngredient = new Ingredient();
                    newIngredient.Name = _ingName;
                    newIngredient.Quanity = _ingQuantity;
                    newIngredient.Unit = _ingUnit;

                    _ingredientList.Add(newIngredient);
                }
                foreach (XmlNode _direction in _directions)
                {
                    Direction newDirection = new Direction();
                    newDirection.direction = _direction.InnerText;
                    _directionsList.Add(newDirection);
                }
                foreach (XmlNode _category in _categories)
                {
                    _categoryList.Add((Category)Enum.Parse(typeof(Category), _category.InnerText));
                }

                try
                {
                    Recipe newRecipe = new Recipe { Name = _name, rating = _doubleRating, PrepTime = _prep, CookTime = _cook, Yeild = _yeild, mealTypes = _mealList, recipeTypes = _recipeList, directions = _directionsList, categories = _categoryList, ingredients = _ingredientList };
                    Recipes.Add(newRecipe);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

            }

            Recipes = Recipes.OrderBy(x => x.Name).ToList();

            //https://github.com/grantwinney/BlogCodeSamples/tree/master/CollectionViewSourceSample/CollectionViewSourceSample
            //http://grantwinney.com/using-a-textbox-and-collectionviewsource-to-filter-a-listview-in-wpf/

        }

    }
}
