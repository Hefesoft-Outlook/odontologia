using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Odontologia.Periodontograma.Entidades;
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
                obtenerDatos();
                implante = new RelayCommand<Entidades.PeriodontogramaEntity>(implanteMetodo);
                furcaCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(furcaMetodo);
                furcaCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(furcaMetodo2);
                sangradoSupuracionCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo);
                sangradoSupuracionCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo2);
                sangradoSupuracionCommand3 = new RelayCommand<Entidades.PeriodontogramaEntity>(sangradoSupuracionMetodo3);
                placaCommand = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo);
                placaCommand2 = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo2);
                placaCommand3 = new RelayCommand<Entidades.PeriodontogramaEntity>(placaMetodonMetodo3);
                saveCommand = new RelayCommand(save);
            }
        }

        public async void save()
        {
            Data.Periodontograma periodontograma = new Data.Periodontograma();
            periodontograma.Listado.AddRange(LstPeriodontogramaParte1);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte2);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte3);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte4);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte5);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte6);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte7);
            periodontograma.Listado.AddRange(LstPeriodontogramaParte8);

            Data.Crud crud = new Data.Crud();            
            await crud.guardar(periodontograma);
        }

        private void placaMetodonMetodo(Entidades.PeriodontogramaEntity obj)
        {
            if(obj.Placa1 == Enumeradores.Placa.ninguno)
            {
                obj.Placa1 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa1 == Enumeradores.Placa.blue)
            {
                obj.Placa1 = Enumeradores.Placa.ninguno;
            }
        }
        private void placaMetodonMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Placa2 == Enumeradores.Placa.ninguno)
            {
                obj.Placa2 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa2 == Enumeradores.Placa.blue)
            {
                obj.Placa2 = Enumeradores.Placa.ninguno;
            }
        }
        private void placaMetodonMetodo3(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Placa3 == Enumeradores.Placa.ninguno)
            {
                obj.Placa3 = Enumeradores.Placa.blue;
            }
            else if (obj.Placa3 == Enumeradores.Placa.blue)
            {
                obj.Placa3 = Enumeradores.Placa.ninguno;
            }
        }

        private void sangradoSupuracionMetodo(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion1 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion1 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }
        private void sangradoSupuracionMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion2 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion2 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }
        private void sangradoSupuracionMetodo3(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.ninguno)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.red;
            }
            else if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.red)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.red_yellow;
            }
            else if (obj.SangradoSupuracion3 == Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                obj.SangradoSupuracion3 = Enumeradores.Sangrado_Supuracion.ninguno;
            }
        }

        private void furcaMetodo2(Entidades.PeriodontogramaEntity obj)
        {
            if (obj.Furca2 == Enumeradores.Furca.ninguno)
            {
                obj.Furca2 = Enumeradores.Furca.vacio;
            }
            else if (obj.Furca2 == Enumeradores.Furca.vacio)
            {
                obj.Furca2 = Enumeradores.Furca.mediolleno;
            }
            else if (obj.Furca2 == Enumeradores.Furca.mediolleno)
            {
                obj.Furca2 = Enumeradores.Furca.lleno;
            }
            else if (obj.Furca2 == Enumeradores.Furca.lleno)
            {
                obj.Furca2 = Enumeradores.Furca.cuadrado;
            }
            else if (obj.Furca2 == Enumeradores.Furca.cuadrado)
            {
                obj.Furca2 = Enumeradores.Furca.ninguno;
            }            
        }

        private void furcaMetodo(PeriodontogramaEntity item)
        {
            var obj = (PeriodontogramaEntity)(item);

            if (obj.Furca == Enumeradores.Furca.ninguno)
            {
                obj.Furca = Enumeradores.Furca.vacio;
            }
            else if(obj.Furca == Enumeradores.Furca.vacio)
            {
                obj.Furca = Enumeradores.Furca.mediolleno;
            }
            else if (obj.Furca == Enumeradores.Furca.mediolleno)
            {
                obj.Furca = Enumeradores.Furca.lleno;
            }
            else if (obj.Furca == Enumeradores.Furca.lleno)
            {
                obj.Furca = Enumeradores.Furca.cuadrado;
            }
            else if (obj.Furca == Enumeradores.Furca.cuadrado)
            {
                obj.Furca = Enumeradores.Furca.ninguno;
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

        private async void obtenerDatos()
        {
            Data.Crud crud = new Data.Crud();
            var result = await crud.get();
            var data = result.First().Listado;

            LstPeriodontogramaParte1 = null;
            LstPeriodontogramaParte2 = null;
            LstPeriodontogramaParte3 = null;
            LstPeriodontogramaParte4 = null;
            LstPeriodontogramaParte5 = null;
            LstPeriodontogramaParte6 = null;
            LstPeriodontogramaParte7 = null;
            LstPeriodontogramaParte8 = null;

            LstPeriodontogramaParte1 = data.Where(a => a.Parte == Enumeradores.Parte.parte1);
            LstPeriodontogramaParte2 = data.Where(a => a.Parte == Enumeradores.Parte.parte2);
            LstPeriodontogramaParte3 = data.Where(a => a.Parte == Enumeradores.Parte.parte3);
            LstPeriodontogramaParte4 = data.Where(a => a.Parte == Enumeradores.Parte.parte4);
            LstPeriodontogramaParte5 = data.Where(a => a.Parte == Enumeradores.Parte.parte5);
            LstPeriodontogramaParte6 = data.Where(a => a.Parte == Enumeradores.Parte.parte6);
            LstPeriodontogramaParte7 = data.Where(a => a.Parte == Enumeradores.Parte.parte7);
            LstPeriodontogramaParte8 = data.Where(a => a.Parte == Enumeradores.Parte.parte8);

            RaisePropertyChanged("LstPeriodontogramaParte1");
            RaisePropertyChanged("LstPeriodontogramaParte2");
            RaisePropertyChanged("LstPeriodontogramaParte3");
            RaisePropertyChanged("LstPeriodontogramaParte4");
            RaisePropertyChanged("LstPeriodontogramaParte5");
            RaisePropertyChanged("LstPeriodontogramaParte6");
            RaisePropertyChanged("LstPeriodontogramaParte7");
            RaisePropertyChanged("LstPeriodontogramaParte8");
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

        public RelayCommand<Entidades.PeriodontogramaEntity> furcaCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> furcaCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> sangradoSupuracionCommand3 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand2 { get; set; }

        public RelayCommand<Entidades.PeriodontogramaEntity> placaCommand3 { get; set; }

        public RelayCommand saveCommand { get; set; }
    }
}
