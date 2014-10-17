using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Pieza_Seleccionada
{
    public class Seleccionado : ViewModelBase, IDisposable
    {
        public Seleccionado()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                oirPiezaSeleccionada();
                tabCommand = new RelayCommand<string>(tab);
            }
        }

        private void tab(string obj)
        {
            if (Elemento_Seleccionado != null)
            {
                Elemento_Seleccionado.click(obj);
            }
        }
     
        private void oirPiezaSeleccionada()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(this, "Pieza Seleccionada", elemento =>
            {
                Elemento_Seleccionado = elemento;
                RaisePropertyChanged("Elemento_Seleccionado");
            });
        }

        public Odontograma.Odontograma Elemento_Seleccionado { get; set; }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(this);
        }

        public RelayCommand<string> tabCommand { get; set; }
    }
}
