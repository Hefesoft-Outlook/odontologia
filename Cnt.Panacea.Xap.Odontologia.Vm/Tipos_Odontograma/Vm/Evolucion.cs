using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Net;
using System.Windows;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Cnt.Std;
using System.Threading.Tasks;
using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Evolucion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight.Command;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm
{
    public partial class Evolucion : ViewModelBase, IDisposable
    {
        public Evolucion()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                usuarioAbandonoCommand = new RelayCommand(Abandono);
                oirGuardar();
                oirCambioOdontograma();
                oirFinalizarTratamiento();
            }
        }

        private void Abandono()
        {
            // Cierra el tratamiento por abandono
            new Finalizar_Tratamiento().cerrarTratamientoTratamiento(EstadoTratamiento.Abandono);
        }

        private void oirFinalizarTratamiento()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Finalizar.Finalizar>(this, finalizar => 
            {                
                new Cnt.Panacea.Xap.Odontologia.Vm.Util.Evolucion.Finalizar_Tratamiento().FinalizarTratamiento();
            });
        }

        #region Metodos

        //Como Este control esta pintado en la pantalla inicial
        //Debe actualizarse cuando se mueva de pestana a pestana
        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, incializarEvolucion);
        }

        //Se cargan los datos del odontograma evolucion
        private async void incializarEvolucion(Cambiar_Tipo_Odontograma obj)
        {
            if (obj.Tipo_Odontograma == Tipo_Odontograma.Evolucion)
            {
                Variables_Globales.Tipo_Odontograma_Activo = Tipo_Odontograma.Evolucion;
                await listarOdontogramaEvolucion();
            }
        }

        internal async Task listarOdontogramaEvolucion()
        {
            Listado = await Contexto_Odontologia.obtenerContexto().ListarOdontogramaTratamiento(Variables_Globales.IdTratamientoActivo, Variables_Globales.IdIps);

            if (Listado.Any())
            {
                Messenger.Default.Send(new Pedir_Pintar_Datos() { lst = Listado });
                mostrarVentana();
            }
            else
            {   
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = "No se encuentran datos"
                });
            }
        }

        private static void mostrarVentana()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Evolucion"                
            });           
        }


        //Oye cuando se cierra la venta y se pide guardar los datos
        private void oirGuardar()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar>(this, "Evolucion", item =>
            {
                GuardarEvolucion();
            });
        }

        /// <summary>
        /// Guarda el plan de evolucion del tratamiento.
        /// </summary>
        /// <param name="ItemSourcePlan">The item source plan.</param>
        public async void GuardarEvolucion()
        {
            TratamientoPadre = Variables_Globales.TratamientosPadre;
            //Trae el view model Grid Evolucion
            var GrillaEvolucion = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
            
            bool mensajeObservacion = false;
            Planes = new Convertir_Elementos_Grilla_Plan_Evolucion().convertirGrillaPlanes(GrillaEvolucion.ListadoEvolucion);

            if (Planes.PlanesTratamientoCollection.Any())
            {
                if (TratamientoPadre.IdSesionActual == null || TratamientoPadre.IdSesionActual == 0)
                {
                    TratamientoPadre.IdSesionActual = 1;
                    TratamientoPadre.FechaInicial = DateTime.Now;
                }
                else if (TratamientoPadre.IdSesionActual == 1)
                {
                    TratamientoPadre.FechaInicial = DateTime.Now;
                    TratamientoPadre.IdSesionActual = short.Parse((TratamientoPadre.IdSesionActual + 1).ToString());
                }
                else
                {
                    TratamientoPadre.IdSesionActual = short.Parse((TratamientoPadre.IdSesionActual + 1).ToString());
                }

                if (mensajeObservacion)
                {
                    Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                    {
                        Nombre = "Observaciones Evolucion",
                        Resultado = resultado
                    });
                }
                else
                {
                    await Contexto_Odontologia.obtenerContexto().ActualizarPlanesTratamiento(TratamientoPadre, Planes.PlanesTratamientoCollection);
                    
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                    {
                        Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Guardar_Odontograma
                    });
                    
                    //Le enviamos un mensaje diciendole al mapa dental que guarde las imagenes que tiene en este momento en cola
                    Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.vm.Messenger.Imagenes.Guardar_Imagenes() { });
                }

                if (Planes.FinalizaTratamiento)
                {
                    FinalizaCumplimientoProcedimientos = true;
                }
                else
                {
                    TratamientoPadre.EstadoTratamiento = EstadoTratamiento.Evolucion;
                }
            }
            else
            {                
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Ningun_Cambio
                });
            }
        }

        private async void resultado(object obj)
        {
            if ((bool)obj == true)
            {
                await Contexto_Odontologia.obtenerContexto().ActualizarPlanesTratamiento(TratamientoPadre, Planes.PlanesTratamientoCollection);
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes.Mostrar_Mensaje_Usuario()
                {
                    Mensaje = Cnt.Panacea.Xap.Odontologia.Recursos.Mensajes.Justificacion_Guardar_Sistema
                });                
            }
        }

        

        #endregion

        #region Propiedades

        public TratamientoEntity TratamientoPadre { get; set; }

        public bool FinalizaCumplimientoProcedimientos { get; set; }
        #endregion

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Finalizar.Finalizar>(this);
        }

        public PlanesTratamientoCollection_Extend Planes { get; set; }

        public System.Collections.ObjectModel.ObservableCollection<OdontogramaEntity> Listado { get; set; }

        public RelayCommand usuarioAbandonoCommand { get; set; }
    }
}
