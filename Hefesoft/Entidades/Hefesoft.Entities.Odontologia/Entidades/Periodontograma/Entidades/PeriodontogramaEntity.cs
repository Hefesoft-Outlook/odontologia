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

        public Parte Parte { get; set; }

        public Dictionary<int, int> margenGingival { get; set; }

        public Dictionary<int, int> profundidadSondaje { get; set; }

        public string margenGingivalColor { get; set; }

        public string profundidadSondajeColor { get; set; }

    }
}
