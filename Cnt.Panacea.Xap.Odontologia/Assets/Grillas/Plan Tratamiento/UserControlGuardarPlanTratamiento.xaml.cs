using Cnt.Panacea.Xap.Odontologia.Recursos;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia
{
	public partial class UserControlGuardarPlanTratamiento : UserControl
    {
        #region Constructor

        public UserControlGuardarPlanTratamiento()
        {
            InitializeComponent();            
        }

        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Click event of the HyprlnkBttnSiguiente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyprlnkBttnSiguiente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HyperlinkButton hp = sender as HyperlinkButton;

            if (hp.Content.ToString().Equals(Mensajes.Siguiente_Paso))
            {   
                HyprlnkBttnAnterior.IsEnabled = true;
                wizard.StoryboardWizard1.Begin();
                hp.Content = "Validar datos y guardar";
            }
            else if (hp.Content.ToString().Equals("Validar datos y guardar"))
            {
                //Por ioc se trae el viewmodel que se encarga de formatear los datos
                var item = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
                item.pedirDatosGrilla();
            }
        }

        /// <summary>
        /// Handles the Click event of the HyprlnkBttnAnterior control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyprlnkBttnAnterior_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HyperlinkButton Anterior = sender as HyperlinkButton;
            Anterior.IsEnabled = false;
            HyprlnkBttnSiguiente.IsEnabled = true;
            wizard.StoryboardWizard2.Begin();

            if (HyprlnkBttnSiguiente.Content.ToString().Equals("Validar datos y guardar"))
            {
                HyprlnkBttnSiguiente.Content = Mensajes.Siguiente_Paso; 
            }
        }       
        
        #endregion 
	}
}