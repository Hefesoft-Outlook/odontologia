
namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Cuando se estan coloreando los elementos del odontograma para seleccionarlos, se distingue entre dos estado posibles
    /// El Estado cuando se encuentran seleccionando las superficies y cuando se estan aplicando los tratamientos, esta clase
    /// Otorga la funcionalidad para manejar estos estados
    /// </summary>
    public class Estado_Aplicar_Tratamiento
    {
        public bool AdicionarElementos { get; set; }
        public bool AplicarTratamientos { get; set; }
    }
}
