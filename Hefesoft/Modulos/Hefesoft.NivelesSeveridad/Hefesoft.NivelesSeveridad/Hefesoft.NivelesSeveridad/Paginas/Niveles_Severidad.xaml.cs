using Hefesoft.Util.W8.UI.Common;
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

namespace Hefesoft.NivelesSeveridad.Paginas
{
    public sealed partial class Niveles_Severidad : Page
    {     
        public Niveles_Severidad()
        {
            this.InitializeComponent();
            addBusy();
            NavigationHelper = new Util.W8.UI.Common.NavigationHelper();
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
            var busy = ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            var elemento = Hefesoft.Util.W8.UI.Assets.BusyBox.Busy.addBusy(busy);
            Grid.SetRowSpan(elemento, 2);
            LayoutRoot.Children.Add(elemento);
        }
    }
}
