using System;
using System.Windows;
using System.Windows.Input;
using Cnt.Panacea.Entities.Odontologia;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Cnt.Panacea.Xap.Odontologia.Assets
{
	public partial class UserControlColorDienteCompleto : UserControl
	{
		public UserControlColorDienteCompleto()
		{
			// Required to initialize variables
			InitializeComponent();            
		}



        #region Realizado (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public bool realizado
        {
            get { return (bool)GetValue(RealizadoProperty); }
            set { SetValue(RealizadoProperty, value); }
        }
        public static readonly DependencyProperty RealizadoProperty =
            DependencyProperty.Register("realizado", typeof(bool), typeof(UserControlColorDienteCompleto),
            new PropertyMetadata(false, new PropertyChangedCallback(OnRealizadoChanged)));

        private static void OnRealizadoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UserControlColorDienteCompleto)d).OnRealizadoChanged(e);
        }

        protected virtual void OnRealizadoChanged(DependencyPropertyChangedEventArgs e)
        {
           
        }

        #endregion


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
            DependencyProperty.Register("Item", typeof(ConfigurarDiagnosticoProcedimOtraEntity), typeof(UserControlColorDienteCompleto),
            new PropertyMetadata(null, new PropertyChangedCallback(OnItemChanged)));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((UserControlColorDienteCompleto)d).OnItemChanged(e);
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
                this.Visibility = Visibility.Visible;
                var item = (ConfigurarDiagnosticoProcedimOtraEntity)e.NewValue;

                limpiar();

                if (item.TipoPanel == TipoPanel.ProcedimientoNoCubreConvenio)
                {
                    convenio();
                }

                if (!string.IsNullOrEmpty(item.Simbolo))
                {
                    texto.Visibility = Visibility.Visible;
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
                    texto.FontFamily = new FontFamily("Portable User Interface");
                    texto.Visibility = Visibility.Visible;

                    if (item.ColorAdicional != null)
                    {
                        texto.Foreground = new SolidColorBrush(Colores.ColorEntero(Convert.ToInt32(item.ColorAdicional)));
                    }
                }
                else
                {
                    if (item.Color != null)
                    {
                        texto.Visibility = Visibility.Collapsed;
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