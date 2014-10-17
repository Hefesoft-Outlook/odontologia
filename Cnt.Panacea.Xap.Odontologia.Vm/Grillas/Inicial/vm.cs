using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Inicial;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial
{
    public class vm : ViewModelBase
    {
        public vm()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                pedirDatosDiagnosticos();
            }
        }

        public void pedirDatosDiagnosticos()
        {
            //Pedimos al mapa dental que nos devuelva los diagnosticos que tiene pintados en este momento
            //Para mostrarlos en la grilla
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Guardar() { Pedir_Tipos_Guardar = Tipo_Odontograma.Inicial, lstGuardar = listadoMostrar });
        }

        private void listadoMostrar(List<Entities.Odontologia.OdontogramaEntity> obj)
        {
            grillaTratamiento = grillaTratamiento.inicializarListaYLimpiar();

            foreach (var item in obj)
            {
                if (item.Diagnostico != null)
                {
                    grillaTratamiento.Add(new GrillaTratamiento()
                    {
                        Diagnostico = item.Diagnostico.Diagnostico.NombreAlterno,
                        Diente = item.Diente.Identificador.ToString(),
                        Superficie = item.Superficie.ToString()
                    });
                }
                else if (item.Diagnostico != null)
                {
                    grillaTratamiento.Add(new GrillaTratamiento()
                    {
                        Diagnostico = item.Procedimiento.Procedimiento.NombreAlterno,
                        Diente = item.Diente.Identificador.ToString(),
                        Superficie = item.Superficie.ToString()
                    });
                }
            }

            RaisePropertyChanged("GrillaTratamiento");
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<GrillaTratamiento> grillaTratamiento = new ObservableCollection<GrillaTratamiento>();

        public ObservableCollection<GrillaTratamiento> GrillaTratamiento
        {
            get { return grillaTratamiento ; }
            set { grillaTratamiento = value; RaisePropertyChanged("GrillaTratamiento"); }
        }
        
    }
}
