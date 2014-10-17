using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageUploader
{
    public class ImageHelper
    {
        /// <summary>
        /// Esta clase se usa para comprimir las imagenes que se subiran al sistema antes de realizar dicha subida
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public static WriteableBitmap GetImageSource(Stream stream, double maxWidth, double maxHeight)
        {
            BitmapImage bmp = new BitmapImage();
            bmp.SetSource(stream);

            Image img = new Image();            
            img.Source = bmp;

            double scaleX = 1;
            double scaleY = 1;

            if (bmp.PixelHeight > maxHeight)
                scaleY = maxHeight / bmp.PixelHeight;
            if (bmp.PixelWidth > maxWidth)
                scaleX = maxWidth / bmp.PixelWidth;

            
            double scale = Math.Min(scaleY, scaleX);

            int newWidth = Convert.ToInt32(bmp.PixelWidth * scale);
            int newHeight = Convert.ToInt32(bmp.PixelHeight * scale);
            WriteableBitmap result = new WriteableBitmap(newWidth, newHeight);
            result.Render(img, new ScaleTransform() { ScaleX = scale, ScaleY = scale });
            result.Invalidate();
            return result;
        }
    }
}
