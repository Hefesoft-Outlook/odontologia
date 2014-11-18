using Hefesoft.Entities.Odontologia.Articulos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public  class DiagnosticoCieEntity
    {
        
        public string Accion { get; set; }
        
        public string Alarma { get; set; }
        
        public RangosHCCollection Alarmas { get; set; }
        
        public CapituloDiagnosticoEntity CapituloDiagnostico { get; set; }
        
        public CategoriaAislamientoEntity CategoriaAislamiento { get; set; }
        
        public CategoriaDiagnosticoEntity CategoriaDiagnostico { get; set; }
        
        public ClasificacionEpidemiologicaEntity ClasificacionEpidemiologica { get; set; }
        
        public string CodigoCie { get; set; }
        
        public string CodigoReportar { get; set; }
        
        public string DescripcionCie { get; set; }
        
        public int? DiagnosticoCiePadre { get; set; }
        
        public DiagnosticosCieCollection DiagnosticosEtiologicos { get; set; }
        
        public short DiasIncapacidad { get; set; }
        
        public bool EsDiagnosticoAltoCosto { get; set; }
        
        public bool EsDiagnosticoCronico { get; set; }
        
        public bool EsDiagosticoDEAislamiento { get; set; }
        
        public bool EsNosocomial { get; set; }
        
        public EspecialidadesCollection Especialidades { get; set; }
        
        public bool Estado { get; set; }
        
        public DateTime FechaActivacion { get; set; }
        
        public PlantillasHCCollection Formatos { get; set; }
        
        public bool GeneraNotificacion { get; set; }
        
        public bool GeneraVigilancia { get; set; }
        
        public GenerosConTodos Genero { get; set; }
        
        public GrupoDiagnosticoEntity GrupoDignostico { get; set; }
        
        public GruposIncapacidadCollection GruposIncapacidad { get; set; }
        
        public bool Hereda { get; set; }
        
        public int Identificador { get; set; }
        
        public Guid IdentificadorBitacora { get; set; }
        
        public DiagnosticosIncapacidadesCollection Incapacidades { get; set; }
        
        public short? Ips { get; set; }
        
        public string NombreAlterno { get; set; }
        
        public string Observaciones { get; set; }
        
        public PartesCuerpoHumanoCollection PartesCuerpo { get; set; }
        
        public ProcedimientosServiciosCollection Procedimientos { get; set; }
        
        public RangosHCCollection RangosEdad { get; set; }
        
        
        public bool RequiereNivelRiesgo { get; set; }
        
        public bool RequiereSeveridad { get; set; }

        public SintomasCollection Sintomas { get; set; }

        public SistemasCuerpoHumanoCollection SistemasCuerpo { get; set; }

        public TipoAltoCostoEntity TipoAltoCosto { get; set; }
        public string TipoEntidadBitacora { get; set; }

        public TiposNotificacionDx TipoNotificacion { get; set; }

        public VersionDiagnosticoEntity VersionDiagnostico { get; set; }
    }
}
