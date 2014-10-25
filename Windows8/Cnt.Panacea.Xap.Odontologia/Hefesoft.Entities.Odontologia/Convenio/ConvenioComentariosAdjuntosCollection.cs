using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Convenio
{
    public class ConvenioComentariosAdjuntosCollection : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public IEnumerable<ConvenioComentarioAdjuntoEntity> ConvenioComentarioAdjuntoEntity { get; set; }
    }
}
