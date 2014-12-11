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

namespace Hefesoft.Odontograma.Grillas.Plan_tratamiento
{
    public sealed partial class Forma_Pago : UserControl
    {
        public Forma_Pago()
        {
            this.InitializeComponent();
            vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
            vm.inicializar();
        }

        private void BttnGuardar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //Por ioc se trae el viewmodel que se encarga de formatear los datos
            vm.pedirDatosGrilla();
        }

        public Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento vm { get; set; }
    }
}
