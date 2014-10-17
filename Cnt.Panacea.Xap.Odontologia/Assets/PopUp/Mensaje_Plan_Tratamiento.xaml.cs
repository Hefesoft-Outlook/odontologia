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
    /// <summary>
    /// 
    /// </summary>
    public partial class Mensaje_Plan_Tratamiento : ChildWindow
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Mensaje_Plan_Tratamiento"/> class.
        /// </summary>
        public Mensaje_Plan_Tratamiento()
        {
            InitializeComponent();
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
        #endregion

        #region Propiedades
        /// <summary>
        /// Gets or sets the mensaje cotizacion.
        /// </summary>
        /// <value>The mensaje cotizacion.</value>
        public Visibility MensajeCotizacion { get; set; }
        /// <summary>
        /// Gets or sets the id tratamiento.
        /// </summary>
        /// <value>The id tratamiento.</value>
        public String IdTratamiento { get; set; }
        /// <summary>
        /// Gets or sets the nombre tratamiento.
        /// </summary>
        /// <value>The nombre tratamiento.</value>
        public String NombreTratamiento { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// Cargars the datos.
        /// </summary>
        public void CargarDatos()
        {
            txtCotizacion.Visibility = MensajeCotizacion;
            txtIdCotizacion.Visibility = MensajeCotizacion;
            txtIdTratamiento.Text = IdTratamiento;
            //TxtDescripcionTratamiento.Text = NombreTratamiento;//DFCF Bug #16105
        }
        #endregion

    }
}

