using System;
using System.Windows.Input;
using Cnt.Panacea.Xap.Odontologia;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight.Command;
using Cnt.Panacea.Xap.Odontologia.Recursos;
using System.Globalization;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Clases;
using System.Linq;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Std;
using System.Windows;
using System.Collections.Generic;
using Proxy;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using Microsoft.Practices.ServiceLocation;


namespace Cnt.Panacea.Xap.ViewModels
{
    public class PacienteTratamientosViewModel : ViewModelBase, IDisposable
    {

        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private PacienteEntity paciente;        
        #endregion Variables

        #region Constructor / Destructor

        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// </summary>        
        public PacienteTratamientosViewModel()
        {
            try
            {   
                nuevoTratamientoCommand = new RelayCommand(nuevoTratamiento);
                seleccionarTratamientoCommand = new RelayCommand<TratamientoEntity>(seleccionarTratamiento);
                odontogramaSinTratamientoCommand = new RelayCommand<OdontogramasPacienteEntity>(odontogramaSinTratamiento);
                cargarDatosPaciente();
                oirNuevoElementoAgregado();
            }
            catch (Exception ex)
            {
                //Mostrar mensaje ex
            }

        }

        /// <summary>
        /// Sucede cuando se agrega un nuevo odontograma y deseamos que se muestre en el listado inicial
        /// </summary>
        private void oirNuevoElementoAgregado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<TratamientoEntity>(this, "Agregar Listado inicial",item => 
            {
                if (tratamientosPaciente == null)
                {
                    tratamientosPaciente = new ObservableCollection<TratamientoEntity>();
                }

                tratamientosPaciente.Add(item);
            });
        }

