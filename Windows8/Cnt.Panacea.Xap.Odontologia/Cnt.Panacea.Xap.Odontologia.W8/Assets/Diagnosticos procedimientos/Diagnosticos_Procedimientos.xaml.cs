using App2.Common;
using Microsoft.Practices.ServiceLocation;
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
            addBusy();
            NavigationHelper = ServiceLocator.Current.GetInstance<App2.Common.NavigationHelper>();
            NavigationHelper.setPage(this);
        }

        public NavigationHelper NavigationHelper { get; set; }     

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            removeBusyFromVisualThree(e);
        }

        private void removeBusyFromVisualThree(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            UIElement item = LayoutRoot.Children.LastOrDefault();
            LayoutRoot.Children.Remove(item);
        }

        public void addBusy()
        {
            var elemento = App2.Util.Busy.Busy.addBusy();
            Grid.SetRowSpan(elemento, 2);
            LayoutRoot.Children.Add(elemento);
        }
    }
}
