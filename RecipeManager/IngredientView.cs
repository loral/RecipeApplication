using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace RecipeManager
{
    public class IngredientView
    {
        public List<string> Ingredients { get; set; }

        public IngredientView(XmlDocument doc)
        {

            XmlNodeList ingredients = doc.SelectNodes("//RecipeManager/IngredientList/Ingredient");

            Ingredients = new List<string>();

            foreach (XmlNode ingredient in ingredients)
            {
                try
                {
                    Ingredients.Add(ingredient.InnerText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }

            }

            Ingredients = Ingredients.OrderBy(x => x).ToList();

        }

    }
}
