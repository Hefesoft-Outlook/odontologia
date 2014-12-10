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

namespace Hefesoft.Periodontograma.Assets.Furca
{
    public sealed partial class Furca : UserControl
    {
        public Furca()
        {
            this.InitializeComponent();
        }


        #region mostraRecuadro (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool mostraRecuadro
        {
            get { return (bool)GetValue(mostraRecuadroProperty); }
            set { SetValue(mostraRecuadroProperty, value); }
        }
        public static readonly DependencyProperty mostraRecuadroProperty =
            DependencyProperty.Register("mostraRecuadro", typeof(bool), typeof(Furca),
            new PropertyMetadata(false, new PropertyChangedCallback(OnmostraRecuadroChanged)));

        private static void OnmostraRecuadroChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Furca)d).OnmostraRecuadroChanged(e);
        }

        private void OnmostraRecuadroChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;

            if(item)
            {
                Rectangle.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                Rectangle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        

        #endregion
        

        #region FurcaProperty (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Hefesoft.Periodontograma.Elastic.Enumeradores.Furca FurcaProperty
        {
            get { return (Hefesoft.Periodontograma.Elastic.Enumeradores.Furca)GetValue(FurcaPropertyProperty); }
            set { SetValue(FurcaPropertyProperty, value); }
        }
        public static readonly DependencyProperty FurcaPropertyProperty =
            DependencyProperty.Register("FurcaProperty", typeof(Hefesoft.Periodontograma.Elastic.Enumeradores.Furca), typeof(Furca),
            new PropertyMetadata(Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.vacio, new PropertyChangedCallback(OnFurcaPropertyChanged)));

        private static void OnFurcaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Furca)d).OnFurcaPropertyChanged(e);
        }

        private void OnFurcaPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Hefesoft.Periodontograma.Elastic.Enumeradores.Furca)e.NewValue;

            imagen.Source = null;

            if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.ninguno)
            {
                imagen.Visibility = Windows.UI.Xaml.Visibility.Collapsed;                
            }
            else
            {
                imagen.Visibility = Windows.UI.Xaml.Visibility.Visible;                

                if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.vacio)
                {
                    url = string.Format("ms-appx:///Assets/Images/Periodontograma/vacio.png");
                    imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                }
                else if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.mediolleno)
                {
                    url = string.Format("ms-appx:///Assets/Images/Periodontograma/mediolleno.png");
                    imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                }
                else if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.lleno)
                {
                    url = string.Format("ms-appx:///Assets/Images/Periodontograma/lleno.png");
                    imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                }
                else if (item == Hefesoft.Periodontograma.Elastic.Enumeradores.Furca.cuadrado)
                {
                    url = string.Format("ms-appx:///Assets/Images/Periodontograma/cuadrado.png");
                    imagen.Source = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                }
            }
        }

        #endregion


        public string url { get; set; }
    }
}
