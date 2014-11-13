using System.Windows;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Diagnosticos_Procedimientos;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Util;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Boca;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.PopUp;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Odontograma
{
    /// <summary>
    /// Clase que sirve como apoyo para lograr la funcionalidad grafica de los elementos del odontograma
    /// </summary>
    public class Odontograma : GalaSoft.MvvmLight.ViewModelBase, IDisposable
    {
        #region Constructor

        public Odontograma()
        {

        }

        public Odontograma(string codigoSPiezaDental)
        {
            this.codigoSPiezaDental = codigoSPiezaDental;

            if (IsInDesignMode)
            {
                DiagnosticoProcedimiento.Superficie1 = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();

                var itemPrueba = new ConfigurarDiagnosticoProcedimOtraEntity()
                {                    
                    AplicaTratamiento = 1,//AplicaTratamiento.Diente,
                    Codigo = "K022",//Codigo Diag
                    Descripcion ="Caries del cemento",
                    Diagnostico = 3777,
                    //Config Simbolo Aplica tambien para Px,Otros
                    Simbolo= "E",
                    ColorAdicional = 6750207,
                    Fuente = "Wingdings 3",                
                    //
                    //Solo para Dx
                    IndiceCOP = true,
                    IndiceCEO =true,
                    IndicePlacaBacteriana=true,
                    Severidad =true,//Si es true debe exitir el comob de Severidad, si es false no es obligatorio
                };

                DiagnosticoProcedimiento.Superficie1.Add(itemPrueba);
            }
            else
            {
                Tipo_Odontograma_Actual = new Cambiar_Tipo_Odontograma();
                Tiene_Diagnosticos = new Tiene_Diagnosticos();
                Estado_Deshacer = new Estado_DesHacer();                
                ClickCommand = new RelayCommand<string>(click);
                Estado_Deshacer.Estado = false;

                diagnosticoSeleccionado();
                oirTipoOdontograma();
                oirDesHacer();                
                oirNivelSeveridad();
                oirProcedimientoRealizado();

                //Ejecuta las operaciones necesarias cuando el usuario da click en nuevo odontograma
                //O carga un tratamiento existente
                oirCargaONuevoOdontograma();
            }            
        }

        private void oirCargaONuevoOdontograma()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento=>
            {
                //Nuevo es cuando se da click en nuevo odontograma
                // Todos cuando se carga un plan de tratamiento
                if (elemento.valor == "Nuevo" || elemento.valor == "Todos")
                {
                    sumaIndice = false;
                    IdiceCEO = false;
                    IdiceCOP = false;
                    indicePlacaBacteriana = 0;
                }
            });
        }


        private void oirProcedimientoRealizado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Procedimiento_Realizado>(this, codigoSPiezaDental, item => 
            {                
                if (item.Superficie.Equals("Superficie1"))
                {                    
                    DiagnosticoProcedimiento.Realizado_Superficie1 = item.Realizado;
                }
                else if (item.Superficie.Equals("Superficie2"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie2 = item.Realizado;
                }

                else if (item.Superficie.Equals("Superficie3"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie3 = item.Realizado;
                }

                else if (item.Superficie.Equals("Superficie4"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie4 = item.Realizado;
                }

                else if (item.Superficie.Equals("Superficie5"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie5 = item.Realizado;
                }

                else if (item.Superficie.Equals("Superficie6"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie6 = item.Realizado;
                }
                else if (item.Superficie.Equals("Superficie7"))
                {
                    DiagnosticoProcedimiento.Realizado_Superficie7 = item.Realizado;
                }
                else if (item.Superficie.Equals("Pieza_Completa"))
                {
                    DiagnosticoProcedimiento.Realizado_Pieza_Completa = item.Realizado;
                }
                else if (item.Superficie.Equals("Boca"))
                {
                    DiagnosticoProcedimiento.Realizado_Boca = item.Realizado;
                }
                else
                {
                    // No encuentra la superficie entonces no hace nada
                    // Si llega aca se esta haciendo algo mal
                }
            });
        }

        private void oirNivelSeveridad()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<NivelSeveridadDXEntity>(this, nivel =>
            {
                NivelSeveridadSeleccionado = nivel;
            });
        }     

        private void oirDesHacer()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Estado_DesHacer>(this, Estado =>
            {
                Estado_Deshacer = Estado;
            });            
        }

        private void oirTipoOdontograma()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {   
                Tipo_Odontograma_Actual = item;
                pintarSuperficiesPlanTratamiento(item);
            });
        }

        public void pintarSuperficiesPlanTratamiento(Cambiar_Tipo_Odontograma item)
        {
            if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
            {
                diagnosticoProcedimiento.habilitarSuperficies();
            }
            else if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
            {
                diagnosticoProcedimiento.inhabilitarSuperficies();
            }
        }
        

        private void diagnosticoSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ConfigurarDiagnosticoProcedimOtraEntity>(this, diagnostico => 
            {
                Paleta_Selecionado = diagnostico;
            });
        }
        #endregion
        
        #region Variables
        string colorcodigoSPiezaDental = null;
        string codigoSpiezaDental = null;
        Tiene_Diagnosticos Tiene_Diagnosticos;
        #endregion

        #region Propiedades        

        private Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.DiagnosticoProcedimiento diagnosticoProcedimiento = new Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.DiagnosticoProcedimiento();

        public Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.DiagnosticoProcedimiento DiagnosticoProcedimiento
        {
            get { return diagnosticoProcedimiento; }
            set { diagnosticoProcedimiento = value; RaisePropertyChanged("DiagnosticoProcedimiento"); }
        }       

        

        public RelayCommand<string> ClickCommand { get; private set; }

        /// <summary>
        /// Identificador de la pieza dental
        /// </summary>
        public int codigoPiezaDental { get; set; }

        /// <summary>
        /// Identificador string de la pieza dental
        /// </summary>
        public string codigoSPiezaDental { get { return codigoSpiezaDental; } set { codigoSpiezaDental = value; RaisePropertyChanged("codigoSPiezaDental"); } }

        /// <summary>
        /// Color del identificador de la pieza dental
        /// </summary>
        public string colorCodigoSPiezaDental { get { return colorcodigoSPiezaDental; } set { colorcodigoSPiezaDental = value; RaisePropertyChanged("colorCodigoSPiezaDental"); } }

        /// <summary>
        /// Variable de control para identificar la superficie seleccionada en el momento
        /// </summary>
        public string SuperficieSeleccionada { get; set; }

        /// <summary>
        /// Hacer referencia a cada una de las superficies de la base de datos
        /// </summary>
        public Superficie CodigoSuperficie1 { get; set; }
        public Superficie CodigoSuperficie2 { get; set; }
        public Superficie CodigoSuperficie3 { get; set; }
        public Superficie CodigoSuperficie4 { get; set; }
        public Superficie CodigoSuperficie5 { get; set; }
        public Superficie CodigoSuperficie6 { get; set; }
        public Superficie CodigoSuperficie7 { get; set; }
        public Superficie CodigoPiezaCompleta { get; set; }

    
        public int UbicacionOdontograma { get; set; }      

        /// <summary>
        /// Variable que permite identificar el elemento seleccionado en el momento
        /// </summary>
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Identifica si la pieza dental es un diente supernumerario
        /// </summary>
        public bool Supernumerario { get; set; }
        /// <summary>
        /// Identifica si el diente seleccionado es supernumerario hacia la izquieda de otro diente
        /// </summary>
        public referenciaSupernumerario Referencia { get; set; }       
      
        /// <summary>
        /// Variable que alamacena este indice para el control odontograma
        /// </summary>
        public bool IdiceCOP { get; set; }
        /// <summary>
        /// Variable que alamacena este indice para el control odontograma
        /// </summary>
        public bool IdiceCEO { get; set; }
        /// <summary>
        /// Identifica si posee un diente supernumerario a la izquierda del diente
        /// </summary>
        public bool TieneDienteSupernumerarioIzquierda { get; set; }
        /// <summary>
        /// Identifica si posee un diente supernumerario a la derecha del diente
        /// </summary>
        public bool TieneDienteSupernumerarioDerecha { get; set; }

        public ConfigurarDiagnosticoProcedimOtraEntity Paleta_Selecionado { get; set; }

        #endregion

        #region Metodos

        public void click(string superFicieSeleccionada)
        {
            // Validamos que en la paleta de diagnosticos y procedimientos 
            // Algo este seleccionado
            if (Paleta_Selecionado != null)
            {
                bool validoNivelSeveridad;
                validoNivelSeveridad = validarNivelSeveridad();

                if (validoNivelSeveridad)
                {
                    this.SuperficieSeleccionada = superFicieSeleccionada;

                    if (Paleta_Selecionado.AplicaTratamiento == 1)
                    {
                        this.SuperficieSeleccionada = "Pieza_Completa";
                    }
                    if (Paleta_Selecionado.AplicaTratamiento == 3)
                    {
                        this.SuperficieSeleccionada = "Boca";
                    }
                    
                    aplicarOdontograma();
                    enviarMensajeBoca();
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "Indique el nivel de severidad"
                    });
                }
            }
        }

        private bool validarNivelSeveridad()
        {
            bool validoNivelSeveridad;
            if (Paleta_Selecionado != null && Paleta_Selecionado.Severidad == true)
            {
                if (NivelSeveridadSeleccionado == null)
                {
                    validoNivelSeveridad = false;
                }
                else
                {
                    validoNivelSeveridad = true;
                }
            }
            else
            {
                validoNivelSeveridad = true;
            }
            return validoNivelSeveridad;
        }

        private void aplicarOdontograma()
        {
            // Valida que se haya seleccionado un elemento en la paleta
            if (Paleta_Selecionado != null)
            {
                //Cuando el check de deshacer esta seleccionado se va por este lado
                if (Estado_Deshacer != null && Estado_Deshacer.Estado)
                {
                    var resultadoDeshacer = DiagnosticoProcedimiento.desHacerOdontograma(Paleta_Selecionado, Tipo_Odontograma_Actual, SuperficieSeleccionada);
                }
                else
                {
                    DiagnosticoProcedimiento_Validaciones resultado;
                    
                    if (Tipo_Odontograma_Actual.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                    {
                        resultado = DiagnosticoProcedimiento.agregarOdontogramaInicial(Paleta_Selecionado, Tipo_Odontograma_Actual, SuperficieSeleccionada, NivelSeveridadSeleccionado, codigoPiezaDental);
                        
                        //Mostrar el diente seleccionado
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(this);
                        
                        //Calculos para indices ceo y ceop
                        calculosIndices(); 
                    }
                    else
                    {
                        resultado = DiagnosticoProcedimiento.agregarOdontogramaPlanTratamiento(Paleta_Selecionado, Tipo_Odontograma_Actual, SuperficieSeleccionada, codigoPiezaDental);
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(this);
                    }

                    if (resultado == DiagnosticoProcedimiento_Validaciones.Existe)
                    {
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                        {
                            Mensaje = "Ya se encuentra en la lista"
                        });
                    }
                    else if (resultado == DiagnosticoProcedimiento_Validaciones.Ya_posee_elementos)
                    {
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                        {
                            Nombre = "Sobreescribir Procedimiento",
                            Resultado = (obj)=>
                            {
                                mensajeSobreescribirAdicionar(obj);
                            }
                        });
                    }
                }
                RaisePropertyChanged("Paleta_Selecionado");
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "Indique un procedimiento o un diagnostico para aplicar"
                });
            }
        }

        private void mensajeSobreescribirAdicionar(object obj)
        {
            var confirmacion = (EstadoSobreEscribirAdicionar)obj;
            if (confirmacion.Adicionar)
            {
                DiagnosticoProcedimiento.adicionar(Paleta_Selecionado, Tipo_Odontograma_Actual, SuperficieSeleccionada);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(this);
                calculosIndices();
            }
            else if (confirmacion.SobreEscribir)
            {
                DiagnosticoProcedimiento.sobreEscribir(Paleta_Selecionado, Tipo_Odontograma_Actual, SuperficieSeleccionada);
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(this);
                calculosIndices();
            }
        }

        private void calculosIndices()
        {
            if (Paleta_Selecionado.IndiceCEO || Paleta_Selecionado.IndiceCOP || Paleta_Selecionado.IndicePlacaBacteriana)
            {
                //Indica que este elemento sumara dentro del calculo ceo o ceop
                sumaIndice = DiagnosticoProcedimiento.lst.Any(a => a.ConfigurarDiagnosticoProcedimOtraEntity.IndiceCEO);

                /*
                 3.	Placa bacteriana: superficies marcadas con placa (solo se debe tomar 4 superficies por cada diente máximo) * 100 / piezas presentes * 4.
                 */
                indicePlacaBacteriana = DiagnosticoProcedimiento.lst.Count(a => a.ConfigurarDiagnosticoProcedimOtraEntity.IndicePlacaBacteriana);

                if (indicePlacaBacteriana > 4)
                {
                    indicePlacaBacteriana = 4;
                }

                //Calculo de indices ceo, ceop, placa bacteriana
                // Envia el mesaje que se debe recalcular
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Indices.Recalcular_Indices());
            }
        }

        private void enviarMensajeBoca()
        {
            if (Paleta_Selecionado.AplicaTratamiento == 3)
            {                
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Boca_Msg() { Boca = this });
            }
        }
       
        #endregion

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<ConfigurarDiagnosticoProcedimOtraEntity>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Estado_DesHacer>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Aplicar_Tratamiento>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<NivelSeveridadDXEntity>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Procedimiento_Realizado>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
        }

        public Estado_DesHacer Estado_Deshacer { get; set; }

        public Cambiar_Tipo_Odontograma Tipo_Odontograma_Actual { get; set; }

        internal Tipo_Diente_Permanente_Temporal Tipo_Diente_Permanente_Temporal{ get; set; }

        public NivelSeveridadDXEntity NivelSeveridadSeleccionado { get; set; }

        public bool sumaIndice { get; set; }


        public int indicePlacaBacteriana { get; set; }               

    }

    /// <summary>
    /// Clase en la cual se genera la estructura por la cual se manejara en el entorno Grafico
    /// los diagnosticos y procedimientos en el control odontograma
    /// </summary>
   

    /// <summary>
    /// Clase que establece las propiedades que tiene cada superficie de la pieza dental
    /// </summary>
    public class EntitySuperficie
    {
        #region Propiedades
        /// <summary>
        /// Identificador de la superficie
        /// </summary>
        public int CodigoSuperficie { get; set; }
        /// <summary>
        /// La superficie se encuentra activa
        /// </summary>
        public bool ActivoSuperficie { get; set; }
        /// <summary>
        /// Nombre de la superficie
        /// </summary>
        public string Superficie { get; set; }
        /// <summary>
        /// Procedimientos y diagnosticos en la superficie
        /// </summary>
        public string ProcedimientosDiagnosticosSuperficie { get; set; }
        #endregion
    }

    /// <summary>
    /// Clase creda para visualizar el componente grafico de la grilla en odontograma inicial y plan de tratamiento
    /// </summary>
    public class SuperficiesGrillaPlanTratamiento
    {
        #region Propiedades
        /// <summary>
        /// Elemento del control que se desea mostrar en la grilla
        /// </summary>
        public Odontograma Odontograma { get; set; }
        /// <summary>
        /// Campo de la columna nombre de la superficie
        /// </summary>
        public string NombreSuperficie { get; set; }
        /// <summary>
        /// Aliaa del nombre de la superficie al interior de la aplicacion
        /// </summary>
        public string NombreSuperficieApp { get; set; }
        /// <summary>
        /// ista de procedimientos que se mostraran en la grilla en esta superficie
        /// </summary>
        public List<ProcedimientoPlanTratamiento> ProcedimientoEntity { get; set; }
        /// <summary>
        /// ista de Diagnosticos que se mostraran en la grilla en esta superficie
        /// </summary>
        public List<ConfiguracionDiagnosticoOdontologiaEntity> DiagnosticoEntity { get; set; }
        /// <summary>
        /// Columna opciones de tratamiento
        /// </summary>
        public ObservableCollection<Cnt.Panacea.Entities.Odontologia.OpcionesTratamiento> OpcionesTratamiento { get; set; }
        /// <summary>
        /// Valor seleccionado para esta columna
        /// </summary>
        public Cnt.Panacea.Entities.Odontologia.OpcionesTratamiento OpcionesTratamientoValor { get; set; }
        /// <summary>
        /// Lista de especialidades
        /// </summary>
        public ObservableCollection<ProcedimientoEspecialidadSedeEntity> ProcedimientosEspecialidad { get; set; }
        /// <summary>
        /// Numero de sessiones para este procedimiento
        /// </summary>
        public ObservableCollection<int> NumeroSesiones { get; set; }
        /// <summary>
        /// Selecionar dentro de la grilla para aplicar un procedimiento
        /// </summary>
        public bool Seleccionar { get; set; }
        /// <summary>
        /// El procedimiento se encuentra cubierto por el convenio
        /// </summary>
        public bool Convenio { get; set; }
        #endregion

    }

    /// <summary>
    /// Estructura de como se manejan los procedimientos al interior de un plan de tratamiento
    /// </summary>
    public class ProcedimientoPlanTratamiento
    {
        #region Propiedades
        /// <summary>
        /// Entidad procedimientos de la base de datos
        /// </summary>
        public ConfiguracionProcedimientoOdontologiaEntity ProcedimientoEntity { get; set; }
        /// <summary>
        /// Entidad plan de tratamiento de la base de datos
        /// </summary>
        public PlanTratamientoEntity PlanTratamientoEntity { get; set; }
        #endregion
    }

    public class referenciaSupernumerario
    {
        public int Diente_Referencia { get; set; }
        public bool Esta_A_la_Derecha { get; set; }
    }

    // Se usa para validar que tipo de diente es para el calculo ceo, cop y placa bacteriana
    internal enum Tipo_Diente_Permanente_Temporal
    {
        Ninguno = 0,
        Permanente = 1,
        Temporal = 2
    }   
}
