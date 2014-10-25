using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class DientesEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public int Arcada { get; set; }
        public int Cuandrante { get; set; }
        public string Descripcion { get; set; }
        public  int Identificador { get; set; }
    }
}
