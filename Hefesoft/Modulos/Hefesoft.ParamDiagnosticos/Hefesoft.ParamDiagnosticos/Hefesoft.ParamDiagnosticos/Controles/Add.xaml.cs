using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Globalization;
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


namespace Hefesoft.ParamDiagnosticos.Controles
{
    public sealed partial class Add : UserControl
    {
        public Add()
        {
            this.InitializeComponent();
            vm = ServiceLocator.Current.GetInstance<Hefesoft.ParamDiagnosticos.Elastic.Diagnosticos_Procedimientos>();
            Color.colorChanged += Color_colorChanged;
        }

        void Color_colorChanged(object sender, EventArgs e)
        {
            byte[] bytes = { Color.SelectedColor.A, Color.SelectedColor.B, Color.SelectedColor.G, Color.SelectedColor.R };
            var color1 = Hefesoft.Standard.Util.Common.Colors.byteArray2Int(bytes);

            vm.DiagnosticoProcedimiento.ColorAdicional = color1;
            vm.DiagnosticoProcedimiento.Color = color1;
            var tColor1 =  ColorEntero(color1);
            vm.forceUiDiagnosticoProcedimiento();
        }

        

        internal static Windows.UI.Color ColorEntero(int color)
        {
            string str = color.ToString("X", CultureInfo.InvariantCulture);
            if (str.Length < 6)
            {
                str = str.PadLeft(6, '0');
            }
            byte num = Convert.ToByte(str.Substring(0, 2), 16);
            byte num1 = Convert.ToByte(str.Substring(2, 2), 16);
            byte num2 = Convert.ToByte(str.Substring(4, 2), 16);
            return Windows.UI.Color.FromArgb(255, num, num1, num2);
        }

        public Elastic.Diagnosticos_Procedimientos vm { get; set; }
    }
}
