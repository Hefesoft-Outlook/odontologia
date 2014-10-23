using Cnt.Panacea.Entities.Odontologia;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Diagnostico
{
    public class HefesoftDiagnosticoProcedimiento : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public ConfigurarDiagnosticoProcedimOtraEntity ConfigurarDiagnosticoProcedimOtraEntity { get; set; }
    }
}
