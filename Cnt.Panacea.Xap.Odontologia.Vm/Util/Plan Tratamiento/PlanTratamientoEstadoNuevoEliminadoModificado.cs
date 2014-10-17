using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento
{
    /// <summary>
    /// Clase que se usa para validar el estado de un plan de tratamiento en una superficie
    /// </summary>
    public class PlanTratamientoEstadoNuevoEliminadoModificado
    {
        /// <summary>
        /// Estado NUevo
        /// </summary>
        public bool Nuevo { get; set; }
        /// <summary>
        /// Estado Modificado
        /// </summary>
        public bool Modificado { get; set; }
        /// <summary>
        /// Estado Eliminado
        /// </summary>
        public bool Eliminado { get; set; }
    }
}
