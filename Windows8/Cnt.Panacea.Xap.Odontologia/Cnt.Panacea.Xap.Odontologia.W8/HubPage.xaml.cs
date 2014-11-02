using App2.Common;
using App2.Data;
using App2.Util;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Contexto;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=321224

namespace App2
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page, IDisposable
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {

            var contexto = ServiceLocator.Current.GetInstance<IContexto_Odontologia>();
            contexto.binding(new Inicializar_Servicio().CreateCustomBinding());
            contexto.url("net.tcp://192.168.1.250:4520/Cnt.Panacea.Web.Host/Silverlight/Odontologia.OdontologiaServicio.svc");

            EndpointAddress endpointAddress = new EndpointAddress("net.tcp://192.168.1.250:4520/Cnt.Panacea.Web.Host/Silverlight/Odontologia.OdontologiaServicio.svc");
            var cliente = new OdontologiaServicioClient(new Inicializar_Servicio().CreateCustomBinding(), endpointAddress);
            
            //El inspector hay que seguir trabajando en el para guardar los datos en table storage
            //Dinamicamente
            //cliente.Endpoint.EndpointBehaviors.Add(new MyBehavior());

            contexto.servicio(cliente);

            // Metodo para pasarle los parametros al binding
            //Ya que windows 8 no tiene archivos client config de configuracion
            contexto.inicializarContexto();

            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            oirMensaje();
            oirOcupado();
            oirCambiosBotones();
        }

        private async void snap()
        {
            App2.Hub_Partial.Snapshot snap = new Hub_Partial.Snapshot();
            await snap.snapShot(LayoutRoot,"imagenprueba.png");
        }

        private void oirOcupado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Mostrar_Cargando>(this, item => 
            {
                if(item.mostrar_Cargando)
                {
                    GrdBusy.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    TxtBlckCargando.Text = item.texto;
                }
                else
                {
                    GrdBusy.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    TxtBlckCargando.Text = "Cargando....";
                }            
            });
        }

        private void oirMensaje()
        {
            Messenger.Default.Register<Mostrar_Mensaje_Usuario>(this, item =>
            {
                mostrarMensaje(item);
            });
        }

        private async void mostrarMensaje(Mostrar_Mensaje_Usuario item)
        {
            // Create the message dialog and set its content
            var msg = new Windows.UI.Popups.MessageDialog(item.Mensaje);
            // Show the message dialog and wait
            await msg.ShowAsync();
        }
        
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-4");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
        }

        
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
        }

        
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }
        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void BttnGuardar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Guardar_Barra_Comando(), "Inicial");
        }

        private void BttnNuevo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Indicarle a los formularios que deben estar en estado  odontograma inicial
            Variables_Globales.IdTratamientoActivo = 2;
            Variables_Globales.Tipo_Odontograma_Activo = Tipo_Odontograma.Inicial;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Pintar_Datos() { nuevoOdontograma = true });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos() { tipo = Tipo.todos });
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Activar_Elementos() { valor = "Nuevo" });  
        }

        #endregion

        private void oirCambiosBotones()
        {
            // Activa o desactiva botones deacuerdo al estado de la pagina
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento =>
            {
                if (elemento.valor == "Nuevo")
                {
                    nuevoTratamientoUI();
                    finalizaTratamientoBtn.Visibility = Visibility.Collapsed;
                }
                else if (elemento.valor == "Plan Tratamiento")
                {
                    planTratamientoUI();
                    finalizaTratamientoBtn.Visibility = Visibility.Collapsed;
                }
                else if (elemento.valor == "Evolucion")
                {
                    evolucionUI();
                    finalizaTratamientoBtn.Visibility = Visibility.Visible;
                }
                else if (elemento.valor == "Todos")
                {
                    finalizaTratamientoBtn.Visibility = Visibility.Collapsed;
                    activarTodosUI();
                }
            });
        }

        private void inicial_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaInicial();
        }

        private void planTratamiento_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaPlanTratamiento();
        }

        private void evolucion_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaEvolucion();
        }

        private void nuevoTratamientoUI()
        {
            Variables_Globales.IdTratamientoActivo = 0;
            odontogramaEvolucionBtn.IsEnabled = false;
            odontogramaPlanTratamientoBtn.IsEnabled = false;

            snap();
        }

        private void planTratamientoUI()
        {
            odontogramaPlanTratamientoBtn.IsEnabled = true;
            odontogramaEvolucionBtn.IsEnabled = false;
        }

        private void evolucionUI()
        {
            odontogramaPlanTratamientoBtn.IsEnabled = true;
            odontogramaEvolucionBtn.IsEnabled = true;
        }

        private void activarTodosUI()
        {
            odontogramaPlanTratamientoBtn.IsEnabled = true;
            odontogramaEvolucionBtn.IsEnabled = true;
        }

        private void finalizaTratamientoBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.finalizar();
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Mostrar_Mensaje_Usuario>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Mostrar_Cargando>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
        }
    }
}
