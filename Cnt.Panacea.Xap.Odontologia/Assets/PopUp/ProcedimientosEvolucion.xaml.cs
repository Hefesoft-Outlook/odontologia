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

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class ProcedimientosEvolucion : ChildWindow
    {
        #region Propiedades
        /// <summary>
        /// Gets or sets the odontograma seleccionado.
        /// </summary>
        /// <value>The odontograma seleccionado.</value>
        //public Odontologia.Clases.Odontograma OdontogramaSeleccionado { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcedimientosEvolucion"/> class.
        /// </summary>
        public ProcedimientosEvolucion()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(UserControlGrillaProcedimientosEvolucion_Loaded);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Handles the Loaded event of the UserControlGrillaProcedimientosEvolucion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void UserControlGrillaProcedimientosEvolucion_Loaded(object sender, RoutedEventArgs e)
        {
            //if (OdontogramaSeleccionado != null)
            //{
            //    CargarProcedimientosDiagnosticos();
            //}
        }

        /// <summary>
        /// Handles the Click event of the HyperlinkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyperlinkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (HyperlinkButton.Content.ToString() == "Procedimientos")
            {
                CargarProcedimientos();
                HyperlinkButton.Content = "Pocedimientos y Diagnosticos";
            }
            else if (HyperlinkButton.Content.ToString() == "Pocedimientos y Diagnosticos")
            {
                CargarProcedimientosDiagnosticos();
                HyperlinkButton.Content = "Pocedimientos";
            }
        }

        #endregion

        #region Metodos
        /// <summary>
        /// Cargar los procedimientos diagnosticos.
        /// </summary>
        private void CargarProcedimientosDiagnosticos()
        {
            Contenedor.Children.Clear();
            //UserControlGridPlanTratamiento Control = new UserControlGridPlanTratamiento() { DataContext = this.DataContext };
            //Control.FiltrarPorPiezaDental(OdontogramaSeleccionado);
            //Contenedor.Children.Add(Control);
            HyperlinkButton.Content = "Procedimientos";
        }

        /// <summary>
        /// Cargar los procedimientos.
        /// </summary>
        private void CargarProcedimientos()
        {
            Contenedor.Children.Clear();
            UserControlGridPlanTratamientoProcedimientos Control = new UserControlGridPlanTratamientoProcedimientos() { DataContext = this.DataContext, MostrarParaSuperficieOdontogramaEvolucion = true };
            //Control.filtrarPiezaDentalEvoluvion(OdontogramaSeleccionado);
            Contenedor.Children.Add(Control);
        }

        #endregion
    }
}

