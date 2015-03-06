using System;
using System.Collections.Generic;

namespace RecipeManager
{
    public enum Category { AppetizerSnack, BeanRiceGrain, Beverage, Bread, CakeFrosting, Candy, CanningFreezing, CheeseEgg, CookieBar, Dessert, Fish, Grill, Meat, Pasta, PieTart, Poultry, SaladDressing, SauceRelish, SlowCooker, SoupStew, VegetablesFruit };
    public enum MealType { Breakfast, Lunch, Dinner };
    public enum RecipeType { MainDish, Side };

    [Serializable()]
    public class Recipe
    {
        public string name { get; set; }
        public string prepTime { get; set; }
        public string cookTime { get; set; }
        public string yeild { get; set; }
        public double? rating { get; set; }

        public List<Ingredient> ingredients = new List<Ingredient>();

        public List<string> directions = new List<string>();

        public List<MealType> mealTypes = new List<MealType>();

        public List<RecipeType> recipeTypes = new List<RecipeType>();

        public List<Category> categories = new List<Category>();
        
    }
}
