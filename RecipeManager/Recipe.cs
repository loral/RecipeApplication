using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class Recipe
    {
        public string name;
        public string prepTime;
        public string cookTime;
        public List<Ingredient> ingredients;
        public List<string> directions;
        public string yeild;
        public enum mealType { Breakfast, Lunch, Dinner };
        public enum recipeType { MainDish, Side, Dessert, Beverage };
    }
}
