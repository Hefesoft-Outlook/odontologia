using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo
{
    public class Cambiar_Tipo_Odontograma : ViewModelBase
    {
        private Tipo_Odontograma  tipo_Odontograma;

        public Tipo_Odontograma  Tipo_Odontograma
        {
            get { return tipo_Odontograma; }
            set 
            { 
                tipo_Odontograma = value;
                Plan_Tratamiento = false;
                OdontogramaInicial = false;
                Plan_Tratamiento = false;

                if (value == Tipo_Odontograma.Evolucion)
                {
                    OdontogramaEvolucion = true;
                }
                else if (value == Tipo_Odontograma.Plan_Tratamiento)
                {
                    Plan_Tratamiento = true;
                }
                else if (value == Tipo_Odontograma.Inicial)
                {
                    OdontogramaInicial = true;
                }

            }
        }
        

        private bool plan_Tratamiento;

        public bool Plan_Tratamiento
        {
            get { return plan_Tratamiento; }
            set { plan_Tratamiento = value; RaisePropertyChanged("Plan_Tratamiento"); }
        }

        private bool odontogramaInicial;

        public bool OdontogramaInicial
        {
            get { return odontogramaInicial; }
            set { odontogramaInicial = value; RaisePropertyChanged("OdontogramaInicial"); }
        }

        private bool odontogramaEvolucion;

        public bool OdontogramaEvolucion
        {
            get { return odontogramaEvolucion; }
            set { odontogramaEvolucion = value; RaisePropertyChanged("OdontogramaEvolucion"); }
        }
        
    }

    public enum Tipo_Odontograma
    {
        Inicial = 0,
        Plan_Tratamiento = 1,
        Evolucion = 2
    }
}
