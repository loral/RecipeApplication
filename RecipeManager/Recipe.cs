using System.Collections.Generic;

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
        public List<MealType> mealTypes;
        public List<RecipeType> recipeTypes;
        public List<Category> categories;
    }
}
