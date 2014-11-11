using Cnt.Std;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cnt.Panacea.Entities.Odontologia;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.General;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental
{
    // Este formulario carga los datos que estan fuera de la grilla
    // La grilla se carga en un control que se llama Wizard adentro de la vista a la que hacer
    // referencia este View Model
    public partial class UserControlGuardarPlanTratamiento : ViewModelBase, IDisposable
    {
        public UserControlGuardarPlanTratamiento()
        {
            if (IsInDesignMode)
            {

            }
            else
            {
                //datosPruebaComprobantes();
                TratamientoPadre = Variables_Globales.TratamientosPadre;
                ValidarSesionesCommand = new RelayCommand(ValidarSesionesConfiguradas);
                CalculoValoresTratamientoCommand = new RelayCommand(CalculoValoresTratamiento);                
                ValorCuotaInicialTratamientoCommand = new RelayCommand<string>(CuotaInicialTratamiento);
                FormaPagoOdontologia = typeof(Cnt.Panacea.Entities.Odontologia.FormaPago).ToExtendedList<Cnt.Panacea.Entities.Odontologia.FormaPago, byte>() as List<KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>>;
                FormaPagoOdontologia.RemoveAt(0);
                cargarComprobantes();                
            }
        }


        # region Metodos
        public void inicializar()
        {
            inicializarElementosReferentesPago();
            PuedeModificar = true;
            RaisePropertyChanged("PuedeModificar");
        }   
      

        #endregion

        #region Propiedades
        

        public ObservableCollection<ProcedimientosGrillaPlanTratamiento> ListadoGrillaPlanTratamiento = new ObservableCollection<ProcedimientosGrillaPlanTratamiento>();    

        public List<OdontogramaEntity> lstOdontogramaEntity { get; set; }

        public bool PuedeModificar { get; set; }

        public RelayCommand ValidarSesionesCommand { get; set; }

        #endregion

        public void Dispose()
        {

        }
    }
}
