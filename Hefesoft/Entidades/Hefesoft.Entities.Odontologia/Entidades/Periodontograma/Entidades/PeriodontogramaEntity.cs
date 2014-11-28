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
            margenGingivalColor = "Red";
            profundidadSondajeColor = "Blue";
            RaisePropertyChanged("margenGingivalColor");
            RaisePropertyChanged("profundidadSondajeColor");
            Furca = Enumeradores.Furca.ninguno;
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
            set 
            { 
                furca = value; 
                RaisePropertyChanged("Furca"); 
            }
        }

        private Furca furca2 = Furca.ninguno;

        public Furca Furca2
        {
            get { return furca2; }
            set
            {
                furca2 = value;
                RaisePropertyChanged("Furca2");
            }
        }
        

        public Parte Parte { get; set; }
        
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

        private Implante implante = Implante.ninguno;

        public Implante Implante
        {
            get { return implante; }
            set { implante = value; RaisePropertyChanged("Implante"); }
        }

        private int margenGingival1 = 0;

        public int MargenGingival1
        {
            get { return margenGingival1; }
            set 
            {
                margenGingival1 = value;
                RaisePropertyChanged("MargenGingival1"); 
            }
        }

        private int margenGingival2;

        public int MargenGingival2
        {
            get { return margenGingival2; }
            set 
            { 
                margenGingival2 = value;                
                RaisePropertyChanged("MargenGingival2"); 
            }
        }

        private int margenGingival3;

        public int MargenGingival3
        {
            get { return margenGingival3; }
            set 
            {
                margenGingival3 = value;                
                RaisePropertyChanged("MargenGingival3"); 
            }
        }

        private int produndidadSondaje1;

        public int ProdundidadSondaje1
        {
            get { return produndidadSondaje1; }
            set 
            { 
                produndidadSondaje1 = value;                
                RaisePropertyChanged("ProdundidadSondaje1"); 
            }
        }

        private int produndidadSondaje2;

        public int ProdundidadSondaje2
        {
            get { return produndidadSondaje2; }
            set 
            { 
                produndidadSondaje2 = value;                
                RaisePropertyChanged("ProdundidadSondaje2"); 
            }
        }

        private int produndidadSondaje3;

        public int ProdundidadSondaje3
        {
            get { return produndidadSondaje3; }
            set 
            { 
                produndidadSondaje3 = value;                
                RaisePropertyChanged("ProdundidadSondaje3"); 
            }
        }

        private Furca_Visualizacion furcaVisualizacion = Furca_Visualizacion.No_Visible;

        public Furca_Visualizacion FurcaVisualizacion
        {
            get { return furcaVisualizacion; }
            set 
            { 
                furcaVisualizacion = value;
                RaisePropertyChanged("FurcaVisualizacion");
            }
        }
        
    }
}
