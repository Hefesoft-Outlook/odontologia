using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Entities.Odontologia;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental
{
    public partial class Abajo : UserControl
    {
        public Abajo()
        {
            InitializeComponent();
        }

        #region Item (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public ConfigurarDiagnosticoProcedimOtraEntity Item
        {
            get { return (ConfigurarDiagnosticoProcedimOtraEntity)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(ConfigurarDiagnosticoProcedimOtraEntity), typeof(Abajo),
            new PropertyMetadata(null, new PropertyChangedCallback(OnItemChanged)));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Abajo)d).OnItemChanged(e);
        }

        private void convenio()
        {
            path.Stroke = new SolidColorBrush(Colors.Red);
            //StoryboardConvenio.Begin();
        }

        protected virtual void OnItemChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var item = (ConfigurarDiagnosticoProcedimOtraEntity)e.NewValue;
                limpiar();

                if (item.TipoPanel == TipoPanel.ProcedimientoNoCubreConvenio)
                {
                    convenio();
                }

                if (!string.IsNullOrEmpty(item.Simbolo))
                {
                    texto.Text = item.Simbolo;
                    if (!string.IsNullOrEmpty(item.Fuente))
                    {
                        texto.FontFamily = new FontFamily(item.Fuente);
                    }

                    if (item.ColorAdicional != null)
                    {
                        texto.Foreground = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else if (!string.IsNullOrEmpty(item.Letra))
                {
                    texto.Text = item.Letra;

                    if (item.ColorAdicional != null)
                    {
                        texto.Foreground = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else
                {
                    if (item.Color != null)
                    {
                        path.Fill = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.Color)));
                    }
                }
            }
            else
            {
                limpiar();
            }
        }

        private void limpiar()
        {
            texto.Text = "";
            path.Fill = new SolidColorBrush(Colors.Transparent);
        }

        #endregion



        #region esperaAplicarPlanTratamiento (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool esperaAplicarPlanTratamiento
        {
            get { return (bool)GetValue(esperaAplicarPlanTratamientoProperty); }
            set { SetValue(esperaAplicarPlanTratamientoProperty, value); }
        }
        public static readonly DependencyProperty esperaAplicarPlanTratamientoProperty =
            DependencyProperty.Register("esperaAplicarPlanTratamiento", typeof(bool), typeof(Abajo),
            new PropertyMetadata(false, new PropertyChangedCallback(OnesperaAplicarPlanTratamientoChanged)));

        private static void OnesperaAplicarPlanTratamientoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Abajo)d).OnesperaAplicarPlanTratamientoChanged(e);
        }

        protected virtual void OnesperaAplicarPlanTratamientoChanged(DependencyPropertyChangedEventArgs e)
        {
            var valor = (bool)e.NewValue;

            if (valor)
            {
                path_fondo.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                path_fondo.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        #endregion
        

    }
}
