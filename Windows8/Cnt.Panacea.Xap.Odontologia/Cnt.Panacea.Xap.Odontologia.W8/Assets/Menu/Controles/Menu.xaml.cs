using App2.Common;
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

namespace App2.Assets.Menu.Controles
{
    public sealed partial class Menu : UserControl, IDisposable
    {
        public Menu()
        {          
            InitializeComponent();
        }

        public void Dispose()
        {

        }

        private void ingresar_Click(object sender, RoutedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("Click", "Menu");
            
        }
    }
}
