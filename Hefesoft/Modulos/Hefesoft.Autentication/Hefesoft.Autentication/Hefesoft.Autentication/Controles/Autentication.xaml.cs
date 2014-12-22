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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hefesoft.Autentication.Controles
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Autentication : Page, IDisposable
    {

        public event EventHandler usuarioIngreso;

        public void onUsuarioIngreso(object sender, EventArgs e)
        {
            if (usuarioIngreso != null)
            {
                usuarioIngreso(sender, e);
            }
        }

        public static bool ingresoInicial = false;
        public Autentication()
        {
            this.InitializeComponent();
            Loaded += Autentication_Loaded; 
            NavigationHelper = new NavigationHelper();
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

        void Autentication_Loaded(object sender, RoutedEventArgs e)
        {
            if (!ingresoInicial)
            {
                ingresoInicial = true;
                login();
            }
        }               

        internal void login()
        {            
            var usuario = Hefesoft.Autentication.Util.Storage.Usuario.obtenerUsuario();

            if (!string.IsNullOrEmpty(usuario.id))
            {
                var UsuarioVm = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
                UsuarioVm.UsuarioActivo = usuario;
                onUsuarioIngreso(UsuarioVm.UsuarioActivo, EventArgs.Empty);
                usuarioIngresoMessage(UsuarioVm.UsuarioActivo);
            }

            oirUsuarioCreado();
        }

        private void oirUsuarioCreado()
        {
            //Ocurre cuando el usuario a sido creado
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Messenger.Usuario_Cargado>(this, item =>
            {
                var valorUsuario = item.Usuario;
                Hefesoft.Autentication.Util.Storage.Usuario.guardarUsuario(item.Usuario);
                onUsuarioIngreso(item.Usuario, EventArgs.Empty);
                usuarioIngresoMessage(item.Usuario);
            });
        }

        public void addBusy()
        {
            var busy = ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            var elemento = Hefesoft.Util.W8.UI.Assets.BusyBox.Busy.addBusy(busy);
            Grid.SetRowSpan(elemento, 2);
            LayoutRoot.Children.Add(elemento);
        }

        private void usuarioIngresoMessage(Hefesoft.Usuario.Entidades.IUsuario usuario)
        {            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("Usuario logueado", "Usuario logueado");
        }

        public void Dispose()
        {
            usuarioIngreso = null;
        }

        private void menu_Click(object sender, RoutedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("Usuario logueado", "Usuario logueado");
        }
    }
}
