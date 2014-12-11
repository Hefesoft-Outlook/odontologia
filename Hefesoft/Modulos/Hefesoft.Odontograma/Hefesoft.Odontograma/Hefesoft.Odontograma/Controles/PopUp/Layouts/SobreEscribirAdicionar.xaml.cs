using Cnt.Panacea.Xap.Odontologia.Vm.Util.PopUp;
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

namespace Hefesoft.Odontograma.Assets.PopUp.Layouts
{
    public sealed partial class SobreEscribirAdicionar : UserControl
    {
        public EstadoSobreEscribirAdicionar EstadoSobreEscribirAdicionar { get; set; }
        public Action<EstadoSobreEscribirAdicionar> Cerrar { get; set; }
        public SobreEscribirAdicionar()
        {
            this.InitializeComponent();
            EstadoSobreEscribirAdicionar = new EstadoSobreEscribirAdicionar();            
        }

        private void limpiar()
        {
            EstadoSobreEscribirAdicionar = null;
            EstadoSobreEscribirAdicionar = new EstadoSobreEscribirAdicionar();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            EstadoSobreEscribirAdicionar.Cancelar = true;
            Cerrar(EstadoSobreEscribirAdicionar);
        }        

        private void Sobreescribir_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            EstadoSobreEscribirAdicionar.SobreEscribir = true;
            Cerrar(EstadoSobreEscribirAdicionar);
        }

        private void Adicionar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
            EstadoSobreEscribirAdicionar.Adicionar = true;
            Cerrar(EstadoSobreEscribirAdicionar);
        }
    }
}
