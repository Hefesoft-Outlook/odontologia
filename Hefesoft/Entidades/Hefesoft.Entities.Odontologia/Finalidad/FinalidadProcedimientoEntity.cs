using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Finalidad
{
    public class FinalidadProcedimientoEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }
        
        public short Codigo { get; set; }
        
        public string Descripcion { get; set; }
        
        public bool Estado { get; set; }
        
        public short Identificador { get; set; }

        public bool generarIdentificador { get; set; }
    }
}
