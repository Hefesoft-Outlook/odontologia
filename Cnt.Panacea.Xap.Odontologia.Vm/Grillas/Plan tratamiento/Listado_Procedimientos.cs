using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento
{
    public class Listado_Procedimientos : ViewModelBase
    {
        public Listado_Procedimientos()
        {
            if (IsInDesignMode)
            {
            }
            else
            {   
            }
        }

        public ObservableCollection<Entities.Odontologia.OdontogramaEntity> Listado { get; set; }

        public void inicializar()
        {
            Listado = Listado.inicializarListaYLimpiar();           

            //Le pedimos al mapa dental que valide si cada diagnostico tiene un procedimiento
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { CargarTodos = true, lstGuardar = formatearDatos });
            
        }

        private void formatearDatos(List<Entities.Odontologia.OdontogramaEntity> obj)
        {
            Listado = obj.ToObservableCollection();
            RaisePropertyChanged("Listado");
        }
    }
}
