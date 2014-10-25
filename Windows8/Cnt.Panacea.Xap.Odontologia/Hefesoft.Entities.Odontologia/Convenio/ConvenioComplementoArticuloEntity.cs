using Dto;
using Hefesoft.Entities.Odontologia.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Convenio
{
    public class ConvenioComplementoArticuloEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        
        public ArticuloEntity ArticuloDependiente { get; set; }
        
        public ArticuloEntity ArticuloIndependiente { get; set; }
        
        public short? Convenio { get; set; }
        
        public int Identificador { get; set; }
        
        public decimal ValorTope { get; set; }

    }
}
