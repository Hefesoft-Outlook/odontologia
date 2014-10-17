using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Std.Xap.Controles;


namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class Contenedor : UserControl, IDisposable
    {

        Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm vm;
        public Contenedor()
        {
            Busy.UserControlCargando(true, "Inicializando elementos por favor espere");

            InitializeComponent();
            vm = this.DataContext as Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm;

            try
            {
                //Se saca el control del xaml ya que algunas propiedades dan error
                //En el designer
                controlPaciente = new CntInformacionPacienteView();

                controlPaciente.SetBinding(CntInformacionPacienteView.IdPacienteProperty, new System.Windows.Data.Binding("IdentificadorPaciente") 
                { 
                    Mode = System.Windows.Data.BindingMode.TwoWay, 
                });

                StackPanel.Children.Insert(0, controlPaciente);

                resourcedictionary = new ResourceDictionary();
                resourcedictionary.Source = new Uri("/Cnt.Panacea.Xap.Estilos;component/Cnt.Xap.Estilos.xaml", UriKind.RelativeOrAbsolute);
                BarraTitulosAsistencial = new Std.Xap.Controles.BarraTitulosAsistencial() { Titulo = "Odontologia", BotonGuardarVisible = true, BotonInformeVisible = false };
                BarraTitulosAsistencial.Command = vm.guardarCommand;

                ((Button)BarraTitulosAsistencial.FindName("CancelarBarraAsistencialButton")).Visibility = System.Windows.Visibility.Collapsed;
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Content = Odontologia.Recursos.Mensajes.Informes, Width = 90, Height = 30, Command = vm.InformesCommand, Style = resourcedictionary["ButtonAsistencialInformes"] as Style });
                ScrllVwrBarra.Content = BarraTitulosAsistencial;
            }
            catch
            {
            }
            
            oirCargando();            
            oirCambiosBotones();
            oirSolicitarValorPaciente();

            //Agrega el mapa dental o odontograma al formulario
            ScrllVwrContenedor.Children.Add(new Views.Odontogramav2());

            try
            {
                odontogramaInicialbtn();
                odontogramaPlanTratamientobtn();
                odontogramaEvolucion();
                odontogramaFinalizaTratamientobtn();

                //Muestra los procedimientos existentes en un pop up
                odontogramaProcedimientos();
                
                
                nuevoTratamiento();

                //Inicializamos estos botones en false para que 
                //El usuario no se vaya a curosiar por alla sin tener un plan de tratamiento (Esta variable esta la clase estatica variables globales)
                odontogramaPlanTratamientoBtn.IsEnabled = false;
                odontogramaEvolucionBtn.IsEnabled = false;
            }
            catch { }
        }        

        private void oirSolicitarValorPaciente()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente>(this, item =>
            {
                dynamic paciente = controlPaciente;
                Variables_Globales.Paciente = paciente.PacienteResultado;
                
                //Valida que alguien este oyendo la rta
                if (item.Paciente != null)
                {
                    item.Paciente(paciente.PacienteResultado);
                }
            });
        }        

        private void oirCambiosBotones()
        {
            // Activa o desactiva botones deacuerdo al estado de la pagina
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, elemento => 
            {
                if (elemento.valor == "Nuevo")
                {
                    nuevoTratamientoUI();
                    finalizaTratamientoBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (elemento.valor == "Plan Tratamiento")
                {
                    planTratamientoUI();
                    finalizaTratamientoBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else if (elemento.valor == "Evolucion")
                {
                    evolucionUI();
                    finalizaTratamientoBtn.Visibility = System.Windows.Visibility.Visible;
                }
                else if (elemento.valor == "Todos")
                {
                    finalizaTratamientoBtn.Visibility = System.Windows.Visibility.Collapsed;
                    activarTodosUI();                    
                }
            });
        }

        public void odontogramaInicialbtn()
        {
            odontogramaInicial = (Button)BarraTitulosAsistencial.FindName("inicialBtn");
            if (odontogramaInicial == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "inicialBtn", Width = 100, Height = 30, Content = "Inicial", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaInicial = (Button)BarraTitulosAsistencial.FindName("inicialBtn");
            }

            odontogramaInicial.Command = vm.odontogramaInicialCommand;
            odontogramaInicial.Visibility = System.Windows.Visibility.Visible;
        }

        public void odontogramaPlanTratamientobtn()
        {            
            odontogramaPlanTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("tratamientolBtn");
            if (odontogramaPlanTratamientoBtn == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "tratamientolBtn", Width = 140, Height = 30, Content = "Plan de tratamiento", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaPlanTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("tratamientolBtn");
            }
            
            odontogramaPlanTratamientoBtn.Visibility = System.Windows.Visibility.Visible;
            //odontogramaPlanTratamientoBtn.IsEnabled = false;
            odontogramaPlanTratamientoBtn.Command = vm.odontogramaPlanTratamientoCommand;
        }

        public void odontogramaEvolucion()
        {
            odontogramaEvolucionBtn = (Button)BarraTitulosAsistencial.FindName("evolucionBtn");
            if (odontogramaEvolucionBtn == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "evolucionBtn", Width = 100, Height = 30, Content = "Evolución", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                odontogramaEvolucionBtn = (Button)BarraTitulosAsistencial.FindName("evolucionBtn");
            }
            
            odontogramaEvolucionBtn.Visibility = System.Windows.Visibility.Visible;
            odontogramaEvolucionBtn.Command = vm.odontogramaEvolucionCommand;
        }

        private void odontogramaFinalizaTratamientobtn()
        {
            finalizaTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("FinalizarTratamientoBtn");
            if (finalizaTratamientoBtn == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "FinalizarTratamientoBtn", Width = 100, Height = 30, Content = "Finalizar tratamiento", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                finalizaTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("FinalizarTratamientoBtn");
            }

            finalizaTratamientoBtn.Visibility = System.Windows.Visibility.Collapsed;
            finalizaTratamientoBtn.Command = vm.finalizarTratamientoCommand;
        }

        public void odontogramaProcedimientos()
        {
            var procedimientos = (Button)BarraTitulosAsistencial.FindName("procedimientosBtn");
            if (procedimientos == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "procedimientosBtn", Width = 100, Height = 30, Content = "Procedimientos", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                procedimientos = (Button)BarraTitulosAsistencial.FindName("procedimientosBtn");
            }
            procedimientos.Visibility = System.Windows.Visibility.Visible;
            procedimientos.Command = vm.procedimientosCommand;
      
        }

        public void nuevoTratamiento()
        {
            nuevoTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("nuevoTratamientoBtn");
            if (nuevoTratamientoBtn == null)
            {
                BarraTitulosAsistencial.BotonesDinamicos.Children.Add(new Button() { Name = "nuevoTratamientoBtn", Width = 100, Height = 30, Content = "Nuevo", Style = resourcedictionary["ButtonAsistencialDefault"] as Style });
                nuevoTratamientoBtn = (Button)BarraTitulosAsistencial.FindName("nuevoTratamientoBtn");
            }
            nuevoTratamientoBtn.Visibility = System.Windows.Visibility.Visible;
            nuevoTratamientoBtn.Command = vm.nuevoTratamientoCommand;
        }

        private void oirCargando()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mostrar_Cargando>(this, item => 
            {
                if (item.mostrar_Cargando)
                {
                    BorderCargando.Visibility = System.Windows.Visibility.Visible;
                    TxtBlckCargando.Text = item.texto;
                }
                else
                {
                    BorderCargando.Visibility = System.Windows.Visibility.Collapsed;
                    TxtBlckCargando.Text = "Cargando...";
                }
            });
        }

        /*
         Dispone la interfaz grafica en modo de un nuevo tratamiento
         */
        private void nuevoTratamientoUI()
        {
            Variables_Globales.IdTratamientoActivo = 0;
            odontogramaEvolucionBtn.IsEnabled = false;
            odontogramaPlanTratamientoBtn.IsEnabled = false;
        }

        private void planTratamientoUI()
        {
            odontogramaPlanTratamientoBtn .IsEnabled= true;
            odontogramaEvolucionBtn.IsEnabled = false;
        }

        private void evolucionUI()
        {
            odontogramaPlanTratamientoBtn.IsEnabled = true;
            odontogramaEvolucionBtn.IsEnabled = true;
        }

        private void activarTodosUI()
        {            
            odontogramaPlanTratamientoBtn.IsEnabled = true;
            odontogramaEvolucionBtn.IsEnabled = true;
        }

        #region Propiedades
        public Std.Xap.Controles.BarraTitulosAsistencial BarraTitulosAsistencial { get; set; }

        public string nombreInforme { get; set; }

        public ResourceDictionary resourcedictionary { get; set; }

        public Button odontogramaPlanTratamientoBtn { get; set; }

        public Button odontogramaEvolucionBtn { get; set; }

        public Button odontogramaInicial { get; set; }

        public Button nuevoTratamientoBtn { get; set; }

        public CntInformacionPacienteView controlPaciente { get; set; }
        #endregion

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mostrar_Cargando>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente>(this);
        }



        public Button finalizaTratamientoBtn { get; set; }
    }
}