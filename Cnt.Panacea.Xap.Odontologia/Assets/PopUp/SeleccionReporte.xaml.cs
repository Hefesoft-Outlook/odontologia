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
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia.PopUp
{
    public partial class SeleccionReporte : ChildWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeleccionReporte"/> class.
        /// </summary>
        public SeleccionReporte()
        {
            InitializeComponent();
            LstBoxReportes.Loaded += new RoutedEventHandler(LstBoxReportes_Loaded);
        }

        /// <summary>
        /// Handles the Loaded event of the LstBoxReportes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void LstBoxReportes_Loaded(object sender, RoutedEventArgs e)
        {
            
            foreach (ListBoxItem pivot in LstBoxReportes.Items)
            {
                if (Variables_Globales.IdTratamientoActivo == 0)
                {
                    if (pivot.Tag.ToString () == "InfConfProcedDiagnos")
                        pivot.Visibility = Visibility.Visible;
                    else
                        pivot.Visibility = Visibility.Collapsed;
                }

                
            
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
    }
}

