﻿#pragma checksum "..\..\CreateMenuWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8839C4BA5E3AEF6E7D6E54A12F4D9CC0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace RecipeManager {
    
    
    /// <summary>
    /// CreateMenuWindow
    /// </summary>
    public partial class CreateMenuWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_meals;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_createMenu;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox email_txtbx;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView RecipeListView;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView IngredientListView;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_saveRecipe;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_emailRecipe;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\CreateMenuWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cancelRecipe;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RecipeManager;component/createmenuwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateMenuWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\CreateMenuWindow.xaml"
            ((RecipeManager.CreateMenuWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.CreateMenuWindowLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cb_meals = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.btn_createMenu = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\CreateMenuWindow.xaml"
            this.btn_createMenu.Click += new System.Windows.RoutedEventHandler(this.createMenu_btn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.email_txtbx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RecipeListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 6:
            
            #line 88 "..\..\CreateMenuWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RandomReplace);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 89 "..\..\CreateMenuWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ManualReplace);
            
            #line default
            #line hidden
            return;
            case 8:
            this.IngredientListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 9:
            
            #line 99 "..\..\CreateMenuWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveIngredients);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_saveRecipe = ((System.Windows.Controls.Button)(target));
            
            #line 107 "..\..\CreateMenuWindow.xaml"
            this.btn_saveRecipe.Click += new System.Windows.RoutedEventHandler(this.saveMenu_btn_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btn_emailRecipe = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\CreateMenuWindow.xaml"
            this.btn_emailRecipe.Click += new System.Windows.RoutedEventHandler(this.emailMenu_btn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btn_cancelRecipe = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\CreateMenuWindow.xaml"
            this.btn_cancelRecipe.Click += new System.Windows.RoutedEventHandler(this.cancelRecipe_btn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

