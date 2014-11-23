using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Entities.Odontologia.Tercero;
using Hefesoft.Entities.Odontologia.Articulos;

namespace Hefesoft.Entities.Odontologia.Convenio
{
    public class TerceroEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public ActividadesEconomicasCollection ActividadesEconomicas { get; set; }
        
        public bool Aprobado { get; set; }
        
        public TercerosAutoretenedoresCollection AutoRetenciones { get; set; }
        
        public TerceroClasesCollection Clases { get; set; }
        
        //public TerceroIntegracion? Cliente { get; set; }

        public TerceroContactosCollection Contactos { get; set; }

        public TerceroEmpresaContactoCollection ContactosEmpresa { get; set; }
        
        public TerceroContableEntity DatosContables { get; set; }
        
        public TerceroTributarioEntity DatosTributarios { get; set; }
        
        public string DigitoVerificacion { get; set; }
        
        public bool Estado { get; set; }
        
        public IEnumerable<TiposRetencion> Exenciones { get; set; }
        
        public DateTime? FechaCreacion { get; set; }
        
        public TerceroHistoricosCollection Historico { get; set; }
        
        public int Identificador { get; set; }

        public Guid IdentificadorBitacora { get; set; }

        public TerceroImagenesCollection Imagenes { get; set; }


        public short Ips { get; set; }

        public string NombreCargoTurnos { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroIdentificacion { get; set; }

        public PeriodosFiscalesCollection PeriodosFiscales { get; set; }

        public ProgramacionesCollection Programaciones { get; set; }
        public string TipoEntidadBitacora { get; set; }

        public TipoIdentificacionEntity TipoIdentificacion { get; set; }

        public string TipoIdentificacionTercero { get; set; }

        public TiposPersonaSinAmbos TipoPersona { get; set; }

        public TiposProfesionalSalud TipoProfesional { get; set; }

        public bool generarIdentificador { get; set; }

        public bool? Activo { get; set; }
    }
}
