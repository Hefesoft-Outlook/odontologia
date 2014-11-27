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



        #region margen1 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public int margen1
        {
            get { return (int)GetValue(margen1Property); }
            set { SetValue(margen1Property, value); }
        }
        public static readonly DependencyProperty margen1Property =
            DependencyProperty.Register("margen1", typeof(int), typeof(Margen),
            new PropertyMetadata(0, new PropertyChangedCallback(Onmargen1Changed)));

        private static void Onmargen1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Margen)d).Onmargen1Changed(e);
        }

        private void Onmargen1Changed(DependencyPropertyChangedEventArgs e)
        {
            Linea.Points.Clear();
            var item = (int)e.NewValue;
            Linea.Points.Add(new Point(0, margen1));
            Linea.Points.Add(new Point(50, margen2));
            Linea.Points.Add(new Point(100, margen3));
        }
        #endregion


        #region margen2 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public int margen2
        {
            get { return (int)GetValue(margen2Property); }
            set { SetValue(margen2Property, value); }
        }
        public static readonly DependencyProperty margen2Property =
            DependencyProperty.Register("margen2", typeof(int), typeof(Margen),
            new PropertyMetadata(0, new PropertyChangedCallback(Onmargen2Changed)));

        private static void Onmargen2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Margen)d).Onmargen2Changed(e);
        }

        private void Onmargen2Changed(DependencyPropertyChangedEventArgs e)
        {
            Linea.Points.Clear();
            var item = (int)e.NewValue;
            Linea.Points.Add(new Point(0, margen1));
            Linea.Points.Add(new Point(50, margen2));
            Linea.Points.Add(new Point(100, 0));
        }

        

        #endregion


        #region margen3 (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public int margen3
        {
            get { return (int)GetValue(margen3Property); }
            set { SetValue(margen3Property, value); }
        }
        public static readonly DependencyProperty margen3Property =
            DependencyProperty.Register("margen3", typeof(int), typeof(Margen),
            new PropertyMetadata(0, new PropertyChangedCallback(Onmargen3Changed)));

        private static void Onmargen3Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Margen)d).Onmargen3Changed(e);
        }

        private void Onmargen3Changed(DependencyPropertyChangedEventArgs e)
        {
            Linea.Points.Clear();
            var item = (int)e.NewValue;
            Linea.Points.Add(new Point(0, margen1));
            Linea.Points.Add(new Point(50, margen2));
            Linea.Points.Add(new Point(100, margen3));
        }

        

        #endregion
        
    }
}
