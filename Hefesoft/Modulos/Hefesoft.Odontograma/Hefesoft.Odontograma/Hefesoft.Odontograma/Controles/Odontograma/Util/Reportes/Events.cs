using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Odontograma.Util.Reportes
{
    public class PrintingEventArgs : EventArgs
    {
        public object DataContext { get; set; }
    }
}
