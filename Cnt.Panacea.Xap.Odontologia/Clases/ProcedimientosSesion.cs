
namespace Cnt.Panacea.Xap.Odontologia.Clases
{

    /// <summary>
    /// Clase que permite asiganar la cantidad d eprocedimientos que se asignan por sesion
    /// </summary>
    public class ProcedimientosSesion
    {
        /// <summary>
        /// Cuenta de lso procedimientos asignados en cada sesion
        /// </summary>
        public int Conteo { get; set; }
        /// <summary>
        /// Sesion en la cual se adiciona un procedimiento
        /// </summary>
        public int Sesion { get; set; }


    }
}
