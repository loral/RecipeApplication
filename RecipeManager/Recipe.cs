using System;
using System.Collections.Generic;
using System.Xml;

namespace RecipeManager
{
    public enum Category { AppetizerSnack, BeanRiceGrain, Beverage, Bread, CakeFrosting, Candy, CanningFreezing, CheeseEgg, CookieBar, Dessert, Fish, Grill, Meat, Pasta, PieTart, Poultry, SaladDressing, SauceRelish, SlowCooker, SoupStew, VegetablesFruit };
    public enum MealType { Breakfast, Lunch, Dinner };
    public enum RecipeType { MainDish, Side };

    [Serializable()]
    public class Recipe
    {
        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedName
        {
            get
            {
                return XmlConvert.DecodeName(_name);
            }
        }

        private string _prepTime;

        public string PrepTime
        {
            get
            {
                return _prepTime;
            }
            set
            {
                _prepTime = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedPrepTime
        {
            get
            {
                return XmlConvert.DecodeName(_prepTime);
            }
        }

        private string _cookTime;

        public string CookTime
        {
            get
            {
                return _cookTime;
            }
            set
            {
                _cookTime = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedCookTime
        {
            get
            {
                return XmlConvert.DecodeName(_cookTime);
            }
        }

        private string _yeild;

        public string Yeild
        {
            get
            {
                return _yeild;
            }
            set
            {
                _yeild = XmlConvert.EncodeLocalName(value);
            }
        }

        public string DecodedYeild
        {
            get
            {
                return XmlConvert.DecodeName(_yeild);
            }
        }

        public double? rating { get; set; }

        public List<Ingredient> ingredients = new List<Ingredient>();

        public List<Direction> directions = new List<Direction>();

        public List<MealType> mealTypes = new List<MealType>();

        public List<RecipeType> recipeTypes = new List<RecipeType>();

        public List<Category> categories = new List<Category>();
        
    }
}
