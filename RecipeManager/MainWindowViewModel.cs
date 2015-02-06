using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Recipes = new List<Recipe>
            {
                new Recipe(){name = "apple"},
                new Recipe(){name = "bannana"},
                new Recipe(){name = "carrot"},
                new Recipe(){name = "delicious"},
                new Recipe(){name = "edible"},
                new Recipe(){name = "fanciful"},
                new Recipe(){name = "grape"},
                new Recipe(){name = "hidalgo"},
                new Recipe(){name = "igloo"},
                new Recipe(){name = "jasmin"},
                new Recipe(){name = "kangaroo"},
                new Recipe(){name = "kit"},
                new Recipe(){name = "lemon"},
                new Recipe(){name = "lemur"},
                new Recipe(){name = "loral"},
                //https://github.com/grantwinney/BlogCodeSamples/tree/master/CollectionViewSourceSample/CollectionViewSourceSample
                //http://grantwinney.com/using-a-textbox-and-collectionviewsource-to-filter-a-listview-in-wpf/
            };
        }

        public List<Recipe> Recipes { get; set; }

        public Recipe SelectedRecipe { get; set; }
    }
}
