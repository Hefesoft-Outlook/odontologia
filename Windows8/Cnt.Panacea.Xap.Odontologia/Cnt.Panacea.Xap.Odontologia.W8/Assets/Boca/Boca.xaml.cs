using System;
using System.Windows;
using System.Windows.Input;
using Cnt.Panacea.Entities.Odontologia;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Boca;
using Microsoft.Practices.ServiceLocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace Cnt.Panacea.Xap.Odontologia
{
	public partial class Boca : UserControl, IDisposable
	{
        /*
        * Este control se usa para mostrar la boca
        * Recibe los mensajes de la superficie seleccionada
        * Y cuando se da click en nuevo se limpia la superficie      
        */
		public Boca()
		{
			// Required to initialize variables
			InitializeComponent();
            oirNuevoOdontograma();
            oirMensajeBoca();
		}

        private void oirNuevoOdontograma()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento =>
            {
               manejoGrafico(new ConfigurarDiagnosticoProcedimOtraEntity());
               limpiar();
            });
        }

        private void oirMensajeBoca()
        {
            Messenger.Default.Register<Boca_Msg>(this, item =>
            {   
                //Se pinta el primer diagnostico o procedimiento de la boca
                var vmBoca = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Boca.Boca>();
                if (vmBoca.Elemento.DiagnosticoProcedimiento.Boca != null)
                {
                    manejoGrafico(vmBoca.Elemento.DiagnosticoProcedimiento.Boca.First());
                }
            });
        }

        private void manejoGrafico(ConfigurarDiagnosticoProcedimOtraEntity item)
        {
            if (item != null)
            {
                limpiar();

                if (!string.IsNullOrEmpty(item.Descripcion))
                {
                    TxtBxBoca1.Text = item.Descripcion;
                }

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
                    texto.FontFamily = new FontFamily("Portable User Interface");
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
                        Rectangle.Fill = new SolidColorBrush(Colores.ColorEntero(Convert.ToInt32(item.Color)));
                    }
                }
            }
            else
            {
                limpiar();
            }            
        }

        private void convenio()
        {
            Rectangle.Stroke = new SolidColorBrush(Colors.Red);
        }

        private void limpiar()
        {
            texto.Text = "";
            TxtBxBoca1.Text = "";
            Rectangle.Fill = new SolidColorBrush(Colors.Transparent);
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
            Messenger.Default.Unregister<Boca_Msg>(this);
        }
    }
}