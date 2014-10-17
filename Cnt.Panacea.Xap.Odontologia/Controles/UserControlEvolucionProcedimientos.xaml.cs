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
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class UserControlEvolucionProcedimientos : UserControl
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlEvolucionProcedimientos"/> class.
        /// </summary>
        
        public UserControlEvolucionProcedimientos()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(UserControlEvolucionProcedimientos_Loaded);
            
            
        }
        #endregion

        #region Eventos
        /// <summary>
        /// Handles the Loaded event of the UserControlEvolucionProcedimientos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void UserControlEvolucionProcedimientos_Loaded(object sender, RoutedEventArgs e)
        {
            if (CmbxSesiones.ItemsSource != null)
                if (((ObservableCollection<int>)CmbxSesiones.ItemsSource).Count >= Variables_Globales.SesionActual)
                {
                    CmbxSesiones.SelectedIndex = Variables_Globales.SesionActual-1;
                }
            CargarGridEvolucion(Variables_Globales.SesionActual);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Cargar grilla de evolucion.
        /// </summary>
        /// <param name="SesionActual">The sesion actual.</param>
        private void CargarGridEvolucion(short SesionActual)
        {
            //List<ProcedimientosGrillaPlanTratamiento> gridEvolucion;
            //Variables_Globales.SesionActual = SesionActual;

            //foreach (var pivote in (this.DataContext as ViewModels.PacienteOdontogramaViewModel).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar.Where(a=>a.NumeroSesionesValor != 0))
            //{
            //    if (pivote.PlanTratamientoEntity.Legalizacion != null)
            //    {
            //        pivote.Legalizado = true;
            //    }                
            //    pivote.Realizado = pivote.PlanTratamientoEntity.EstadoProcedimiento;
            //    pivote.Sesion = pivote.PlanTratamientoEntity.SesionesPlanTratamiento;
              
            //}

            //gridTratamientos.ItemsSource = null;
            //gridEvolucion = (this.DataContext as ViewModels.PacienteOdontogramaViewModel).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar.ToList();

           // gridEvolucion.ForEach(a =>
           //     {
           //         if (a.FinalidadesProcedimientoValor == null)
           //         {
           //             a.FinalidadesProcedimientoValor = a.FinalidadesProcedimiento.FirstOrDefault(p => p.Identificador == Variables_Globales.IdFinalidadProcedimiento);
           //         }
           //     });
                
           // for (int i = gridEvolucion.Count - 1; i >= 0; i--)
           // {
           //     if (gridEvolucion[i].Sesion != null)
           //     {
           //         if (gridEvolucion[i].Sesion.Where(p => p.IdSesion == Variables_Globales.SesionActual).Count() <= 0)
           //         {
           //             gridEvolucion.Remove(gridEvolucion[i]);
           //         }
           //         else if (gridEvolucion[i].NumeroSesionesValor == 0)
           //         {
           //             gridEvolucion.Remove(gridEvolucion[i]);
           //         }
           //     }
           //     else
           //     {
           //         gridEvolucion.Remove(gridEvolucion[i]);
           //     }
           // }

           // List<int> lstSesiones = new List<int>();
           //     gridEvolucion.ForEach(a=>
           //     {
           //         if (a.PlanTratamientoEntity.SesionesPlanTratamiento != null)
           //         { 
           //         foreach(var pivote in a.PlanTratamientoEntity.SesionesPlanTratamiento)
           //         {
           //             if(pivote.IdSesion == Variables_Globales.SesionActual+1)
           //             {
           //                 lstSesiones.Add(a.ProcedimientoEntity.Identificador);        
           //             }
           //         }
           //         }
           //     });

           // (this.DataContext as ViewModels.PacienteOdontogramaViewModel).mainViewModel.MainPage.IdTratamiento = (this.DataContext as ViewModels.PacienteOdontogramaViewModel).TratamientoPadre.Identificador.ToString();
           //// (this.DataContext as ViewModels.PacienteOdontogramaViewModel).mainViewModel.MainPage.OrdenesSiguienteSesion = lstSesiones;
           // (this.DataContext as ViewModels.PacienteOdontogramaViewModel).mainViewModel.MainPage.MainPage_ProcedimientosPrimeraSession(null, null);
           // gridTratamientos.ItemsSource = new System.Windows.Data.PagedCollectionView(gridEvolucion);
        }

        /// <summary>
        /// HyprlnkBttn que carga el control de Bodega.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HyprlnkBttnBodega_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Odontologia.PopUp.Bodega Bodega = new PopUp.Bodega();
            //ParametrosInsumos parametros = new ParametrosInsumos();
            //if (((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).PlanTratamientoEntity.Articulos != null && ((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).PlanTratamientoEntity.Articulos.Any() && ((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).PlanTratamientoEntity.AplicacionesCanasta != null)
            //{
            //        parametros.EstadoControl = Std.EstadosEntidad.Original;
            //        parametros.AplicacionesCanasta = ((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).PlanTratamientoEntity.AplicacionesCanasta;   
            //}
            //else
            //{ 
            
            //parametros.AplicacionesCanasta = new Entities.Parametrizacion.AplicacionesCanastaDtoCollection();
            //parametros.IdBodega = Variables_Globales.IdBodega;
            //parametros.IdIps = Variables_Globales.IdIps;
            //parametros.EstadoControl = Std.EstadosEntidad.Creado;
            //parametros.IdPaciente = Variables_Globales.IdPaciente;
            //parametros.IdPx = ((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).ProcedimientoEntity.Procedimiento.Identificador;
            //}
            //parametros.IdPlanTratamiento = ((ProcedimientosGrillaPlanTratamiento)(this.gridTratamientos.ActiveCell.Row.Data)).PlanTratamientoEntity.Identificador;
            //Bodega.ctrlBodega.DataContext = parametros;
            //Bodega.DataContext = this.DataContext;
            //Bodega.CerrarVentanaEvento();
            //Bodega.Show();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CmbxSesiones.SelectedItem != null)
            {
                CargarGridEvolucion((short.Parse(CmbxSesiones.SelectedItem.ToString())));
            }
            else
            {
                CargarGridEvolucion(Variables_Globales.SesionActual);
            }
        }
               
        #endregion

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {   
            //CheckBox ch = sender as CheckBox;
            //var item = ch.DataContext as ProcedimientosGrillaPlanTratamiento;

            //if (item.NombreSuperficieApp == "Superficie1")
            //{
            //    item.Odontograma.realizadoSuperficie1 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie2")
            //{
            //    item.Odontograma.realizadoSuperficie2 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie3")
            //{
            //    item.Odontograma.realizadoSuperficie3 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie4")
            //{
            //    item.Odontograma.realizadoSuperficie4 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie5")
            //{
            //    item.Odontograma.realizadoSuperficie5 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie6")
            //{
            //    item.Odontograma.realizadoSuperficie6 = true;
            //}
            //else if (item.NombreSuperficieApp == "Superficie7")
            //{
            //    item.Odontograma.realizadoSuperficie7 = true;
            //}
            //else if (item.NombreSuperficieApp == "PiezaCompleta")
            //{
            //    item.Odontograma.realizadoPiezaCompleta = true;
            //}
        }
        
    }
}