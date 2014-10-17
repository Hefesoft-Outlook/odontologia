using FluxJpeg.Core;
using FluxJpeg.Core.Encoder;
using System.IO;
using System.Windows.Media.Imaging;

namespace ImageUploader
{
    public class JpgEncoder
    {
        /// <summary>
        /// Codificador para realizar la compresion al interior del aplicativo.
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static Stream Encode(WriteableBitmap bitmap, int quality)
        {            
            int width = bitmap.PixelWidth;
            int height = bitmap.PixelHeight;
            int bands = 3;

            byte[][,] raster = new byte[bands][,];

            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int pixel = bitmap.Pixels[width * row + column];
                    raster[0][column, row] = (byte)(pixel >> 16);
                    raster[1][column, row] = (byte)(pixel >> 8);
                    raster[2][column, row] = (byte)pixel;
                }
            }

            ColorModel model = new ColorModel { colorspace = ColorSpace.RGB };

            FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);

        
            MemoryStream stream = new MemoryStream();
            JpegEncoder encoder = new JpegEncoder(img, quality, stream);

            encoder.Encode();

        
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }

}
