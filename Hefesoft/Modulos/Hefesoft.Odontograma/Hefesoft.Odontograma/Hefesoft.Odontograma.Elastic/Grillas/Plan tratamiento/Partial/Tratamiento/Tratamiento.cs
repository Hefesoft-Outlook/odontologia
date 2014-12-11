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

        TratamientoEntity _tratamientoPadre = new TratamientoEntity();
        /// <summary>
        /// Gets or sets the tratamiento padre.
        /// </summary>
        /// <value>The tratamiento padre.</value>
        public TratamientoEntity TratamientoPadre
        {
            get
            {
                return _tratamientoPadre;
            }
            set
            {
                if (value != null)
                {
                    if (value.EstadoTratamiento == EstadoTratamiento.Abandono || value.EstadoTratamiento == EstadoTratamiento.Terminacion)
                    {
                        PuedeModificar = false;
                        RaisePropertyChanged("PuedeModificar");
                    }

                    _tratamientoPadre = value;
                    RaisePropertyChanged("TratamientoPadre");
                }
                else
                {
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = "No existe un tratamiento activo valide que tenga un tratamiento en el odontograma inicial guardado"
                    });
                }
            }
        }

        public void tratamientoPadre()
        {
            TratamientoPadre = Variables_Globales.TratamientosPadre;
            TratamientoPadre.Cotizacion = EsCotizacion;
            TratamientoPadre.CuotaInicial = ValorCuotaInicial;
            TratamientoPadre.EstadoTratamiento = EstadoTratamiento.PlanDeTratamiento;
            TratamientoPadre.Cuotas = NumeroSesionesTratamiento;
            TratamientoPadre.TipoPago = (short)FormaPagoSeleccionado.GetHashCode();
            TratamientoPadre.ValorCuota = ValorCuotaTratamiento;


            if (TratamientoPadre.IdSesionActual == null || TratamientoPadre.IdSesionActual <= 1)
            {
                TratamientoPadre.IdSesionActual = 1;
            }
            TratamientoPadre.EstadoRegistro = Std.EstadosEntidad.Modificado;
        }

        async void cargarComprobantes()
        {
            //Valida que no este cargado antes
            if (Comprobantes == null)
            {
                Comprobantes = await Contexto_Odontologia.obtenerContexto().ListarComprobantes(Variables_Globales.IdIps, Variables_Globales.UsuarioActual, Variables_Globales.IdSede);
            }

            if (Comprobantes.Any())
            {
                ComprobanteSeleccionado = Comprobantes.FirstOrDefault();
                RaisePropertyChanged("ComprobanteSeleccionado");
                RaisePropertyChanged("Comprobantes");
            }
        }
    }
}
