using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Adjuntar.vm
{
    public class vm : ViewModelBase
    {
        public vm()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                //Por Ioc cargamos las imagenes que estan en el formulario del mapa dental
                var mapaDental = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
                TratamientoImagenEntity = mapaDental.LstImagenes.ToObservableCollection();
                verImagenCommand = new RelayCommand<TratamientoImagenEntity>(verImagen);
            }
        }

        private void verImagen(TratamientoImagenEntity obj)
        {
            TratamientoImagenEntity Tratamiento = obj;

            if (Tratamiento.Identificador == 0)
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
                {
                    Nombre = "Ver imagen",
                    Propiedad_Adicional = Tratamiento
                });
            }
            else
            {
                DescargarImagen(Tratamiento.Identificador);
            }
        }

        /// <summary>
        /// Descarga la imagen seleccionada al equipo del usuario
        /// </summary>
        /// <param name="idImagenTratamiento"></param>
        public async void DescargarImagen(int idImagenTratamiento)
        {
            var resultado = await Contexto_Odontologia.ConsultarImagenTratamiento(idImagenTratamiento);
            var imagen = resultado.Contenido;

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana()
            {
                Nombre = "Descargar imagen",
                Propiedad_Adicional = resultado
            });            
        }


        public RelayCommand<TratamientoImagenEntity> verImagenCommand { get; set; }

        

        private ObservableCollection<TratamientoImagenEntity> tratamientoImagenEntity = new ObservableCollection<TratamientoImagenEntity>();

        public ObservableCollection<TratamientoImagenEntity> TratamientoImagenEntity
        {
            get { return tratamientoImagenEntity; }
            set { tratamientoImagenEntity = value; RaisePropertyChanged("TratamientoImagenEntity"); }
        }
        
    }
}
