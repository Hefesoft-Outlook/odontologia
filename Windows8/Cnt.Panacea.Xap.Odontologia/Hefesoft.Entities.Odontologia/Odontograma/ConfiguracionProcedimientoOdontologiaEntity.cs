using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class ConfiguracionProcedimientoOdontologiaEntity
    {
        public string CodigoProcedimiento { get; set; }
        
        public bool cubiertoConvenio { get; set; }
        
        public override int Identificador { get; set; }
        public int IdProcedimiento { get; set; }
        public string NombreProcedimiento { get; set; }
        
        public ProcedimientoServicioEntity Procedimiento { get; set; }
    }
}
