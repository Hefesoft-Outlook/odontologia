using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up
{
    public class Mostrar_Ventana
    {
        public string Nombre { get; set; }
        public object DataContext { get; set; }
        public Action<object> Resultado { get; set; }
        public dynamic Propiedad_Adicional { get; set; }
    }
}
