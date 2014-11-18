using Microsoft.Live;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Auth
{
    public sealed partial class windows : UserControl, IDisposable
    {
        public windows()
        {
            this.InitializeComponent();
            UserVm = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
            oirAutologin();
        }

        private void oirAutologin()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Messenger.Login>(this, "Outlook", ingreso);
        }

        private async void ingreso(Hefesoft.Usuario.Messenger.Login obj)
        {
            var resultado = await logIn();
            obj.Resultado(resultado);
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            var resultado = await logIn();
            Busy.UserControlCargando(false);
        }

        private async Task<dynamic> logIn()
        {            
            try
            {
                Busy.UserControlCargando(true, "Ingresando");
                var authClient = new LiveAuthClient();
                LiveLoginResult result = await authClient.LoginAsync(new string[] { "wl.signin", "wl.skydrive" });

                if (result.Status == LiveConnectSessionStatus.Connected)
                {         
                    var connectClient = new LiveConnectClient(result.Session);
                    var meResult = await connectClient.GetAsync("me");
                    dynamic meData = meResult.Result;

                    var photoResult = await connectClient.GetAsync("me/picture");
                    dynamic photoResultdyn = photoResult.Result;
                    string profileUrl = photoResultdyn.location;
                    
                    //updateUI(meData);
                    ApplicationData.Current.LocalSettings.Values["login"] = "Outlook";
                    crearUsuario(meData, profileUrl);
                    return meData;
                }
                else
                {
                    return null;
                }
            }
            catch (LiveAuthException ex)
            {
                // Display an error message.
                return null;
            }
            catch (LiveConnectException ex)
            {
                // Display an error message.
                return null;
            }
        }

        private async Task crearUsuario(dynamic result, string profilePictureUrl)
        {
            Hefesoft.Usuario.Entidades.Outlook outlook = new Hefesoft.Usuario.Entidades.Outlook();
            outlook.nombre = result.name;
            outlook.imagenRuta = profilePictureUrl;
            outlook.id = result.id;
            outlook.RowKey = result.id;
            await UserVm.insert(outlook);

            var usuario = Hefesoft.Standard.Util.Convertir_Entidades.Convertir_Entidades.ConvertirEntidades(outlook, new Hefesoft.Usuario.Entidades.Usuario());
            UserVm.UsuarioActivo = usuario;
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Usuario.Messenger.Login>(this, "Outlook");
        }

        public Hefesoft.Usuario.ViewModel.Usuarios UserVm { get; set; }
    }
}
