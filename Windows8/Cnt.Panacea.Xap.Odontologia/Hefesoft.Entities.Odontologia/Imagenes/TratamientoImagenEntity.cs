using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Entities.Odontologia.Odontograma
{
    public class TratamientoImagenEntity
    {
        public object Contenido { get; set; }
        public bool Estado { get; set; }
        public int Identificador { get; set; }
        public object Imagen { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string RutaVirtual { get; set; }
        public string RutaVirtualAlterna { get; set; }
        public TiposImagenes TipoImagen { get; set; }
        public string TipoMime { get; set; }
        public long Tratamiento { get; set; }
    }
}
