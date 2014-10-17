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
using Cnt.Panacea.Entities.Odontologia;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class Procedimientos : ChildWindow
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Procedimientos"/> class.
        /// </summary>
        public Procedimientos()
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

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            var item = btn.DataContext as ConfiguracionProcedimientoOdontologiaEntity;
            //(this.DataContext as ViewModels.PacienteOdontogramaViewModel).Seleccionado = item;
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles the Loaded event of the XamGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void XamGrid_Loaded(object sender, RoutedEventArgs e)
        {
        }
        #endregion
    }
}

