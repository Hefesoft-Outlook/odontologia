using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
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
                oirDiagnosticoAgregado();
                oirTipoOdontogramaSeleccionado();
                tabCommand = new RelayCommand<string>(tab);
                eliminarCommand = new RelayCommand<object>(eliminar);
            }
        }

        public void eliminar(object obj)
        {
            var diagnosticoProcedimiento = (Extensiones.Clases.DiagnosticoProcedimiento_Extend)obj;
            var vmMapaDental = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
            var elemento = vmMapaDental.lstOdontograma.First(a => a.codigoPiezaDental == Elemento_Seleccionado.codigoPiezaDental);
            elemento.DiagnosticoProcedimiento.lst.Remove(diagnosticoProcedimiento);
            Listado_Diagnostico_Procedimientos.Remove(diagnosticoProcedimiento);
            Listado_Diagnostico_Procedimientos_Agrupados_Superficie = from diag in Listado_Diagnostico_Procedimientos group diag by diag.Nombre_Superficie into grp orderby grp.Key select grp;
            RaisePropertyChanged("Listado_Diagnostico_Procedimientos_Agrupados_Superficie");
        }

        private void oirDiagnosticoAgregado()
        {            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(this, item => 
            {           
                try
                {
                    mostrarDiagnosticosProcedimientos(item);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            });
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
                mostrarDiagnosticosProcedimientos(elemento);
            });
        }

        private void mostrarDiagnosticosProcedimientos(Odontograma.Odontograma elemento)
        {
            Elemento_Seleccionado = elemento;
            Listado_Diagnostico_Procedimientos = elemento.DiagnosticoProcedimiento.lst;
            Listado_Diagnostico_Procedimientos_Agrupados_Superficie = from diag in Listado_Diagnostico_Procedimientos group diag by diag.Nombre_Superficie into grp orderby grp.Key select grp;

            RaisePropertyChanged("Elemento_Seleccionado");
            RaisePropertyChanged("Listado_Diagnostico_Procedimientos");
            RaisePropertyChanged("Listado_Diagnostico_Procedimientos_Agrupados_Superficie");
        }

        public Odontograma.Odontograma Elemento_Seleccionado { get; set; }        

        public RelayCommand<string> tabCommand { get; set; }

        public Tipo_Odontograma Modo_Odontograma { get; set; }

        public List<Extensiones.Clases.DiagnosticoProcedimiento_Extend> Listado_Diagnostico_Procedimientos { get; set; }

        public IOrderedEnumerable<IGrouping<string, Extensiones.Clases.DiagnosticoProcedimiento_Extend>> Listado_Diagnostico_Procedimientos_Agrupados_Superficie { get; set; }

        #region Dispose
        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma>(this);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
        }
        #endregion

        public RelayCommand<object> eliminarCommand { get; set; }
    }
}
