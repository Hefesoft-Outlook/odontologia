using Hefesoft.Util.W8.UI.Common;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hefesoft.Odontologia.Test
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IDisposable
    {
        public MainPage()
        {
            //El modulo de autenticacion dispara este evento cuando el usuario se loguea
            oirUsuarioIngreso();
            oirCambioUsuario();
            oirMenuSeleccionado();
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
            
            addBusy();
            NavigationHelper = new Util.W8.UI.Common.NavigationHelper();
            NavigationHelper.setPage(this);            
            Host.Navigating += Host_Navigating;
            Host.Navigated += Host_Navigated;
            backButtonHost.Command = NavigationHelper.GoBackCommand;

            
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

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {  
            this.Host.Navigate(typeof(Hefesoft.Autentication.Controles.Autentication));
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<string>(this, "Usuario logueado");
        }

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
