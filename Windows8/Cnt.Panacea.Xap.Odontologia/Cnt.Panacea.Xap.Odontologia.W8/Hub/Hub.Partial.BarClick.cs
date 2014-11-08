using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
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
        private void inicial_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaInicial();
        }

        private void planTratamiento_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaPlanTratamiento();
        }

        private void evolucion_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.odontogramaEvolucion();
        }

        private void finalizaTratamientoBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
            vm.finalizar();
        }

        private void deshacer_Click(object sender, RoutedEventArgs e)
        {
            AppBarToggleButton appBtn = sender as AppBarToggleButton;

            if (desHacerBool)
            {
                desHacerBool = false;
                appBtn.IsChecked = true;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Estado_DesHacer() { Estado = true });
            }
            else
            {
                desHacerBool = true;
                appBtn.IsChecked = false;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Estado_DesHacer() { Estado = false });
            }
        }

        private async void adjuntar_Click(object sender, RoutedEventArgs e)
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
                    Windows.Storage.Streams.IRandomAccessStream fileStream =
                        await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                        new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                    
                    var bytes = new byte[fileStream.Size];

                    Busy.UserControlCargando(true, "Subiendo imagen");
                    var nombreImagen = string.Format("{0}_{1}", Variables_Globales.PCL.PlanTratamiento.tratamiento.Identificador, file.Name);
                    var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
                    Busy.UserControlCargando(false);
                }                
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Mostrar_Mensaje_Usuario() { Mensaje = mensaje });
            }
        }       

        private async void foto_Click(object sender, RoutedEventArgs e)
        {
            if (validoAdjuntarImagenes())
            {
                AppBarToggleButton appBtn = sender as AppBarToggleButton;
                appBtn.IsChecked = false;

                var foto = await new CameraCaptureUI().CaptureFileAsync(CameraCaptureUIMode.Photo);
                var file = await foto.CopyAsync(ApplicationData.Current.LocalFolder);
                Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                var bytes = new byte[fileStream.Size];

                Busy.UserControlCargando(true, "Subiendo imagen");
                var nombreImagen = string.Format("{0}_{1}", Variables_Globales.PCL.PlanTratamiento.tratamiento.Identificador, file.Name);
                var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
                Busy.UserControlCargando(false);
            }
            else
            {
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Mostrar_Mensaje_Usuario() { Mensaje = mensaje });
            }
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
