using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.MenuOdontologia.Elastic.Entidades
{
    public class Menu : ViewModelBase
    {
        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; RaisePropertyChanged("Codigo"); }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; RaisePropertyChanged("Nombre"); }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; RaisePropertyChanged("Descripcion"); }
        }        

        private string urlImagen;

        public string UrlImagen
        {
            get { return urlImagen; }
            set { urlImagen = value; RaisePropertyChanged("UrlImagen"); }
        }

        private string urlMini;

        public string UrlMini
        {
            get { return urlMini; }
            set { urlMini = value; RaisePropertyChanged("UrlMini"); }
        }


        public Enumeradores.Paginas Pagina { get; set; }
    }
}
