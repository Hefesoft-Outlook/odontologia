using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Hefesoft.Util.W8.UI.Assets.BusyBox
{
    public class Busy
    {

        /// <summary>
        /// Se utiliza este metodo para utilizar siempre el mismo busybox que esta en 
        /// memoria y no sobrecargar la ram del dispositivo
        /// </summary>
        /// <returns></returns>
        public static BusyBox addBusy(dynamic locator)
        {
            var busy = new Assets.BusyBox.BusyBox();

            var parent = busy.Parent;

            if (parent != null && parent is Windows.UI.Xaml.Controls.Grid)
            {
                ((Windows.UI.Xaml.Controls.Grid)parent).Children.Remove(busy);
            }

            busy.SetBinding(Assets.BusyBox.BusyBox.IsBusyProperty, new Binding() { Source = locator, Path = new Windows.UI.Xaml.PropertyPath("IsBusy") });
            busy.SetBinding(Assets.BusyBox.BusyBox.TextoProperty, new Binding() { Source = locator, Path = new Windows.UI.Xaml.PropertyPath("Texto") });

            
            return busy;
        }
    }
}
