using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight.Messaging;
using Hefesoft.Entities.Odontologia.Odontograma;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public sealed partial class HubPage : Page, IDisposable
    {

        private void BttnGuardar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Inicial)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Guardar_Barra_Comando(), "Inicial");
            }
            else if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Plan_Tratamiento)
            {
                var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
                vm.pedirDatosGrilla();
            }
            else if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Evolucion)
            {
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { }, "Evolucion");
            }
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
            Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
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

        private void finalizaTratamientoBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.finalizar();
        }

        private void deshacer_Click(object sender, RoutedEventArgs e)
        {
            AppBarToggleButton appBtn = sender as AppBarToggleButton;

            if (desHacerBool)
            {
                desHacerBool = false;
                appBtn.IsChecked = true;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Estado_DesHacer() { Estado = true });
            }
            else
            {
                desHacerBool = true;
                appBtn.IsChecked = false;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Estado_DesHacer() { Estado = false });
            }
        }

        private async void adjuntar_Click(object sender, RoutedEventArgs e)
        {
            await adjuntarImagenes(sender);
        }

        private async void foto_Click(object sender, RoutedEventArgs e)
        {
            await tomarFoto(sender);
        }

        private void verImagenes_Click(object sender, RoutedEventArgs e)
        {
            var vmMapaDental = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
            vmMapaDental.verImagenes();
        }

        private void adicionarPaciente_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
