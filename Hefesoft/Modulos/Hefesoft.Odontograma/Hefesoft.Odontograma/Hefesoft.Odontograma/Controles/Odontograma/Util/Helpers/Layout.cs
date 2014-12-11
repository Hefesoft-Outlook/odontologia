using Hefesoft.Odontograma.Hub_Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Hefesoft.Odontograma.Util.Helpers
{
    class Layout
    {
        public static async void buscarElemento(DependencyObject element, Type tipo)
        {
            var snap = new Snapshot();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                var tipoChild = child.GetType();

                if (tipoChild.Equals(tipo))
                {
                    string nombreImagen = string.Format("{0}.png", new Random().Next().ToString());
                    await snap.snapShot((FrameworkElement)child, nombreImagen);
                }
            }
        }
    }
}
