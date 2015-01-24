using System.Collections.Generic;

namespace RecipeManager
{
    public class Recipe
    {
        public string name { get; set; }
        public string prepTime { get; set; }
        public string cookTime { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<string> directions { get; set; }
        public string yeild { get; set; }
        public List<MealType> mealTypes { get; set; }
        public List<RecipeType> recipeTypes { get; set; }
        public List<Category> categories { get; set; }
        public Rating rating { get; set; }
    }
}
