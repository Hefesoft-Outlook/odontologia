using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
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

namespace Cnt.Panacea.Xap.Odontologia.Assets.Pieza_Dental
{
    public partial class Pieza_Seleccionada_Mostrar_Click_Odontograma : UserControl
    {
        public Pieza_Seleccionada_Mostrar_Click_Odontograma()
        {
            InitializeComponent();
        }


        #region Item (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public DiagnosticoProcedimiento Item
        {           
            get { return (DiagnosticoProcedimiento)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(DiagnosticoProcedimiento), typeof(Pieza_Seleccionada_Mostrar_Click_Odontograma),
            new PropertyMetadata(new DiagnosticoProcedimiento(), new PropertyChangedCallback(OnItemChanged)));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Pieza_Seleccionada_Mostrar_Click_Odontograma)d).OnItemChanged(e);
        }

        protected virtual void OnItemChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = e.NewValue as DiagnosticoProcedimiento;

            if (item.Superficie1.Any())
            {
                Central.Item = item.Superficie1.First();
            }
            else
            {
                Central.Item = null;
            }

            if (item.Superficie2.Any())
            {
                Izquierda.Item = item.Superficie2.First();
            }
            else
            {
                Izquierda.Item = null;
            }

            if (item.Superficie3.Any())
            {
                Derecha.Item = item.Superficie3.First();
            }
            else
            {
                Derecha.Item = null;
            }

            if (item.Superficie4.Any())
            {
                Abajo.Item = item.Superficie4.First();
            }
            else
            {
                Abajo.Item = null;
            }

            if (item.Superficie5.Any())
            {
                Arriba.Item = item.Superficie5.First();
            }
            else
            {
                Arriba.Item = null;
            }

            if (item.Superficie6.Any())
            {
                Inferior.Item = item.Superficie6.First();
            }
            else
            {
                Inferior.Item = null;
            }

            if (item.Superficie7.Any())
            {
                Superior.Item = item.Superficie7.First();
            }
            else
            {
                Superior.Item = null;
            }

            if (item.PiezaCompleta.Any())
            {
                UserControlColorDienteCompleto.Item = item.PiezaCompleta.First();
            }
            else
            {
                UserControlColorDienteCompleto.Item = null;
            }
        }

        public void limpiarPiezaCompleta()
        {
            UserControlColorDienteCompleto.Item = null;
        }

        #endregion
        
    }
}
