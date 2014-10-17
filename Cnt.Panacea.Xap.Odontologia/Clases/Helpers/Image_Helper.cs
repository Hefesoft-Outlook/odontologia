using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Clases.Helpers
{
    public class Image_Helper
    {
        internal BitmapImage imageFromUrl(string url)
        {
            Uri uri = new Uri(url, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
    }
}
