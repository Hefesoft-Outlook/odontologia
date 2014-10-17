using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Clases
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
