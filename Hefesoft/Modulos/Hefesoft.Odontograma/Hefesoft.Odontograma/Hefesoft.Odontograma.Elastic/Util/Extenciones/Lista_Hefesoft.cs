using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Util.Extenciones
{
    public class Lista_Hefesoft<T> : List<T>
    {        

        public event EventHandler OnAdd;

        public void Add(T item)
        {
            if (null != OnAdd)
            {
                OnAdd(item, null);
            }
            base.Add(item);
        }

        public event EventHandler OnRemove;

        public void Remove(T item)
        {
            if (null != OnRemove)
            {
                OnRemove(item, null);
            }
            base.Remove(item);
        }
            
    }
}
