using Hefesoft.Util.W8.UI.Common;
using Microsoft.Practices.ServiceLocation;
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

namespace Hefesoft.Pacientes.Controles
{    
    public sealed partial class Pacientes : Page, IDisposable
    {
        public Pacientes()
        {
            this.InitializeComponent();
            addBusy();
            oirPacienteSeleccionado();
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

        private void oirPacienteSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Entidades.Usuario>(this, "Paciente seleccionado", item => 
            {
                
            });
        }

        private void odontograma_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public void addBusy()
        {
            var busy = ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
            var elemento = Hefesoft.Util.W8.UI.Assets.BusyBox.Busy.addBusy(busy);
            Grid.SetRowSpan(elemento, 2);
            LayoutRoot.Children.Add(elemento);
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Usuario.Entidades.Usuario>(this, "Paciente seleccionado");
        }        
    }
}
