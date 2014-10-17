using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Diagnosticos_Procedimientos
{
    public class Tiene_Diagnosticos : ViewModelBase
    {

        private bool superficie1;

        public bool Superficie1
        {
            get { return superficie1; }
            set { superficie1 = value; RaisePropertyChanged("Superficie1"); }
        }

        private bool superficie2;

        public bool Superficie2
        {
            get { return superficie2; }
            set { superficie2 = value; RaisePropertyChanged("Superficie2"); }
        }

        private bool superficie3;

        public bool Superficie3
        {
            get { return superficie3; }
            set { superficie3 = value; RaisePropertyChanged("Superficie3"); }
        }

        private bool superficie4;

        public bool Superficie4
        {
            get { return superficie4; }
            set { superficie4 = value; RaisePropertyChanged("Superficie4"); }
        }

        private bool superficie5;

        public bool Superficie5
        {
            get { return superficie5; }
            set { superficie5 = value; RaisePropertyChanged("Superficie5"); }
        }

        private bool superficie6;

        public bool Superficie6
        {
            get { return superficie6; }
            set { superficie6 = value; RaisePropertyChanged("Superficie6"); }
        }

        private bool superficie7;

        public bool Superficie7
        {
            get { return superficie7; }
            set { superficie7 = value; RaisePropertyChanged("Superficie7"); }
        }

        private bool piezaCompleta;

        public bool PiezaCompleta
        {
            get { return piezaCompleta; }
            set { piezaCompleta = value; RaisePropertyChanged("PiezaCompleta"); }
        }

    }
}
