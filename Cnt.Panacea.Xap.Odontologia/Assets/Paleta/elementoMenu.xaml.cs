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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cnt.Panacea.Xap.Odontologia.Clases.Assets;
using Cnt.Panacea.Xap.Odontologia.Clases.Helpers;
using Cnt.Panacea.Entities.Odontologia;

namespace Cnt.Panacea.Xap.Odontologia.Assets
{
    public partial class elementoMenu : UserControl
    {
        public elementoMenu()
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
            DependencyProperty.Register("Item", typeof(ConfigurarDiagnosticoProcedimOtraEntity), typeof(elementoMenu),
            new PropertyMetadata(null, new PropertyChangedCallback(OnItemChanged)));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((elementoMenu)d).OnItemChanged(e);
        }

        private void convenio()
        {
            border1.BorderBrush = new SolidColorBrush(Colors.Red);
            //StoryboardConvenio.Begin();
        }

        protected virtual void OnItemChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var item = (ConfigurarDiagnosticoProcedimOtraEntity)e.NewValue;

                fijarColorBorde(item);

                if (item.TipoPanel == TipoPanel.ProcedimientoNoCubreConvenio)
                {
                    convenio();
                }

                if (!string.IsNullOrEmpty(item.Descripcion))
                {
                    TxtBlckDescripcion.Text = item.Descripcion;
                }

                if (!string.IsNullOrEmpty(item.Simbolo))
                {
                    txtBlckSimbolo.Visibility = System.Windows.Visibility.Visible;
                    txtBlckLetra.Visibility = System.Windows.Visibility.Collapsed;
                    rectangle.Visibility = System.Windows.Visibility.Collapsed;

                    Tipo_Elemento_Seleccionado = Tipo_Elemento.Simbolo;
                    txtBlckSimbolo.Text = item.Simbolo;
                    if (!string.IsNullOrEmpty(item.Fuente))
                    {
                        txtBlckSimbolo.FontFamily = new FontFamily(item.Fuente);
                    }

                    if (item.ColorAdicional != null)
                    {
                        txtBlckSimbolo.Foreground = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else if (!string.IsNullOrEmpty(item.Letra))
                {
                    txtBlckSimbolo.Visibility = System.Windows.Visibility.Collapsed;
                    txtBlckLetra.Visibility = System.Windows.Visibility.Visible;
                    rectangle.Visibility = System.Windows.Visibility.Collapsed;

                    Tipo_Elemento_Seleccionado = Tipo_Elemento.Letra;
                    txtBlckLetra.Text = item.Letra;

                    if (item.ColorAdicional != null)
                    {
                        txtBlckLetra.Foreground = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else
                {                    
                    txtBlckSimbolo.Visibility = System.Windows.Visibility.Collapsed;
                    txtBlckLetra.Visibility = System.Windows.Visibility.Collapsed;
                    rectangle.Visibility = System.Windows.Visibility.Visible;
                    
                    Tipo_Elemento_Seleccionado = Tipo_Elemento.Color;

                    if(item.Color != null)
                    {
                        rectangle.Fill = new SolidColorBrush(Cnt.Std.Comun.ColorEntero(Convert.ToInt32(item.Color)));
                    }
                }
            }            
        }

        private void fijarColorBorde(ConfigurarDiagnosticoProcedimOtraEntity item)
        {
            if (item.TipoPanel == TipoPanel.Diagnostico)
            {
                border1.BorderBrush = new SolidColorBrush(Colors.Green);
            }
            else if (item.TipoPanel == TipoPanel.Procedimiento)
            {
                border1.BorderBrush = new SolidColorBrush(Colors.Orange);
            }
            else if (item.TipoPanel == TipoPanel.OtraCaracteristica)
            {
                border1.BorderBrush = new SolidColorBrush(Colors.Blue);
            }
        }

     

        #endregion



   

        public Tipo_Elemento Tipo_Elemento_Seleccionado { get; set; }
    }

   

   

}
