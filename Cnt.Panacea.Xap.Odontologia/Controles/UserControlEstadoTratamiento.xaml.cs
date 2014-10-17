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

namespace Cnt.Panacea.Xap.Odontologia.Controles
{
    public partial class UserControlEstadoTratamiento : UserControl
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlEstadoTratamiento"/> class.
        /// </summary>
        public UserControlEstadoTratamiento()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(UserControlEstadoTratamiento_Loaded);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Loaded event of the UserControlEstadoTratamiento control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void UserControlEstadoTratamiento_Loaded(object sender, RoutedEventArgs e)
        {
            //UserControlGridPlanTratamientoProcedimientos.gridTratamientos.ItemsSource = null;
            //UserControlGridPlanTratamientoProcedimientos.gridTratamientos.ItemsSource = ((Cnt.Panacea.Xap.ViewModels.PacienteOdontogramaViewModel)(this.DataContext)).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar;
        }
        #endregion
        
    }
}
