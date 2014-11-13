using App2.Common;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util;
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

namespace App2.Grillas.Evolucion
{
    public sealed partial class SplitEvolucion : UserControl
    {
        public SplitEvolucion()
        {
            this.InitializeComponent();
            vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
        }

        internal void inicializar()
        {            
            vm.InicializarEvolucion();
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
    }
}
