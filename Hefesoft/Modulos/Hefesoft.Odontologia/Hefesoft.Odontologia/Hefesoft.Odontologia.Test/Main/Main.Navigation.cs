using Hefesoft.Standard.BusyBox;
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


namespace Hefesoft.Odontologia.Test
{    
    public sealed partial class MainPage : Page
    {
        private void navegacion()
        {
            //addBusy();
            //NavigationHelper = new Util.W8.UI.Common.NavigationHelper();
            //NavigationHelper.Mode = Modo.Frame;
            //NavigationHelper.FrameNavigation = this.Host;
            //Host.Navigating += Host_Navigating;
            //Host.Navigated += Host_Navigated;
        }      

        void Host_Navigated(object sender, NavigationEventArgs e)
        {
            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(false);
        }

        void Host_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(true);
        }

        public NavigationHelper NavigationHelper { get; set; }

        public void addBusy()
        {
            var busy = ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            var busyBox = Hefesoft.Util.W8.UI.Assets.BusyBox.Busy.addBusy(busy);
            Grid.SetRow(busyBox, 1);
            LayoutRoot.Children.Add(busyBox);

            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(true, "Prueba");
        }
    }
}
