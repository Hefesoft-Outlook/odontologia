using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.General;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento
{
    public partial class GridPlanTratamientoProcedimientosWizard : ViewModelBase
    {
        public GridPlanTratamientoProcedimientosWizard()
        {
            if (IsInDesignMode)
            {
                Listado = new ObservableCollection<ProcedimientosGrillaPlanTratamiento>();
                Listado.Add(new ProcedimientosGrillaPlanTratamiento()
                {
                    Odontograma = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma("1") { codigoSPiezaDental = "1" },
                    NombreSuperficie = "Distal",
                    NombreProcedimiento = "Un nombre",
                    ProcedimientoEntity = new ConfiguracionProcedimientoOdontologiaEntity() { NombreProcedimiento = "Un nombre" },
                    ProcedimientosEspecialidad = new ObservableCollection<EspecialidadEntity>() { },
                    ProcedimientosEspecialidadValor = new EspecialidadEntity(),
                    NumeroSesiones = new ObservableCollection<int>(),
                    NumeroSesionesValor = 2,
                    NumeroSesionesProcedimiento = 4,
                    ValorProcedimiento = 1000,
                    ValorPaciente = 1000,
                });
            }
            else
            {
                //datosPruebaOdontologosHigienistas();

                var planTratamiento = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento>();
                OdontologosIps = planTratamiento.OdontologosIps;
                HigientistasIps = planTratamiento.HigientistasIps;
                Listado = planTratamiento.Listado;

                cambiarConvenioCommand = new RelayCommand<ProcedimientosGrillaPlanTratamiento>(cambiarConvenio);
                opcionTratamiento();
                procedimientosEspecialidad();
                oirPedidoListaGrilla();
                inicializarElementos();
            }
        }


        #region metodos
        public void CalculoValoresTratamiento()
        {
            NumeroSesionesTratamiento = short.Parse(Listado.Sum(a => a.NumeroSesionesProcedimiento).ToString());
            ValorTotalTratamiento = Decimal.Parse(Listado.Where(a => a.Cobra == true).Sum(a => a.ValorPaciente).ToString());
            
            if (TieneCuotaInicial)
            {
                ValorCuotaTratamiento = ((ValorTotalTratamiento - ValorCuotaInicial) / NumeroSesionesTratamiento);
            }
            else
            {
                if (NumeroSesionesTratamiento > 0)
                {
                    ValorCuotaTratamiento = (ValorTotalTratamiento / NumeroSesionesTratamiento);
                }
            }

            HabilitarControlesPago = true;
            RaisePropertyChanged("NumeroSesionesTratamiento");
            RaisePropertyChanged("HabilitarControlesPago");
            RaisePropertyChanged("ValorCuotaTratamiento");
            RaisePropertyChanged("ValorTotalTratamiento");
        }


        private void procedimientosEspecialidad()
        {
            if (Variables_Globales.ProcedimientosEspecialidad != null)
            {
                ProcedimientosEspecialidad = Variables_Globales.ProcedimientosEspecialidad.obtenerEspecialidades().ToObservableCollection();
                RaisePropertyChanged("ProcedimientosEspecialidad");
            }
        }

        internal async void inicializarElementos()
        {
            // Llamada en paralello esperamos hasta que se carguen los 3 elementos antes de continuar
            await TaskEx.WhenAll
                (
                    consultarConvenio(),
                    consultarPrestador()
                );
            
            //Pinta con colores la pieza dental del procedimiento para que sea mas visual
            pintarProcedimientosColoresPiezadental();
        }

        

        private void opcionTratamiento()
        {
            OpcionTratamiento = EnumHelper.GetValues<OpcionesTratamiento>().ToObservableCollection();
        }

        private void cambiarConvenio(ProcedimientosGrillaPlanTratamiento item)
        {
            //Pedimos que se muestre una ventana para cambiar de convenios
            //Y recibimos el resultado
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Convenio",
                    Resultado =(obj)=> 
                    { 
                        item.OdontogramaEntity.PlanTratamiento.Convenio = (short)obj;
                    }
                }
                );
        }

        private void oirPedidoListaGrilla()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Pedir_Datos_Grilla_Plan_Tratamiento>(this, pedido =>
            {
                var result = Listado.validaListadoGrilla();

                if (result.valido)
                {
                    pedido.lst(Listado);
                }
                else
                {
                    if (pedido.fallido != null)
                    {
                        pedido.fallido(result);
                    }
                }
            });
        }

        public async Task consultarConvenio()
        {
            ConvenioAtencion = await Contexto_Odontologia.obtenerContexto().consultarConvenio(Variables_Globales.IdConvenio);
        }

        public async Task consultarPrestador()
        {
            Prestador = await Contexto_Odontologia.obtenerContexto().ConsultarPrestador(Variables_Globales.IdPrestador);//LFDO Bug 16006            
        }        

        #endregion

        #region Propiedades
        private ObservableCollection<ProcedimientosGrillaPlanTratamiento> listado = new ObservableCollection<ProcedimientosGrillaPlanTratamiento>();

        public ObservableCollection<ProcedimientosGrillaPlanTratamiento> Listado
        {
            get 
            { 
                return listado; 
            }
            set 
            { 
                listado = value;  
                RaisePropertyChanged("Listado");

                if(value.Any())
                {
                    //Este elemento se usa para dejar seleccionado el primer elemento de la lista
                    ElementoSeleccionado = value.FirstOrDefault();
                    RaisePropertyChanged("ElementoSeleccionado");
                }
            }
        }


        public ObservableCollection<int> NumeroSesiones { get; set; }

        public ObservableCollection<TerceroEntity> OdontologosIps { get; set; }

        public ObservableCollection<TerceroEntity> HigientistasIps { get; set; }

        public TerceroEntity Prestador { get; set; }

        public ConvenioEntity ConvenioAtencion { get; set; }

        public short NumeroSesionesTratamiento { get; set; }

        public decimal ValorTotalTratamiento { get; set; }

        public bool TieneCuotaInicial { get; set; }

        public decimal ValorCuotaInicial { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        public bool HabilitarControlesPago { get; set; }

        public ObservableCollection<PacienteConvenioEntity> ConveniosPaciente { get; set; }

        public bool MuestraConveniosPaciente { get; set; }

        public RelayCommand<ProcedimientosGrillaPlanTratamiento> cambiarConvenioCommand { get; set; }

        public ObservableCollection<OpcionesTratamiento> OpcionTratamiento { get; set; }

        /// <summary>
        /// Especialidades para esta superficie
        /// </summary>
        public ObservableCollection<EspecialidadEntity> ProcedimientosEspecialidad { get; set; }

        #endregion

        public ProcedimientosGrillaPlanTratamiento ElementoSeleccionado { get; set; }
    }
}
