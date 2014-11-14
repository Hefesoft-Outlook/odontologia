using Microsoft.Live;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Auth
{
    public sealed partial class windows : UserControl
    {
        public windows()
        {
            this.InitializeComponent();
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            bool connected = false;
            try
            {
                var authClient = new LiveAuthClient();
                LiveLoginResult result = await authClient.LoginAsync(new string[] { "wl.signin", "wl.skydrive" });

                if (result.Status == LiveConnectSessionStatus.Connected)
                {
                    connected = true;
                    var connectClient = new LiveConnectClient(result.Session);
                    var meResult = await connectClient.GetAsync("me");
                    dynamic meData = meResult.Result;
                    //updateUI(meData);
                }
            }
            catch (LiveAuthException ex)
            {
                // Display an error message.
            }
            catch (LiveConnectException ex)
            {
                // Display an error message.
            }            
        }
    }
}