        public async void odontogramaSinTratamiento(OdontogramasPacienteEntity odonto)
        {
            Variables_Globales.NuevoOdontograma = false;            
            Variables_Globales.IdTratamientoActivo = odonto.Tratamiento;
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma() { Odontogra_Cargar = "Inicial" });
            Messenger.Default.Send(new Cerrar_Pop_Up_Generico() { });
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos() { valor = "Todos" });
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente());

            Busy.UserControlCargando();
            Variables_Globales.TratamientosPadre = await Contexto_Odontologia.obtenerContexto().SeleccionarTratamientoActivo(Variables_Globales.IdTratamientoActivo);
            modoLectura();
            Busy.UserControlCargando(false);
        }

        private static void modoLectura()
        {
            if (Variables_Globales.TratamientosPadre.EstadoTratamiento == EstadoTratamiento.Terminacion || Variables_Globales.TratamientosPadre.EstadoTratamiento == EstadoTratamiento.Abandono)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Modo_Lectura.Modo_Lectura() { Solo_Lectura = true });
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Modo_Lectura.Modo_Lectura() { Solo_Lectura = false });
            }
        }

        public async void seleccionarTratamiento(TratamientoEntity tratamiento)
        {
            Variables_Globales.NuevoOdontograma = false;            
            Variables_Globales.OdontogramaPacientetity = tratamiento.IdOdontogramaInicial;
            Variables_Globales.IdTratamientoActivo = tratamiento.Identificador;
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma() { Odontogra_Cargar = "Inicial" });
            Messenger.Default.Send(new Cerrar_Pop_Up_Generico() { });
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos() { valor = "Todos" });
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente());

            //Con esta variable se hacen la mayoria de calculos
            Busy.UserControlCargando(true, "Cargando tratamientos guardados");
            Variables_Globales.TratamientosPadre = await Contexto_Odontologia.obtenerContexto().SeleccionarTratamientoActivo(Variables_Globales.IdTratamientoActivo);
            modoLectura();
            Busy.UserControlCargando(false);   
        }        

        #endregion Constructor / Destructor

        #region Propiedades

        /// <summary>
        /// Almacena el id del paciente para cargar sus datos
        /// </summary>
        private long identificadorPaciente;

        public long IdentificadorPaciente
        {
            get { return identificadorPaciente; }
            set
            {   
                identificadorPaciente = value;
                if (value != 0)
                {
                    Variables_Globales.IdPaciente = Convert.ToInt32(value);
                }
            }
        }
        


        /// <summary>
        /// Almacena los tratamientos de un paciente
        /// </summary>
        public ObservableCollection<TratamientoEntity> tratamientosPaciente { get; private set; }
        /// <summary>
        /// Gets or sets the tipos tratamiento.
        /// </summary>
        /// <value>The tipos tratamiento.</value>
        public ObservableCollection<TipoTratamientoEntity> TiposTratamiento { get; private set; }

        /// <summary>
        /// Habilita el nuevo tratamiento dependiendo si un usuario es clasificador odontologico 
        /// </summary>
        public bool HabilitarNuevoTratamiento { get; private set; }

        /// <summary>
        /// Habilita el nuevo tratamiento dependiendo si un usuario es clasificador odontologico
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [modificar plan tratamiento]; otherwise, <c>false</c>.
        /// </value>
        public bool ModificarPlanTratamiento { get; set; }

        /// <summary>
        /// Propiedad que almacena los odontogramas sin tratamiento que tiene un paciente
        /// </summary>
        public ObservableCollection<OdontogramasPacienteEntity> OdontogramasSinTratamiento { get; set; }      



        #endregion Propiedades

        #region Commands

        /// <summary>
        /// Se dispara cuando se crea un nuevo tratamiento, se dirige a la pestaña odontograma inicial
        /// </summary>
        public RelayCommand odontogramasPacienteCommand { get; private set; }

        #endregion

        #region Metodos

        /// <summary>
        /// Carga la informacion del paciente en el control paciente, y carga los datagrid
        /// con los tratamientos y los odontogramas de un paciente 
        /// </summary>OdontogramasSinTratamiento
        public async void cargarDatosPaciente()
        {
            IdentificadorPaciente = Variables_Globales.IdPaciente;
            RaisePropertyChanged("IdentificadorPaciente");
            CargarParametrosConvenio();//LFDO Bug 14541 Se realiza el llamado al iniciar la navegacion

            await cargarTratamientosPaciente();
            await odontogramasSinTratamiento();
        
            //Vamos a obtener el valor actual del paciente
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente());
        }

        private async System.Threading.Tasks.Task odontogramasSinTratamiento()
        {
            OdontogramasSinTratamiento = await Contexto_Odontologia.obtenerContexto().ListarOdontogramasSinTratamiento(Variables_Globales.IdPaciente, Variables_Globales.IdIps);            
            RaisePropertyChanged("OdontogramasSinTratamiento");
        }

        private async System.Threading.Tasks.Task cargarTratamientosPaciente()
        {
            try
            {
                tratamientosPaciente = await Contexto_Odontologia.obtenerContexto().ConsultarTratamientosPaciente(Variables_Globales.IdIps, Variables_Globales.IdPaciente);

                Variables_Globales.TratamientosPaciente = new System.Collections.Generic.List<long>();
                foreach (TratamientoEntity item in tratamientosPaciente)
                {
                    if (item.EstadoTratamiento != EstadoTratamiento.Abandono 
                        && item.EstadoTratamiento != EstadoTratamiento.Terminacion
                        && item.TipoTratamiento != null
                        && item.TipoTratamiento.Identificador != 0 )
                    {
                        Variables_Globales.TratamientosPaciente.Add(item.TipoTratamiento.Identificador);
                    }
                    Variables_Globales.TratamientosPaciente = Variables_Globales.TratamientosPaciente.Distinct().ToList();
                }

                RaisePropertyChanged("tratamientosPaciente");
            }
            catch (Exception ex)
            {

            }
        }
    

        private void nuevoTratamiento()
        {
            if ((Variables_Globales.ParametroConvenio.RequiereClasificador && Variables_Globales.ClasificadorOdontologico)//LFDO Bug 15568
               || !Variables_Globales.ParametroConvenio.RequiereClasificador)
            {                
                Variables_Globales.Paciente.EdadPaciente = Variables_Globales.Paciente.EdadPaciente;
                //Mensaje Nuevo odontograma
                //cargarOdontogramainicial(0, 0);

                // Crear un nuevo tratamiento
                Variables_Globales.IdTratamientoActivo = 0;
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma() { Odontogra_Cargar = "Nuevo" });
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente());
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Requiere clasificador"                    
                });
            }
        }

        /// <summary>
        /// Cargar los parametros de acuerdo al convenio.
        /// </summary>
        public async void CargarParametrosConvenio()
        {
            var Convenio = await Contexto_Odontologia.obtenerContexto().ListarParametrosConvenio(Variables_Globales.IdIps, Variables_Globales.IdConvenio);

            if (Convenio != null)
            {
                ParametroOdontologicoConvenioEntity parametro = Convenio.FirstOrDefault(f => f.Convenio.Identificador == Variables_Globales.IdConvenio);
                if (parametro != null)
                {
                    Variables_Globales.ParametroConvenio = parametro;
                    Variables_Globales.MaximaCantidadProcedimientosSesion = parametro.MaximoProcedimientosSesion;
                }
                else
                {
                    Variables_Globales.ParametroConvenio = new ParametroOdontologicoConvenioEntity();
                }
            }
            else
            {
                Variables_Globales.ParametroConvenio = new ParametroOdontologicoConvenioEntity();
            }
        }
       

        #endregion metodos

        #region Commands
        public RelayCommand nuevoTratamientoCommand { get; set; }

        public RelayCommand<TratamientoEntity> seleccionarTratamientoCommand { get; set; }

        public RelayCommand<OdontogramasPacienteEntity> odontogramaSinTratamientoCommand { get; set; }
        #endregion

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<TratamientoEntity>(this, "Agregar Listado inicial");
        }
    }
}
