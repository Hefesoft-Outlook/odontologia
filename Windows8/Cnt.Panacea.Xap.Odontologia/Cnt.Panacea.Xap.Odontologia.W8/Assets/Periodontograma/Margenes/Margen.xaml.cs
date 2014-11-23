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

namespace App2.Assets.Periodontograma.Margenes
{
    public sealed partial class Margen : UserControl
    {
        public Margen()
        {
            this.InitializeComponent();         
        }


        #region color (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public SolidColorBrush color
        {
            get { return (SolidColorBrush)GetValue(colorProperty); }
            set { SetValue(colorProperty, value); }
        }
        public static readonly DependencyProperty colorProperty =
            DependencyProperty.Register("color", typeof(SolidColorBrush), typeof(Margen),
            new PropertyMetadata(Colors.Red, new PropertyChangedCallback(OncolorChanged)));

        private static void OncolorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Margen)d).OncolorChanged(e);
        }

        private void OncolorChanged(DependencyPropertyChangedEventArgs e)
        {
            Linea.Stroke = (SolidColorBrush)e.NewValue;
            Linea.Points.Clear();
        }


        #endregion



        #region puntos (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Dictionary<int,int> puntos
        {
            get { return (Dictionary<int,int>)GetValue(puntosProperty); }
            set { SetValue(puntosProperty, value); }
        }
        public static readonly DependencyProperty puntosProperty =
            DependencyProperty.Register("puntos", typeof(Dictionary<int,int>), typeof(Margen),
            new PropertyMetadata(new Dictionary<int,int>(), new PropertyChangedCallback(OnpuntosChanged)));

        private static void OnpuntosChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Margen)d).OnpuntosChanged(e);
        }

        private void OnpuntosChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Dictionary<int, int>)e.NewValue;

            if (item.Any())
            {
                Linea.Points.Clear();

                foreach (var elemento in item)
                {
                    Linea.Points.Add(new Point(elemento.Key, elemento.Value));
                }
            }
        }

        

        #endregion
                
        
    }
}
