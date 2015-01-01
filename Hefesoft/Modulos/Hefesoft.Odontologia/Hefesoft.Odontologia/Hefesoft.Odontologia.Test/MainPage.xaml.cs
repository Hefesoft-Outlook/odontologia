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
            navegacion();


            Hefesoft.Util.W8.UI.Util.RegisterElement.registrarClaseUI<Hefesoft.Util.W8.UI.PopUp.Modal>();
            var modal = ServiceLocator.Current.GetInstance<Hefesoft.Util.W8.UI.PopUp.Modal>();
            this.LayoutRoot.Children.Add(modal);

        }

     

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {  
            this.Host.Navigate(typeof(Hefesoft.Autentication.Controles.Autentication));
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<string>(this, "Usuario logueado");
        }
    }
}
