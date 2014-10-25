using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class OdontogramasPacienteEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        
        public int CantidadDientes { get; set; }
        
        public string Descripcion { get; set; }
        
        public long Identificador { get; set; }
        
        public short? IdIps { get; set; }
        
        public string JustificacionCambio { get; set; }
        
        public long Tratamiento { get; set; }

    }
}
