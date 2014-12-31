using GalaSoft.MvvmLight.Messaging;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Microsoft.Practices.ServiceLocation;

namespace Hefesoft.Odontograma.Assets.PopUp
{
    public sealed partial class Modal : UserControl, IDisposable
    {
        public Modal()
        {            
            this.InitializeComponent();            
            oirMostrarVentana();
            oirCerraVentana();            

            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void generarVentana(Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            if (obj.Nombre == "Plan tratamiento")
            {
                elementoOtraVentana = obj;
                var wizard = new Hefesoft.Odontograma.Grillas.Plan_tratamiento.UserControlGuardarPlanTratamiento() {HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch};
                var vCerrada = MostrarModal(wizard, "Plan de tratamiento");
                vCerrada = cerrar;
            }
            else if (obj.Nombre == "Evolucion")
            {
                elementoOtraVentana = obj;
                var wizard = new Hefesoft.Odontograma.Grillas.Evolucion.SplitEvolucion() { HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch };
                var vCerrada = MostrarModal(wizard, "Evolucion");
                vCerrada = cerrar;
            }
            else if (obj.Nombre == "Tratamientos")
            {
                elementoOtraVentana = obj;
                var reporte = new Hefesoft.Odontograma.Util.Reportes.Templates.Plan_Tratamiento();
                MostrarModal(reporte, "Reporte plan de tratamiento");                
            }
            else if (obj.Nombre == "Listado imagenes")
            {
                elementoOtraVentana = obj;
                var reporte = new Hefesoft.Odontograma.Fotos.SplitFotos() { DataContext = obj.Propiedad_Adicional };
                MostrarModal(reporte, "Imagenes");
            }
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp.Cerrar_Pop_Up_Generico>(this);
        }

        
    }
}
