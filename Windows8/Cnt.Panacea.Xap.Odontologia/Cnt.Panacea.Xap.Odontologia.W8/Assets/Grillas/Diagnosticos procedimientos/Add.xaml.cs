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

namespace App2.Assets.Grillas.Diagnosticos_procedimientos
{
    public sealed partial class Add : UserControl
    {
        public Add()
        {
            this.InitializeComponent();
            vm = ServiceLocator.Current.GetInstance<Hefesoft.Entities.Odontologia.ViewModel.Diagnosticos_Procedimientos.Diagnosticos_Procedimientos>();
            Color.colorChanged += Color_colorChanged;
        }

        void Color_colorChanged(object sender, EventArgs e)
        {
            byte[] bytes = { Color.SelectedColor.A, Color.SelectedColor.B, Color.SelectedColor.G, Color.SelectedColor.R };
            var color1 = Hefesoft.Standard.Util.Common.Colors.byteArray2Int(bytes);

            vm.DiagnosticoProcedimiento.ColorAdicional = color1;
            vm.DiagnosticoProcedimiento.Color = color1;
            var tColor1 =  Colores.ColorEntero(color1);
            vm.forceUiDiagnosticoProcedimiento();
        }

        public Hefesoft.Entities.Odontologia.ViewModel.Diagnosticos_Procedimientos.Diagnosticos_Procedimientos vm { get; set; }
    }
}
