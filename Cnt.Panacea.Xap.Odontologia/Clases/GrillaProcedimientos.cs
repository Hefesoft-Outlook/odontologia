using System;
using Cnt.Panacea.Entities.Odontologia;
using System.Collections.Generic;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{

    /// <summary>
    /// Esta clase pinta el datagrid de procedimientos en en cu Realizar plan de tratamiento
    /// </summary>
    public class GrillaProcedimientos
    {
        /// <summary>
        /// Nombre del diente
        /// </summary>
        public string Diente { get; set; }
        /// <summary>
        /// Id del diente
        /// </summary>
        public int idDiente { get; set; }
        /// <summary>
        /// id de la superficie
        /// </summary>
        public int idSuperficie { get; set; }
        /// <summary>
        /// nombre d ela superficie
        /// </summary>
        public string Superficie { get; set; }

        /// <summary>
        /// Nombre del diagnostico que se aplico en el cu odontograma inicial
        /// </summary>
        public string Diagnostico { get; set; }
        
        /// <summary>
        /// En caso de ser un diente supernumerario almacena el diente q sl supernumerario tiene a la izquierda
        /// </summary>
        public int DienteReferencia1 { get; set; }
        /// <summary>
        /// /// En caso de ser un diente supernumerario almacena el diente q sl supernumerario tiene a la derecha
        /// </summary>
        public int DienteReferencia2 { get; set; }
        
        /// <summary>
        /// Lista de procedimientos configurados que se aplican al diente o superficie
        /// </summary>
        public List<ConfiguracionProcedimientoOdontologiaEntity> ProcedimientoAsignado { get; set; }

        /// <summary>
        /// Nombre del procedimiento para mostrarlo en el datagrid
        /// </summary>
        public String Procedimientos { get; set; }

        /// <summary>
        /// Nombre de la especialidad que realizara el procedimiento 
        /// </summary>
        public string Especialidad { get; set; }
        /// <summary>
        /// id d ela especialidad que realizara el procedimiento
        /// </summary>
        public int IdEspecialidad { get; set; }

        /// <summary>
        /// Sesion en la cual sera realizado el procedimiento
        /// </summary>
        public int Sesion { get; set; }


    }
}
