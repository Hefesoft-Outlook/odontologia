using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Menus.Entidades
{
    public class Menu_Odontograma : ViewModelBase
    {
        public Menu_Odontograma()
        {
                
        }

        private int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; RaisePropertyChanged("Codigo"); }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; RaisePropertyChanged("Descripcion"); }
        }
        
        
    }
}
