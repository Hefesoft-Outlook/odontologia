using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dto
{
    interface IEntidadBase
    {
         string PartitionKey {get; set;}
         string RowKey { get; set; }
         string nombreTabla { get; set; }
    }
}
