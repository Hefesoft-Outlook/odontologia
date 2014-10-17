using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Recursos;
using System.Linq;
using Cnt.Panacea.Xap.Odontologia.Clases.Helpers;
using System.Collections.Generic;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;

namespace Cnt.Panacea.Xap.Odontologia
{
    /*
     * Este control se usa para mostrar el diente seleccionado
     * Recibe los mensajes de la superficie seleccionada
     * Y cuando se da click en nuevo se limpia la superficie      
     */
	public partial class Pieza_Seleccionada : UserControl, IDisposable
	{
        
		public Pieza_Seleccionada()
		{
			// Required to initialize variables
			InitializeComponent();        
            //Muestra El elemento seleccionado
            oirPiezaSeleccionada();
            //Limpia las superficies para el nuevo odontograma
            oirNuevoOdontograma();
		}

        private void oirNuevoOdontograma()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento =>
            {
                mostrarElementoSeleccionadoMapaDental(new Odontograma());
                textCodigo.Text = "";

                //Limpia los elementos correspondientes a la pieza completa
                TxtBxPiezaCompleta.Text = "";
                PiezaDental.limpiarPiezaCompleta();
            });
        }

        private void oirPiezaSeleccionada()
        {
            Messenger.Default.Register<Odontograma>(this, "Pieza Seleccionada", item => 
            {
                mostrarElementoSeleccionadoMapaDental(item);
                textCodigo.Text = item.codigoSPiezaDental;
            });
        }

        private void mostrarElementoSeleccionadoMapaDental(Odontograma Elemento)
        {
            var item = Elemento.DiagnosticoProcedimiento;

            // Si es diferente de boca
            if (Elemento.Paleta_Selecionado != null && Elemento.Paleta_Selecionado.AplicaTratamiento != 2)
            {
                PiezaDental.Item = item;

                TxtBx1.Text = "Sano";
                TxtBx2.Text = "Sano";
                TxtBx3.Text = "Sano";
                TxtBx4.Text = "Sano";
                TxtBx5.Text = "Sano";
                TxtBx6.Text = "Sano";
                TxtBx7.Text = "Sano";
                TxtBxPiezaCompleta.Text = "Sano";

                if (item.Superficie1.Any())
                {
                    TxtBx1.Text = item.Superficie1.First().Descripcion;
                }

                if (item.Superficie2.Any())
                {
                    TxtBx2.Text = item.Superficie2.First().Descripcion;
                }

                if (item.Superficie3.Any())
                {
                    TxtBx3.Text = item.Superficie3.First().Descripcion;
                }

                if (item.Superficie4.Any())
                {
                    TxtBx4.Text = item.Superficie4.First().Descripcion;
                }

                if (item.Superficie5.Any())
                {
                    TxtBx5.Text = item.Superficie5.First().Descripcion;
                }

                if (item.Superficie6.Any())
                {
                    TxtBx6.Text = item.Superficie6.First().Descripcion;
                }

                if (item.Superficie7.Any())
                {
                    TxtBx7.Text = item.Superficie7.First().Descripcion;
                }

                if (item.PiezaCompleta.Any() && item.PiezaCompleta.First().Descripcion != null)
                {
                    TxtBxPiezaCompleta.Text = item.PiezaCompleta.First().Descripcion;
                }
            }
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Odontograma>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
        }

        
    }
}