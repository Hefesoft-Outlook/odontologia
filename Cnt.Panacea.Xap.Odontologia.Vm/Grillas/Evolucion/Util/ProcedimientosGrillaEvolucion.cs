using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util
{
    public class ProcedimientosGrillaEvolucion : ViewModelBase
    {
        #region Variables
        private decimal valorPaciente;
        private decimal valorProcedimiento;
        private ConvenioEntity convenio;
        private Cnt.Panacea.Entities.Odontologia.OpcionesTratamiento opcionesTratamientoValor = new OpcionesTratamiento();
        ConfiguracionProcedimientoOdontologiaEntity procedimientoEntity = new ConfiguracionProcedimientoOdontologiaEntity();
        bool cobro = true;
        string color = "Transparent";
        TerceroEntity odontologosIpsValor = new TerceroEntity();
        FinalidadProcedimientoEntity finalidadesProcedimientoValor;
        TerceroEntity higienistasIpsValor = new TerceroEntity();
        Cnt.Panacea.Entities.Odontologia.SesionesPlanTratamientosCollection sesion = new SesionesPlanTratamientosCollection();
        EspecialidadEntity procedimientosEspecialidadValor = new EspecialidadEntity();


        #endregion

        public ProcedimientosGrillaEvolucion()
        {

        }




        #region Propiedades
        /// <summary>
        /// Superficie a la cual se le ingresaran los datos
        /// </summary>
        private Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma odontograma = new Odontograma.Odontograma();

        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Odontograma
        {
            get { return odontograma; }
            set 
            { 
                odontograma = value; 
                RaisePropertyChanged("Odontograma"); 
            }
        }
        


        /// <summary>
        /// Nombre de la superficie
        /// </summary>
        public string NombreSuperficie { get; set; }
        /// <summary>
        /// Alias usado al interior de la palicacion
        /// </summary>
        public string NombreSuperficieApp { get; set; }
        /// <summary>
        /// Procedimientos para esta superficie
        /// </summary>
        public ConfiguracionProcedimientoOdontologiaEntity ProcedimientoEntity { get { return procedimientoEntity; } set { procedimientoEntity = value; RaisePropertyChanged("ProcedimientoEntity"); } }
        /// <summary>
        /// Opciones de tratamiento para esta superficie
        /// </summary>

        public short OpcionesTratamiento { get; set; }

        /// <summary>
        /// Especialidades para esta superficie
        /// </summary>
        public ObservableCollection<EspecialidadEntity> ProcedimientosEspecialidad { get; set; }

        /// <summary>
        /// Esta propiedad se usa para el key del xam grid
        /// </summary>
        public short OdontologosIps { get; set; }
        /// <summary>
        /// Esta propiedad se usa para el key del xam grid
        /// </summary>
        public short HigienistasIps { get; set; }


        /// <summary>
        /// Listado de finalidades de procedimiento
        /// </summary>
        public ObservableCollection<FinalidadProcedimientoEntity> FinalidadesProcedimiento { get; set; }
        /// <summary>
        /// Listado de numero de sessiones
        /// </summary>
        public ObservableCollection<int> NumeroSesiones { get; set; }
        /// <summary>
        /// Plan de tratamiento para el procedimiento
        /// </summary>
        private PlanTratamientoEntity planTratamientoEntity = new PlanTratamientoEntity();

        public PlanTratamientoEntity PlanTratamientoEntity
        {
            get { return planTratamientoEntity; }
            set { planTratamientoEntity = value; RaisePropertyChanged("PlanTratamientoEntity"); }
        }
        

        /// <summary>
        /// Opciones de procedimiento seleccionado
        /// </summary>
        public Cnt.Panacea.Entities.Odontologia.OpcionesTratamiento OpcionesTratamientoValor
        {
            get
            {
                return Entities.Odontologia.OpcionesTratamiento.Tratamiento;
            }
            set
            {
                if (value == Entities.Odontologia.OpcionesTratamiento.Ninguno)
                {
                    throw new ArgumentException("La opción seleccionada es incorrecta");
                }
                else
                {
                    opcionesTratamientoValor = value;
                    OdontogramaEntity.PlanTratamiento.OpcionTratamiento = Int16.Parse(value.GetHashCode().ToString());
                    RaisePropertyChanged("OpcionesTratamientoValor");
                }
            }
        }

        /// <summary>
        /// Especialidad seleccionada
        /// </summary>
        public EspecialidadEntity ProcedimientosEspecialidadValor
        {
            get { return procedimientosEspecialidadValor; }
            set
            {
                procedimientosEspecialidadValor = value;

                RaisePropertyChanged("ProcedimientosEspecialidadValor");
            }
        }
        /// <summary>
        /// Odontologo seleccionado
        /// </summary>
        public TerceroEntity OdontologosIpsValor
        {
            get { return odontologosIpsValor; }
            set
            {
                odontologosIpsValor = value;

                if (odontologosIpsValor == null)
                {
                    OdontogramaEntity.PlanTratamiento.PrestadorOdontologo = null;
                }
                else
                {
                    OdontogramaEntity.PlanTratamiento.PrestadorOdontologo = odontologosIpsValor.Identificador;
                }

                RaisePropertyChanged("OdontologosIpsValor");
            }
        }
        /// <summary>
        /// Finalidad seleccionada
        /// </summary>
        public FinalidadProcedimientoEntity FinalidadesProcedimientoValor
        {
            get { return finalidadesProcedimientoValor; }
            set
            {
                finalidadesProcedimientoValor = value;
                RaisePropertyChanged("FinalidadesProcedimientoValor");
            }
        }
        /// <summary>
        /// Higienista seleccionado
        /// </summary>
        public TerceroEntity HigienistasIpsValor
        {
            get
            {
                return higienistasIpsValor;
            }
            set
            {
                higienistasIpsValor = value;
                if (value == null)
                {
                    OdontogramaEntity.PlanTratamiento.PrestadorHigienista = null;
                }
                else
                {
                    OdontogramaEntity.PlanTratamiento.PrestadorHigienista = value.Identificador;
                }

                RaisePropertyChanged("HigienistasIpsValor");
            }
        }

        //Para los key del xamgrid
        public int OdontologosHigienistasIps { get; set; }

        /// <summary>
        /// Seleccion de la mezcla de la lista de higienista y odontologo en odontograma evolucion
        /// </summary>
        public TerceroEntity OdontologosHigienistasIpsValor { get; set; }
        /// <summary>
        /// Numero de session seleccionado
        /// </summary>
        public int NumeroSesionesValor { get; set; }
        /// <summary>
        /// Variable de contro sobre si se encuentra seleccionadio el registro
        /// </summary>
        public bool Seleccionar { get; set; }
        /// <summary>
        /// El procedimiento tiene convenio
        /// </summary>
        public bool Convenio { get; set; }
        /// <summary>
        /// El procedimiento se cobrara
        /// </summary>
        public bool Cobra { get { return cobro; } set { cobro = value; RaisePropertyChanged("Cobra"); } }
        /// <summary>
        /// Numero de sessiones para el procedimiento
        /// </summary>

        private short numeroSesionesProcedimiento;

        public short NumeroSesionesProcedimiento
        {
            get { return numeroSesionesProcedimiento; }
            set
            {
                numeroSesionesProcedimiento = value;
                OdontogramaEntity.PlanTratamiento.NumeroSesionesProcedimiento = value;
                OdontogramaEntity.numeroSessiones();
            }
        }



        /// <summary>
        /// Valor para el procedimiento que se mostrara en la grilla
        /// </summary>
        public decimal ValorProcedimiento { get { return valorProcedimiento; } set { valorProcedimiento = value; RaisePropertyChanged("ValorProcedimiento"); } }
        /// <summary>
        /// Valor para el paciente que se mostrara en la grilla
        /// </summary>
        public decimal ValorPaciente { get { return valorPaciente; } set { valorPaciente = value; RaisePropertyChanged("ValorPaciente"); } }
        /// <summary>
        /// Tercero al cual se le realiza el procedimiento
        /// </summary>
        public ConvenioEntity ConvenioAtencion { get { return convenio; } set { convenio = value; RaisePropertyChanged("ConvenioAtencion"); } }

        /// Tercero al cual se le realiza el procedimiento
        /// </summary>
        public TerceroEntity PrestadorAtencion { get; set; }

        /// <summary>
        /// Numero de sessiones para este procedimiento
        /// </summary>
        public string SesionesProcedimiento { get; set; }
        /// <summary>
        /// Observaciones que se realizan en el odontograma evolucion
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// El procedimiento se encuentra realizado
        /// </summary>
        public bool Realizado { get; set; }  
      
        /// <summary>
        /// Numero de factura asociado al procedimiento
        /// </summary>

        public String NoFactura { get; set; }
        /// <summary>
        /// Fecha en la que se realizo el procedimiento
        /// </summary>
        public String FechaRealizado { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String UsuarioRecibe { get; set; }
        /// <summary>
        /// Nombre del procedimiento a realizar
        /// </summary>
        public String NombreProcedimiento { get; set; }
        /// <summary>
        /// Variable que se usa para colorear los procedimientos que se han de realizar en la session seleccionada
        /// </summary>
        public string ColorearSessionActual { get { return color; } set { color = value; RaisePropertyChanged("ColorearSessionActual"); } }
        /// <summary>
        /// Numero de sessiones que tiene asociada el procedimiento
        /// </summary>
        public Cnt.Panacea.Entities.Odontologia.SesionesPlanTratamientosCollection Sesion { get { return sesion; } set { sesion = value; RaisePropertyChanged("Sesion"); } }
        /// <summary>
        /// Establece la fecha de realizacion del procedimiento
        /// </summary>
        public DateTime? FechaRealizacion { get; set; }
        /// <summary>
        /// Si ya esta realizado en una sesion Anterior
        /// </summary>
        public bool RealizadoAnterior { get; set; }
        /// <summary>
        /// Define que este legalizado el procedimiento
        /// </summary>
        public bool Legalizado { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [procedimiento aplicado].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [procedimiento aplicado]; otherwise, <c>false</c>.
        /// </value>
        bool procedimientoAplicado = false;
        public bool ProcedimientoAplicado
        {
            get
            {
                return procedimientoAplicado;
            }
            set
            {
                procedimientoAplicado = value;
                RaisePropertyChanged("ProcedimientoAplicado");
            }
        }

        public OdontogramaEntity OdontogramaEntity { get; set; }

        public ConfigurarDiagnosticoProcedimOtraEntity ConfigurarDiagnosticoProcedimOtraEntity { get; set; }

        #endregion        
    }
}
