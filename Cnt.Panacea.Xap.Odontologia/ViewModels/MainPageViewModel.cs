using System;
using System.Windows.Controls;
using System.Windows.Input;
using Cnt.Std.Xap;
using GalaSoft.MvvmLight.Command;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia;
using System.Linq;
using Cnt.Std.Xap.Controles;
using System.Windows.Browser;
using System.Globalization;
using System.Windows;
using Cnt.Panacea.Xap.Odontologia.PopUp;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Barra;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.ViewModels
{
    public class MainPageViewModel : GalaSoft.MvvmLight.ViewModelBase, IDisposable
    {

        /// <summary>
        /// View Model que controla la vista principal.
        /// </summary>
        /// <remarks><code>
        /// *-----------------------------------------------------------------------------------------*
        /// * Copyright (C) 2008 CNT Sistemas de Información S.A., Todos los Derechos Reservados
        /// * http://www.cnt.com.co - mailto:produccion_panacea@cnt.com.co
        /// *
        /// * Archivo:      Contexto.cs
        /// * Tipo:         Clase de Apoyo
        /// * Autor:        Jose Ramirez
        /// * Fecha:        2011 Mayo 24
        /// * Propósito:    Administrar la vista Principal.
        /// *----------------------------------------------------------------------------------------
        /// </code></remarks>          

        #region Constructor / Destructor
        /// <summary>
        /// Inicializa una nueva instancia de la clase.
        /// </summary>
        public MainPageViewModel()
        {
            try
            {
                resourcedictionary = new ResourceDictionary();
                resourcedictionary.Source = new Uri("/Cnt.Panacea.Xap.Estilos;component/Cnt.Xap.Estilos.xaml", UriKind.RelativeOrAbsolute);
                datosPacienteCommand = new RelayCommand(CargardatosPaciente);
                InformesCommand = new RelayCommand<string>(CargarInformes);                
                oirMostrarVolver();

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void oirMostrarVolver()
        {
            Messenger.Default.Register<Mostrar_Botones_Barra>(this, item => 
            {
                if (item.elemento == "Volver")
                {
                    mostrarVolver();
                }            
            });
        }

        #endregion Constructor / Destructor

        #region Variables

        #region Variables de fecha y hora
        /// <summary>
        /// 
        /// </summary>
        private ResourceDictionary resourcedictionary;
        /// <summary>
        /// FechaInicialRangoFechas
        /// </summary>
        private DateTime fecha = DateTime.Now.Date;

        /// <summary>
        /// Hora
        /// </summary>
        private DateTime hora = DateTime.Now;

        #endregion Variables de fecha y hora


        #endregion Variables

        #region Propiedades

        #region  fechas y hora

        /// <summary>
        /// Obtiene o establece la propiedad fechaInicialRangoFechas.
        /// </summary>
        /// <value>FechaInicialRangoFechas.</value>
        public DateTime Fecha
        {
            get { return this.fecha; }
            set
            {
                if (this.fecha != value)
                {
                    this.fecha = value;
                    RaisePropertyChanged("Fecha");
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la propiedad hora.
        /// </summary>
        /// <value>Hora.</value>
        public DateTime Hora
        {
            get { return this.hora; }
            set
            {
                if (this.hora != value)
                {
                    this.hora = value;
                    RaisePropertyChanged("Hora");
                }
            }
        }

        /// <summary>
        /// Obtiene o establece la propiedad fechaCompleta.
        /// </summary>
        /// <value>FechaCompleta.</value>
        public DateTime FechaCompleta
        {
            get
            {
                return new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Hora.Hour, Hora.Minute, Hora.Second);
            }
        }



        public Grid UserControl { get; set; }

        public OdontogramasPacienteEntity odontogramaPaciente { get; set; }

        #endregion  fechas y hora

        #region Commands

        /// <summary>
        /// Gets or sets the identificador paciente.
        /// </summary>
        /// <value>The identificador paciente.</value>
        public int IdentificadorPaciente { get; private set; }


        /// <summary>
        /// Gets or sets the datos paciente command.
        /// </summary>
        /// <value>The datos paciente command.</value>
        public ICommand datosPacienteCommand { get; private set; }


        /// <summary>
        /// Comando de la barra de título.
        /// </summary>
        /// <value>barra titulo command.</value>
        public ICommand BarraTituloCommand { get; set; }

        /// <summary>
        /// Comando del botón aceptar del popup de terceros.
        /// </summary>
        /// <value>Aceptar Command.</value>
        public ICommand AceptarCommand { get; set; }

        /// <summary>
        /// Comando del botón aceptar del popup de terceros.
        /// </summary>
        /// <value>AceptarMensajeCommand.</value>
        public ICommand AceptarMensajeCommand { get; set; }

        /// <summary>
        /// Comando del botón aceptar del popup de justificación.
        /// </summary>
        /// <value>Aceptar Justificacion Command.</value>
        public ICommand AceptarJustificacionCommand { get; set; }

        /// <summary>
        /// Comando del botón cancelar del popup de terceros.
        /// </summary>
        /// <value>Cancelar Command.</value>
        public ICommand CancelarCommand { get; set; }

        /// <summary>
        /// Comando del botón cancelar del popup de justificación.
        /// </summary>
        /// <value>Cancelar Justificacion Command.</value>
        public ICommand CancelarJustificacionCommand { get; set; }

        /// <summary>
        /// Gets or sets the informes command.
        /// </summary>
        /// <value>The informes command.</value>
        public RelayCommand<string> InformesCommand { get; set; }

        /// <summary>
        /// Gets or sets the informes command.
        /// </summary>
        /// <value>The informes command.</value>
        public ICommand BarraCancelarCommand { get; set; }

        #endregion Commands

        public int IdPrestador { get; set; }
        public double ActualWith { get; set; }
        public bool ClasificadorOdontologico { get; set; }
        public int IdPaciente { get; set; }
        public Grid Barra { get; set; }
        public BarraTitulosAsistencial BarraTitulosAsistencial { get; set; }
        
        
        #endregion Propiedades

        #region Metodos


        /// <summary>
        /// Cargars the informes.
        /// </summary>
        /// <param name="nombreInforme">The nombre informe.</param>
        public void CargarInformes(string nombreInforme)
        {
            SeleccionReporte popReporte = new SeleccionReporte();
            popReporte.Show();
            popReporte.OKButton.Click += (sender, e) =>
                {
                    ListBoxItem listItem = popReporte.LstBoxReportes.SelectedItem as ListBoxItem;
                    nombreInforme = listItem.Tag.ToString();
                    GenerarInforme(nombreInforme);
                };
        }

        /// <summary>
        /// Generars the informe.
        /// </summary>
        /// <param name="nombreInforme">The nombre informe.</param>
        private void GenerarInforme(string nombreInforme)
        {           
            long idTratamientoAux = Variables_Globales.IdTratamientoActivo;
            //LFDO para q no se envie el tratamiento cuando el reporte no lo requiere
            if (nombreInforme == "InfConfProcedDiagnos")
                { idTratamientoAux = 0; }

            string appuri = HtmlPage.Document.DocumentUri.ToString().Replace("Historia_Clinica/RegistrarAtencionDatosForm.aspx", "ReportesForm.aspx");
            string url = string.Format(CultureInfo.CurrentCulture, "{0}?reporte={1}&idtratamiento={2}",
                appuri, nombreInforme, idTratamientoAux);

            HtmlPage.Window.Invoke("MostarVentanaInformes", url);
        }

        /// <summary>
        /// Carga los datos del paciente
        /// </summary>
        public void CargardatosPaciente()
        {
            if (Variables_Globales.IdPaciente == 0)
            {
                
            }
            if (Variables_Globales.IdFinalidadProcedimiento == 0)
            {
                Variables_Globales.IdFinalidadProcedimiento = 1;
            }

            
            ClasificadorOdontologico = true;
            
            


            

            odontogramaInicialbtn();
            odontogramaPlanTratamientobtn();
            odontogramaEvolucion();
            odontogramaProcedimientos();
        }

        internal void ventanaProcedimientos()
        {
            //Se muestran los procedimientos
            var procedimientos = new Odontologia.Views.PacienteTratamientos() { DataContext = new ViewModels.PacienteTratamientosViewModel() {  } };
            Generico popUp = new Generico();
            //En que fila lo queremos ubicar
            Grid.SetRow(procedimientos,0);
            popUp.LayoutRoot.Children.Add(procedimientos);
            popUp.OKButton.Visibility = Visibility.Collapsed;
            popUp.CancelButton.Visibility = Visibility.Collapsed;
            popUp.Width = 900;
            popUp.Height = 900;
            popUp.Show();
        }

        /// <summary>
        /// Invoca el metodo para actualizar los enlaces a datos con interfaz grafica desde otros formularios
        /// </summary>
        /// <param name="propiedad"></param>    
        public void RaiseUI(string propiedad)
        {
            RaisePropertyChanged(propiedad);
        }
      

        public void mostrarVolver() 
        {   
            var Volver = (Button)BarraTitulosAsistencial.FindName("VolverButton");
            if (Volver == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "VolverButton", Width = 80, Height = 30, Content = Odontologia.Recursos.Mensajes.Volver, Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                Volver = (Button)BarraTitulosAsistencial.FindName("VolverButton");
            }
            Volver.Visibility = System.Windows.Visibility.Visible;            

            Volver.Click += (m, n) =>
            {
                CargardatosPaciente();
                Variables_Globales.IdTratamientoActivo = 0;
            };
        }

        public void odontogramaInicialbtn()
        {
            var odontogramaInicial = (Button)BarraTitulosAsistencial.FindName("odontogramaInicialBtn");
            if (odontogramaInicial == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "inicialBtn", Width = 160, Height = 30, Content = "Odontograma inicial", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaInicial = (Button)BarraTitulosAsistencial.FindName("inicialBtn");
            }
            odontogramaInicial.Visibility = System.Windows.Visibility.Visible;            

            odontogramaInicial.Click += (m, n) =>
            {
                Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });   
            };
        }

        public void odontogramaPlanTratamientobtn()
        {
            var odontogramaPlanTratamiento = (Button)BarraTitulosAsistencial.FindName("odontogramaPlanTratamientoBtn");
            if (odontogramaPlanTratamiento == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "tratamientolBtn", Width = 160, Height = 30, Content = "Odontograma plan de tratamiento", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaPlanTratamiento = (Button)BarraTitulosAsistencial.FindName("tratamientolBtn");
            }
            odontogramaPlanTratamiento.Visibility = System.Windows.Visibility.Visible;
            odontogramaPlanTratamiento.Click += (m, n) =>
            {
                Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Plan_Tratamiento });
            };
        }

        public void odontogramaEvolucion()
        {
            var odontogramaEvolucion = (Button)BarraTitulosAsistencial.FindName("odontogramaEvolucion");
            if (odontogramaEvolucion == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "evolucionBtn", Width = 160, Height = 30, Content = "Odontograma evolución", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaEvolucion = (Button)BarraTitulosAsistencial.FindName("evolucionBtn");
            }
            odontogramaEvolucion.Visibility = System.Windows.Visibility.Visible;
            odontogramaEvolucion.Click += (m, n) =>
            {
                Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Evolucion });
            };
        }

        public void odontogramaProcedimientos()
        {
            var procedimientos = (Button)BarraTitulosAsistencial.FindName("procedimientosBtn");
            if (procedimientos == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "procedimientosBtn", Width = 100, Height = 30, Content = "Odontograma inicial", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                procedimientos = (Button)BarraTitulosAsistencial.FindName("inicialProcedimientos");
            }
            procedimientos.Visibility = System.Windows.Visibility.Visible;

            procedimientos.Click += (m, n) =>
            {
                ventanaProcedimientos();
            };
        }

        #endregion

        public void Dispose()
        {
            Messenger.Default.Unregister<Mostrar_Botones_Barra>(this);
        }
    }
}
