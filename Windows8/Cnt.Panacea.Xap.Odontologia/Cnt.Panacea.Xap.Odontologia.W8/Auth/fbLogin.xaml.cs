using Facebook;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Auth
{
    public sealed partial class fbLogin : UserControl, IDisposable
    {
        public fbLogin()
        {
            oirAutologin();
            this.InitializeComponent();            
        }

        private void oirAutologin()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Messenger.Login>(this, "Facebook", ingreso);
        }

        private async void ingreso(Hefesoft.Usuario.Messenger.Login obj)
        {
            var result = await logIn();
            obj.Resultado(result);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await logIn();
            Busy.UserControlCargando(false);
        }

        private async Task<dynamic> logIn()
        {            
            App.FacebookId = "734443959971377";

            FacebookClient _fb = new FacebookClient();
            var loginUrl = _fb.GetLoginUrl(new
            {
                client_id = "734443959971377",
                redirect_uri = Windows.Security.Authentication.Web.WebAuthenticationBroker.GetCurrentApplicationCallbackUri().AbsoluteUri,
                scope = "public_profile",
                display = "popup",
                response_type = "token"
            });

            Busy.UserControlCargando(true, "Ingresando");
            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                  WebAuthenticationOptions.None,
                  loginUrl);

            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var callbackUri = new Uri(WebAuthenticationResult.ResponseData.ToString());
                var facebookOAuthResult = _fb.ParseOAuthCallbackUrl(callbackUri);

                // Retrieve the Access Token. You can now interact with Facebook on behalf of the user
                // using the Access Token.
                var accessToken = facebookOAuthResult.AccessToken;
                App.Token = accessToken;
                return await LoadUserInfo(accessToken);
            }
            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {
                // handle authentication failure
                return null;
            }
            else
            {
                // The user canceled the authentication
                return null;
            }
        }

        private async Task<dynamic> LoadUserInfo(string token)
        {
            FacebookClient fb = new FacebookClient(token);

            dynamic parameters = new ExpandoObject();
            parameters.access_token = token;
            parameters.fields = "name";

            dynamic result = await fb.GetTaskAsync("me", parameters);

            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", App.FacebookId, "large", fb.AccessToken);

            this.Imagen.Source = new BitmapImage(new Uri(profilePictureUrl));
            this.Nombre.Text = result.name;

            ApplicationData.Current.LocalSettings.Values["login"] = "Facebook";
            return result;
        }

        public async void getInformation(string token) 
        {
            FacebookClient fb = new FacebookClient(token);

            dynamic friendsTaskResult = await fb.GetTaskAsync("/me/friends");
            var result = (IDictionary<string, object>)friendsTaskResult;
            var data = (IEnumerable<object>)result["data"];
            foreach (var item in data)
            {
                var friend = (IDictionary<string, object>)item;
                // now you can retrieve data from the dictionary above
                string friendName = (string)friend["name"];
                string friendFacebookId = (string)friend["id"];
            }
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Usuario.Messenger.Login>(this, "Facebook");
        }
    }
}
