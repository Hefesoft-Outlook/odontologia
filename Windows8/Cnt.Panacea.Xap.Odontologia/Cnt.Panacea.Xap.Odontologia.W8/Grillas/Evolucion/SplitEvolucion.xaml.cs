﻿using App2.Common;
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
        }

        internal void inicializar()
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
            vm.InicializarEvolucion();
        }

        private void BttnGuardar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { }, "Evolucion");
        }        
    }
}
