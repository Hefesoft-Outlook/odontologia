using Hefesoft.Odontologia.Periodontograma.Enumeradores;
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

namespace App2.Assets.Periodontograma.Control
{
    public sealed partial class Item_Template : UserControl
    {
        public Item_Template()
        {
            this.InitializeComponent();
        }


       
        #region mostrarRecuadro (DependencyProperty)

        /// <summary>
        /// Para que no muestre el cuadrado morado en la pieza dental
        /// </summary>
        public bool mostrarRecuadro
        {
            get { return (bool)GetValue(mostrarRecuadroProperty); }
            set { SetValue(mostrarRecuadroProperty, value); }
        }
        public static readonly DependencyProperty mostrarRecuadroProperty =
            DependencyProperty.Register("mostrarRecuadro", typeof(bool), typeof(Item_Template),
            new PropertyMetadata(true, new PropertyChangedCallback(OnmostrarRecuadroChanged)));

        private static void OnmostrarRecuadroChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Item_Template)d).OnmostrarRecuadroChanged(e);
        }

        private void OnmostrarRecuadroChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;

            if(item)
            {
                Furca1.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Furca2.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Furca1.mostraRecuadro = true;
                Furca2.mostraRecuadro = true;
            }
            else
            {
                Furca1.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Furca2.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Furca1.mostraRecuadro = false;
                Furca2.mostraRecuadro = false;
            }  

        }

        

        #endregion
        

        #region mostrarOpciones (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool mostrarOpciones
        {
            get { return (bool)GetValue(mostrarOpcionesProperty); }
            set { SetValue(mostrarOpcionesProperty, value); }
        }
        public static readonly DependencyProperty mostrarOpcionesProperty =
            DependencyProperty.Register("mostrarOpciones", typeof(bool), typeof(Item_Template),
            new PropertyMetadata(true, new PropertyChangedCallback(OnmostrarOpcionesChanged)));

        private static void OnmostrarOpcionesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Item_Template)d).OnmostrarOpcionesChanged(e);
        }

        private void OnmostrarOpcionesChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (bool)e.NewValue;

            if(item)
            {
                menuOpciones.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
            else
            {
                menuOpciones.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        

        #endregion
        

        #region furcaVisualizacion (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Furca_Visualizacion furcaVisualizacion
        {
            get { return (Furca_Visualizacion)GetValue(furcaVisualizacionProperty); }
            set { SetValue(furcaVisualizacionProperty, value); }
        }
        public static readonly DependencyProperty furcaVisualizacionProperty =
            DependencyProperty.Register("furcaVisualizacion", typeof(Furca_Visualizacion), typeof(Item_Template),
            new PropertyMetadata(Furca_Visualizacion.No_Visible, new PropertyChangedCallback(OnfurcaVisualizacionChanged)));

        private static void OnfurcaVisualizacionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Item_Template)d).OnfurcaVisualizacionChanged(e);
        }

        private void OnfurcaVisualizacionChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Furca_Visualizacion)e.NewValue;

            Furca1.mostraRecuadro = false;
            Furca2.mostraRecuadro = false;
            Furca1.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Furca2.Rectangle.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (item == Furca_Visualizacion.No_Visible)
            {
                Furca1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Furca2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;               
                
            }
            else if (item == Furca_Visualizacion.Visible_Un_Elemento)
            {
                VisualStateManager.GoToState(this, "VisualState", true);
                Furca1.Visibility = Windows.UI.Xaml.Visibility.Visible;              
            }
            else if (item == Furca_Visualizacion.Visible_Dos_Elementos)
            {
                VisualStateManager.GoToState(this, "VisualStateDosElementos", true);
                Furca1.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Furca2.Visibility = Windows.UI.Xaml.Visibility.Visible;           
            }
        }

        

        #endregion
        
    }
}
