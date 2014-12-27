using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Design_Data
{
    public class Odontograma_Elemento : ViewModelBase
    {
        public Odontograma_Elemento()
        {
            
            
        }

        private string letra = "a";

        public string Letra
        {
            get { return letra; }
            set { letra = value; RaisePropertyChanged("Letra"); }
        }
        
    }
}
