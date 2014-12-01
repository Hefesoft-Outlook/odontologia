using App2.Assets.BusyBox;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace App2.Util.Busy
{
    public class Busy
    {

        /// <summary>
        /// Se utiliza este metodo para utilizar siempre el mismo busybox que esta en 
        /// memoria y no sobrecargar la ram del dispositivo
        /// </summary>
        /// <returns></returns>
        public static BusyBox addBusy()
        {
            var locator = App.Current.Resources["Locator"];
            var busy = ServiceLocator.Current.GetInstance<Assets.BusyBox.BusyBox>();

            var parent = busy.Parent;

            if (parent != null && parent is Windows.UI.Xaml.Controls.Grid)
            {
                ((Windows.UI.Xaml.Controls.Grid)parent).Children.Remove(busy);
            }

            busy.SetBinding(Assets.BusyBox.BusyBox.IsBusyProperty, new Binding() { Source = locator, Path = new Windows.UI.Xaml.PropertyPath("Busy.IsBusy") });
            busy.SetBinding(Assets.BusyBox.BusyBox.TextoProperty, new Binding() { Source = locator, Path = new Windows.UI.Xaml.PropertyPath("Busy.Texto") });
            return busy;
        }
    }
}
