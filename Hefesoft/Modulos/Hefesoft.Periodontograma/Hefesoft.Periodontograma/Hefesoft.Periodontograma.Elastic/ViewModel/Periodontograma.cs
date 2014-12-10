using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Hefesoft.Standard.BusyBox;
using Microsoft.Practices.ServiceLocation;
using Hefesoft.Standard.Util.Collection.Observables;

namespace Hefesoft.Periodontograma.Elastic.ViewModel
{
    public partial class Periodontograma : ViewModelBase
    {
        #region Constructor
        public Periodontograma()
        {
            if(IsInDesignMode)
            {
                listadoPeriodontogramasPorPaciente.Add(new Data.Periodontograma() 
                { 
                    Fecha_Ultima_Modificacion = DateTime.Now,
                });
            }
            else
            {
                inicializarDatos();
                implante = new RelayCommand<Entidades.PeriodontogramaEntity>(implanteMetodo);
                furcaCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(furcaMetodo);
                furcaCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(furcaMetodo2);
                listarPorPacienteCommand = new RelayCommand(listarPorPaciente);
                sangradoSupuracionCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo);
                sangradoSupuracionCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo2);
                sangradoSupuracionCommand3 = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo3);
                placaCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo);
                placaCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo2);
                placaCommand3 = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo3);
                newCommand = new RelayCommand(newMethod);
                saveCommand = new RelayCommand(save);
                cargarPeriododntogramaSeleccionadoCommand = new RelayCommand<Data.Periodontograma>(cargarPeriodontogramaSeleccionado);

                //Ocurre cuando un paciente a sido seleccionado en el listado de pacientes
                //Falta controlar un modo para identificar cuando se debe cargar para aca 
                //y cuando para el odontograma
                oirPacienteSeleccionado();
            }
        }
        #endregion

        #region Messenger
        private void oirPacienteSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Paciente seleccionado", item => 
            {
                BusyBox.UserControlCargando(true);
                listarPorPaciente();
                newMethod();
                BusyBox.UserControlCargando(false);
            });
        }
        #endregion 

        #region Propiedades

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte1 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte2 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte3 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte4 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte5 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte6 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte7 { get; set; }

        public IEnumerable<Entidades.PeriodontogramaEntity> LstPeriodontogramaParte8 { get; set; }

        private Entidades.PeriodontogramaEntity seleccionado;

        public Entidades.PeriodontogramaEntity Seleccionado
        {
            get { return seleccionado; }
            set 
            { 
                seleccionado = value;
                RaisePropertyChanged("Seleccionado");
            }
        }
        


        public RelayCommand<Entidades.PeriodontogramaEntity> implante { get; set; }

        public RelayCommand<object> margeGingivalCommand { get; set; }

        public RelayCommand<object> margeGingivalCommand2 { get; set; }

        public RelayCommand<object> margeGingivalCommand3 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> furcaCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> furcaCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand3 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand3 { get; set; }

        public RelayCommand saveCommand { get; set; }

        public RelayCommand listarPorPacienteCommand { get; set; }

        private ObservableCollection<Data.Periodontograma> _listadoPeriodontogramasPorPaciente = new ObservableCollection<Data.Periodontograma>();

        public ObservableCollection<Data.Periodontograma> listadoPeriodontogramasPorPaciente
        {
            get { return _listadoPeriodontogramasPorPaciente; }
            set { _listadoPeriodontogramasPorPaciente = value; }
        }

        public RelayCommand<Data.Periodontograma> cargarPeriododntogramaSeleccionadoCommand { get; set; }

        public RelayCommand newCommand { get; set; }

        #endregion
    }
}
