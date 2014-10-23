using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta.Placa_Bacteriana;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Paleta
{
    public class Paleta : ViewModelBase, IDisposable
    {
        #region Constructor
        public Paleta()
        {
            if (IsInDesignMode)
            {
                Listado = new Cnt.Panacea.Xap.Odontologia.Vm.Datos_de_prueba.Paleta().datos.ToObservableCollection();
                this.NivelesSeveridad = new ObservableCollection<NivelSeveridadDXEntity>();
                this.NivelesSeveridad.Add(new NivelSeveridadDXEntity()
                {
                    Descripcion = "Prueba 1"
                });

                this.NivelesSeveridad.Add(new NivelSeveridadDXEntity()
                {
                    Descripcion = "Prueba 2"
                });

                RaisePropertyChanged("Listado");
            }
            else
            {
                mostrarTodosCommand = new RelayCommand<bool>(mostrarTodos);
                seleccionadoCommand = new RelayCommand<ConfigurarDiagnosticoProcedimOtraEntity>(diagnosticoProcedimientoSeleccionado);
                cargarDiagnosticosProcedimientosSinConvenio = new RelayCommand<bool>(mostrarSinConvenio);
                Listado = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();
                cargaNivelSeveridad();
                cargarDiagnosticos();
                Listado = new Cnt.Panacea.Xap.Odontologia.Vm.Datos_de_prueba.Paleta().datos.ToObservableCollection();

                Listado.fillTables();

                datosProvenientesOtrosFormularios();
                oirNumeroPiezasDentalesDelOdontogramaInicial();
                oirNuevoTratamiento();
            }

            Convenios = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();
            ListadoTodos = Listado.ToList();
            Buscador = new RelayCommand<string>(buscar);
            NivelSeveridad = new RelayCommand<NivelSeveridadDXEntity>(nivelSeveridadSeleccionado);
            Numero_Piezas_Dentales_Command = new RelayCommand<string>(piezasDentales);
            oirCargarDiagnosticos();
            oirCargarProcedimientosdaoListadoIds();
            oirNumeroPiezasDentalesDelOdontogramaInicial();
            RaisePropertyChanged("NivelesSeveridad");
        }

        private void diagnosticoProcedimientoSeleccionado(ConfigurarDiagnosticoProcedimOtraEntity obj)
        {
            Seleccionado = obj;

            if (obj.AplicaTratamiento == 1)
            {
                Texto_Aplica_A = "Pieza completa";
            }
            else if (obj.AplicaTratamiento == 2)
            {
                Texto_Aplica_A = "Superficie";
            }
            else if (obj.AplicaTratamiento == 3)
            {
                Texto_Aplica_A = "Boca";
            }

            RaisePropertyChanged("Texto_Aplica_A");
        }

        private void oirNuevoTratamiento()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this, nuevo =>
            {
                if (nuevo.valor == "Nuevo")
                {
                    Numero_Piezas_Dentales_Actual = 0;
                    RaisePropertyChanged("Numero_Piezas_Dentales_Actual");
                }
            });
        }

        #endregion

        #region Metodos

        private void oirNumeroPiezasDentalesDelOdontogramaInicial()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Carga_Odontograma_Inicial>(this, item =>
            {
                Numero_Piezas_Dentales_Actual = item.Cantidad_Dientes;

                //Le indica al control pieza dental a mostrar que debe calcular indice de placa bacteriana, ceo y cop
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<double>(Numero_Piezas_Dentales_Actual, "Numero Piezas Dentales");
                RaisePropertyChanged("Numero_Piezas_Dentales_Actual");
            });
        }

        private async void mostrarSinConvenio(bool obj)
        {
            Busy.UserControlCargando(true, "Cargando procedimientos sin convenio");
            await cargarConvenios();

            Listado.Clear();
            if (obj)
            {
                Listado = ListadoTodos.OrderBy(a => a.Descripcion).ToObservableCollection();
            }
            else
            {
                if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Plan_Tratamiento)
                {
                    Listado = ListadoTodos.Intersect(ListadoProcedimientosAsociados).ToObservableCollection();
                }
                if (Variables_Globales.Tipo_Odontograma_Activo == Tipo_Odontograma.Inicial)
                {
                    Listado = ListadoTodos.OrderBy(a => a.Descripcion).Where(a => a.TipoPanel != TipoPanel.ProcedimientoNoCubreConvenio).ToObservableCollection();
                }
            }
            RaisePropertyChanged("Listado");
            Busy.UserControlCargando(false);
        }

        private void mostrarTodos(bool obj)
        {
            Listado.Clear();
            if (obj)
            {
                //Cuando esta en modo plan de tratamiento solo debe cargar los procedimientos
                Listado = ListadoTodos.Where(a => a.TipoPanel != TipoPanel.ProcedimientoNoCubreConvenio && a.TipoPanel == TipoPanel.Procedimiento).OrderBy(a => a.Descripcion).ToObservableCollection();
            }
            else
            {
                Listado = ListadoTodos.Intersect(ListadoProcedimientosAsociados).ToObservableCollection();
            }
            RaisePropertyChanged("Listado");
        }

        private void oirCargarProcedimientosdaoListadoIds()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<cargar_Diagnosticos_Procedimientos>(this, elemento =>
            {
                Listado.Clear();
                if (elemento.tipo == Tipo.listado)
                {
                    //Carga los procedimientos dado los id's asociados                 
                    ListadoProcedimientosAsociados = elemento.listadoProcedimientos.Select(a => new ConfigurarDiagnosticoProcedimOtraEntity() { Identificador = a });
                    Listado = ListadoTodos.Intersect(ListadoProcedimientosAsociados).ToObservableCollection();
                }
                else if (elemento.tipo == Tipo.todos)
                {
                    Listado = ListadoTodos.Where(a => a.TipoPanel != TipoPanel.ProcedimientoNoCubreConvenio).OrderBy(a => a.Descripcion).ToObservableCollection();
                }
                else if (elemento.tipo == Tipo.todosIncluyendoNoCubreConvenio)
                {
                    Listado = ListadoTodos.OrderBy(a => a.Descripcion).ToObservableCollection();
                }
                RaisePropertyChanged("Listado");
            });
        }

        private void oirCargarDiagnosticos()
        {
            // Otro formulario avisa para que se carguen los diagnosticos
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cargar_Diagnosticos>(this, item =>
            {
                cargarDiagnosticos();
            });
        }

        private void nivelSeveridadSeleccionado(NivelSeveridadDXEntity obj)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(obj);
        }

        private void aplicarTratamiento()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.Messenger.Aplicar_Tratamiento() { });
        }

        private async Task cargarConvenios()
        {
            // Cargar los convenios si no estan cargados
            if (!Convenios.Any())
            {
                Convenios = await Contexto_Odontologia.obtenerContexto().ConsultarProcedimientosConvenio(Variables_Globales.IdPaciente, Variables_Globales.IdConvenio, Variables_Globales.IdIps);

                foreach (var item in Convenios.ToList())
                {
                    ListadoTodos.Add(item);
                }
            }
        }

        private async void cargarDiagnosticos()
        {
            try
            {
                if (Variables_Globales.IdIps != 0)
                {

                    var diagnosticos = await Contexto_Odontologia.obtenerContexto().ConsultarDiagnosticosConfigurados(Variables_Globales.IdIps);
                    var procedimientos = await Contexto_Odontologia.obtenerContexto().ConsultarProcedimientosConfigurados(Variables_Globales.IdIps);
                                        
                    ListadoTodos = diagnosticos.OrderBy(a => a.Descripcion).ToList();
                    ListadoTodos.AddRange(procedimientos);
                    
                    Listado = ListadoTodos.OrderBy(a => a.Descripcion).Where(a => a.TipoPanel != TipoPanel.ProcedimientoNoCubreConvenio).ToObservableCollection();

                    cargarListadoWindows8(Listado);

                    ListadoTodos.ToObservableCollection().fillTables();

                    RaisePropertyChanged("Listado");
                }
            }
            catch { }

        }

        private void cargarListadoWindows8(ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Listado)
        {
            Listado_Windows8 = Listado_Windows8.inicializarListaYLimpiar();

            foreach (var item in Listado)
            {
                Listado_Windows8.Add(new ConfigurarDiagnosticoProcedimOtraEntity_Extend() { ConfigurarDiagnosticoProcedimOtraEntity = item });
            }

            RaisePropertyChanged("Listado_Windows8");
        }

        private void piezasDentales(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                Variables_Globales.Numero_Piezas_Dentales = Convert.ToDouble(obj);
                Numero_Piezas_Dentales_Actual = Variables_Globales.Numero_Piezas_Dentales;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<double>(Convert.ToDouble(obj), "Numero Piezas Dentales");
            }
        }

        private void datosProvenientesOtrosFormularios()
        {

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Paleta>(this, Item =>
            {

            });
        }


        private async void cargaNivelSeveridad()
        {
            try
            {
                NivelesSeveridad = await Contexto_Odontologia.obtenerContexto().ConsultarNivelSeveridad();
                RaisePropertyChanged("NivelesSeveridad");
            }
            catch { }
        }

        private void buscar(string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                Listado.Clear();
                Listado = ListadoTodos.ToObservableCollection();
                cargarListadoWindows8(Listado);
            }
            else
            {
                Listado = ListadoTodos.Where(a => a.Descripcion.Contains(obj)).ToObservableCollection();
                cargarListadoWindows8(Listado);
            }

            RaisePropertyChanged("Listado");
        }



        internal void elementoSeleccionadoPaleta()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(Seleccionado);
        }

        #endregion

        #region Propiedades

        public RelayCommand<bool> mostrarTodosCommand { get; set; }

        public IEnumerable<ConfigurarDiagnosticoProcedimOtraEntity> ListadoProcedimientosAsociados { get; set; }
        public ObservableCollection<NivelSeveridadDXEntity> NivelesSeveridad { get; set; }
        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Listado { get; set; }
        public List<ConfigurarDiagnosticoProcedimOtraEntity> ListadoTodos { get; set; }
        private ConfigurarDiagnosticoProcedimOtraEntity seleccionado;
        public RelayCommand<string> Buscador { get; set; }
        public RelayCommand<string> Numero_Piezas_Dentales_Command { get; set; }
        public RelayCommand<bool> cargarDiagnosticosProcedimientosSinConvenio { get; set; }


        public ConfigurarDiagnosticoProcedimOtraEntity Seleccionado
        {
            get { return seleccionado; }
            set
            {
                seleccionado = value;
                elementoSeleccionadoPaleta();
            }
        }

        private NivelSeveridadDXEntity nivelSeverdidadSeleccionado;

        public NivelSeveridadDXEntity NivelSeverdidadSeleccionado
        {
            get { return nivelSeverdidadSeleccionado; }
            set
            {
                nivelSeverdidadSeleccionado = value;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(value);
            }
        }


        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> Convenios { get; set; }

        public RelayCommand<NivelSeveridadDXEntity> NivelSeveridad { get; set; }

        #endregion

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cargar_Diagnosticos>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<cargar_Diagnosticos_Procedimientos>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<double>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Carga_Odontograma_Inicial>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Activar_Elementos>(this);
        }


        public double Numero_Piezas_Dentales_Actual { get; set; }

        public RelayCommand<ConfigurarDiagnosticoProcedimOtraEntity> seleccionadoCommand { get; set; }

        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity_Extend> Listado_Windows8 { get; set; }

        private ConfigurarDiagnosticoProcedimOtraEntity_Extend seleccionadoW8;

        public ConfigurarDiagnosticoProcedimOtraEntity_Extend SeleccionadoW8
        {
            get { return seleccionadoW8; }
            set 
            {
                if (value != null)
                {
                    seleccionadoW8 = value;
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(value.ConfigurarDiagnosticoProcedimOtraEntity);
                    RaisePropertyChanged("SeleccionadoW8");
                }
            }
        }


        public string Texto_Aplica_A { get; set; }
    }
}
