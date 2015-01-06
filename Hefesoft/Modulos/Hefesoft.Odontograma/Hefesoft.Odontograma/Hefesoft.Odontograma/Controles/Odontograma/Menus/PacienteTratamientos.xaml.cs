using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight.Messaging;
using Hefesoft.Standard.BusyBox;
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

namespace Hefesoft.Odontograma.Grillas
{
    public sealed partial class PacienteTratamientos : UserControl
    {
        public PacienteTratamientos()
        {
            this.InitializeComponent();
            oirPacienteSeleccionado();
        }

        private void oirPacienteSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Paciente seleccionado", item =>
            {
                var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel>();
                var vmPaciente = ServiceLocator.Current.GetInstance<Hefesoft.Pacientes.Elastic.ViewModel.Pacientes>();
                var vmUsuario = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
                Variables_Globales.IdPacienteHefesoft = vmPaciente.Paciente.RowKey;

                BusyBox.UserControlCargando(true);
                vm.cargarDatosPaciente();
                BusyBox.UserControlCargando(false);
            });
        }
      
    }
}
