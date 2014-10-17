using Cnt.Panacea.Xap.Odontologia.Vm.Util.PopUp;
using System.Windows;
using System.Windows.Controls;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class SobreEscribirAdicionar : ChildWindow
    {
        #region Variables
        public EstadoSobreEscribirAdicionar EstadoSobreEscribirAdicionar { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SobreEscribirAdicionar"/> class.
        /// </summary>
        public SobreEscribirAdicionar()
        {
            InitializeComponent();
            EstadoSobreEscribirAdicionar = new EstadoSobreEscribirAdicionar() { Adicionar = false, Cancelar = false, SobreEscribir = false };
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EstadoSobreEscribirAdicionar.Cancelar = true;
            this.DialogResult = false;
        }

        /// <summary>
        /// Handles the Click event of the Agregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            EstadoSobreEscribirAdicionar.Adicionar = true;
            this.DialogResult = true;
        }

        /// <summary>
        /// Handles the Click event of the Sobreescribir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Sobreescribir_Click(object sender, RoutedEventArgs e)
        {
            EstadoSobreEscribirAdicionar.SobreEscribir = true;
            this.DialogResult = true;
        }
        #endregion        
    }

    
}

