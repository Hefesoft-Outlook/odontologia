using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto
{
    public class ParametroOdontologicoConvenio : IEntidadBase
    {
        public int Convenio { get; set; }
        public Guid id { get; set; }

        public string PartitionKey { get; set; }

        public string RowKey {get; set;}

        public string nombreTabla { get; set; }

        
        
    }
}
