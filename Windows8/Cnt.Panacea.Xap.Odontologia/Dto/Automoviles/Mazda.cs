using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto.Automoviles
{
    public class Mazda : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public int Cilindraje { get; set; }

        public string Numero_Puertas { get; set; }

        public string Modelo { get; set; }

        public string Color { get; set; }

        public bool estrellado { get; set; }
    }
}
