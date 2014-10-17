using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Recursos;
using Infragistics.Controls.Grids;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class UserControlGridPlanTratamientoProcedimientos : UserControl
    {
        #region Variables
        public bool WizarPantalla1 = true;
        #endregion

        #region Propiedades
        /// <summary>
        /// Gets or sets a value indicating whether [mostrar para superficie odontograma evolucion].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [mostrar para superficie odontograma evolucion]; otherwise, <c>false</c>.
        /// </value>
        public bool MostrarParaSuperficieOdontogramaEvolucion { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlGridPlanTratamientoProcedimientos"/> class.
        /// </summary>
        public UserControlGridPlanTratamientoProcedimientos()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(UserControlGridPlanTratamientoProcedimientos_Loaded);
        }
        #endregion

        #region Eventos

        /// <summary>
        /// Handles the Loaded event of the UserControlGridPlanTratamientoProcedimientos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void UserControlGridPlanTratamientoProcedimientos_Loaded(object sender, RoutedEventArgs e)
        {
            if(MostrarParaSuperficieOdontogramaEvolucion == false )
            {
                CargarColumnas();
                RecargarGrilla();
            }
            else if (MostrarParaSuperficieOdontogramaEvolucion == true)
            {
                OcultarColumnas();
                MostrarColumnasClickSuperficieOdontogramaEvolucion();
            }
        }

        /// <summary>
        /// Handles the Click event of the HyperlinkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //(this.DataContext as ViewModels.PacienteOdontogramaViewModel).CambiarConvenio(((ProcedimientosGrillaPlanTratamiento)(((System.Windows.FrameworkElement)(sender)).DataContext)), ((ProcedimientosGrillaPlanTratamiento)(((System.Windows.FrameworkElement)(sender)).DataContext)).ProcedimientosEspecialidadValor.Identificador);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((System.Windows.FrameworkElement)(sender)).DataContext != null)
            {
                //((ProcedimientosGrillaPlanTratamiento)(((System.Windows.FrameworkElement)(sender)).DataContext)).HigienistasIpsValor = null;
            }
        }

        /// <summary>
        /// Handles the 1 event of the ComboBox_SelectionChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (((System.Windows.FrameworkElement)(sender)).DataContext != null)
            {
                //((ProcedimientosGrillaPlanTratamiento)(((System.Windows.FrameworkElement)(sender)).DataContext)).OdontologosIpsValor = null;
            }
        }
        
        #endregion

        #region Metodos

        /// <summary>
        /// Recarga grilla.
        /// </summary>
        public void RecargarGrilla()
        {
            //gridTratamientos.ItemsSource = null;
            //gridTratamientos.ItemsSource = new System.Windows.Data.PagedCollectionView((this.DataContext as ViewModels.PacienteOdontogramaViewModel).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar);
        }

        /// <summary>
        /// Ocultar columnas.
        /// </summary>
        public void OcultarColumnas()
        {
            foreach (var pivote in gridTratamientos.Columns)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Muestra las columnas requeridas para el odontograma de evolucion.
        /// </summary>
        public void MostrarColumnasClickSuperficieOdontogramaEvolucion()
        {            
            gridTratamientos.Columns[1].Visibility = Visibility.Visible;
            gridTratamientos.Columns[2].Visibility = Visibility.Visible;
            gridTratamientos.Columns[3].Visibility = Visibility.Visible;
            gridTratamientos.Columns[4].Visibility = Visibility.Visible;
            gridTratamientos.Columns[16].Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Cargar las columnas dependiendo si es plan de tratamiento o de evolucion.
        /// </summary>
        public void CargarColumnas()
        {
            foreach (var pivote in gridTratamientos.Columns)
            {
                if (Variables_Globales.Plantratamiento)
                {
                    if (pivote.Tag != null && pivote.Tag.ToString() == "Plantratamiento")
                    {
                        pivote.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (pivote.Tag != null && pivote.Tag.ToString() == "Evolucion")
                    {
                        pivote.Visibility = System.Windows.Visibility.Collapsed;
                    }

                    if (WizarPantalla1)
                    {
                        WizardPantalla2(pivote);
                    }
                    else
                    {
                        WizardPantalla1(pivote);
                    }

                }
                else if (Variables_Globales.Evolucion)
                {
                    if (pivote.Tag != null && pivote.Tag.ToString() == "Plantratamiento")
                    {
                        pivote.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    else if (pivote.Tag != null && pivote.Tag.ToString() == "Evolucion")
                    {
                        pivote.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        /// <summary>
        /// Muestra en el Wizard los campos habilitados en la pantalla1.
        /// </summary>
        /// <param name="pivote">The pivote.</param>
        public void WizardPantalla1(ColumnBase pivote)
        {
            if (pivote.HeaderText == Mensajes.Especialidad)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Valor_Procedimiento)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Valor_Paciente)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Odontologo)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Higienista)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Cobro)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Muestra en el Wizard los campos habilitados en la pantalla2.
        /// </summary>
        /// <param name="pivote">The pivote.</param>
        public void WizardPantalla2(ColumnBase pivote)
        {
            if (pivote.HeaderText == Mensajes.Tipo_Sesion)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Sesion)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Numero_Sesiones)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Tercero)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Convenio)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Tipo_Sesion)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Sesion_Cuotas)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Sesiones_Programadas)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
            else if (pivote.HeaderText == Mensajes.Sesiones_Realizadas)
            {
                pivote.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Filtrarpieza dental evoluvion.
        /// </summary>
        /// <param name="PiezaDental">The pieza dental.</param>
        public void filtrarPiezaDentalEvoluvion(Odontograma PiezaDental)
        {
           // ObservableCollection<ProcedimientosGrillaPlanTratamiento> lst = ((Cnt.Panacea.Xap.ViewModels.PacienteOdontogramaViewModel)(this.DataContext)).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar;

           // gridTratamientos.ItemsSource = null;

           // gridTratamientos.ItemsSource =
           //new System.Windows.Data.PagedCollectionView(lst.Where(a => a.Odontograma.codigoPiezaDental == PiezaDental.codigoPiezaDental));
        }
        
        #endregion
      
    }
}