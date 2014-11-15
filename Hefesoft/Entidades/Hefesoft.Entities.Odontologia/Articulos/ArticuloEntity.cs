using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Articulos
{
    public class ArticuloEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

 
        public bool BajaRotacion { get; set; }

        public InformacionBibliograficaNoPosCollection BibliografiaNoPos { get; set; }

        public int CantidadExistencia { get; set; }

        public short? CantidadVencimiento { get; set; }

        public ClasesArticulo ClaseArticulo { get; set; }

        public string Codigo { get; set; }

        public decimal? Concentracion { get; set; }

        public decimal CostoPromedioTotal { get; set; }

        public decimal CostoPromedioUnitario { get; set; }

        public string DescripcionConcentracion { get; set; }

        public EspecialidadesCollection Especialidades { get; set; }


        public bool Estado { get; set; }

        public FormaFarmaceuticaEntity FormaFarmaceutica { get; set; }

        public ArticuloHistoricoCollection Historial { get; set; }

        public  int Identificador { get; set; }

        public Guid IdentificadorBitacora { get; set; }

        public short IdFormaFarmaceutica { get; set; }


        public short IdImpuestoIva { get; set; }

        public byte IdUnidadConcentracion { get; set; }

        public byte IdUnidadMinima { get; set; }

        public ImpuestoEntity ImpuestoIva { get; set; }

        public short? Ips { get; set; }

        public ArticulosLaboratoriosCollection Laboratorios { get; set; }

        public string Nombre { get; set; }

        public string NombreUsuarioCambio { get; set; }

        public string Observaciones { get; set; }

        public bool Posfechado { get; set; }

        public decimal PrecioUnitario { get; set; }

        public ProgramacionesCollection Programaciones { get; set; }

        public short? TiempoVigenciaPos { get; set; }

        public short? TiempoVigenciaPre { get; set; }

        public TiposArticuloAsistencial TipoArticuloAsistencial { get; set; }

        public TiposCostoArticulo TipoCosto { get; set; }
        public string TipoEntidadBitacora { get; set; }

        public TiposUnidadMedida TipoUnidadConcentracion { get; set; }

        public TiposUnidadMedida TipoUnidadUnidadMinima { get; set; }

        public decimal UltimoCosto { get; set; }

        public decimal UltimoPrecio { get; set; }

        public UnidadMedidaEntity UnidadConcentracion { get; set; }

        public UnidadMedidaEntity UnidadMinima { get; set; }

        public UnidadesPeriodicidad UnidadVencimiento { get; set; }

        public byte? UnidadVigenciaPos { get; set; }

        public byte? UnidadVigenciaPre { get; set; }

        public bool generarIdentificador { get; set; }

    }
}
