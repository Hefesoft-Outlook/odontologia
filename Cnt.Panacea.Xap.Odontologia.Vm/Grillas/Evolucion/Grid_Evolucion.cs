using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Cnt.Std;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Evolucion;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion
{
    public partial class Grid_Evolucion : ViewModelBase, IDisposable
    {
        public Grid_Evolucion()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                //Crear datos de prueba
                datosPruebaFinalidadProcedimiento();


                realizadoCommand = new RelayCommand<ProcedimientosGrillaEvolucion>(procedimientoRealizado);
                bodegaCommand = new RelayCommand<ProcedimientosGrillaEvolucion>(bodega);
                cambioSesionCommand = new RelayCommand<int>(SesionCambiada);
                InicializarEvolucion();
            }
        }
               

        #region metodos


        private async void cargarListadoMostrar()
        {
            
            TratamientoPadre = Variables_Globales.TratamientosPadre;
            await listarOdontogramaPlanTratamiento();
        }

        private async void bodega(ProcedimientosGrillaEvolucion obj)
        {
            var bodega = await Contexto_Odontologia.obtenerContexto().ListarParametrosBodega(Variables_Globales.IdIps);
            Variables_Globales.IdBodega = bodega.BodegaInventario.Identificador;            

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Bodega",
                Propiedad_Adicional = obj
            });

        }

        public void procedimientoRealizado(ProcedimientosGrillaEvolucion obj)
        {
            if (obj != null)
            {
                var item = obj.OdontogramaEntity.odontogramaEntityToDiagnosticoProcedimiento_Extend();
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Procedimiento_Realizado()
                {
                    Superficie = item.Superficie,
                    Realizado = obj.PlanTratamientoEntity.EstadoProcedimiento
                }, obj.Odontograma.codigoSPiezaDental); // Este parametro o token lo enviamos para que solo nos oiga el diente al que vamos a cambiar
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "Por favor seleccione una pieza dental"
                });
            }

        }

        private void numeroSessiones()
        {
            NumeroSesiones = new ObservableCollection<int>();
            for (int i = 1; i <= 36; i++)
            {
                NumeroSesiones.Add(i);
            }

            RaisePropertyChanged("NumeroSesiones");
        }


        /// <summary>
        /// Inicializar el plan de evolucion.
        /// </summary>
        public async void InicializarEvolucion()
        {
            cargarListadoMostrar();           

            numeroSessiones();
            TratamientoPadre = new TratamientoEntity();
            TratamientoPadre.Identificador = Variables_Globales.IdTratamientoActivo;
            FormaPagoOdontologia = typeof(Cnt.Panacea.Entities.Odontologia.FormaPago).ToExtendedList<Cnt.Panacea.Entities.Odontologia.FormaPago, byte>() as List<KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>>;
            FormaPagoOdontologia.RemoveAt(0);              


            if (TratamientoPadre != null)
            {
                tratamientoActivo(TratamientoPadre.Identificador);
                await SesionesConfiguradasTratamientos();

                FinalidadesProcedimiento = await Contexto_Odontologia.obtenerContexto().ListarFinalidadesProcedimiento(Variables_Globales.IdIps);
                OdontologosHigienistasIps = await Contexto_Odontologia.obtenerContexto().ListarOdontologosPorIps(Variables_Globales.IdIps);

                IdReciboCaja = await Contexto_Odontologia.obtenerContexto().ReciboCajaTratamiento(Variables_Globales.IdTratamientoActivo);

                if (TratamientoPadre.IdSesionActual != null)
                {
                    SesionActualCarga = (int)TratamientoPadre.IdSesionActual;
                    Variables_Globales.SesionActual = (short)TratamientoPadre.IdSesionActual;
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario() 
                    {
                        Mensaje = string.Format("Error el elemento {0} es nulo", "TratamientoPadre.IdSesionActual") 
                    });
                }


                ValorPagoTratamiento = await Contexto_Odontologia.obtenerContexto().ValorPagoTratamiento(TratamientoPadre.Identificador);
                ValorSaldoTratamiento = ValorTotalTratamiento - ValorPagoTratamiento;

                RaisePropertyChanged("NumeroSesionesConfiguradas");
                RaisePropertyChanged("FinalidadesProcedimiento");
                RaisePropertyChanged("OdontologosHigienistasIps");
                RaisePropertyChanged("ValorPagoTratamiento");
                RaisePropertyChanged("ValorSaldoTratamiento");
                RaisePropertyChanged("SesionActualCarga");
                RaisePropertyChanged("IdReciboCaja");
                RaisePropertyChanged("DescripcionTratamiento");
                RaisePropertyChanged("TiposTratamientoSeleccionado");
                RaisePropertyChanged("MuestraOpcionesNuevoTratamiento");
                RaisePropertyChanged("FormaPagoSeleccionado");
                RaisePropertyChanged("HabilitarTipoTratamiento");
                RaisePropertyChanged("PuedeModificar");
            }
            else 
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "No hay un plan de tratamiento activo"
                });
                
            }
        }

        private void SesionCambiada(int obj)
        {

            var elementosMostrar = new Convertir_Elementos_Grilla_Plan_Evolucion().obtenerElementosParaNumeroSesion(ListadoEvolucionTodos, obj).ToObservableCollection();

            if (elementosMostrar.Any())
            {
                ListadoEvolucion = ListadoEvolucion.inicializarListaYLimpiar();
                ListadoEvolucion = elementosMostrar;

                Convertir_Elemento_Grilla_Dibujo_Odontograma.Convertir(ListadoEvolucion);
            }
            else
            {
                elementosMostrar.Clear();
            }
            RaisePropertyChanged("ListadoEvolucion");
        }

        internal async Task listarOdontogramaPlanTratamiento()
        {
            var vmEvolucion = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Evolucion>();
            await generarGrilla(vmEvolucion.Listado);
            
        }

        private Task<bool> generarGrilla(ObservableCollection<OdontogramaEntity> resultado)
        {
            var tarea = new TaskCompletionSource<bool>();
            List<ProcedimientosGrillaEvolucion> lst = new List<ProcedimientosGrillaEvolucion>(); 

            foreach (var item in resultado)
            {
                var elementoAgregar = new ProcedimientosGrillaEvolucion()
                {
                    NombreSuperficie = item.Superficie.ToString(),
                    OdontogramaEntity = item,                    
                    PlanTratamientoEntity = item.PlanTratamiento,
                    Sesion = item.PlanTratamiento.SesionesPlanTratamiento,
                    Odontograma = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma(item.Diente.Identificador.ToString()),
                    ProcedimientoEntity = item.Procedimiento
                };

                //Le decimos al mapa dental que marque el procedimiento como realizado
                if (item.PlanTratamiento.EstadoProcedimiento)
                {
                    procedimientoRealizado(elementoAgregar);
                }

                lst.Add(elementoAgregar);
            }

            ListadoEvolucion = ListadoEvolucion.inicializarListaYLimpiar();
            ListadoEvolucion = lst.ToObservableCollection();
            ListadoEvolucionTodos = new List<ProcedimientosGrillaEvolucion>();
            ListadoEvolucionTodos = lst;
            RaisePropertyChanged("ListadoEvolucion");

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

            tarea.TrySetResult(true);

            return tarea.Task;
        }

        /// <summary>
        /// Validar modo lectura.
        /// </summary>
        private void ValidarModoLectura()
        {
            //validar xq llega vacio
            if (Variables_Globales.ParametroConvenio == null)
            {
                Variables_Globales.ParametroConvenio = new ParametroOdontologicoConvenioEntity() { RequiereClasificador = false };
            }

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
            }
        }

        private void tratamientoActivo(long idTratamiento)
        {
            TratamientoPadre = Variables_Globales.TratamientosPadre;

            if (TratamientoPadre.IdSesionActual != null)
            {
                Variables_Globales.SesionActual = (short)TratamientoPadre.IdSesionActual;
            }

            if (TiposTratamiento != null)
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

            this.Sesiones = TratamientoPadre.Sesiones.ToString();
            this.CuotaInicial = TratamientoPadre.CuotaInicial.ToString();
            this.ValorCuota = TratamientoPadre.ValorCuota.ToString();            
            if (TratamientoPadre.TipoTratamiento != null)
            {
                MostrarOpcionesNuevoTratamiento = true;
            }            
            if (FinalizaCumplimientoProcedimientos)
            {
                // Llama a la clase encargada de esto
                // Como no todas las veces se cerrara un procedimiento se paso a otra clase
                new Finalizar_Tratamiento().FinalizarTratamiento();
            }
        }

        private async Task SesionesConfiguradasTratamientos()
        {
            SesionesConfiguradasTratamiento = await Contexto_Odontologia.obtenerContexto().ConsultarSesionesTratamiento(Variables_Globales.IdTratamientoActivo); 

            // Gnera el numero de sesiones que se mostraran en el combo
            if (SesionesConfiguradasTratamiento != null && SesionesConfiguradasTratamiento.Count > 0)
            {
                int SesionMaxima;
                NumeroSesionesConfiguradas = NumeroSesiones.ToList().ToObservableCollection();
                SesionMaxima = SesionesConfiguradasTratamiento.Max(p => p.IdSesion);

                for (int i = NumeroSesionesConfiguradas.Count - 1; i >= 0; i--)
                {
                    if (NumeroSesionesConfiguradas[i] > SesionMaxima)
                    {
                        NumeroSesionesConfiguradas.RemoveAt(i);
                    }
                }
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.No_Existen_Sessiones_Asociadas
                });
            }

            RaisePropertyChanged("SesionesConfiguradasTratamiento");            
        }
        #endregion

        #region Propiedades
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
        /// <summary>
        /// Gets or sets the tipos tratamiento.
        /// </summary>
        /// <value>The tipos tratamiento.</value>
        public ObservableCollection<TipoTratamientoEntity> TiposTratamiento { get; private set; }

        /// <summary>
        /// Numero sesiones configuradas.
        /// </summary>
        /// <value>The numero sesiones configuradas.</value>
        public ObservableCollection<int> NumeroSesionesConfiguradas { get; set; }

        /// <summary>
        /// Sesiones configuradas tratamiento.
        /// </summary>
        /// <value>The sesiones configuradas tratamiento.</value>
        public ObservableCollection<SesionesPlanTratamientoEntity> SesionesConfiguradasTratamiento { get; set; }

        public ObservableCollection<OdontogramaEntity> OdontogramaTratamiento { get; set; }
        /// <summary>
        /// Numero sesiones.
        /// </summary>
        /// <value>The numero sesiones.</value>
        public ObservableCollection<int> NumeroSesiones { get; set; }

        
        public TratamientoEntity TratamientoPadre { get; set; }

        private TipoTratamientoEntity tipoTratamientoSeleccionado = new TipoTratamientoEntity();
        /// <summary>
        /// Para validar que tipo se selecciono, usar en paciente odontograma
        /// </summary>
        public TipoTratamientoEntity TiposTratamientoSeleccionado
        {
            set
            {
                tipoTratamientoSeleccionado = value;
            }
            get
            {
                return tipoTratamientoSeleccionado;
            }

        }

        public string DescripcionTratamiento { get; set; }
        
        public bool HabilitarTipoTratamiento { get; set; }

        private KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string> formaPagoSeleccionado;
        /// <summary>
        /// Gets or sets the forma pago seleccionado.
        /// </summary>
        /// <value>The forma pago seleccionado.</value>
        public KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string> FormaPagoSeleccionado
        {
            get
            {
                return formaPagoSeleccionado;
            }
            set
            {
                formaPagoSeleccionado = value;
            }

        }

        public short NumeroSesionesTratamiento { get; set; }

        public decimal ValorCuotaTratamiento { get; set; }

        public string Sesiones { get; set; }

        /// <summary>
        /// Forma pago odontologia.
        /// </summary>
        /// <value>The forma pago odontologia.</value>
        public List<KeyValueTriplet<Cnt.Panacea.Entities.Odontologia.FormaPago, byte, string>> FormaPagoOdontologia { get; set; }

        public string CuotaInicial { get; set; }

        public string ValorCuota { get; set; }
        
        public bool FinalizaCumplimientoProcedimientos { get; set; }

        public string usuarioFinaliza;
        /// <summary>
        /// Usuario finaliza tratamiento.
        /// </summary>
        /// <value>The usuario finaliza.</value>
        public String UsuarioFinaliza
        {
            get
            {
                return usuarioFinaliza;
            }
            set
            {
                usuarioFinaliza = value;
                RaisePropertyChanged("UsuarioFinaliza");
            }
        }

        public string passUsuarioFinaliza;
        /// <summary>
        /// Password usuario finaliza tratamiento.
        /// </summary>
        /// <value>The pass usuario finaliza.</value>
        public String PassUsuarioFinaliza
        {
            get
            {
                return passUsuarioFinaliza;
            }
            set
            {
                passUsuarioFinaliza = value;
                RaisePropertyChanged("PassUsuarioFinaliza");
            }
        }

        public int SesionActualCarga { get; set; }
        
        public decimal ValorPagoTratamiento { get; set; }
        /// <summary>
        /// Valor total tratamiento.
        /// </summary>
        /// <value>The valor total tratamiento.</value>
        public decimal ValorTotalTratamiento { get; set; }

        public long IdReciboCaja { get; set; }

        public decimal ValorSaldoTratamiento { get; set; }

        public PlanTratamientoEstadoNuevoEliminadoModificado PlanTratamientoEstadoNuevoEliminadoModificado { get; set; }

        public bool PuedeModificar { get; set; }

        public ObservableCollection<ProcedimientosGrillaEvolucion> ListadoEvolucion { get; set; }

        public ObservableCollection<Entities.Parametrizacion.FinalidadProcedimientoEntity> FinalidadesProcedimiento { get; set; }

        public ObservableCollection<Entities.Parametrizacion.TerceroEntity> OdontologosHigienistasIps { get; set; }

        public RelayCommand<ProcedimientosGrillaEvolucion> realizadoCommand { get; set; }

        public RelayCommand<ProcedimientosGrillaEvolucion> bodegaCommand { get; set; }

        public PlanesTratamientoCollection Planes { get; set; }

        public List<ProcedimientosGrillaEvolucion> ListadoEvolucionTodos { get; set; }

        public RelayCommand<int> cambioSesionCommand { get; set; }

        private bool mostrarOpcionesNuevoTratamiento;

        public bool MostrarOpcionesNuevoTratamiento
        {
            get { return mostrarOpcionesNuevoTratamiento; }
            set { mostrarOpcionesNuevoTratamiento = value; RaisePropertyChanged("MostrarOpcionesNuevoTratamiento"); }
        }

        #endregion
        

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Guardar>(this);
        }        
    }
}
