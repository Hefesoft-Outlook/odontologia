using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Hefesoft.Pacientes.Controles.Grillas.Pacientes
{
    public sealed partial class SplitPacientes : UserControl, IDisposable
    {

        #region verTitulo (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool verTitulo
        {
            get { return (bool)GetValue(verTituloProperty); }
            set { SetValue(verTituloProperty, value); }
        }
        public static readonly DependencyProperty verTituloProperty =
            DependencyProperty.Register("verTitulo", typeof(bool), typeof(SplitPacientes),
            new PropertyMetadata(true, new PropertyChangedCallback(OnverTituloChanged)));

        private static void OnverTituloChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplitPacientes)d).OnverTituloChanged(e);
        }

        private void OnverTituloChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;

            if (item)
            {
                TxtBlckTitulo.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                TxtBlckTitulo.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        #endregion


        #region verSeleccionado (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool verSeleccionado
        {
            get { return (bool)GetValue(verSeleccionadoProperty); }
            set { SetValue(verSeleccionadoProperty, value); }
        }
        public static readonly DependencyProperty verSeleccionadoProperty =
            DependencyProperty.Register("verSeleccionado", typeof(bool), typeof(SplitPacientes),
            new PropertyMetadata(true, new PropertyChangedCallback(OnverSeleccionadoChanged)));

        private static void OnverSeleccionadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SplitPacientes)d).OnverSeleccionadoChanged(e);
        }

        private void OnverSeleccionadoChanged(DependencyPropertyChangedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Hefesoft.Pacientes.Elastic.ViewModel.Pacientes>();
            var item = (bool)e.NewValue;
            vm.VerSeleccionar = item;
        }

        

        #endregion
        
    }
}
