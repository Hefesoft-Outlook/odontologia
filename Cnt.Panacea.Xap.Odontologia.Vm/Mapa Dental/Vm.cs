using System;
using System.Windows;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Command;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using System.IO;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Boca;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM
{
    /// <summary>
    /// Este viewmodel corresponde al mapa dental
    /// Es un control primario es el encargado de recibir las peticiones para pintar datos
    /// Normalmente pinta superficies con la entidad OdontogramaEntity
    /// Tambien se encarga de escuchar las peticiones de otros controles
    /// Ej:  El odontograma inicial necesita saber que superficies estan pintadas 
    /// Este viewmodel le responde en el formato que el lo requiere con la funcion Messenger
    /// Que esta en el framework de galasoft
    /// Tambien cambia de estado dependiendo del estado del modulo 
    /// Ej: Por medio del messenger se le avisa a este viewmodel que debe limpiar sus superficies
    /// ya que se creara un odontograma nuevo
    /// </summary>
    public class Vm : ViewModelBase, IDisposable
    {
        #region Constructor
        public Vm()
        {            
            if (IsInDesignMode)
            {
                
            }
            else
            {
                // El control arranca en modo odontograma inicial
                this.Modo = new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial };
                Texto_Modo_Actual = "Odontograma inicial";

                clickDerechoContenedorPiezaDentalCommand = new RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(clickDerechoContenedorPiezaDental);
                mostrarProcedimientosEvolucionCommand = new RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(mostrarProcedimientosEvolucion);
                imprimirCommand = new RelayCommand(imprimir);
                agregarSuperNumerarioCommand = new RelayCommand<object>(agregarSupernumerario);
                clickNumeroSuperficieCommand = new RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(clickNumeroSuperficie);
                AdjuntarImagenCommand = new RelayCommand(AdjuntarImagen);
                verImagenesCommand = new RelayCommand(verImagenes);

                //Cuando cambia de Odontograma inicial a plan de tratamiento etc
                oirCambioOdontograma();
                oirGuardarImagenes();
                cargarPartes();
                oirMensajeBoca();
                
                //Si otro formulario Ej: Odontograma inicial requiere los datos pintados para guardarlos
                //Este comando los transforma y se los envia con el formato que se requiera
                oirPeticionesDatos();
                oirPeticionesValidaciones();
                oirPeticionPintarDatos();
                oirPeticionesImagenesCargadas();
                DeshacerCommand = new RelayCommand<bool>(deshacer);                
            }
        }

        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                Modo = item;                

                if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    Texto_Modo_Actual = "Odontograma inicial";
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
                {
                    Texto_Modo_Actual = "Plan de tratamiento";
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Evolucion)
                {
                    Texto_Modo_Actual = "Evolución";
                }

                RaisePropertyChanged("Texto_Modo_Actual");

            });
        }

        private void oirGuardarImagenes()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.vm.Messenger.Imagenes.Guardar_Imagenes>(this, elemento => 
            {
                new Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.Imagenes.Imagenes().GuardarImagenes(this);
            });
        }

        private void mostrarProcedimientosEvolucion(Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma obj)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Procedimientos evolucion",                
            });   
        }

        public void imprimir()
        {
            //Saca todos los elementos pintados en el odontograma
            var elementos = new Validaciones().convertirAEntidadesOdontogramaEntityTodos(this);
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Imprimir.Imprimir() { Listado = elementos });
        }


        public void clickDerechoContenedorPiezaDental(Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma obj)
        {           
            new Cnt.Panacea.Xap.Odontologia.Vm.Supernumerario.Agregar_Supernumerario().clickDerechoContenedorPiezaDental(this, obj);  
        }        

        //El parametro que se recibe es el tag del hyperlink
        //Que indica la accion a realizar si es izquierda derecha o eliminar
        private void agregarSupernumerario(object elemento)
        {   
            new Cnt.Panacea.Xap.Odontologia.Vm.Supernumerario.Agregar_Supernumerario().menuSupernumerario(this, elemento);
        }
        #endregion


        #region metodos
        

        //Se mueve la funcionalidad a una nueva clase ya que es una funcionalidad 
        //Excepcional
        private void AdjuntarImagen()
        {
            //Le envia el mensaje al control mapa dental para que cargue las imagenes
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Imagenes.Adjuntar());
        }


        private void clickNumeroSuperficie(Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma obj)
        {
            //Indicarle al control que muestra la superficie seleccionada que se hizo             
            Messenger.Default.Send(obj, "Pieza Seleccionada");
        }        

        private void oirPeticionesValidaciones()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental.Validaciones>(this, validar =>
            {
                var validaciones = new Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental.Validaciones();
                Busy.UserControlCargando(true, "Validando que cada superficie tenga un procedimiento");
                var resultadoValidacion = validaciones.validarcadaDiagnosticoTieneProcedimientos(this);
                Busy.UserControlCargando(false);

                // Le respondemos al que nos pregunta sobre la validacion
                validar.Result(resultadoValidacion);
            });
        }

        private void oirPeticionesImagenesCargadas()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental.Imagenes.listado_Imagenes_Cargadas>(this, imagen =>
            {
                //Envia el listado de imagenes cargadas al formulario que lo necesite
                imagen.listadoImagenesCargadas(LstImagenes);
            });
        }

        //Oye las peticiones de los otros formularios para pintar datos
        private void oirPeticionPintarDatos()
        {
            Messenger.Default.Register<Pedir_Pintar_Datos>(this, datos =>
            {
                if (datos.Limpiar_Datos)
                {
                    //Quita las piezas dentales que son supernumerarias
                    new Cnt.Panacea.Xap.Odontologia.Vm.Supernumerario.Agregar_Supernumerario().limpiarSuperNumerarios(this);                    
                }

                // Cuando el pedido es falso es xq va a pintar algo que viene desde la bd
                if (!datos.nuevoOdontograma)
                {
                    var listado = new Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental.ui().ConvertirInterfazOdontograma(this ,datos.lst);
                    var result = new Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental.ui().pintarDatos(this, listado);
                    
                    // Le enviamos los resultados a los formularios que lo necesiten
                    // Ej: La grilla de evolucion lo necesita para saber que debe pintar
                    // Se valida que alguien este oyendo la respuesta
                    if (datos.Resultado != null)
                    {
                        datos.Resultado(result);
                    }
                }
            });
        }

        private void verImagenes()
        {
            new Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.Imagenes.Imagenes().mostrarImagenes(this);
        }

        private void oirPeticionesDatos()
        {
            Messenger.Default.Register<Guardar>(this, peticion =>
            {
                if (peticion.Tipo_Datos_Solicitar == Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Tipo_Datos_Solicitar.Odontograma_Entity)
                {
                    new Cnt.Panacea.Xap.Odontologia.Vm.Util.Mapa_dental.ui().datosTransformadosOdontogramaEntity(this, peticion);
                }
                else if (peticion.Tipo_Datos_Solicitar == Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Tipo_Datos_Solicitar.Clase_Odontograma)
                {
                    peticion.lstOdontogramas(lstOdontograma);
                }
            });
        }        

        private void deshacer(bool obj)
        {
            Messenger.Default.Send(new Estado_DesHacer() { Estado = obj });
        }

        private void oirMensajeBoca()
        {
            Messenger.Default.Register<Boca_Msg>(this, item =>
            {
                Boca = item.Boca;
            });
        }

        void cargarPartes()
        {
            
            //Esta clase carga los diferentes listviews del mapa dental
            new Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.util.Cargar_Partes().cargarPartes(this);
        }

        #endregion     


        #region Propiedades

        private Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma lstSeleccionado;

        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma LstSeleccionado
        {
            get { return lstSeleccionado; }
            set
            {
                if (value != null)
                {
                    lstSeleccionado = value;                
                    Messenger.Default.Send(value, "Pieza Seleccionada");
                }
            }
        }

        public List<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lstOdontograma { get; set; }

        public RelayCommand<bool> DeshacerCommand { get; set; }

        public Cambiar_Tipo_Odontograma Modo { get; set; }

        public RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> clickNumeroSuperficieCommand { get; set; }

        public List<TratamientoImagenEntity> LstImagenes { get; set; }

        public ObservableCollection<TratamientoImagenEntity> TratamientoImagenEntity { get; set; }

        public RelayCommand AdjuntarImagenCommand { get; set; }

        public RelayCommand verImagenesCommand { get; set; }

        private Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma boca = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma("99");

        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma Boca
        {
            get { return boca; }
            set { boca = value; RaisePropertyChanged("Boca"); }
        }


        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte1 { get; set; }
        /// <summary>
        /// LstOdontogramaParte2
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte2 { get; set; }
        /// <summary>
        /// LstOdontogramaParte3
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte3 { get; set; }
        /// <summary>
        /// LstOdontogramaParte4
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte4 { get; set; }
        /// <summary>
        /// LstOdontogramaParte5
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte5 { get; set; }
        /// <summary>
        /// LstOdontogramaParte6
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte6 { get; set; }
        /// <summary>
        /// LstOdontogramaParte7
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte7 { get; set; }
        /// <summary>
        /// LstOdontogramaParte8
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> LstOdontogramaParte8 { get; set; }

        private bool lupa_Activa = true;

        public bool Lupa_Activa
        {
            get { return lupa_Activa; }
            set { lupa_Activa = value; RaisePropertyChanged("Lupa_Activa"); }
        }

        /// <summary>
        /// SupernumerariosActuales
        /// </summary>
        public int SupernumerariosActuales { get; set; }
      

        public RelayCommand<object> agregarSuperNumerarioCommand { get; set; }

        public RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> clickDerechoContenedorPiezaDentalCommand { get; set; }

        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma OdontogramaSeleccionadoSupernumerario { get; set; }

        public RelayCommand imprimirCommand { get; set; }

        public RelayCommand<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> mostrarProcedimientosEvolucionCommand { get; set; }

        #endregion


        public void Dispose()
        {
            Messenger.Default.Unregister<Boca_Msg>(this);
            Messenger.Default.Unregister<Pedir_Pintar_Datos>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental.Validaciones>(this);
            Messenger.Default.Unregister<Guardar>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.vm.Messenger.Imagenes.Guardar_Imagenes>(this);
        }

        public string Texto_Modo_Actual { get; set; }
    }
}
