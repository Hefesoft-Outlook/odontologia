using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
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
    }
}
