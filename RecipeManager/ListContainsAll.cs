using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public static class ListContainsAll
    {
        public static bool ContainsAllItems(List<Category> a, List<Category> b)
        {
            return !b.Except(a).Any();
        }

        public static bool ContainsAllItems(List<MealType> a, List<MealType> b)
        {
            return !b.Except(a).Any();
        }

        public static bool ContainsAllItems(List<RecipeType> a, List<RecipeType> b)
        {
            return !b.Except(a).Any();
        }
    }
}
