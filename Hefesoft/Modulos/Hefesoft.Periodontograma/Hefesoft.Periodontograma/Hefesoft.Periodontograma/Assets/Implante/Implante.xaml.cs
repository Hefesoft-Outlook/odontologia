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

namespace Hefesoft.Periodontograma.Assets.Implante
{
    public sealed partial class Implante : UserControl
    {
        public Implante()
        {
            this.InitializeComponent();
        }



        #region ImplanteElemento (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Hefesoft.Periodontograma.Elastic.Enumeradores.Implante ImplanteElemento
        {
            get { return (Hefesoft.Periodontograma.Elastic.Enumeradores.Implante)GetValue(ImplanteElementoProperty); }
            set { SetValue(ImplanteElementoProperty, value); }
        }
        public static readonly DependencyProperty ImplanteElementoProperty =
            DependencyProperty.Register("ImplanteElemento", typeof(Hefesoft.Periodontograma.Elastic.Enumeradores.Implante), typeof(Implante),
            new PropertyMetadata(Hefesoft.Periodontograma.Elastic.Enumeradores.Implante.ninguno, new PropertyChangedCallback(OnImplanteElementoChanged)));

        private static void OnImplanteElementoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Implante)d).OnImplanteElementoChanged(e);
        }

        private void OnImplanteElementoChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Hefesoft.Periodontograma.Elastic.Enumeradores.Implante)e.NewValue;

            if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Implante.ninguno)
            {
                VisualStateManager.GoToState(this, "VisualState", true);
            }
            else if (item ==  Hefesoft.Periodontograma.Elastic.Enumeradores.Implante.black)
            {
                VisualStateManager.GoToState(this, "VisualStateBlack", true);
            }
        }

        #endregion


    }
}
