using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Historia
{
    public class SesionesPlanTratamientoEntity
    {
        
        public bool Estado { get; set; }
        
        public long? Factura { get; set; }
        
        public DateTime? FechaRealizacion { get; set; }
        
        public long Identificador { get; set; }
        
        public short? IdIps { get; set; }
        
        public long IdOrden { get; set; }
        
        public short IdSesion { get; set; }
        
        public bool? Legalizacion { get; set; }
        
        public long PlanTratamiento { get; set; }
        
        public int Procedimiento { get; set; }
        
        public bool SesionRealizada { get; set; }
        
        public long Tratamiento { get; set; }
    }
}
