using Dto;
using Hefesoft.Entities.Odontologia.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Convenio
{
    public class ConvenioEntity : IEntidadBase
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string nombreTabla { get; set; }

        
        public bool AceptaBeneficiarios { get; set; }
        
        public ConvenioComentariosAdjuntosCollection Adjuntos { get; set; }
        
        public bool Aprobado { get; set; }
        
        public ConvenioComplementoArticulosCollection ArticulosComplementarios { get; set; }
        
        public ClasesTercero ClaseTercero { get; set; }
        
        public bool Clonado { get; set; }
        
        public ConvenioCoberturasCollection Coberturas { get; set; }
        
        public string Codigo { get; set; }
        
        public ConvenioContactosCollection Contactos { get; set; }
        
        public ContratoLiquidacionEntity ContratoLiquidacion { get; set; }
        
        public ConvenioControlesCollection Controles { get; set; }
        
        public short ConvenioContrato { get; set; }        
        
        public short? ConvenioPadre { get; set; }
        
        
        public bool CubrimientoPorcentual { get; set; }
        
        public ConvenioDocumentosSoporteCollection DocumentosSoporte { get; set; }
        
        public byte? DuracionCita { get; set; }
        
        public bool Estado { get; set; }
        
        public EtapasConvenio Etapa { get; set; }
        
        public DateTime? FechaFinalNegociacion { get; set; }
        public DateTime? FechaFinalPrestacion { get; set; }
        public DateTime? FechaInicialNegociacion { get; set; }
        public DateTime? FechaInicialPrestacion { get; set; }
        public DateTime? FechaTerminancion { get; set; }
        public byte? FrecuenciaCargue { get; set; }
        public FuentesConvenio FuenteConvenio { get; set; }
        public bool GeneraRips { get; set; }
        public ConvenioGruposEtareosCiudadesCollection GruposEtareosCiudades { get; set; }
        public ConvenioHistoricosCollection HistorialCambios { get; set; }
        public  short Identificador { get; set; }

        public int IdPptoAsignado { get; set; }

        public int IdPptoAsignadoApropiado { get; set; }
        public int IdTerceroConvenio { get; set; }
        public bool IncluirAdicionales { get; set; }
        
        public short? Ips { get; set; }
        
        public bool JustificacionNopos { get; set; }
        
        public bool ManejaAgenda { get; set; }
        
        public byte? MaximoCitasDia { get; set; }
        
        public MotivoCambioDatoEntity MotivoInactivar { get; set; }
        
        public bool MovilidadEntreSedes { get; set; }
        
        public NivelesTriageCollection NivelesTriage { get; set; }
        
        public string Nombre { get; set; }
        
        public OrigenConvenioEntity OrigenConvenio { get; set; }
        
        public ConvenioComplementoOtrosConceptosCollection OtrosConceptosComplementarios { get; set; }
        
        public short? Pais { get; set; }
        
        public int PoblacionMaxima { get; set; }
        
        public PrestadoresCollection PrestadoresSociedadMedica { get; set; }
        
        public bool PrestaEquipos { get; set; }
        
        public ConvenioComplementoProcedimientosCollection ProcedimientosComplementarios { get; set; }
        
        public ConvenioRenegociacionesCollection Renegociaciones { get; set; }
        
        
        public bool RestriccionBaseDatos { get; set; }
        
        public bool RestriccionPorSede { get; set; }
        
        public ConvenioSeccionesCollection SeccionesDiligenciadas { get; set; }
        
        public ContratosSeccionesCollection SeccionesDiligenciadasContrato { get; set; }
        
        public ConvenioSedesCollection Sedes { get; set; }
        
        public TerceroEntity Tercero { get; set; }
        
        public int TerceroClase { get; set; }
        
        public bool TieneRenegociacion { get; set; }
        
        
        public TiposAlarmas TipoAlarmaPoblacion { get; set; }
        
        public TiposAlarmas TipoAlarmaSemanas { get; set; }
        
        public byte? TipoConvenio { get; set; }
        
        public TipoConvenioMovimiento TipoConvenioMovimiento { get; set; }
        
        public TiposDesagregacionPoblacion TipoDesagregacionPoblacion { get; set; }
        
        public string TipoIdentificacionTercero { get; set; }
        
        public TiposSedesContrato TipoSedePrestador { get; set; }
        
        public short? TipoUsuario { get; set; }
        
        public TiposValorConvenio TipoValor { get; set; }
        
        public ConvenioTopesPacientesCollection TopesPacientes { get; set; }      
        
        public UnidadesPeriodicidad UnidadMedidaCargue { get; set; }

        public bool generarIdentificador { get; set; }
        
    }
}
