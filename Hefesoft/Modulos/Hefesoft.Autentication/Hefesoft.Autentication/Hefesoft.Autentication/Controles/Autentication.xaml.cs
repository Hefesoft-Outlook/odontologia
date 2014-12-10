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
    public sealed partial class Autentication : Page
    {
        public static bool ingresoInicial = false;
        public Autentication()
        {
            this.InitializeComponent();            
            Loaded += Autentication_Loaded;
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
                //this.Frame.Navigate(typeof(Assets.Menu.Menu));
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
                //this.Frame.Navigate(typeof(Assets.Menu.Menu));
            });
        }
    }
}
