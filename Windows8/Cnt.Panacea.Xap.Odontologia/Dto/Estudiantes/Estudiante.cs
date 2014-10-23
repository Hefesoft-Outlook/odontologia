using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto.Estudiantes
{
    public class Estudiante : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public string Nombre_Estudiante { get; set; }
        public string Codigo_Estudiante { get; set; }
    }
}
