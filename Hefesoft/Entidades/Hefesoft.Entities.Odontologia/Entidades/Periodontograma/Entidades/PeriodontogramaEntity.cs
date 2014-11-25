using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Odontologia.Periodontograma.Enumeradores;

namespace Hefesoft.Odontologia.Periodontograma.Entidades
{
    public class PeriodontogramaEntity : ViewModelBase
    {
        public PeriodontogramaEntity()
        {
            margenGingival = new Dictionary<int, int>();
            margenGingival.Add(0, 0);
            margenGingival.Add(50, 20);
            margenGingival.Add(100, 0);

            profundidadSondaje = new Dictionary<int, int>();
            profundidadSondaje.Add(0, 0);
            profundidadSondaje.Add(50, 50);
            profundidadSondaje.Add(100, 0);

            margenGingivalColor = "Red";
            profundidadSondajeColor = "Blue";

            RaisePropertyChanged("margenGingival");
            RaisePropertyChanged("profundidadSondaje");
            RaisePropertyChanged("margenGingivalColor");
            RaisePropertyChanged("profundidadSondajeColor");

            Furca = Enumeradores.Furca.mediolleno;
        }

        private int numero;

        public int Numero
        {
            get { return numero; }
            set 
            { 
                numero = value;
                RaisePropertyChanged("Numero");
            }
        }
        

        private Arriba_Abajo arribaAbajo = Arriba_Abajo.abajo;

        public Arriba_Abajo Arriba_Abajo
        {
            get { return arribaAbajo; }
            set 
            { 
                arribaAbajo = value;
                RaisePropertyChanged("Arriba_Abajo");
            }
        }

        private Cara cara = Cara.a;

        public Cara Cara
        {
            get { return cara; }
            set 
            { 
                cara = value;
                RaisePropertyChanged("Cara");
            }
        }

        private Tipo_Pieza tipoPieza = Tipo_Pieza.normal;

        public Tipo_Pieza Tipo_Pieza
        {
            get { return tipoPieza; }
            set 
            { 
                tipoPieza = value;
                RaisePropertyChanged("Tipo_Pieza");
            }
        }

        private Furca furca = Furca.ninguno;

        public Furca Furca
        {
            get { return furca; }
            set { furca = value; RaisePropertyChanged("Furca"); }
        }
        

        public Parte Parte { get; set; }

        public Dictionary<int, int> margenGingival { get; set; }

        public Dictionary<int, int> profundidadSondaje { get; set; }

        public string margenGingivalColor { get; set; }

        public string profundidadSondajeColor { get; set; }

        
        private Sangrado_Supuracion sangradoSupuracion1 = Sangrado_Supuracion.ninguno;

        public Sangrado_Supuracion SangradoSupuracion1
        {
            get { return sangradoSupuracion1; }
            set { sangradoSupuracion1 = value; RaisePropertyChanged("SangradoSupuracion"); }
        }

        private Sangrado_Supuracion sangradoSupuracion2 = Sangrado_Supuracion.red;

        public Sangrado_Supuracion SangradoSupuracion2
        {
            get { return sangradoSupuracion2; }
            set { sangradoSupuracion2 = value; RaisePropertyChanged("SangradoSupuracion2"); }
        }

        private Sangrado_Supuracion sangradoSupuracion3 = Sangrado_Supuracion.red_yellow;

        public Sangrado_Supuracion SangradoSupuracion3
        {
            get { return sangradoSupuracion3; }
            set { sangradoSupuracion2 = value; RaisePropertyChanged("SangradoSupuracion2"); }
        }

        private Placa placa1 = Placa.ninguno;

        public Placa Placa1
        {
            get { return placa1; }
            set { placa1 = value; RaisePropertyChanged("Placa1"); }
        }

        private Placa placa2 = Placa.ninguno;

        public Placa Placa2
        {
            get { return placa2; }
            set { placa2 = value; RaisePropertyChanged("Placa2"); }
        }

        private Placa placa3 = Placa.blue;

        public Placa Placa3
        {
            get { return placa3; }
            set { placa3 = value; RaisePropertyChanged("Placa3"); }
        }

        private int? movilidad;

        public int? Movilidad
        {
            get { return movilidad; }
            set { movilidad = value; RaisePropertyChanged("Movilidad"); }
        }

        private Implante implante = Implante.black;

        public Implante Implante
        {
            get { return implante; }
            set { implante = value; RaisePropertyChanged("Implante"); }
        }
        
    }
}
