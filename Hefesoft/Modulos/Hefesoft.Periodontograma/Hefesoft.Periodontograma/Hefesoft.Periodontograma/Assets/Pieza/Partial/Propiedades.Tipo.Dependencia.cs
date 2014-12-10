using Hefesoft.Periodontograma.Elastic.Enumeradores;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Hefesoft.Periodontograma.Assets.Pieza
{
    public sealed partial class Pieza : UserControl
    {

        #region Numero (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public int Numero
        {
            get { return (int)GetValue(NumeroProperty); }
            set { SetValue(NumeroProperty, value); }
        }
        public static readonly DependencyProperty NumeroProperty =
            DependencyProperty.Register("Numero", typeof(int), typeof(Pieza),
            new PropertyMetadata(30, new PropertyChangedCallback(OnNumeroChanged)));

        private static void OnNumeroChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pieza)d).OnNumeroChanged(e);
        }

        private void OnNumeroChanged(DependencyPropertyChangedEventArgs e)
        {
            url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-abajo-{0}.png", e.NewValue);
            imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
        }        

        #endregion


        #region TipoPieza (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Tipo_Pieza TipoPieza
        {
            get { return (Tipo_Pieza)GetValue(TipoPiezaProperty); }
            set { SetValue(TipoPiezaProperty, value); }
        }
        public static readonly DependencyProperty TipoPiezaProperty =
            DependencyProperty.Register("TipoPieza", typeof(Tipo_Pieza), typeof(Pieza),
            new PropertyMetadata(Tipo_Pieza.normal, new PropertyChangedCallback(OnTipoPiezaChanged)));

        private static void OnTipoPiezaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pieza)d).OnTipoPiezaChanged(e);
        }

        private void OnTipoPiezaChanged(DependencyPropertyChangedEventArgs e)
        {
            var elemento = (Tipo_Pieza)e.NewValue;
            imagen.Source = null;

            if (elemento == Tipo_Pieza.normal)
            {
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-{1}-{0}{2}.png", Numero, abajoArribaString, caraString);
                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                tipoPiezaString = "";
            }
            else if (elemento == Tipo_Pieza.tachado)
            {
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-{1}-tachados-{0}{2}.png", Numero, abajoArribaString, caraString);
                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                tipoPiezaString = "tachados";
            }
            else if (elemento == Tipo_Pieza.tornillo)
            {
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-{1}-tornillo-{0}{2}.png", Numero, abajoArribaString, caraString);
                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                tipoPiezaString = "tornillo";
            }
        }

        

        #endregion


        #region cara (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Cara cara
        {
            get { return (Cara)GetValue(caraProperty); }
            set { SetValue(caraProperty, value); }
        }
        public static readonly DependencyProperty caraProperty =
            DependencyProperty.Register("cara", typeof(Cara), typeof(Pieza),
            new PropertyMetadata(Cara.a, new PropertyChangedCallback(OncaraChanged)));

        private static void OncaraChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pieza)d).OncaraChanged(e);
        }

        private void OncaraChanged(DependencyPropertyChangedEventArgs e)
        {
            var elemento = (Cara)e.NewValue;
            imagen.Source = null;

            if (elemento ==  Cara.a)
            {
                caraString = "";
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-{1}-{0}.png", Numero, abajoArribaString);
                urlFondo = "ms-appx:///Assets/Images/Periodontograma/fondo-grafico.png";

                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                imagenFondo.Source = new BitmapImage(new Uri(urlFondo, UriKind.RelativeOrAbsolute));
            }
            else if (elemento == Cara.b)
            {
                caraString = "b";
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-{1}-{0}b.png", Numero, abajoArribaString);
                urlFondo = "ms-appx:///Assets/Images/Periodontograma/fondo-grafico-inf.png";

                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                imagenFondo.Source = new BitmapImage(new Uri(urlFondo, UriKind.RelativeOrAbsolute));
            }
        }


        #endregion


        #region Arriba_Abajo (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Arriba_Abajo Arriba_Abajo
        {
            get { return (Arriba_Abajo)GetValue(Arriba_AbajoProperty); }
            set { SetValue(Arriba_AbajoProperty, value); }
        }
        public static readonly DependencyProperty Arriba_AbajoProperty =
            DependencyProperty.Register("Arriba_Abajo", typeof(Arriba_Abajo), typeof(Pieza),
            new PropertyMetadata(Arriba_Abajo.abajo, new PropertyChangedCallback(OnArriba_AbajoChanged)));
        

        private static void OnArriba_AbajoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pieza)d).OnArriba_AbajoChanged(e);
        }

        private void OnArriba_AbajoChanged(DependencyPropertyChangedEventArgs e)
        {
            var elemento = (Arriba_Abajo)e.NewValue;

            imagen.Source = null;

            if (elemento == Arriba_Abajo.abajo)
            {
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-abajo-{0}.png", Numero);
                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                abajoArribaString = "abajo";
            }
            else if (elemento == Arriba_Abajo.arriba)
            {
                url = string.Format("ms-appx:///Assets/Images/Periodontograma/periodontograma-dientes-arriba-{0}{1}.png", Numero, caraString);
                imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                abajoArribaString = "arriba";
            }
        }       

        #endregion
        

        public string url { get; set; }
        private string caraString;
        private string abajoArribaString;
        private string tipoPiezaString;
        private string urlFondo;
    }
}
