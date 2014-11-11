using Cnt.Std;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cnt.Panacea.Entities.Odontologia;
using System.Collections.ObjectModel;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.General;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental
{
    public partial class UserControlGuardarPlanTratamiento : ViewModelBase, IDisposable
    {
        private void ValidarSesionesConfiguradas()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Datos_Grilla_Plan_Tratamiento()
            {
                //Usamos una expresion lambda para hacer la validacion para no generar tanto codigo
                //Esto lo que hace es decirle a la grimma que por favor le devuelva los datos que contiene en este momento
                lst = (ObservableCollection<ProcedimientosGrillaPlanTratamiento> obj) =>
                {
                    if (obj.validarSesionesConfiguradas() && FormaPagoSeleccionado.GetHashCode() == Cnt.Panacea.Entities.Odontologia.FormaPago.Sesión.GetHashCode())
                    {
                        HabilitarControlesPagoSesion = true;
                        RaisePropertyChanged("HabilitarControlesPagoSesion");
                        ListadoGrillaPlanTratamiento = obj;
                        CalculoValoresTratamiento();
                    }
                    else
                    {
                        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                        {
                            Mensaje = "Por favor indique la sesion para cada procedimiento"
                        });
                    }
                },
                fallido = (error) =>
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "Por favor indique la sesion para cada procedimiento"
                    });
                }
            });
        }

        public void pedirDatosGrilla()
        {
            //Valida que el usuario pueda generar cambios
            if (PuedeModificar)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Datos_Grilla_Plan_Tratamiento() { fallido = fallo, lst = listado });
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Solo_Lectura
                });
            }
        }

        private void fallo(falla obj)
        {
            string errores = "";

            if (obj.mensaje != null)
            {
                foreach (var item in obj.mensaje)
                {
                    errores += System.Environment.NewLine + item;
                }

                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = errores
                });
            }
        }

        private void listado(ObservableCollection<ProcedimientosGrillaPlanTratamiento> obj)
        {
            ListadoGrillaPlanTratamiento = obj;
            var validaciones = new Grilla_Plan_Tratamiento();
            var result = validaciones.validarAntesGuardar(this);

            if (result.valido)
            {
                ListadoGrillaPlanTratamiento = obj;
                lstOdontogramaEntity = new List<OdontogramaEntity>();

                foreach (var item in ListadoGrillaPlanTratamiento)
                {
                    lstOdontogramaEntity.Add(item.OdontogramaEntity);
                }

                tratamientoPadre();
                CalculoValoresTratamiento();
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cerrar_Pop_Up_Generico());
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = result.mensaje
                });
            }
        }
    }
}
