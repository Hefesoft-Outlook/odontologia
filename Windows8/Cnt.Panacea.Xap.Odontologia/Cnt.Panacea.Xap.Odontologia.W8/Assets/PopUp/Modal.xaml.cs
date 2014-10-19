using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App2.Assets.PopUp
{
    public sealed partial class Modal : UserControl, IDisposable
    {
        public Modal()
        {            
            this.InitializeComponent();
            oirMostrarVentana();
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            this.Titulo.Margin = new Thickness(20, 20, 20, 0);
            this.LayoutRoot.Margin = new Thickness(20, 0, 20, 20);
        }

        private void oirMostrarVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this, generarVentana);
        }

        private void generarVentana(Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            if (obj.Nombre == "Plan tratamiento")
            {
                var wizard = new App2.Grillas.Plan_tratamiento.UserControlGuardarPlanTratamiento() {HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch};
                MostrarModal(wizard);
            }
        }

        private void MostrarModal(UIElement elementoMostrar)
        {
            
            LayoutRoot.Children.Clear();
            LayoutRoot.Children.Add(elementoMostrar);
            this.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void ocultarModal()
        {
            LayoutRoot.Children.Clear();            
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }


        #region padre (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public FrameworkElement padre
        {
            get { return (FrameworkElement)GetValue(padreProperty); }
            set { SetValue(padreProperty, value); }
        }
        public static readonly DependencyProperty padreProperty =
            DependencyProperty.Register("padre", typeof(FrameworkElement), typeof(Modal),
            new PropertyMetadata(null, new PropertyChangedCallback(OnpadreChanged)));

        private static void OnpadreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Modal)d).OnpadreChanged(e);
        }

        private void OnpadreChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        #endregion


        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this);
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ocultarModal();
        }
    }
}
