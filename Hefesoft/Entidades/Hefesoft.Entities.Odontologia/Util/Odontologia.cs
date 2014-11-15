using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Util
{
    public class Odontologia : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }
        public object Item { get; set; }

        public int Identificador { get; set; }

        public bool generarIdentificador { get; set; }
        
    }
}
