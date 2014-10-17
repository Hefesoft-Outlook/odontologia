
namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Clase que se usa para mostrar la grill con los procedimientos y diagnosticos asociados a cada superficie
    /// </summary>
    public class GrillaTratamiento
    {
        /// <summary>
        /// Numero del diente
        /// </summary>
        public string Diente { get; set; }
        /// <summary>
        /// Nombre de la superficies
        /// </summary>
        public string Superficie { get; set; }
        /// <summary>
        /// Diagnostico que se efectuo en el odontograma inicial de la superficie
        /// </summary>
        public string Diagnostico { get; set; }
    }
}
