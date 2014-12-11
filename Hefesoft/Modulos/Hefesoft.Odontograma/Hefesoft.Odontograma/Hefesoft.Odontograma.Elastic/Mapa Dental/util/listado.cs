using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.util
{
    public class Cargar_Partes
    {
        // Esta clase se encarga de llenar los lisview con los datos correspondientes
        // Se mueve para hacer legible el codigo
        public void cargarPartes(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            vm.LstOdontogramaParte1 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte2 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte3 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte4 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte5 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte6 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte7 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();
            vm.LstOdontogramaParte8 = new ObservableCollection<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>();


            vm.lstOdontograma = new Inicializar_Datos().inicializarDatosOdontograma();
            if (!vm.LstOdontogramaParte1.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 1), vm.LstOdontogramaParte1);
            }
            if (!vm.LstOdontogramaParte2.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 2), vm.LstOdontogramaParte2);
            }

            if (!vm.LstOdontogramaParte3.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 3), vm.LstOdontogramaParte3);
            }

            if (!vm.LstOdontogramaParte4.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 4), vm.LstOdontogramaParte4);
            }

            if (!vm.LstOdontogramaParte5.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 5), vm.LstOdontogramaParte5);
            }

            if (!vm.LstOdontogramaParte6.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 6), vm.LstOdontogramaParte6);
            }

            if (!vm.LstOdontogramaParte7.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 7), vm.LstOdontogramaParte7);
            }

            if (!vm.LstOdontogramaParte8.Any())
            {
                adicionar(vm.lstOdontograma.Where(a => a.UbicacionOdontograma == 8), vm.LstOdontogramaParte8);
            }
        }

        private void adicionar(IEnumerable<Odontograma.Odontograma> enumerable, ObservableCollection<Odontograma.Odontograma> observableCollection)
        {
            foreach (var item in enumerable)
            {
                observableCollection.Add(item);
            }
        }
    }
}
