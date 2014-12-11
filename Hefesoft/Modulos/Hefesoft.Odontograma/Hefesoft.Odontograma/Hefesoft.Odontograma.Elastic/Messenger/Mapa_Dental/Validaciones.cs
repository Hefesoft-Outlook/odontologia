using System;
using System.Net;
using System.Windows;

namespace Cnt.Panacea.Xap.Odontologia.Util.Messenger.Mapa_Dental
{
    public class Validaciones
    {
        public Action<bool> Result { get; set; }
        public tipo  Tipo  { get; set; }
    }

    public enum tipo 
    {
        Cada_Superficie_Con_Diagnostico_Tiene_Procedimientos = 0,
    }
}
