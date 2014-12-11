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
    public sealed partial class Agregar_Supernumerario : UserControl
    {
        public Action<bool> Cerrar { get; set; }
        public Agregar_Supernumerario()
        {
            this.InitializeComponent();
            hyprlnkBttnIzquierda.Click += hyprlnkBttn_Click;
            hyprlnkBttnDerecha.Click += hyprlnkBttn_Click;
            HyprlnkBttnEliminar.Click += hyprlnkBttn_Click;
        }

        void hyprlnkBttn_Click(object sender, RoutedEventArgs e)
        {
            Cerrar(true);
        }
    }
}
