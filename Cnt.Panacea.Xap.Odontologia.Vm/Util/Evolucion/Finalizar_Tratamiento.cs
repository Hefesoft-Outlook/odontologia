using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Finalizar;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Evolucion
{
    //Se mueve funcionalidad a una clase para mejor lectura de las mismas
    public class Finalizar_Tratamiento: IDisposable
    {

        public Finalizar_Tratamiento()
        {
            oirConsultarUsuarioFinaliza();
        }

        private void oirConsultarUsuarioFinaliza()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Consultar_Usuario_Cierra>(this, usuario=>
            {
                validarUsuario(usuario);
            });
        }

        private async void validarUsuario(Consultar_Usuario_Cierra usuario)
        {
            var resultado = await Contexto_Odontologia.obtenerContexto().ValidarusuarioCierraTratamiento(usuario.Nombre, usuario.Password);
            if (resultado)
            {                
                cerrarTratamientoTratamiento(Entities.Odontologia.EstadoTratamiento.Terminacion);
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = string.Format("Usuario invalido")
                });
            }
        }

        private void resultadoFinalizar(object obj)
        {
            FinalizarTratamiento();
        }

        /// <summary>
        /// Finaliza el plan de tratamiento cuando se realizan la totalidad de procedimientos.
        /// </summary>
        public void FinalizarTratamiento()
        {
            //Se trae el viewmodel de evolucion
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();

            if (!vm.FinalizaCumplimientoProcedimientos)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana
                {
                   Nombre = "Finalizar tratamiento",
                   Resultado = resultado
                });
            }
            else
            {
                ValidarAutorizacionCerrarTratamiento(vm);
            }
        }

        private void resultado(object obj)
        {
            if ((bool)obj)
            {
                //Se trae el viewmodel de evolucion
                var gridEvolucion = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
                ValidarAutorizacionCerrarTratamiento(gridEvolucion);
            }
        }

        /// <summary>
        /// Valida permisos para cerrar el plan de tratamiento.
        /// </summary>
        private void ValidarAutorizacionCerrarTratamiento(Grid_Evolucion vm)
        {
            if ((Variables_Globales.ParametroConvenio.RequiereClasificador && Variables_Globales.ClasificadorOdontologico)//LFDO Bug 15568
               || !Variables_Globales.ParametroConvenio.RequiereClasificador)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana
                {
                    Nombre = "Autorizacion"
                });
            }
            else
            {
                cerrarTratamientoTratamiento(Entities.Odontologia.EstadoTratamiento.Terminacion);
            }
        }

       
        /// <summary>
        /// Finaliza el plan de tratamiento.
        /// </summary>
        public async void cerrarTratamientoTratamiento(Entities.Odontologia.EstadoTratamiento estado)
        {
            Busy.UserControlCargando(true, "Cerrando tratamiento");            
            Variables_Globales.TratamientosPadre.EstadoTratamiento = estado;
            var result = await Contexto_Odontologia.obtenerContexto().Actualizartratamiento(Variables_Globales.TratamientosPadre);
            if (result)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Messenger.Modo_Lectura.Modo_Lectura() { Solo_Lectura = true });
            }
            Busy.UserControlCargando(false);
            
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Consultar_Usuario_Cierra>(this);
        }
    }
}
