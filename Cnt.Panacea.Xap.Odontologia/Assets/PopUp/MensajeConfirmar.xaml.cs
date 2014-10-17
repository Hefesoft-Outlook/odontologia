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
    public partial class MensajeConfirmar : ChildWindow
    {
        #region Propiedades
        /// <summary>
        /// Mensaje que se mostrara al usuario
        /// </summary>
        /// <value>The mensaje.</value>
        public string Mensaje { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MensajeConfirmar"/> class.
        /// </summary>
        public MensajeConfirmar()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MensajeConfirmar_Loaded);
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Setea el mensaje
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void MensajeConfirmar_Loaded(object sender, RoutedEventArgs e)
        {
            if (Mensaje != null)
            {
                txtMensaje.Text = Mensaje;
            }
        }

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
        
    }
}

