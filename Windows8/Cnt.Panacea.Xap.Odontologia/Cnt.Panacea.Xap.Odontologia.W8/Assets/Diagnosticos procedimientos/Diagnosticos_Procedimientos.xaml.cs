using App2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App2.Assets.Diagnosticos_procedimientos
{
    
    public sealed partial class Diagnosticos_Procedimientos : Page
    {
        public Diagnosticos_Procedimientos()
        {
            this.InitializeComponent(); 
            this.navigationHelper = new NavigationHelper(this);
        }

        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
    }
}
