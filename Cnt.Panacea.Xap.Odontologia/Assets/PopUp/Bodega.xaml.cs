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
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Std.Xap.Controles;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class Bodega : ChildWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bodega"/> class.
        /// </summary>
        public Bodega() 
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the GetPlanesValores event of the Bodega control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Bodega_GetPlanesValores(object sender, EventArgs e)
        {
            //(this.DataContext as ViewModels.PacienteOdontogramaViewModel).InsumosProcedimientoEvolucion(ctrlBodega.AplicacionesCanasta, (ctrlBodega.DataContext as Clases.ParametrosInsumos).IdPlanTratamiento);
            //this.Close();
        }

        /// <summary>
        /// Cerrars the ventana evento.
        /// </summary>
        public void CerrarVentanaEvento()
        {
            if ((this.ctrlBodega.DataContext as ParametrosInsumos) != null)
                (this.ctrlBodega.DataContext as ParametrosInsumos).GetPlanesValores += new EventHandler(Bodega_GetPlanesValores);
        }

        #region Eventos
        void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
        #endregion

        
    }
}

