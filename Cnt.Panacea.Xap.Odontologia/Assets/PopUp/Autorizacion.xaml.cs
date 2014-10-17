using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Finalizar;
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
    public partial class Autorizacion : ChildWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Autorizacion"/> class.
        /// </summary>
        public Autorizacion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
          
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
        /// Handles the Click event of the btnIngresar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void btnIngresar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Consultar_Usuario_Cierra() { Nombre = this.txtbxUser.Text, Password = this.txtbxPass.Password });            
            this.DialogResult = true;
            this.Close();

        }
    }
}

