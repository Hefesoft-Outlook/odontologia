﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Cnt.Panacea.Entities.Odontologia;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental
{
    public partial class Arriba : UserControl
    {
        public Arriba()
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
            DependencyProperty.Register("Item", typeof(ConfigurarDiagnosticoProcedimOtraEntity), typeof(Arriba),
            new PropertyMetadata(null, new PropertyChangedCallback(OnItemChanged)));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Arriba)d).OnItemChanged(e);
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
                        texto.Foreground = new SolidColorBrush(Colores.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else if (!string.IsNullOrEmpty(item.Letra))
                {
                    texto.Text = item.Letra;

                    if (item.ColorAdicional != null)
                    {
                        texto.Foreground = new SolidColorBrush(Colores.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else
                {
                    if (item.Color != null)
                    {
                        path.Fill = new SolidColorBrush(Colores.ColorEntero(Convert.ToInt32(item.Color)));
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
    }
}
