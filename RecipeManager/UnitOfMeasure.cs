using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class UnitsOfMeasure
    {
        public List<string> units = new List<string> { "Tbsp", "tsp", "oz", "fl oz", "cup", "pint", "qt", "gal", "liter", "ml", "lb", "mg", "gram" };

        public List<string> GetList()
        {
            return units;
        }
    }
}
