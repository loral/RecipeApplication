using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Check if this was launched by double-clicking a doc. If so, use that as the startup file name.
            if (e.Args != null && e.Args.Length > 0)
            {
                string fname = "No filename given";
                try
                {
                    fname = e.Args[0];

                    // It comes in as a URI; this helps to convert it to a path.
                    Uri uri = new Uri(fname);
                    fname = uri.LocalPath;

                    this.Properties["StartFile"] = fname;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            base.OnStartup(e);
        }
    }
}
