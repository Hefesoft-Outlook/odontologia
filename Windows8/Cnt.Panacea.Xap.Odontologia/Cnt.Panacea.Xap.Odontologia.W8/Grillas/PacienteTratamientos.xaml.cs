using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
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

// La plantilla de elemento Control de usuario está documentada en http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Grillas
{
    public sealed partial class PacienteTratamientos : UserControl
    {
        public PacienteTratamientos()
        {
            this.InitializeComponent();
        }

        private void HyprlnkBttnSeleccionar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HyperlinkButton hp = sender as HyperlinkButton;
            var item = hp.DataContext as TratamientoEntity;
            Variables_Globales.IdTratamientoActivo = item.Identificador;
            var PacienteViewModel = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel>();
            PacienteViewModel.seleccionarTratamiento(item);
            Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
        }
    }
}
