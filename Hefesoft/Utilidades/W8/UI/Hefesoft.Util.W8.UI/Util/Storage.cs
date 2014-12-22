using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Hefesoft.Util.W8.UI.Util
{
    public class Storage
    {
        public Storage()
        {
            
        }

        /// <summary>
        /// Key nombre del elemento a guardar
        /// </summary>
        /// <param name="key"></param>
        public void Add(string key, object valor)
        {
            var valores = ApplicationData.Current.LocalSettings.Values;
            valores[key] = valor;
        }

        public object get(string key)
        {
            var valores = ApplicationData.Current.LocalSettings.Values;
            return valores[key];
        }

        public bool exist(string key)
        {
            var valores = ApplicationData.Current.LocalSettings.Values;
            return valores.Any(a => a.Key == key);
        }

        public bool remove(string key)
        {
            try
            {
                var valores = ApplicationData.Current.LocalSettings.Values;
                valores.Remove(key);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
