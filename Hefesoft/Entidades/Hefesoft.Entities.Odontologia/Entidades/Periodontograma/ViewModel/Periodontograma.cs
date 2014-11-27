using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontologia.Periodontograma.ViewModel
{
    public class Periodontograma : ViewModelBase
    {
        public Periodontograma()
        {
            if(IsInDesignMode)
            {
                
            }
            else
            {
                inicializarDatos();
                implante = new RelayCommand<Entidades.PeriodontogramaEntity>(implanteMetodo);        
            }
        }
        

        private void implanteMetodo(Entidades.PeriodontogramaEntity obj)
        {            
            if (obj.Implante == Enumeradores.Implante.ninguno)
            {
                obj.Tipo_Pieza = Enumeradores.Tipo_Pieza.tornillo;
                obj.Implante = Enumeradores.Implante.black;                
            }
            else if (obj.Implante == Enumeradores.Implante.black)
            {
                obj.Tipo_Pieza = Enumeradores.Tipo_Pieza.normal;
                obj.Implante = Enumeradores.Implante.ninguno;
            }
        }

        private void inicializarDatos()
        {
            Hefesoft.Odontologia.Periodontograma.Util.Generar_datos datos = new Util.Generar_datos();
            var data = datos.inicializarDatos();

            LstPeriodontogramaParte1 = data.Where(a => a.Parte == Enumeradores.Parte.parte1);
            LstPeriodontogramaParte2 = data.Where(a => a.Parte == Enumeradores.Parte.parte2);
            LstPeriodontogramaParte3 = data.Where(a => a.Parte == Enumeradores.Parte.parte3);
            LstPeriodontogramaParte4 = data.Where(a => a.Parte == Enumeradores.Parte.parte4);
            LstPeriodontogramaParte5 = data.Where(a => a.Parte == Enumeradores.Parte.parte5);
            LstPeriodontogramaParte6 = data.Where(a => a.Parte == Enumeradores.Parte.parte6);
            LstPeriodontogramaParte7 = data.Where(a => a.Parte == Enumeradores.Parte.parte7);
            LstPeriodontogramaParte8 = data.Where(a => a.Parte == Enumeradores.Parte.parte8);
        }

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
    }
}
