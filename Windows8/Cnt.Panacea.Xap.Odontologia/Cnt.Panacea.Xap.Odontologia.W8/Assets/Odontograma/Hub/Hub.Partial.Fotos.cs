using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Hefesoft.Entities.Odontologia.Odontograma;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public sealed partial class HubPage : Page, IDisposable
    {   

        private async Task adjuntarImagenes(object sender)
        {
            if (validoAdjuntarImagenes())
            {
                AppBarToggleButton appBtn = sender as AppBarToggleButton;
                appBtn.IsChecked = false;

                Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
                openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

                // Filter to include a sample subset of file types.
                openPicker.FileTypeFilter.Clear();
                openPicker.FileTypeFilter.Add(".bmp");
                openPicker.FileTypeFilter.Add(".png");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".jpg");

                // Open the file picker.
                Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

                // file is null if user cancels the file picker.
                if (file != null)
                {                    
                    // Open a stream for the selected file.
                    Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                                        

                    byte[] bytes = new byte[fileStream.AsStream().Length];
                    await fileStream.AsStream().ReadAsync(bytes, 0, bytes.Length);

                    Busy.UserControlCargando(true, "Subiendo imagen");
                    var nombreImagen = string.Format("{0}_{1}", Variables_Globales.PCL.PlanTratamiento.tratamiento.Identificador, file.Name);
                    var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);                    
                    cargarImagen(bytes, result, file);
                    Busy.UserControlCargando(false);
                }
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Mostrar_Mensaje_Usuario() { Mensaje = mensaje });
            }
        }

        private async Task tomarFoto(object sender)
        {
            if (validoAdjuntarImagenes())
            {
                AppBarToggleButton appBtn = sender as AppBarToggleButton;
                appBtn.IsChecked = false;

                var foto = await new CameraCaptureUI().CaptureFileAsync(CameraCaptureUIMode.Photo);
                var file = await foto.CopyAsync(ApplicationData.Current.LocalFolder);
                Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                byte[] bytes = new byte[fileStream.AsStream().Length];
                await fileStream.AsStream().ReadAsync(bytes, 0, bytes.Length);

                Busy.UserControlCargando(true, "Subiendo imagen");
                var nombreImagen = string.Format("{0}_{1}", Variables_Globales.PCL.PlanTratamiento.tratamiento.Identificador, file.Name);
                var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
                cargarImagen(bytes, result, file);
                Busy.UserControlCargando(false);
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Mostrar_Mensaje_Usuario() { Mensaje = mensaje });
            }
        }


        private void cargarImagen(byte[] bytes, string result, Windows.Storage.StorageFile file)
        {
            var imagen = new Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity()
            {
                Nombre = file.Name,
                Contenido = bytes,
                Identificador = int.Parse(Variables_Globales.PCL.PlanTratamiento.tratamiento.Identificador.ToString()),
                TipoImagen = Cnt.Panacea.Entities.Parametrizacion.TiposImagenes.Foto,
                Ruta = result                
            };

            var vmInicial = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Odontograma_Inicial>();
            var vmMapaDental = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();

            if (vmInicial.listadoImagenesGuardar == null)
            {
                vmInicial.listadoImagenesGuardar = new List<Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity>();
            }

            if (vmMapaDental.LstImagenes == null)
            {
                vmMapaDental.LstImagenes = new List<Cnt.Panacea.Entities.Odontologia.TratamientoImagenEntity>();
            }

            vmInicial.listadoImagenesGuardar.Add(imagen);
            vmMapaDental.LstImagenes.Add(imagen);

            //Enviar mensaje para que guarde las imagenes 
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Util.vm.Messenger.Imagenes.Guardar_Imagenes());
        }

        private bool validoAdjuntarImagenes()
        {
            var valido = true;

            if (Variables_Globales.PCL.PlanTratamiento == null)
            {
                mensaje = "Primero debe existir un tratamiento activo";
                valido = false;
            }

            return valido;
        }

        public string mensaje { get; set; }     
    }
}
