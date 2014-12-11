using App2.Util;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Contexto;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight.Messaging;
using Hefesoft.Util.W8.UI.Common;
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
            oirMensaje();
            oirOcupado();
            oirCambiosBotones(); 
            NavigationHelper = new Hefesoft.Util.W8.UI.Common.NavigationHelper();
            NavigationHelper.setPage(this);
        }

        public NavigationHelper NavigationHelper { get; set; }

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

        public void Dispose()
        {
            Messenger.Default.Unregister<Mostrar_Mensaje_Usuario>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Mostrar_Cargando>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Usuario.Messenger.Usuario_Cargado>(this);
        }
        bool desHacerBool;        
    }
}
