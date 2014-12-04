using App2.Common;
using App2.Intefaz;
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


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2.Assets.Periodontograma
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Periodontograma : Page, IBusyBox
    {

        public Periodontograma()
        {
            this.InitializeComponent();            
            addBusy();
            NavigationHelper = ServiceLocator.Current.GetInstance<App2.Common.NavigationHelper>();
            NavigationHelper.setPage(this);
        }

        public NavigationHelper NavigationHelper { get; set; }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            removeBusyFromVisualThree(e);
        }

        private void removeBusyFromVisualThree(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            UIElement item = LayoutRoot.Children.LastOrDefault();
            LayoutRoot.Children.Remove(item);
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
            vm.save();
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
            vm.newCommand.Execute(null);
        }

        public void addBusy()
        {
            var elemento = App2.Util.Busy.Busy.addBusy();
            Grid.SetRowSpan(elemento, 2);
            LayoutRoot.Children.Add(elemento);
        }

       
    }
}
