using Hefesoft.Entities.Odontologia.Diagnostico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class ConfiguracionProcedimientoOdontologiaEntity : DiagnosticoProcedimientoOdontologiaEntity
    {
        public string CodigoProcedimiento { get; set; }
        
        public bool cubiertoConvenio { get; set; }
        
        public int Identificador { get; set; }
        public int IdProcedimiento { get; set; }
        public string NombreProcedimiento { get; set; }
        
        public ProcedimientoServicioEntity Procedimiento { get; set; }
    }
}
