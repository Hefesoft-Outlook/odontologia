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
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class UserControlGridPlanTratamientoProcedimientosWizard : UserControl
    {
        #region Variables
        
        //Cnt.Panacea.Xap.Odontologia.Assets.Grillas.Wizard_Plan_de_tratamiento.vm.GridPlanTratamientoProcedimientosWizard vm;
        #endregion

        #region Constructor
       
        public UserControlGridPlanTratamientoProcedimientosWizard()
        {
            InitializeComponent();
            //vm = this.DataContext as Cnt.Panacea.Xap.Odontologia.Assets.Grillas.Wizard_Plan_de_tratamiento.vm.GridPlanTratamientoProcedimientosWizard;
        }

        #endregion        


        #region Metodos

        
        //public void filtrarPiezaDentalEvoluvion(Odontologia.Clases.Odontograma PiezaDental)
        //{
        //    //ObservableCollection<ProcedimientosGrillaPlanTratamiento> lst = ((Cnt.Panacea.Xap.ViewModels.PacienteOdontogramaViewModel)(this.DataContext)).OdontogramaDatacontexto.LstOdontograma.First().ListaProcedimientosPlanTratamientoGuardar;
        //    //gridTratamientos.ItemsSource = null;
        //    //gridTratamientos.ItemsSource =  new System.Windows.Data.PagedCollectionView(lst.Where(a => a.Odontograma.codigoPiezaDental == PiezaDental.codigoPiezaDental));
        //}
        
        #endregion

        

    }
}