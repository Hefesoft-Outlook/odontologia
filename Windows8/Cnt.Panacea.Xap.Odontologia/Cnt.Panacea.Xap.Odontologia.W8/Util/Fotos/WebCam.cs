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
using Hefesoft.Standard.BusyBox;

namespace App2.Util.Fotos
{
    public class WebCam
    {
        public async Task<string> takePicture(string nombreImagenGuardar)
        {            
            var foto = await new CameraCaptureUI().CaptureFileAsync(CameraCaptureUIMode.Photo);
            var file = await foto.CopyAsync(ApplicationData.Current.LocalFolder);
            Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

            byte[] bytes = new byte[fileStream.AsStream().Length];
            await fileStream.AsStream().ReadAsync(bytes, 0, bytes.Length);

            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(true, "Subiendo imagen");
            var nombreImagen = string.Format("{0}_{1}", nombreImagenGuardar, file.Name);
            var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(false);
            return result;
        }
    }
}
