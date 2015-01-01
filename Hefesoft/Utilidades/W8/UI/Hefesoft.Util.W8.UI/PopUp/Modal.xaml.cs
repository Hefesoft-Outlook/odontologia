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

namespace Hefesoft.Util.W8.UI.PopUp
{
    public sealed partial class Modal : UserControl
    {
        public Modal()
        {
            this.InitializeComponent();
        }

        public Action<object> MostrarModal(UIElement elementoMostrar, string titulo = "")
        {
            var bounds = Window.Current.Bounds;
            this.Visibility = Windows.UI.Xaml.Visibility.Visible;
            Dialog.Width = bounds.Width;
            Dialog.Title = titulo;
            Dialog.Content = elementoMostrar;
            Dialog.HorizontalContentAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            Dialog.IsOpen = true;
            return ventanaCerrada;
        }

        public void ocultarModal(bool dialogResult)
        {
            Dialog.Title = "";
            Dialog.Content = null;
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Dialog.IsOpen = false;

            if (dialogResult)
            {
                //Callback
                //ventanaCerrada = cerrar;
                ventanaCerrada(null);
            }
        }

        public Action<object> ventanaCerrada { get; set; }
    }
}
