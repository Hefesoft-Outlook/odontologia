using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
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
                Modo_Odontograma = Tipo_Odontograma.Inicial;
                oirPiezaSeleccionada();
                oirTipoOdontogramaSeleccionado();
                tabCommand = new RelayCommand<string>(tab);
            }
        }

        private void oirTipoOdontogramaSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, elemento => 
            {
                Modo_Odontograma = elemento.Tipo_Odontograma;
            });
        }

        private void tab(string obj)
        {
            if (Elemento_Seleccionado != null)
            {
                //Cuando es odontograma inicial no se debe validar
                if (validarElementoTieneDiagnosticos(Elemento_Seleccionado) || Modo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    Elemento_Seleccionado.click(obj);
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Mostrar_Mensaje_Usuario() 
                    { 
                        Mensaje = "El elemento seleccionado no posee diagnosticos en el odontograma inicial" 
                    });
                }
            }
        }

        private bool validarElementoTieneDiagnosticos(Odontograma.Odontograma Elemento_Seleccionado)
        {
            var valido = false;

            if(Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie1.Any(a=>a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie2.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie3.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie4.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie5.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie6.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie7.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.Superficie8.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            if (Elemento_Seleccionado.DiagnosticoProcedimiento.PiezaCompleta.Any(a => a.TipoPanel == Entities.Odontologia.TipoPanel.Diagnostico))
            {
                valido = true;
            }
            return valido;
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
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
        }

        public RelayCommand<string> tabCommand { get; set; }

        public Tipo_Odontograma Modo_Odontograma { get; set; }
    }
}
