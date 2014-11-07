using Dto;
using Hefesoft.Entities.Odontologia.Convenio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Tratamiento
{
    public class TratamientoEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public long? AtencionInicial { get; set; }        
        
        public ConvenioEntity Convenio { get; set; }

        public bool Cotizacion { get; set; }
        public decimal? CuotaInicial { get; set; }
        public short? Cuotas { get; set; }
        public string Descripcion { get; set; }
        public EstadoTratamiento EstadoTratamiento { get; set; }
        public DateTime? FechaFinal { get; set; }
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public long Identificador { get; set; }
        public long? IdFacturaCliente { get; set; }
        public short? IdIps { get; set; }
        public long IdOdontogramaInicial { get; set; }
        public short? IdSesionActual { get; set; }
        public int? IdTercero { get; set; }
        public string JustificacionCambio { get; set; }
        public string JustificacionCancelacion { get; set; }
        public string ObservacionesTerminacion { get; set; }
        public int Paciente { get; set; }        
        public TerceroEntity Prestador { get; set; }
        public decimal SaldoCuotaInicial { get; set; }
        public short? Sede { get; set; }
        public short? Sesiones { get; set; }
        public int? SesionesProgramadas { get; set; }
        public int? SesionesRealizadas { get; set; }
        public short? TipoPago { get; set; }

        
        public TipoTratamientoEntity TipoTratamiento { get; set; }
        public decimal? ValorCuota { get; set; }
        public decimal? ValorSesion { get; set; }

    }
}
