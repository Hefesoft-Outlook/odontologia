using Cnt.Panacea.Xap.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Hefesoft.Odontograma.Grillas.Evolucion
{
    public sealed partial class SplitEvolucion : UserControl, IDisposable
    {
        public SplitEvolucion()
        {          
            InitializeComponent();
            cargarElementos();            
        }

        private async void cargarElementos()
        {           
            vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
            oirEventosViewModel();
            await vm.InicializarEvolucion();            
        }

        private void oirEventosViewModel()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Evolucion Elemento seleccionado", elemento => 
            {
                if (itemListView.ItemsSource != null)
                {
                    itemListView.SelectedIndex = 0;
                }
            });

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Evolucion listado sesiones cargado", elemento => 
            {
                //Hay algun error en los binding y toco configurarlo por behind
                CmbxSesiones.ItemsSource = vm.NumeroSesionesConfiguradas;
                CmbxSesiones.SelectedIndex = 0;
            });            
        }

     

        private void BttnGuardar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { }, "Evolucion");
        }

        private void realizado_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ch = sender as CheckBox;         
            var item = ch.DataContext as ProcedimientosGrillaEvolucion;
            vm.procedimientoRealizado(item);
        }


        public Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion vm { get; set; }


        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<string>(this, "Evolucion Elemento seleccionado");
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<string>(this, "Evolucion listado sesiones cargado");
        }
    }
}
