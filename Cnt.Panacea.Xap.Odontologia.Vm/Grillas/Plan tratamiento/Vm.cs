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
using Cnt.Panacea.Entities.Parametrizacion;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental
{
    // Este formulario carga los datos que estan fuera de la grilla
    // La grilla se carga en un control que se llama Wizard adentro de la vista a la que hacer
    // referencia este View Model
    public class UserControlGuardarPlanTratamiento : ViewModelBase, IDisposable
    {
        public UserControlGuardarPlanTratamiento()
        {
            if (IsInDesignMode)
            {

            }
            else
            {
                TratamientoPadre = Variables_Globales.TratamientosPadre;
                ValidarSesionesCommand = new RelayCommand(ValidarSesionesConfiguradas);
                CalculoValoresTratamientoCommand = new RelayCommand(CalculoValoresTratamiento);                
                ValorCuotaInicialTratamientoCommand = new RelayCommand<string>(CuotaInicialTratamiento);
                FormaPagoOdontologia = typeof(Cnt.Panacea.Entities.Odontologia.FormaPago).ToExtendedList<Cnt.Panacea.Entities.Odontologia.FormaPago, byte>() as List<KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>>;
                FormaPagoOdontologia.RemoveAt(0);
                cargarSesiones();
                inicializarTratamiento();
            }
        }


        # region Metodos
        private void inicializarTratamiento()
        {
            PuedeModificar = true;
            RaisePropertyChanged("PuedeModificar");
        }

        //Este metodo controla la seleccion sobre el combo
        //y valida que se llenen los campos antes de generar los valores

        private void ValidarSesionesConfiguradas()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Datos_Grilla_Plan_Tratamiento()
            {
                //Usamos una expresion lambda para hacer la validacion para no generar tanto codigo
                //Esto lo que hace es decirle a la grimma que por favor le devuelva los datos que contiene en este momento
                lst = (ObservableCollection<ProcedimientosGrillaPlanTratamiento> obj) =>
                {
                    if (obj.validarSesionesConfiguradas() && FormaPagoSeleccionado.GetHashCode() == Cnt.Panacea.Entities.Odontologia.FormaPago.Sesión.GetHashCode())
                    {
                        HabilitarControlesPagoSesion = true;
                        RaisePropertyChanged("HabilitarControlesPagoSesion");
                        ListadoGrillaPlanTratamiento = obj;
                        CalculoValoresTratamiento();
                    }
                    else
                    {
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                        {
                            Mensaje = "Por favor indique la sesion para cada procedimiento"
                        });
                    }
                },
                fallido = (error) =>
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "Por favor indique la sesion para cada procedimiento"
                    });
                }
            });
        }

        public void pedirDatosGrilla()
        {
            //Valida que el usuario pueda generar cambios
            if (PuedeModificar)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Datos_Grilla_Plan_Tratamiento() { fallido = fallo, lst = listado });
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Solo_Lectura
                });
            }
        }

        private void fallo(falla obj)
        {
            string errores = "";

            if (obj.mensaje != null)
            {
                foreach (var item in obj.mensaje)
                {
                    errores += System.Environment.NewLine + item;
                }

                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = errores
                });
            }
        }

        private void listado(ObservableCollection<ProcedimientosGrillaPlanTratamiento> obj)
        {
            ListadoGrillaPlanTratamiento = obj;
            var validaciones = new Grilla_Plan_Tratamiento();
            var result = validaciones.validarAntesGuardar(this);

            if (result.valido)
            {
                ListadoGrillaPlanTratamiento = obj;
                lstOdontogramaEntity = new List<OdontogramaEntity>();

                foreach (var item in ListadoGrillaPlanTratamiento)
                {
                    lstOdontogramaEntity.Add(item.OdontogramaEntity);
                }

                tratamientoPadre();
                CalculoValoresTratamiento();
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cerrar_Pop_Up_Generico());
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = result.mensaje
                });
            }
        }

        async void cargarSesiones()
        {
            //Valida que no este cargado antes
            if (Comprobantes == null)
            {
                Comprobantes = await Contexto_Odontologia.ListarComprobantes(Variables_Globales.IdIps, Variables_Globales.UsuarioActual, Variables_Globales.IdSede);
            }

            if (Comprobantes.Any())
            {
                ComprobanteSeleccionado = Comprobantes.FirstOrDefault();
                RaisePropertyChanged("ComprobanteSeleccionado");
                RaisePropertyChanged("Comprobantes");
            }
        }


        /// <summary>
        /// Calcula los valores del plan de tratamiento.
        /// </summary>
        public void CalculoValoresTratamiento()
        {
            // Llamar por messenger para hacer el calculo

            NumeroSesionesTratamiento = short.Parse(ListadoGrillaPlanTratamiento.Sum(a => a.NumeroSesionesProcedimiento).ToString());
            ValorTotalTratamiento = Decimal.Parse(ListadoGrillaPlanTratamiento.Where(a => a.Cobra == true).Sum(a => a.ValorPaciente).ToString());

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

        public void tratamientoPadre()
        {
            TratamientoPadre = Variables_Globales.TratamientosPadre;
            TratamientoPadre.Cotizacion = EsCotizacion;
            TratamientoPadre.CuotaInicial = ValorCuotaInicial;
            TratamientoPadre.EstadoTratamiento = EstadoTratamiento.PlanDeTratamiento;
            TratamientoPadre.Cuotas = NumeroSesionesTratamiento;
            TratamientoPadre.TipoPago = (short)FormaPagoSeleccionado.GetHashCode();
            TratamientoPadre.ValorCuota = ValorCuotaTratamiento;


            if (TratamientoPadre.IdSesionActual == null || TratamientoPadre.IdSesionActual <= 1)
            {
                TratamientoPadre.IdSesionActual = 1;
            }
            TratamientoPadre.EstadoRegistro = Std.EstadosEntidad.Modificado;
        }

        /// <summary>
        /// Recibe la cuota inicial del plan de tratamiento
        /// </summary>
        /// <param name="CuotaInicialTexto">The cuota inicial texto.</param>
        public void CuotaInicialTratamiento(string CuotaInicialTexto)
        {
            CuotaInicialTexto = CuotaInicialTexto.Replace("$", "");
            CuotaInicialTexto = CuotaInicialTexto.Replace(",", "");
            Decimal cuota;
            if (Decimal.TryParse(CuotaInicialTexto, out cuota))
            {
                ValorCuotaInicial = Decimal.Parse(CuotaInicialTexto);
                RaisePropertyChanged("ValorCuotaInicial");
                CalculoValoresTratamiento();
            }
        }

        #endregion

        #region Propiedades

        /// <summary>
        /// Numero sesiones tratamiento.
        /// </summary>
        /// <value>The numero sesiones tratamiento.</value>
        public short NumeroSesionesTratamiento { get; set; }

        /// <summary>
        /// Valor cuota inicial.
        /// </summary>
        /// <value>The valor cuota inicial.</value>
        public decimal ValorCuotaInicial { get; set; }
        /// <summary>
        /// Tiene cuota inicial].
        /// </summary>
        /// <value><c>true</c> if [tiene cuota inicial]; otherwise, <c>false</c>.</value>
        public bool TieneCuotaInicial { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [es cotizacion].
        /// </summary>
        /// <value><c>true</c> if [es cotizacion]; otherwise, <c>false</c>.</value>
        public bool EsCotizacion { get; set; }

        
        public List<KeyValueTriplet<Entities.Odontologia.FormaPago, byte, string>> FormaPagoOdontologia { get; set; }

        public KeyValueTriplet<Entities.Odontologia.FormaPago, byte, string> FormaPagoSeleccionado { get; set; }

        private ComprobanteEntity comprobanteSeleccionado;

        public ObservableCollection<int> NumeroSesiones { get; set; }

        public ObservableCollection<ComprobanteEntity> Comprobantes { get; set; }

        public ComprobanteEntity ComprobanteSeleccionado { get; set; }

        public RelayCommand CalculoValoresTratamientoCommand { get; set; }

        public bool HabilitarControlesPago { get; set; }

        /// <summary>
        /// Obtiene o establece la propiedad maximoProcedimientosSesion.
        /// </summary>
        /// <value>MaximoProcedimientosSesion.</value>
        /// LFDO Bug 16469
        public int MaximoProcedimientosSesion
        {
            get { return Variables_Globales.MaximaCantidadProcedimientosSesion; }
        }

        /// <summary>
        /// Habilitar controles pago sesion.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [habilitar controles pago sesion]; otherwise, <c>false</c>.
        /// </value>
        public bool HabilitarControlesPagoSesion { get; set; }


        TratamientoEntity _tratamientoPadre = new TratamientoEntity();
        /// <summary>
        /// Gets or sets the tratamiento padre.
        /// </summary>
        /// <value>The tratamiento padre.</value>
        public TratamientoEntity TratamientoPadre
        {
            get
            {
                return _tratamientoPadre;
            }
            set
            {
                if (value != null)
                {
                    if (value.EstadoTratamiento == EstadoTratamiento.Abandono || value.EstadoTratamiento == EstadoTratamiento.Terminacion)
                    {
                        PuedeModificar = false;
                        RaisePropertyChanged("PuedeModificar");
                    }

                    _tratamientoPadre = value;
                    RaisePropertyChanged("TratamientoPadre");
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "No existe un tratamiento activo valide que tenga un tratamiento en el odontograma inicial guardado"
                    });
                }
            }
        }

        public RelayCommand<string> ValorCuotaInicialTratamientoCommand { get; set; }

        public ObservableCollection<ProcedimientosGrillaPlanTratamiento> ListadoGrillaPlanTratamiento = new ObservableCollection<ProcedimientosGrillaPlanTratamiento>();

        public decimal ValorTotalTratamiento { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        public List<OdontogramaEntity> lstOdontogramaEntity { get; set; }

        public bool PuedeModificar { get; set; }

        public RelayCommand ValidarSesionesCommand { get; set; }

        #endregion

        public void Dispose()
        {

        }
    }
}
