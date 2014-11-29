using Hefesoft.Odontologia.Periodontograma.Entidades;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Assets.Periodontograma.Menu_Opciones
{
    public sealed partial class Sangrado_Supuracion : UserControl
    {
        public Sangrado_Supuracion()
        {
            this.InitializeComponent();
        }

        private void sangradoSupuracion1_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as App2.Assets.Periodontograma.Sangrado_Supuracion.Sangrado_Supuracion_Elemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;
            
            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
                vm.sangradoSupuracionCommand.Execute(datacontext);
            }
        }

        private void sangradoSupuracion2_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as App2.Assets.Periodontograma.Sangrado_Supuracion.Sangrado_Supuracion_Elemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
                vm.sangradoSupuracionCommand2.Execute(datacontext);
            }
        }

        private void sangradoSupuracion3_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as App2.Assets.Periodontograma.Sangrado_Supuracion.Sangrado_Supuracion_Elemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
                vm.sangradoSupuracionCommand3.Execute(datacontext);
            }
        }
    }
}
