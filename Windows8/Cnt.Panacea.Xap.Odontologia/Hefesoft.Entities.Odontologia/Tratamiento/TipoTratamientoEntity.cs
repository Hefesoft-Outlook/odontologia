using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Tratamiento
{
    public class TipoTratamientoEntity
    {
        public bool Estado { get; set; }
        
        public short Identificador { get; set; }
        
        public short Ips { get; set; }
        
        public string Nombre { get; set; }
    }
}
