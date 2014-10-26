using Hefesoft.Entities.Odontologia.Diagnostico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class ConfiguracionDiagnosticoOdontologiaEntity : DiagnosticoProcedimientoOdontologiaEntity
    {
        
        public DiagnosticoCieEntity Diagnostico { get; set; }
        
        public int Identificador { get; set; }
    }
}
