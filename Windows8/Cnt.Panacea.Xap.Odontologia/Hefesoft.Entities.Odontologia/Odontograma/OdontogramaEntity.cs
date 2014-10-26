using Dto;
using Hefesoft.Entities.Odontologia.Diagnostico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class OdontogramaEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public ConfiguracionDiagnosticoOdontologiaEntity Diagnostico { get; set; }

        public DientesEntity Diente { get; set; }

        public DientesEntity DienteReferencia1 { get; set; }
        
        public DientesEntity DienteReferencia2 { get; set; }
        
        public DateTime FechaRealizacion { get; set; }
        
        public long Identificador { get; set; }
        
        public short? IdIps { get; set; }
        
        public bool Inicial { get; set; }
        
        public NivelSeveridadDXEntity NivelSeveridad { get; set; }
        
        public long OdontogramaPaciente { get; set; }
        
        public PlanTratamientoEntity PlanTratamiento { get; set; }
        
        public bool? PrimerInicial { get; set; }
        
        public ConfiguracionProcedimientoOdontologiaEntity Procedimiento { get; set; }
        
        public Superficie Superficie { get; set; }

        public long Tratamiento { get; set; }

    }
}
