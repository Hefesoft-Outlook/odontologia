using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using Cnt.Std;
using System.Collections.ObjectModel;
using System.Globalization;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;


namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    public class Plan_Tratamiento : ViewModelBase, IDisposable
    {
        #region Constructor
        public Plan_Tratamiento()
        {
            if (IsInDesignMode)
            {
            }
            else
            {                
                oirCambioOdontograma();

                //Muestra los Procedimientos activos
                mostrarGrillaTratamientosCommand = new RelayCommand(mostrarGrillaTratamientos);
                
                //Captura el texto descripcion de plan de tratamiento
                DescripcionTratamientoCommand = new RelayCommand<string>(DescripcionTratamientoOdontologia);
                
                //Muestra la ventana donde se configuran los procedimientos seleccionados
                siguientePasoCommand = new RelayCommand(siguientePaso);
            }
        }
        

        //Como Este control esta pintado en la pantalla inicial
        //Debe actualizarse cuando se mueva de pestana a pestana
        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, incializarElementosPlanTratamiento);
        }

        private void incializarElementosPlanTratamiento(Cambiar_Tipo_Odontograma item)
        {
            if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
            {
                //Se limpian los elementos que puedan estar inicializados
                TratamientoPadre = null;
                TratamientoPadre = new TratamientoEntity();

                TratamientoPadre = Variables_Globales.TratamientosPadre;
                inicializarElementos();
                cargarNumeroSessiones();
                pedirDiagnosticosProcedimientosOdontogramaInicial();
                ValidarModoLectura();
            }
        }

        private void mostrarGrillaTratamientos()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana() 
            {
                Nombre = "Tratamientos",
                Propiedad_Adicional = this.TratamientoPadre
            });            
        }

        private async Task cargarOdontogramaPlanTratamiento()
        {
            // La validacion indica si es un nuevo plan de tratamiento
            if (Variables_Globales.IdTratamientoActivo != 0)
            {
                Busy.UserControlCargando(true, "Cargando plan de tratamiento");
                var resultado = await Contexto_Odontologia.ListarOdontogramaTratamiento(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);
                Messenger.Default.Send(new Pedir_Pintar_Datos() { lst = resultado, Limpiar_Datos = false });
                Busy.UserControlCargando(false);
            }
        }
        #endregion

        #region metodos

        private void siguientePaso()
        {
            //Le pedimos al mapa dental que valide si cada diagnostico tiene un procedimiento
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental.Validaciones()
            {
                Tipo = Util.Messenger.Mapa_Dental.tipo.Cada_Superficie_Con_Diagnostico_Tiene_Procedimientos,
                //La respuesta de la validacion
                Result = resultadoValidacion
            });
            
        }

        private void resultadoValidacion(bool obj)
        {
            if (obj)
            {
                var validaciones = new Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Validaciones.Plan_Tratamiento();
                var resultado = validaciones.validarDatos(this);
               
                if(resultado.valido)
                {
                    pedirProcedimientos();
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = resultado.mensaje
                    });
                }
            }
            else
            {                
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "No todas las superficiestienen un procedimiento asociado"
                });
            }
        }        

        // Pide los procedimientos que se encuentran en el mapa dental
        private void pedirProcedimientos()
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { Pedir_Tipos_Guardar = Tipo_Odontograma.Plan_Tratamiento, lstGuardar = formatearDatos });
        }

        internal async Task<ObservableCollection<long>> guardarTratamiento(Entities.Odontologia.TratamientoEntity Padre, ComprobanteEntity comprobanteSeleccionado, List<OdontogramaEntity> lstOdontogramaEntity)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<long>>();
            TratamientoPadre = Padre;

            OdontogramasPacienteEntity OdontogramasPacienteEntity = new Entities.Odontologia.OdontogramasPacienteEntity();
            OdontogramasPacienteEntity.Tratamiento = Padre.Identificador;
            
            
            TratamientoPadre.Convenio = new ConvenioEntity() { Identificador = Variables_Globales.IdConvenio };

            TratamientoPadre.Descripcion = DescripcionTratamiento;
            
            if (JustificacionModificacionTratamiento != String.Empty)
            {
                OdontogramasPacienteEntity.JustificacionCambio = JustificacionModificacionTratamiento;
            }

            if (TratamientoPadre.IdSesionActual == null || TratamientoPadre.IdSesionActual <= 1)
            {
                TratamientoPadre.IdSesionActual = 1;
            }

            TratamientoPadre.Usuario = Variables_Globales.UsuarioActual;            
            TratamientoPadre.IPOrigen = Variables_Globales.IpCliente;
            TratamientoPadre.EstadoTratamiento = EstadoTratamiento.PlanDeTratamiento;
            TratamientoPadre.EstadoRegistro = Std.EstadosEntidad.Modificado;
            TratamientoPadre.TipoTratamiento.EstadoRegistro = EstadosEntidad.Modificado;
            TratamientoPadre.Modificado = DateTime.Now;

            PacienteEntity Paciente = new PacienteEntity() { Identificador = Variables_Globales.IdPaciente };
            var result = await Contexto_Odontologia.GuardarPlanTratamiento(TratamientoPadre, 1, OdontogramasPacienteEntity, lstOdontogramaEntity.ToObservableCollection(), EsCotizacion, Paciente, comprobanteSeleccionado, Variables_Globales.IdIps, Variables_Globales.UsuarioActual, Variables_Globales.IdSede, Variables_Globales.IdConvenio);

            tcs.SetResult(result);
            finalizarGuardarPlanTratamiento(result);
            llenarOrdenes(lstOdontogramaEntity);

            return result;
        }

        internal async Task<ObservableCollection<OdontogramaEntity>> listarOdontogramaPlanTratamiento()
        {
            var resultado = await Contexto_Odontologia.ListarOdontogramaTratamiento(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);
            
            PlanTratamientoEstadoNuevoEliminadoModificado = new PlanTratamientoEstadoNuevoEliminadoModificado();

            if (resultado.Any())
            {                
                MostrarOpcionesNuevoTratamiento = true;
                RaisePropertyChanged("MostrarOpcionesNuevoTratamiento");
                PlanTratamientoEstadoNuevoEliminadoModificado.Modificado = true;
                ValidarModoLectura();
            }
            else
            {
                ValidarModoLectura();
                PlanTratamientoEstadoNuevoEliminadoModificado.Nuevo = true;
            }

            return resultado;
            
        }

        /// <summary>
        /// Validar modo lectura.
        /// </summary>
        private void ValidarModoLectura()
        {
            try
            {
                if (!((Variables_Globales.ParametroConvenio.RequiereClasificador && Variables_Globales.ClasificadorOdontologico)//LFDO Bug 15568
                      || !Variables_Globales.ParametroConvenio.RequiereClasificador)
                    || TratamientoPadre.EstadoTratamiento == EstadoTratamiento.Terminacion || TratamientoPadre.EstadoTratamiento == EstadoTratamiento.Abandono)
                {                    
                    PuedeModificar = false;
                    RaisePropertyChanged("PuedeModificar");

                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Solo_Lectura
                    });
                }
                else if ((Variables_Globales.ParametroConvenio.RequiereClasificador && Variables_Globales.ClasificadorOdontologico)//LFDO Bug 15568
                        || !Variables_Globales.ParametroConvenio.RequiereClasificador)
                {                 
                    PuedeModificar = true;
                    RaisePropertyChanged("PuedeModificar");
                }
            }
            catch { }
        }

        /// <summary>
        /// Almacena imagenes.
        /// </summary>
        private async void GuardarImagenes()
        {
            //if (vmOdontograma.LstImagenes != null && vmOdontograma.LstImagenes.Count > 0)
            //{
            //    int x = 0;
            //    vmOdontograma.LstImagenes.ToList().ForEach(a => a.Identificador = x + 1);
            //    await Contexto_Odontologia.GuardarImagenTratamiento(Variables_Globales.IdTratamientoActivo, vmOdontograma.LstImagenes.ToObservableCollection());
            //}
        }

        private async void finalizarGuardarPlanTratamiento(ObservableCollection<long> e)
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos() { valor = "Evolucion" });            
            long idCotizacion;
            await listarOdontogramaPlanTratamiento();
            GuardarImagenes();

            if (!EsCotizacion)
            {
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Aprueba plan tratamiento",
                    Resultado = resultadoAprobacionUsuario
                });
            }
            else
            {
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Cotizacion",
                    Propiedad_Adicional = this,
                });
            }
        }

        private void resultadoAprobacionUsuario(object obj)
        {
            if ((bool)obj)
            {
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Generar plan tratamiento",
                    Propiedad_Adicional = TratamientoPadre
                });
            }
        }

        private void llenarOrdenes(List<OdontogramaEntity> lstOdontogramaEntity)
        {
            List<int> lstOrdenes = new List<int>();

            foreach (var item in lstOdontogramaEntity)
            {
                foreach (var pivote in item.PlanTratamiento.SesionesPlanTratamiento)
                {
                    if (pivote.IdSesion == 1)
                    {
                        lstOrdenes.Add(pivote.Procedimiento);
                    }
                }    
            }
        }

        private void formatearDatos(System.Collections.Generic.List<OdontogramaEntity> obj)
        {
            Listado = Listado.inicializarListaYLimpiar();
            convertirProcedimientosGrillaPlanTratamiento(obj);

            mostrarVentana();
        }

        //Se separa para poder hacer llamados desde el listado de procedimientos
        internal ObservableCollection<ProcedimientosGrillaPlanTratamiento> convertirProcedimientosGrillaPlanTratamiento(System.Collections.Generic.List<OdontogramaEntity> obj)
        {
            int i = 0;

            foreach (var item in obj)
            {
                // Valida que lo se este agregando es un procedimiento
                if (item.Procedimiento != null && item.Procedimiento.Identificador > 0)
                {
                    // Aca solo se van a guardar procedimientos
                    item.Diagnostico = null;
                    ProcedimientosGrillaPlanTratamiento elementoAgregar = new ProcedimientosGrillaPlanTratamiento();
                    elementoAgregar.Odontograma = new Odontograma(item.Diente.Identificador.ToString()) { codigoSPiezaDental = item.Diente.Identificador.ToString() };
                    elementoAgregar.NombreSuperficie = item.Superficie.ToString();
                    elementoAgregar.NombreSuperficieApp = item.Superficie.nomenclaturaSuperficie();
                    elementoAgregar.NumeroSesiones = NumeroSesiones;
                    elementoAgregar.PrestadorAtencion = Prestador;
                    elementoAgregar.ConvenioAtencion = ConvenioAtencion;

                    elementoAgregar.OdontogramaEntity = new OdontogramaEntity();
                    elementoAgregar.OdontogramaEntity = item;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento = new PlanTratamientoEntity();
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Convenio = Variables_Globales.IdConvenio;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Procedimiento = item.Procedimiento.Identificador;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.EstadoProcedimiento = false;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.EstadoRegistro = Std.EstadosEntidad.Creado;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.FechaRegistro = DateTime.Now;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.IdAtencion = Variables_Globales.IdAtencion;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.SesionesPlanTratamiento = new SesionesPlanTratamientosCollection();
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Usuario = Variables_Globales.UsuarioActual;


                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Procedimiento = item.Procedimiento.Identificador;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Prestador = Variables_Globales.IdPrestador;
                    elementoAgregar.OdontogramaEntity.Identificador = i + 1;
                    elementoAgregar.OdontogramaEntity.Inicial = false;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.Activo = true;

                    // Contador identificador
                    i = i + 1;
                    Listado.Add(elementoAgregar);
                }
            }

            return Listado;
        }

        /*
         * La vantena muestra los datos que el usuario llena y cuando esta se cierra 
         * Manda a guardar los datos que se llenaron en esa ventana tomando el viewmodel de esa ventana
         * 
         */
        private void mostrarVentana()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Plan tratamiento",
                Resultado = resultado
            });
        }

        private async void resultado(object obj)
        {
            Busy.UserControlCargando(true, "Guardando plan de tratamiento");
            var vmPopUp = obj as Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento;
            TratamientoPadre = vmPopUp.TratamientoPadre;
            TratamientoPadre.TipoTratamiento = TiposTratamientoSeleccionado;

            var resultadoIds = await guardarTratamiento(TratamientoPadre, vmPopUp.Comprobantes.First(), vmPopUp.lstOdontogramaEntity);
            Busy.UserControlCargando(false);
        }
        

        private void cargarNumeroSessiones()
        {
            NumeroSesiones = new ObservableCollection<int>();
            for (int i = 1; i <= 36; i++)
            {
                NumeroSesiones.Add(i);
            }
        }        

        private void pedirDiagnosticosProcedimientosOdontogramaInicial()
        {
            // Le pedimos al mapa dental que nos devuelva el listado de elementos para guardar
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { Pedir_Tipos_Guardar = Tipo_Odontograma.Inicial, lstGuardar = listadoOdontogramaInicial });
        }

        private void listadoOdontogramaInicial(List<OdontogramaEntity> obj)
        {
            cargarProcedimientosAsociadosADiagnosticos(obj);
        } 

        private async void inicializarElementos()
        {
            TiposTratamientoSeleccionado = new TipoTratamientoEntity();

            await cargarOdontogramaPlanTratamiento();

            FormaPagoOdontologia = typeof(Cnt.Panacea.Entities.Odontologia.FormaPago).ToExtendedList<Cnt.Panacea.Entities.Odontologia.FormaPago, byte>() as List<KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>>;
            FormaPagoOdontologia.RemoveAt(0);

            //Este carga el combo de tipo de atencion
            //Y debe ir primero para que se pueda mostrar el tipo al que esta asociado el procedimiento
            //O se puede poner un async en paralelo (Validarlo)
            await tiposTratamiento();

            if (Variables_Globales.IdTratamientoActivo != 0)
            {
                tratamientoActivo();
            }

            ConvenioAtencion = await Contexto_Odontologia.consultarConvenio(Variables_Globales.IdConvenio);            
            RaisePropertyChanged("ConvenioAtencion");
        }

        private void DescripcionTratamientoOdontologia(string obj)
        {
            DescripcionTratamiento = obj;
        }

        /// <summary>
        /// Cargar procedimientos asociados a los diagnosticos realizados en el odontograma inicial
        /// </summary>
        private async void cargarProcedimientosAsociadosADiagnosticos(List<OdontogramaEntity> resultado)
        {
            Busy.UserControlCargando(true, "Cargando procedimientos asociados a los diagnosticos del odontograma inicial");
            IEnumerable<string> ids = (from item in resultado.Where(a => a.Diagnostico != null)
                                       select Convert.ToInt32(item.Diagnostico.Diagnostico.Identificador).ToString(CultureInfo.CurrentCulture)).Distinct();
            string identificadores = String.Join("|", ids.ToArray<string>());

            if (!string.IsNullOrEmpty(identificadores))
            {
                ProcedimientosOdontogramaTratamiento = await Contexto_Odontologia.ConsultarProcedimientosPorDiagnostico(Variables_Globales.IdIps,identificadores);

                List<int> lstProcedimientos = new List<int>();
                foreach (var item in ProcedimientosOdontogramaTratamiento.ToList())
                {
                    lstProcedimientos.Add(item.Procedimiento);                    
                }
                //  Le envia un mensaje a la paleta pidiendole que cargue
                // Los procedimientos asociados al diagnostico del odontograma inicial
                Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos() {listadoProcedimientos = lstProcedimientos, tipo = Tipo.listado});

                var result = await Contexto_Odontologia.ConsultarEspecialidadesPorProcedimiento(Variables_Globales.IdSede, identificadores);

                if (result.Any())
                {
                    Variables_Globales.ProcedimientosEspecialidad = result.ToList().Unique(a => a.Identificador);
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "No se encontraron procedimientos asociados a los diagnosticos"
                    });
                }
            }
            Busy.UserControlCargando(false);
        }

        private async System.Threading.Tasks.Task tiposTratamiento()
        {
            if (TiposTratamiento == null)
            {
                TiposTratamiento = await Contexto_Odontologia.ConsultarTiposTratamiento(Variables_Globales.IdIps);
                RaisePropertyChanged("TiposTratamiento");
            }
            if (TratamientoPadre != null)
            {
                TiposTratamientoSeleccionado = TratamientoPadre.TipoTratamiento;
            }
        }

        private void tratamientoActivo()
        {
            
            //Disparar el mensaje de carga
            Busy.UserControlCargando();
            TratamientoPadre = Variables_Globales.TratamientosPadre;

            if (TiposTratamiento != null && TratamientoPadre != null)
            {
                TiposTratamientoSeleccionado = TiposTratamiento.FirstOrDefault(p => p.Identificador == TratamientoPadre.TipoTratamiento.Identificador);
            }
            if (TratamientoPadre.TipoTratamiento != null && TratamientoPadre.TipoTratamiento.Identificador != 0)
            {
                HabilitarTipoTratamiento = false;
            }
            else
            {
                HabilitarTipoTratamiento = true;
            }

            DescripcionTratamiento = TratamientoPadre.Descripcion;
            EsCotizacion = TratamientoPadre.Cotizacion;
            
            if (TratamientoPadre.CuotaInicial != null)
            {
                ValorCuotaInicial = (decimal)TratamientoPadre.CuotaInicial;
            }
            if (TratamientoPadre.Cuotas != null)
            {
                NumeroSesionesTratamiento = (short)TratamientoPadre.Cuotas;
            }
            if (TratamientoPadre.TipoPago != null)
            {
                FormaPagoSeleccionado = FormaPagoOdontologia.Where(a => a.NumericKey == TratamientoPadre.TipoPago).First();
            }
            if (TratamientoPadre.ValorCuota != null)
            {
                ValorCuotaTratamiento = (decimal)TratamientoPadre.ValorCuota;
            }

            Sesiones = TratamientoPadre.Sesiones.ToString();
            CuotaInicial = TratamientoPadre.CuotaInicial.ToString();
            ValorCuota = TratamientoPadre.ValorCuota.ToString();
            

            if (TratamientoPadre.TipoTratamiento != null)
            {               
                MostrarOpcionesNuevoTratamiento = true;
            }

            RaisePropertyChanged("MuestraOpcionesNuevoTratamiento");
            RaisePropertyChanged("FormaPagoSeleccionado");
            RaisePropertyChanged("HabilitarTipoTratamiento");
            RaisePropertyChanged("DescripcionTratamiento");
            RaisePropertyChanged("TiposTratamientoSeleccionado");
            Busy.UserControlCargando(false);
        }
        #endregion

        #region Propiedades
        public RelayCommand<string> DescripcionTratamientoCommand { get; private set; }


        private Entities.Odontologia.TratamientoEntity tratamientoPadre = new TratamientoEntity();

        public Entities.Odontologia.TratamientoEntity TratamientoPadre
        {
            get { return tratamientoPadre; }
            set { tratamientoPadre = value; RaisePropertyChanged("TratamientoPadre"); }
        }
        

        public System.Collections.ObjectModel.ObservableCollection<Entities.Odontologia.TipoTratamientoEntity> TiposTratamiento { get; set; }

        public Entities.Odontologia.TipoTratamientoEntity TiposTratamientoSeleccionado { get; set; }

        public bool HabilitarTipoTratamiento { get; set; }

        public string DescripcionTratamiento { get; set; }

        public bool EsCotizacion { get; set; }

        public decimal ValorCuotaInicial { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        public string Sesiones { get; set; }

        public string CuotaInicial { get; set; }

        public string ValorCuota { get; set; }        

        public Entities.Parametrizacion.ConvenioEntity ConvenioAtencion { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<Entities.Parametrizacion.TerceroEntity> OdontologosIps { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<Entities.Parametrizacion.TerceroEntity> HigientistasIps { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<Entities.Parametrizacion.TerceroEntity> LstOdontologosHigienistas { get; set; }

        public List<KeyValueTriplet<Entities.Odontologia.FormaPago, byte, string>> FormaPagoOdontologia { get; set; }

        public KeyValueTriplet<Entities.Odontologia.FormaPago, byte, string> FormaPagoSeleccionado { get; set; }

        public ObservableCollection<Entities.Odontologia.RelaDiagnosticoProcedEntity> ProcedimientosOdontogramaTratamiento { get; set; }

        public RelayCommand siguientePasoCommand { get; set; }

        private ObservableCollection<ProcedimientosGrillaPlanTratamiento> listado;

        public ObservableCollection<ProcedimientosGrillaPlanTratamiento> Listado
        {
            get { return listado; }
            set { listado = value; RaisePropertyChanged("Listado"); }
        }

        public ObservableCollection<int> NumeroSesiones { get; set; }

        public TerceroEntity Prestador { get; set; }

        public String JustificacionModificacionTratamiento { get; set; }

        /// <summary>
        /// Estado plan de tratamiento nuevo eliminado modificado.
        /// </summary>
        /// <value>The plan tratamiento estado nuevo eliminado modificado.</value>
        public PlanTratamientoEstadoNuevoEliminadoModificado PlanTratamientoEstadoNuevoEliminadoModificado { get; set; }

        private bool mostrarOpcionesNuevoTratamiento;

        public bool MostrarOpcionesNuevoTratamiento
        {
            get { return mostrarOpcionesNuevoTratamiento; }
            set { mostrarOpcionesNuevoTratamiento = value; RaisePropertyChanged("MostrarOpcionesNuevoTratamiento"); }
        }

        /// <summary>
        /// Puede modificar.
        /// </summary>
        /// <value><c>true</c> if [puede modificar]; otherwise, <c>false</c>.</value>
        public bool PuedeModificar { get; set; }

        public RelayCommand mostrarGrillaTratamientosCommand { get; set; }

        public short NumeroSesionesTratamiento { get; set; }
        
        #endregion

        public void Dispose()
        {
            Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);                        
        }
    }
}
