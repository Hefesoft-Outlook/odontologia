using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Hefesoft.Util.W8.UI.Util
{
    public class Snapshot
    {
        /// <summary>
        /// Devuelve la ruta de la imagen guardada en un blob
        /// </summary>
        /// <param name="uielement"></param>
        /// <param name="nombreImagen"></param>
        /// <returns></returns>
        public async Task<string> snapShot(FrameworkElement uielement, string nombreImagen)
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(uielement);

            // Get the pixels
            IBuffer pixelBuffer = await bitmap.GetPixelsAsync();
            byte[] pixels = pixelBuffer.ToArray();

            var stream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 60, 60, pixels);

            await encoder.FlushAsync();
            stream.Seek(0);

            var reader = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];
            await reader.LoadAsync((uint)stream.Size);
            reader.ReadBytes(bytes);

            var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
            return result;
        }

        public async Task<string> snapShot(FrameworkElement uielement, string nombreImagen, double height, double width)
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(uielement);

            // Get the pixels
            IBuffer pixelBuffer = await bitmap.GetPixelsAsync();
            byte[] pixels = pixelBuffer.ToArray();

            var stream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, stream);

            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, height, width, pixels);

            await encoder.FlushAsync();
            stream.Seek(0);

            var reader = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];
            await reader.LoadAsync((uint)stream.Size);
            reader.ReadBytes(bytes);

            var result = await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", nombreImagen, bytes);
            return result;
        }

        /// <summary>
        /// Devuelve una imagen del elemento que se captura
        /// </summary>
        /// <param name="uielement"></param>
        /// <returns></returns>
        public async Task<RenderTargetBitmap> snapShot(FrameworkElement uielement)
        {
            var bitmap = new RenderTargetBitmap();
            await bitmap.RenderAsync(uielement);
            return bitmap;
        }

        public async Task<RenderTargetBitmap> SaveScreenshotAsync(FrameworkElement uielement)
        {
            var file = await PickSaveImageAsync();
            return await SaveToFileAsync(uielement, file);
        }

        async Task<StorageFile> PickSaveImageAsync()
        {
            var filePicker = new FileSavePicker();
            filePicker.FileTypeChoices.Add("Bitmap", new List<string>() { ".bmp" });
            filePicker.FileTypeChoices.Add("JPEG format", new List<string>() { ".jpg" });
            filePicker.FileTypeChoices.Add("Compuserve format", new List<string>() { ".gif" });
            filePicker.FileTypeChoices.Add("Portable Network Graphics", new List<string>() { ".png" });
            filePicker.FileTypeChoices.Add("Tagged Image File Format", new List<string>() { ".tif" });
            filePicker.DefaultFileExtension = ".jpg";
            filePicker.SuggestedFileName = "screenshot";
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.SettingsIdentifier = "picture picker";
            filePicker.CommitButtonText = "Save picture";

            return await filePicker.PickSaveFileAsync();
        }

        async Task<RenderTargetBitmap> SaveToFileAsync(FrameworkElement uielement, StorageFile file)
        {
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);

                Guid encoderId = GetBitmapEncoder(file.FileType);

                try
                {
                    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        return await CaptureToStreamAsync(uielement, stream, encoderId);
                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message);
                }

                var status = await CachedFileManager.CompleteUpdatesAsync(file);
            }

            return null;
        }

        Guid GetBitmapEncoder(string fileType)
        {
            Guid encoderId = BitmapEncoder.JpegEncoderId;
            switch (fileType)
            {
                case ".bmp":
                    encoderId = BitmapEncoder.BmpEncoderId;
                    break;
                case ".gif":
                    encoderId = BitmapEncoder.GifEncoderId;
                    break;
                case ".png":
                    encoderId = BitmapEncoder.PngEncoderId;
                    break;
                case ".tif":
                    encoderId = BitmapEncoder.TiffEncoderId;
                    break;
            }

            return encoderId;
        }

        async Task<RenderTargetBitmap> CaptureToStreamAsync(FrameworkElement uielement, IRandomAccessStream stream, Guid encoderId)
        {
            try
            {
                var renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(uielement);

                var pixels = await renderTargetBitmap.GetPixelsAsync();

                var logicalDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
                var encoder = await BitmapEncoder.CreateAsync(encoderId, stream);
                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Ignore,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    logicalDpi,
                    logicalDpi,
                    pixels.ToArray());

                await encoder.FlushAsync();

                var reader = new DataReader(stream.GetInputStreamAt(0));
                var bytes = new byte[stream.Size];
                await reader.LoadAsync((uint)stream.Size);
                reader.ReadBytes(bytes);

                await Hefesoft.Azure.Helpers.Azure_Helper.PutBlob_async("imagenes", "snapshot.png", bytes);


                return renderTargetBitmap;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }

            return null;
        }

        async void DisplayMessage(string error)
        {
            var dialog = new MessageDialog(error);
            await dialog.ShowAsync();
        }
    }
}
