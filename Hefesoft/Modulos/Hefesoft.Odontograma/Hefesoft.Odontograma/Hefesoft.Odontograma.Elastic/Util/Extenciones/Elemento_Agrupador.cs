using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Util.Extenciones
{
    public class Elemento_Agrupador<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public TKey Key
        {
            get;
            set;
        }

        public TElement Element { get; set; }
    }
}
