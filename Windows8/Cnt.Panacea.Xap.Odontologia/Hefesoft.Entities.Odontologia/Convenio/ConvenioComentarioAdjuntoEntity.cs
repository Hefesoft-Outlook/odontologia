using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Convenio
{
    public class ConvenioComentarioAdjuntoEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        
        public string Comentario { get; set; }
        public object Contenido { get; set; }
        
        public short? Convenio { get; set; }
        
        public DateTime? Fecha { get; set; }
        
        public override short Identificador { get; set; }
        
        public string Nombre { get; set; }
        
        public string Ruta { get; set; }
        
        public string RutaVirtual { get; set; }
        
        public object Soporte { get; set; }
        
        public string TipoMime { get; set; }
        
        public string UsuarioComentario { get; set; }

    }
}
