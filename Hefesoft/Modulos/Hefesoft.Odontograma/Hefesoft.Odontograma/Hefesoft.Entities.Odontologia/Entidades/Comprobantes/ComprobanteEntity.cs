using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Standard.Interfaces;

namespace Hefesoft.Entities.Odontologia.Comprobantes
{
    public class ComprobanteEntity: IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        
        public ComprobantesReporteCollection AdjuntoReporte { get; set; }
        
        public bool Bloqueado { get; set; }
        
        public short? ComprobanteAnula { get; set; }
        
        public string Descripcion { get; set; }
        
        public ComprobanteDetallesCollection Detalles { get; set; }
        
        
        public string EncabezadoAdicional { get; set; }
        
        public bool EsDependiente { get; set; }
        
        public bool Estado { get; set; }
        
        public ComprobanteHistoricoCollection Historial { get; set; }
        
        public short Identificador { get; set; }
        
        public Guid IdentificadorBitacora { get; set; }
        
        public short? Ips { get; set; }
        
        public ModuloFuncionalEntity Modulo { get; set; }
        
        public string NombreInforme { get; set; }
        
        public bool PermiteExportar { get; set; }
        
        public string Prefijo { get; set; }
        
        public bool RequiereAutorizacion { get; set; }
        
        public TipoComprobanteEntity TipoComprobante { get; set; }
        
        public TiposConsecutivo TipoConsecutivo { get; set; }
        public string TipoEntidadBitacora { get; set; }
        
        public ComprobantesDependenciaCollection TiposComprobanteDependencia { get; set; }

        public bool generarIdentificador { get; set; }
    }
}
