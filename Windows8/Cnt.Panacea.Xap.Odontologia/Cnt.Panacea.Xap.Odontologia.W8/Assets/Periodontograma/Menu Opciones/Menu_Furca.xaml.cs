using Hefesoft.Odontologia.Periodontograma.Entidades;
using Hefesoft.Odontologia.Periodontograma.Enumeradores;
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

namespace App2.Assets.Periodontograma.Menu_Opciones
{
    public sealed partial class Menu_Furca : UserControl
    {
        public Menu_Furca()
        {
            this.InitializeComponent();
        }


        #region UnoODosElementos (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Furca_Visualizacion UnoODosElementos
        {
            get { return (Furca_Visualizacion)GetValue(UnoODosElementosProperty); }
            set { SetValue(UnoODosElementosProperty, value); }
        }
        public static readonly DependencyProperty UnoODosElementosProperty =
            DependencyProperty.Register("UnoODosElementos", typeof(Furca_Visualizacion), typeof(Menu_Furca),
            new PropertyMetadata(Furca_Visualizacion.No_Visible, new PropertyChangedCallback(OnUnoODosElementosChanged)));

        private static void OnUnoODosElementosChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Menu_Furca)d).OnUnoODosElementosChanged(e);
        }

        private void OnUnoODosElementosChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Furca_Visualizacion)e.NewValue;

            if(item == Furca_Visualizacion.No_Visible)
            {
                this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else if (item == Furca_Visualizacion.Visible_Un_Elemento)
            {
                this.Visibility = Windows.UI.Xaml.Visibility.Visible;
                VisualStateManager.GoToState(this, "VisualState", true);
            }
            else if (item == Furca_Visualizacion.Visible_Dos_Elementos)
            {
                this.Visibility = Windows.UI.Xaml.Visibility.Visible;
                VisualStateManager.GoToState(this, "VisualStateDosElementos", true);
            }
        }

        

        #endregion     

        private void furca1_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var item = sender as App2.Assets.Periodontograma.Furca.Furca;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
                vm.furcaCommand.Execute(datacontext);
            }
        }

        private void furca2_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var item = sender as App2.Assets.Periodontograma.Furca.Furca;
            var datacontext = item.DataContext as PeriodontogramaEntity;

            if (datacontext != null)
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Periodontograma.ViewModel.Periodontograma>();
                vm.furcaCommand2.Execute(datacontext);
            }
        }
        
    }
}
