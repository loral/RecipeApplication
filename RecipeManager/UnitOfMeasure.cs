using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class UnitsOfMeasure
    {
        public List<string> units = new List<string> { "Tbsp(s)", "tsp(s)", "oz(s)", "fl oz(s)", "cup(s)", "pint(s)", "qt(s)", "gal(s)", "liter(s)", "ml(s)", "lb(s)", "mg(s)", "gram(s)" };

        public List<string> GetList()
        {
            return units;
        }
    }
}
