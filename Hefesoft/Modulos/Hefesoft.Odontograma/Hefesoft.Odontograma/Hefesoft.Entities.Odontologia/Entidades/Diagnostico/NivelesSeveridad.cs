using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Diagnostico
{

    public sealed class NivelSeveridadDXEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public short Identificador { get; set; }

        public bool generarIdentificador { get; set; }

        public bool? Activo { get; set; }
    }
}
