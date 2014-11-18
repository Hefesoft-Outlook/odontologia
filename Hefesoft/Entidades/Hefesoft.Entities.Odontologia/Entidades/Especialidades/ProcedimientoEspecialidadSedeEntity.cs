using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Especialidades
{
    public class ProcedimientoEspecialidadSedeEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public string CodigoProcedimiento { get; set; }
        public EspecialidadEntity Especialidad { get; set; }
        
        public bool EsPrestado { get; set; }
        
        public bool EstaHabilitado { get; set; }
        
        public int Identificador { get; set; }
        
        public string NombreProcedimiento { get; set; }
        
        public int Procedimiento { get; set; }
        
        public SedeEntity Sede { get; set; }
        
        public TiposProcedimiento TipoProcedimiento { get; set; }
        
        public string UsuarioAutoriza { get; set; }

        public bool generarIdentificador { get; set; }
    }
}
