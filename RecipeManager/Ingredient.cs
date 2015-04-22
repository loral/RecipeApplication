using System;
using System.Windows;
using System.Xml;
namespace RecipeManager
{
    [Serializable()]
    public class Ingredient
    {
        public string Name { get; set;}

        public string Quanity { get; set;}

        public string Unit { get; set;}

    }
}
