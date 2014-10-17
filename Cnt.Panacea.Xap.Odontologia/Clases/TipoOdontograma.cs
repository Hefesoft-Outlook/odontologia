
namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Estados que toma el control odontograma a lo largo del tiempo
    /// </summary>
    public static class TipoOdontograma
    {
        /// <summary>
        /// Estado Inicial
        /// </summary>
        public static bool Inicial { get; set; }
        /// <summary>
        /// Odontograma inicial en estado de carga
        /// </summary>
        public static bool InicialCargandoDatos { get; set; }
    }
}
