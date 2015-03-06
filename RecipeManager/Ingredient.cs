using System;
namespace RecipeManager
{
    [Serializable()]
    public class Ingredient
    {
        public string Name { get; set; }
        public string Quanity { get; set; }
        public string Unit { get; set; }
    }
}
