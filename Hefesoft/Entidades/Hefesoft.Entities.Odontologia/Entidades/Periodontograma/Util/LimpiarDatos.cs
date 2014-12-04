using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontologia.Periodontograma.Util
{
    public class LimpiarDatos
    {
        public static void limpiarListado(IEnumerable<Entidades.PeriodontogramaEntity> listado)
        {
            foreach (var item in listado)
            {
                item.Clean();
            }
        }
    }
}
