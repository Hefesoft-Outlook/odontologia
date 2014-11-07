using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Validaciones;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Cnt.Std;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FormaPago = Cnt.Panacea.Entities.Odontologia.FormaPago;
using Mensajes = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    public partial class Plan_Tratamiento : ViewModelBase, IDisposable
    {
        #region Constructor

        public Plan_Tratamiento()
        {
            if (IsInDesignMode)
            {
                createSampleData();
            }
            else
            {
                //Crea datos de ejemplo de ser necesario en la bd
                //createSampleData();
                //datosPruebaEspecialidades();

                //Oye cuando se da click en plan de tratamiento
                oirCambioOdontograma();

                //Muestra los Procedimientos activos
                mostrarGrillaTratamientosCommand = new RelayCommand(mostrarGrillaTratamientos);

                //Captura el texto descripcion de plan de tratamiento
                DescripcionTratamientoCommand = new RelayCommand<string>(DescripcionTratamientoOdontologia);

                //Muestra la ventana donde se configuran los procedimientos seleccionados
                siguientePasoCommand = new RelayCommand(siguientePaso);
            }
        }


        private async Task cargarOdontogramaPlanTratamiento()
        {
            // La validacion indica si es un nuevo plan de tratamiento
            if (Variables_Globales.IdTratamientoActivo != 0)
            {
                Busy.UserControlCargando(true, "Cargando plan de tratamiento");
                ObservableCollection<OdontogramaEntity> resultado = new ObservableCollection<OdontogramaEntity>();
                resultado = await Contexto_Odontologia.obtenerContexto().ListarOdontogramaTratamiento(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);
                Busy.UserControlCargando(false);

                if (resultado.Any())
                {
                    Messenger.Default.Send(new Pedir_Pintar_Datos { lst = resultado, Limpiar_Datos = false });
                }
            }
        }

        private void incializarElementosPlanTratamiento(Cambiar_Tipo_Odontograma item)
        {
            if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
            {
                Variables_Globales.Tipo_Odontograma_Activo = Tipo_Odontograma.Plan_Tratamiento;

                //Se limpian los elementos que puedan estar inicializados
                TratamientoPadre = null;
                TratamientoPadre = new TratamientoEntity();

                TratamientoPadre = Variables_Globales.TratamientosPadre;

                try
                {
                    inicializarElementos();
                    cargarNumeroSessiones();
                    pedirDiagnosticosProcedimientosOdontogramaInicial();
                    ValidarModoLectura();
                }
                catch
                {

                }
            }
        }

        private void mostrarGrillaTratamientos()
        {
            Messenger.Default.Send(new Mostrar_Ventana
            {
                Nombre = "Tratamientos",
                Propiedad_Adicional = TratamientoPadre
            });
        }

        //Como Este control esta pintado en la pantalla inicial
        //Debe actualizarse cuando se mueva de pestana a pestana
        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, incializarElementosPlanTratamiento);
        }

        #endregion

        #region metodos

        //Se separa para poder hacer llamados desde el listado de procedimientos
        internal ObservableCollection<ProcedimientosGrillaPlanTratamiento> convertirProcedimientosGrillaPlanTratamiento(
            List<OdontogramaEntity> obj)
        {
            int i = 0;

            foreach (OdontogramaEntity item in obj)
            {
                // Valida que lo se este agregando es un procedimiento
                if (item.Procedimiento != null && item.Procedimiento.Identificador > 0)
                {
                    // Aca solo se van a guardar procedimientos
                    item.Diagnostico = null;
                    var elementoAgregar = new ProcedimientosGrillaPlanTratamiento();
                    elementoAgregar.Odontograma = new Odontograma(item.Diente.Identificador.ToString())
                    {
                        codigoSPiezaDental = item.Diente.Identificador.ToString()
                    };
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
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.EstadoRegistro = EstadosEntidad.Creado;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.FechaRegistro = DateTime.Now;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.IdAtencion = Variables_Globales.IdAtencion;
                    elementoAgregar.OdontogramaEntity.PlanTratamiento.SesionesPlanTratamiento =
                        new SesionesPlanTratamientosCollection();
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

        internal async Task<ObservableCollection<long>> guardarTratamiento(TratamientoEntity Padre,
            ComprobanteEntity comprobanteSeleccionado, List<OdontogramaEntity> lstOdontogramaEntity)
        {
            var tcs = new TaskCompletionSource<ObservableCollection<long>>();
            TratamientoPadre = Padre;

            var OdontogramasPacienteEntity = new OdontogramasPacienteEntity();
            OdontogramasPacienteEntity.Tratamiento = Padre.Identificador;

            TratamientoPadre.Convenio = new ConvenioEntity {Identificador = Variables_Globales.IdConvenio};
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
            TratamientoPadre.EstadoRegistro = EstadosEntidad.Modificado;
            TratamientoPadre.TipoTratamiento.EstadoRegistro = EstadosEntidad.Modificado;
            TratamientoPadre.Modificado = DateTime.Now;

            var Paciente = new PacienteEntity {Identificador = Variables_Globales.IdPaciente};
            ObservableCollection<long> result =
                await    Contexto_Odontologia.obtenerContexto()
                        .GuardarPlanTratamiento(TratamientoPadre, 1, OdontogramasPacienteEntity,
                            lstOdontogramaEntity.ToObservableCollection(), EsCotizacion, Paciente,
                            comprobanteSeleccionado, Variables_Globales.IdIps, Variables_Globales.UsuarioActual,
                            Variables_Globales.IdSede, Variables_Globales.IdConvenio);

            tcs.SetResult(result);
            finalizarGuardarPlanTratamiento(result);
            llenarOrdenes(lstOdontogramaEntity);

            return result;
        }

        internal async Task<ObservableCollection<OdontogramaEntity>> listarOdontogramaPlanTratamiento()
        {
            ObservableCollection<OdontogramaEntity> resultado =
                await
                    Contexto_Odontologia.obtenerContexto()
                        .ListarOdontogramaTratamiento(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);

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

        private void cargarNumeroSessiones()
        {
            NumeroSesiones = new ObservableCollection<int>();
            for (int i = 1; i <= 36; i++)
            {
                NumeroSesiones.Add(i);
            }
        }

        /// <summary>
        ///     Cargar procedimientos asociados a los diagnosticos realizados en el odontograma inicial
        /// </summary>
        private async void cargarProcedimientosAsociadosADiagnosticos(List<OdontogramaEntity> resultado)
        {
            Busy.UserControlCargando(true,
                "Cargando procedimientos asociados a los diagnosticos del odontograma inicial");
            IEnumerable<string> ids = (from item in resultado.Where(a => a.Diagnostico != null)
                select Convert.ToInt32(item.Diagnostico.Diagnostico.Identificador).ToString(CultureInfo.CurrentCulture))
                .Distinct();
            string identificadores = String.Join("|", ids.ToArray());

            if (!string.IsNullOrEmpty(identificadores))
            {
                ProcedimientosOdontogramaTratamiento =
                    await
                        Contexto_Odontologia.obtenerContexto()
                            .ConsultarProcedimientosPorDiagnostico(Variables_Globales.IdIps, identificadores);

                var lstProcedimientos = new List<int>();
                foreach (RelaDiagnosticoProcedEntity item in ProcedimientosOdontogramaTratamiento.ToList())
                {
                    lstProcedimientos.Add(item.Procedimiento);
                }
                //  Le envia un mensaje a la paleta pidiendole que cargue
                // Los procedimientos asociados al diagnostico del odontograma inicial
                Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos
                {
                    listadoProcedimientos = lstProcedimientos,
                    tipo = Tipo.listado
                });

                ObservableCollection<ProcedimientoEspecialidadSedeEntity> result =
                    await
                        Contexto_Odontologia.obtenerContexto()
                            .ConsultarEspecialidadesPorProcedimiento(Variables_Globales.IdSede, identificadores);

                if (result.Any())
                {
                    Variables_Globales.ProcedimientosEspecialidad = result.ToList().Unique(a => a.Identificador);
                }
                else
                {
                    Messenger.Default.Send(new Mostrar_Mensaje_Usuario
                    {
                        Mensaje = "No se encontraron procedimientos asociados a los diagnosticos"
                    });
                }
            }
            Busy.UserControlCargando(false);
        }

        private void DescripcionTratamientoOdontologia(string obj)
        {
            DescripcionTratamiento = obj;
        }

        private async void finalizarGuardarPlanTratamiento(ObservableCollection<long> e)
        {
            Messenger.Default.Send(new Activar_Elementos {valor = "Evolucion"});
            long idCotizacion;
            await listarOdontogramaPlanTratamiento();
            GuardarImagenes();

            if (!EsCotizacion)
            {
                Messenger.Default.Send(new Mostrar_Ventana
                {
                    Nombre = "Aprueba plan tratamiento",
                    Resultado = resultadoAprobacionUsuario
                });
            }
            else
            {
                Messenger.Default.Send(new Mostrar_Ventana
                {
                    Nombre = "Cotizacion",
                    Propiedad_Adicional = this,
                });
            }
        }

        private void formatearDatos(List<OdontogramaEntity> obj)
        {
            try
            {
                Listado = Listado.inicializarListaYLimpiar();
                convertirProcedimientosGrillaPlanTratamiento(obj);

                mostrarVentana();
            }
            catch { }
        }

        /// <summary>
        ///     Almacena imagenes.
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

        private async void inicializarElementos()
        {
            TiposTratamientoSeleccionado = new TipoTratamientoEntity();

            await cargarOdontogramaPlanTratamiento();

            FormaPagoOdontologia = typeof (FormaPago).ToExtendedList<FormaPago, byte>();
            FormaPagoOdontologia.RemoveAt(0);

            //Este carga el combo de tipo de atencion
            //Y debe ir primero para que se pueda mostrar el tipo al que esta asociado el procedimiento
            //O se puede poner un async en paralelo (Validarlo)
            await tiposTratamiento();

            if (Variables_Globales.IdTratamientoActivo != 0)
            {
                tratamientoActivo();
            }

            ConvenioAtencion = await Contexto_Odontologia.obtenerContexto().consultarConvenio(Variables_Globales.IdConvenio);
            RaisePropertyChanged("ConvenioAtencion");
        }

        private void listadoOdontogramaInicial(List<OdontogramaEntity> obj)
        {
            cargarProcedimientosAsociadosADiagnosticos(obj);
        }

        private void llenarOrdenes(List<OdontogramaEntity> lstOdontogramaEntity)
        {
            var lstOrdenes = new List<int>();

            foreach (OdontogramaEntity item in lstOdontogramaEntity)
            {
                foreach (SesionesPlanTratamientoEntity pivote in item.PlanTratamiento.SesionesPlanTratamiento)
                {
                    if (pivote.IdSesion == 1)
                    {
                        lstOrdenes.Add(pivote.Procedimiento);
                    }
                }
            }
        }

        private void mostrarVentana()
        {
            Messenger.Default.Send(new Mostrar_Ventana
            {
                Nombre = "Plan tratamiento",
                Resultado = resultado
            });
        }

        private void pedirDiagnosticosProcedimientosOdontogramaInicial()
        {
            // Le pedimos al mapa dental que nos devuelva el listado de elementos para guardar
            Messenger.Default.Send(new Guardar
            {
                Pedir_Tipos_Guardar = Tipo_Odontograma.Inicial,
                lstGuardar = listadoOdontogramaInicial
            });
        }

        // Pide los procedimientos que se encuentran en el mapa dental
        private void pedirProcedimientos()
        {
            Messenger.Default.Send(new Guardar
            {
                Pedir_Tipos_Guardar = Tipo_Odontograma.Plan_Tratamiento,
                lstGuardar = formatearDatos
            });
        }

        private async void resultado(object obj)
        {
            Busy.UserControlCargando(true, "Guardando plan de tratamiento");
            var vmPopUp = obj as UserControlGuardarPlanTratamiento;
            TratamientoPadre = vmPopUp.TratamientoPadre;
            TratamientoPadre.TipoTratamiento = TiposTratamientoSeleccionado;

            ObservableCollection<long> resultadoIds =
                await guardarTratamiento(TratamientoPadre, vmPopUp.Comprobantes.First(), vmPopUp.lstOdontogramaEntity);
            Busy.UserControlCargando(false);
        }

        private void resultadoAprobacionUsuario(object obj)
        {
            if ((bool) obj)
            {
                Messenger.Default.Send(new Mostrar_Ventana
                {
                    Nombre = "Generar plan tratamiento",
                    Propiedad_Adicional = TratamientoPadre
                });
            }
        }

        private void resultadoValidacion(bool obj)
        {
            if (obj)
            {
                var validaciones = new Validaciones.Plan_Tratamiento();
                falla resultado = validaciones.validarDatos(this);

                if (resultado.valido)
                {
                    pedirProcedimientos();
                }
                else
                {
                    Messenger.Default.Send(new Mostrar_Mensaje_Usuario
                    {
                        Mensaje = resultado.mensaje
                    });
                }
            }
            else
            {
                Messenger.Default.Send(new Mostrar_Mensaje_Usuario
                {
                    Mensaje = "No todas las superficiestienen un procedimiento asociado"
                });
            }
        }

        private void siguientePaso()
        {
            //Le pedimos al mapa dental que valide si cada diagnostico tiene un procedimiento
            Messenger.Default.Send(new Util.Messenger.Mapa_Dental.Validaciones
            {
                Tipo = tipo.Cada_Superficie_Con_Diagnostico_Tiene_Procedimientos,
                //La respuesta de la validacion
                Result = resultadoValidacion
            });
        }

        private async Task tiposTratamiento()
        {
            if (TiposTratamiento == null)
            {
                TiposTratamiento =
                    await Contexto_Odontologia.obtenerContexto().ConsultarTiposTratamiento(Variables_Globales.IdIps);
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

            if (TiposTratamiento != null && TratamientoPadre != null && TratamientoPadre.TipoTratamiento != null)
            {
                TiposTratamientoSeleccionado =  TiposTratamiento.FirstOrDefault(p => p.Identificador == TratamientoPadre.TipoTratamiento.Identificador);
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
                ValorCuotaInicial = (decimal) TratamientoPadre.CuotaInicial;
            }
            if (TratamientoPadre.Cuotas != null)
            {
                NumeroSesionesTratamiento = (short) TratamientoPadre.Cuotas;
            }
            if (TratamientoPadre.TipoPago != null)
            {
                FormaPagoSeleccionado =
                    FormaPagoOdontologia.Where(a => a.NumericKey == TratamientoPadre.TipoPago).First();
            }
            if (TratamientoPadre.ValorCuota != null)
            {
                ValorCuotaTratamiento = (decimal) TratamientoPadre.ValorCuota;
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

        /// <summary>
        ///     Validar modo lectura.
        /// </summary>
        private void ValidarModoLectura()
        {
            try
            {
                if (!((Variables_Globales.ParametroConvenio.RequiereClasificador &&
                       Variables_Globales.ClasificadorOdontologico) //LFDO Bug 15568
                      || !Variables_Globales.ParametroConvenio.RequiereClasificador)
                    || TratamientoPadre.EstadoTratamiento == EstadoTratamiento.Terminacion ||
                    TratamientoPadre.EstadoTratamiento == EstadoTratamiento.Abandono)
                {
                    PuedeModificar = false;
                    RaisePropertyChanged("PuedeModificar");

                    Messenger.Default.Send(new Mostrar_Mensaje_Usuario
                    {
                        Mensaje = Mensajes.Solo_Lectura
                    });
                }
                else if ((Variables_Globales.ParametroConvenio.RequiereClasificador &&
                          Variables_Globales.ClasificadorOdontologico) //LFDO Bug 15568
                         || !Variables_Globales.ParametroConvenio.RequiereClasificador)
                {
                    PuedeModificar = true;
                    RaisePropertyChanged("PuedeModificar");
                }
            }
            catch
            {
            }
        }

        /*
         * La vantena muestra los datos que el usuario llena y cuando esta se cierra 
         * Manda a guardar los datos que se llenaron en esa ventana tomando el viewmodel de esa ventana
         * 
         */

        #endregion

        #region Propiedades

        private ObservableCollection<ProcedimientosGrillaPlanTratamiento> listado;

        private bool mostrarOpcionesNuevoTratamiento;

        private TratamientoEntity tratamientoPadre = new TratamientoEntity();

        public ConvenioEntity ConvenioAtencion { get; set; }

        public string CuotaInicial { get; set; }

        public string DescripcionTratamiento { get; set; }

        public RelayCommand<string> DescripcionTratamientoCommand { get; private set; }
        public bool EsCotizacion { get; set; }

        public List<KeyValueTriplet<FormaPago, byte, string>> FormaPagoOdontologia { get; set; }

        public KeyValueTriplet<FormaPago, byte, string> FormaPagoSeleccionado { get; set; }

        public bool HabilitarTipoTratamiento { get; set; }

        public ObservableCollection<TerceroEntity> HigientistasIps { get; set; }

        public String JustificacionModificacionTratamiento { get; set; }

        public ObservableCollection<ProcedimientosGrillaPlanTratamiento> Listado
        {
            get { return listado; }
            set
            {
                listado = value;
                RaisePropertyChanged("Listado");
            }
        }

        public ObservableCollection<TerceroEntity> LstOdontologosHigienistas { get; set; }

        public RelayCommand mostrarGrillaTratamientosCommand { get; set; }

        public bool MostrarOpcionesNuevoTratamiento
        {
            get { return mostrarOpcionesNuevoTratamiento; }
            set
            {
                mostrarOpcionesNuevoTratamiento = value;
                RaisePropertyChanged("MostrarOpcionesNuevoTratamiento");
            }
        }

        public ObservableCollection<int> NumeroSesiones { get; set; }

        public short NumeroSesionesTratamiento { get; set; }

        public ObservableCollection<TerceroEntity> OdontologosIps { get; set; }

        /// <summary>
        ///     Estado plan de tratamiento nuevo eliminado modificado.
        /// </summary>
        /// <value>The plan tratamiento estado nuevo eliminado modificado.</value>
        public PlanTratamientoEstadoNuevoEliminadoModificado PlanTratamientoEstadoNuevoEliminadoModificado { get; set; }

        public TerceroEntity Prestador { get; set; }

        public ObservableCollection<RelaDiagnosticoProcedEntity> ProcedimientosOdontogramaTratamiento { get; set; }

        /// <summary>
        ///     Puede modificar.
        /// </summary>
        /// <value><c>true</c> if [puede modificar]; otherwise, <c>false</c>.</value>
        public bool PuedeModificar { get; set; }

        public string Sesiones { get; set; }

        public RelayCommand siguientePasoCommand { get; set; }

        public ObservableCollection<TipoTratamientoEntity> TiposTratamiento { get; set; }

        public TipoTratamientoEntity TiposTratamientoSeleccionado { get; set; }

        public TratamientoEntity TratamientoPadre
        {
            get { return tratamientoPadre; }
            set
            {
                tratamientoPadre = value;
                RaisePropertyChanged("TratamientoPadre");
            }
        }

        public string ValorCuota { get; set; }

        public decimal ValorCuotaInicial { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        #endregion

        public void Dispose()
        {
            Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
        }
    }
}