using App2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace App2.Pacientes
{    
    public sealed partial class Pacientes : Page, IDisposable
    {
        public Pacientes()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            oirPacienteSeleccionado();
        }

        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private void oirPacienteSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Entidades.Usuario>(this, "Paciente seleccionado", item => 
            {
                this.Frame.Navigate(typeof(HubPage),item);
            });
        }


        private void tratamientos_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HubPage));
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Usuario.Entidades.Usuario>(this, "Paciente seleccionado");
        }
    }
}
