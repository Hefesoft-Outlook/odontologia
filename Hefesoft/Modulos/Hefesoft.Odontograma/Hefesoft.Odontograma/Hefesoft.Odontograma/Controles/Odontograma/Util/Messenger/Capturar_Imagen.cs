using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Hefesoft.Odontograma.Util.Messenger
{
    public class Capturar_Imagen
    {
        public Action<RenderTargetBitmap> Imagen { get; set; }
    }
}
