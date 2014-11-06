using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Entities.Odontologia.Historia;
using Hefesoft.Entities.Odontologia.Articulos;
using Hefesoft.Entities.Odontologia.Especialidades;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class ProcedimientoServicioEntity
    {   
        public PaqueteArticulosCollection ArticulosPaquete { get; set; }
        
        public InformacionBibliograficaNoPosCollection BibliografiaNoPos { get; set; }
        
        public ClasificacionesProcedimiento ClasificacionProcedimiento { get; set; }
        
        public string Codigo { get; set; }
        
        public short ConceptoServicioSalud { get; set; }
        public string DescripcionTipoProcedimiento { get; set; }
        
        public DiagnosticosCieCollection DiagnosticosPaquete { get; set; }
        
        public short DiasIncapacidad { get; set; }
        
        public short EdadFinal { get; set; }
        
        public int EdadFinalDias { get; set; }
        
        public short EdadInicial { get; set; }
        
        public int EdadInicialDias { get; set; }
        
        public EspecialidadesCollection Especialidades { get; set; }
        
        public EspecialidadEntity EspecialidadPertenece { get; set; }
                
        public bool Estado { get; set; }
        
        public bool EstaGravadoConIva { get; set; }
        
        public bool EstaIncluidoPos { get; set; }
        
        public EventosPaquetes Evento { get; set; }
        
        public bool Extramural { get; set; }
        
        public bool GeneraIncapacidad { get; set; }
        
        public GenerosConTodos Genero { get; set; }
        
        public ProcedimientoHistoricosCollection Historial { get; set; }
        
        public long IdAdmision { get; set; }
        
        public long IdAIU { get; set; }
        
        public int Identificador { get; set; }
        
        public Guid IdentificadorBitacora { get; set; }
        
        public short IdEspecialidad { get; set; }
        
        public short IdGrupoServicioAgenda { get; set; }
        
        public short IdImpuesto { get; set; }
        
        public ImpuestoEntity Impuesto { get; set; }
        
        public ProcedimientosIncapacidadesCollection Incapacidades { get; set; }
        
        public short? Ips { get; set; }
        
        public short NivelAtencion { get; set; }
        
        public short? NivelAutorizacion { get; set; }
        
        public NivelesComplejidad NivelComplejidad { get; set; }
        
        public string Nombre { get; set; }
        
        public string NombreAlterno { get; set; }
        
        public short? NumeroMuestras { get; set; }
        
        public string ObservacionesPaquete { get; set; }
        
        public PaqueteOtrosConceptosFacturacionCollection OtrosConceptosPaquete { get; set; }
        
        public bool Posfechado { get; set; }
        
        public PaqueteProcedimientosCollection ProcedimientosPaquete { get; set; }
        
        public ProgramacionesCollection Programaciones { get; set; }
        
        public PruebaPreTransfusionCollection PruebasPreTransfusionales { get; set; }
        
        public bool RequiereConsentimientoInformado { get; set; }
        
        public bool RequiereReferencia { get; set; }
        
        public ProcedimientosServiciosCollection RompePaquetes { get; set; }
                
        public short SemanasCotizacion { get; set; }
        
        public bool ServicioAltoCosto { get; set; }
        
        public short? TiempoVigenciaPos { get; set; }
        
        public short? TiempoVigenciaPre { get; set; }
        public string TipoEntidadBitacora { get; set; }
        
        public TiposPaquete TipoPaquete { get; set; }
        
        public TiposProcedimiento TipoProcedimiento { get; set; }
        
        public Collection<TiposAtencion> TiposAtencion { get; set; }
        
        public UnidadesMedidaEdad UnidadMedidaEdad { get; set; }
        
        public byte? UnidadVigenciaPos { get; set; }
        
        public byte? UnidadVigenciaPre { get; set; }
    }
}
