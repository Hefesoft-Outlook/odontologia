using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Diagnostico
{
    public class DiagnosticoProcedimiento : IEntidadBase
    {
        
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        public byte? AplicaTratamiento { get; set; }
           
        public string Codigo { get; set; }
           
        public int? Color { get; set; }
           
        public int? ColorAdicional { get; set; }
           
        public string Descripcion { get; set; }
           
        public int? Diagnostico { get; set; }
           
        public string Fuente { get; set; }
           
        public short? IdIps { get; set; }
           
        public bool IndiceCEO { get; set; }
           
        public bool IndiceCOP { get; set; }
           
        public bool IndicePlacaBacteriana { get; set; }
           
        public string Letra { get; set; }
           
        public bool NoCubreConvenio { get; set; }
           
        public short? OtraCaracteristica { get; set; }
           
        public int? Procedimiento { get; set; }
        public int[] ProcedimientosAsociados { get; set; }
           
        public bool SesionRelizada { get; set; }
           
        public bool Severidad { get; set; }
           
        public string Simbolo { get; set; }
           
        public TipoPanel TipoPanel { get; set; }

        public int Identificador { get; set; }

        public bool generarIdentificador { get; set; }
    }

}
