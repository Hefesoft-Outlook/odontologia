using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Estaticas
{
    /// <summary>
    /// Clase que es transversal a toda la aplicacion, maneja los datos que se desean esten disponibles en toda la aplicacion    
    /// </summary>
    public static class Variables_Globales
    {
        #region Propiedades
        /// <summary>
        /// ips
        /// </summary>
        public static short IdIps { get; set; }
        /// <summary>
        /// idpaciente
        /// </summary>
        public static int IdPaciente { get; set; }

        /// <summary>
        /// id Finalidad Procedimiento
        /// </summary>
        public static int IdFinalidadProcedimiento { get; set; }

        /// <summary>
        /// 
        /// </summary
        private static long? idAtencion;

        /// <summary>
        /// idprestador
        /// </summary>
        public static long? IdAtencion
        {
            get { return Variables_Globales.idAtencion; }
            set { Variables_Globales.idAtencion = value; }
        }
        public static int IdPrestador { get; set; }
        /// <summary>
        /// idSesion
        /// </summary>
        public static string IdSesion { get; set; }
        /// <summary>
        /// idUsuario
        /// </summary>
        public static string IdUsuario { get; set; }
        /// <summary>
        /// Session id
        /// </summary>
        public static string SessionId { get; set; }
        /// <summary>
        /// ip cliente
        /// </summary>
        public static string IpCliente { get; set; }
        /// <summary>
        /// Usuario actual
        /// </summary>
        public static string UsuarioActual { get; set; }
        /// <summary>
        /// Identifica si es o no clasificador odontologico
        /// </summary>
        public static bool ClasificadorOdontologico { get; set; }
        /// <summary>
        /// Identifica si permite imprimir
        /// </summary>
        public static string ImprimirOdontograma { get; set; }
        /// <summary>
        /// Lista de tratamientos
        /// </summary>
        public static List<Int64> TratamientosPaciente { get; set; }        
        /// <summary>
        /// Convenio
        /// </summary>
        public static short IdConvenio { get; set; }
        /// <summary>
        /// Sede
        /// </summary>
        public static short IdSede { get; set; }
        /// <summary>
        /// Cantidad de procedimientos para la session
        /// </summary>
        public static short MaximaCantidadProcedimientosSesion { get; set; }
        
        /// <summary>
        /// Paciente
        /// </summary>
        public static dynamic Paciente { get; set; }
        /// <summary>
        /// Procedimientos por especialidad
        /// </summary>
        public static List<ProcedimientoEspecialidadSedeEntity> ProcedimientosEspecialidad { get; set; }
        /// <summary>
        /// La aplicacion se encuentra en odontograma inicial
        /// </summary>
        public static bool OdontogramaInicial { get; set; }
        /// <summary>
        /// La aplicacion se encuentra en plan de tratamiento
        /// </summary>
        public static bool Plantratamiento { get; set; }
        /// <summary>
        /// La aplicacion se encuentra en odontograma evolucion
        /// </summary>
        public static bool Evolucion { get; set; }
        /// <summary>
        /// Odontograma paciente
        /// </summary>
        public static Int64 OdontogramaPacientetity { get; set; }
        /// <summary>
        /// Identificador del tratamiento activo
        /// </summary>
        public static Int64 IdTratamientoActivo { get; set; }
        /// <summary>
        ///En estado de nuevo odontograma
        /// </summary>
        public static bool NuevoOdontograma { get; set; }
        /// <summary>
        /// Sesion actual
        /// </summary>
        public static short SesionActual { get; set; }

        public static int IdBodega { get; set; }

        public static bool CargarEvolucion { get; set; }

        public static ParametroOdontologicoConvenioEntity ParametroConvenio { get; set; }

        public static bool mostrar_Diagnosticos_plan_tratamiento { get; set; }

        #endregion

        public static Tipo_Odontograma Tipo_Odontograma_Activo { get; set; }

        public static TratamientoEntity TratamientosPadre { get; set; }

        public static int COP { get; set; }

        public static int CEO { get; set; }

        public static double Indice_Placa_Bacteriana { get; set; }

        public static double Numero_Piezas_Dentales { get; set; }

        public static PCL PCL { get; set; }

        public static Util.Modos.Modo Modo { get; set; }

        public static string IdPacienteHefesoft { get; set; }

        public static string urlImagenTratamiento { get; set; }
    }

    public class PCL
    {

        public Hefesoft.Entities.Odontologia.Odontograma.Odontograma PlanTratamiento { get; set; }
    }
}
