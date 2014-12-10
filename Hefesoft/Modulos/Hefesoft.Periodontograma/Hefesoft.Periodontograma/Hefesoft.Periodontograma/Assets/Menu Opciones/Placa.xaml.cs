using Hefesoft.Periodontograma.Elastic.Entidades;
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

namespace Hefesoft.Periodontograma.Assets.Menu_Opciones
{
    public sealed partial class Placa : UserControl
    {
        public Placa()
        {
            this.InitializeComponent();
        }

        private void placaElemento1_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as Hefesoft.Periodontograma.Assets.Placa.PlacaElemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma>();
                vm.placaCommand.Execute(datacontext);
            }
        }

        private void placaElemento2_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as Hefesoft.Periodontograma.Assets.Placa.PlacaElemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma>();
                vm.placaCommand2.Execute(datacontext);
            }
        }

        private void placaElemento3_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            var item = sender as Hefesoft.Periodontograma.Assets.Placa.PlacaElemento;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Periodontograma.Elastic.ViewModel.Periodontograma>();
                vm.placaCommand3.Execute(datacontext);
            }
        }
    }
}
