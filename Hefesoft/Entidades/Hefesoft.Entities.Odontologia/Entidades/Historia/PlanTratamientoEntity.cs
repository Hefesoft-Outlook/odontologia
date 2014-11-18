using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Entities.Odontologia.Historia;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class PlanTratamientoEntity
    {
        
        public bool Activo { get; set; }
        
        //public AplicacionesCanastaDtoCollection AplicacionesCanasta { get; set; }
        
        //public AtencionOdontologiaArticulosCollection Articulos { get; set; }
        
        public bool Cobra { get; set; }
        
        public string CodigoProcedimiento { get; set; }
        
        public short Convenio { get; set; }
        
        public bool CubiertoConvenio { get; set; }
        
        public short? Especialidad { get; set; }
        
        public bool EstadoProcedimiento { get; set; }
        
        public long? Factura { get; set; }
        
        public DateTime? FechaPago { get; set; }
        
        public DateTime? FechaRealizacion { get; set; }
        
        public DateTime? FechaRegistro { get; set; }
        
        public short? FinalidadProcedimiento { get; set; }
        
        public long? IdAtencion { get; set; }
        
        public long Identificador { get; set; }
        
        public int? IdPrestadorRealiza { get; set; }
        
        public string JustificacionEliminacion { get; set; }
        
        public long? Legalizacion { get; set; }
        
        public string NombreConvenio { get; set; }
        
        public string NombreEspecialidad { get; set; }
        
        public string NombrePrestador { get; set; }
        
        public string NombreProcedimiento { get; set; }
        
        public string NombreTercero { get; set; }
        
        public short? NumeroSesion { get; set; }
        
        public short NumeroSesionesProcedimiento { get; set; }
        
        public string Observaciones { get; set; }
        
        public long Odontograma { get; set; }
        
        public short OpcionTratamiento { get; set; }
        
        public int Prestador { get; set; }
        
        public int? PrestadorHigienista { get; set; }
        
        public int? PrestadorOdontologo { get; set; }
        
        public int? Procedimiento { get; set; }

        public IEnumerable<SesionesPlanTratamientoEntity> SesionesPlanTratamiento { get; set; }
        
        public string UsuarioModificador { get; set; }
        
        public string UsuarioRecibe { get; set; }
        
        public decimal? ValorPaciente { get; set; }
        
        public decimal? ValorServicio { get; set; }
    }
}
