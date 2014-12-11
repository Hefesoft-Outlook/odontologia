using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.PopUp
{
    /// <summary>
    /// Clase que identifica los tres estados posiblen cuando una superficie del diente ya tiene asociado un tratamiento
    /// </summary>
    public class EstadoSobreEscribirAdicionar
    {
        public bool Adicionar { get; set; }
        public bool SobreEscribir { get; set; }
        public bool Cancelar { get; set; }
    }
}
