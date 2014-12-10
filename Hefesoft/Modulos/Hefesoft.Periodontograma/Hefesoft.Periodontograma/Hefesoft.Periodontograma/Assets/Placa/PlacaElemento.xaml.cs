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

namespace Hefesoft.Periodontograma.Assets.Placa
{
    public sealed partial class PlacaElemento : UserControl
    {
        public PlacaElemento()
        {
            this.InitializeComponent();
        }


        #region Placa (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Hefesoft.Periodontograma.Elastic.Enumeradores.Placa Placa
        {
            get { return (Hefesoft.Periodontograma.Elastic.Enumeradores.Placa)GetValue(PlacaProperty); }
            set { SetValue(PlacaProperty, value); }
        }
        public static readonly DependencyProperty PlacaProperty =
            DependencyProperty.Register("Placa", typeof(Hefesoft.Periodontograma.Elastic.Enumeradores.Placa), typeof(PlacaElemento),
            new PropertyMetadata(Hefesoft.Periodontograma.Elastic.Enumeradores.Placa.ninguno, new PropertyChangedCallback(OnPlacaChanged)));

        private static void OnPlacaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PlacaElemento)d).OnPlacaChanged(e);
        }

        private void OnPlacaChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Hefesoft.Periodontograma.Elastic.Enumeradores.Placa)e.NewValue;

            if(item == Hefesoft.Periodontograma.Elastic.Enumeradores.Placa.ninguno)
            {
                VisualStateManager.GoToState(this, "VisualState", true);
            }
            else if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Placa.blue)
            {
                VisualStateManager.GoToState(this, "VisualStateBlue", true);
            }
        }

        #endregion
        
    }
}
