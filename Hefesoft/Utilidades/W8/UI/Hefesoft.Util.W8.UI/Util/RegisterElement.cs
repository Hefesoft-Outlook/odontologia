using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Util.W8.UI.Util
{
    public static class RegisterElement
    {
        public static void registrarClaseUI<T>() where T : class
        {
            if (!GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.IsRegistered<T>())
            {
                GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<T>(true);
            }
        }
    }
}
