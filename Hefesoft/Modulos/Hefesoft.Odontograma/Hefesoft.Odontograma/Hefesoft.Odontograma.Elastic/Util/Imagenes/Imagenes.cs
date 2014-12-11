using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using GalaSoft.MvvmLight.Messaging;


namespace Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.Imagenes
{
    public class Imagenes
    {
        public void mostrarImagenes(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Listado imagenes",
                Propiedad_Adicional = vm.LstImagenes
            });
        }

        /// <summary>
        /// Almacena imagenes.
        /// </summary>
        internal async void GuardarImagenes(Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm vm)
        {
            Busy.UserControlCargando(true, "Guardando imagenes");
            if (vm.LstImagenes != null && vm.LstImagenes.Any())
            {
                int x = 0;              

                foreach (var item in vm.LstImagenes.ToList())
                {
                    x = x + 1;
                    item.Identificador = x+1;
                }

                await Contexto_Odontologia.obtenerContexto().GuardarImagenTratamiento(Variables_Globales.IdTratamientoActivo, vm.LstImagenes.ToObservableCollection());
            }
            Busy.UserControlCargando(false);
        }

    }
}
