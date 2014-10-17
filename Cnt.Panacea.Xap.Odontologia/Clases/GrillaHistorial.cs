using System;
using Cnt.Panacea.Entities.Odontologia;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Esta Clase pinta lel grid de historial del odontograma
    /// </summary>
    public class GrillaHistorial

    {
        /// <summary>
        /// Propiedad con el id del diente
        /// </summary>
        public int Diente { get; set; }
        
        /// <summary>
        /// Propiedad Superfice
        /// </summary>
        public Superficie Superficie { get; set; }

        /// <summary>
        /// Usuario que realizo el cambio
        /// </summary>
        public string Usuario { get; set; }

        /// <summary>
        /// Muestra el procedinmiento o el diagnostico q se aplico a el diente y la superficie
        /// </summary>
        public string Aplica { get; set; }
        
        /// <summary>
        /// Fecha en la cual se realizo el cambio
        /// </summary>
        public DateTime Fecha { get; set; }
    }
}
