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
    public sealed partial class Periodontograma : Page
    {

        public Periodontograma()
        {
            this.InitializeComponent();        
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
            vm.save();
        }    
    }
}
