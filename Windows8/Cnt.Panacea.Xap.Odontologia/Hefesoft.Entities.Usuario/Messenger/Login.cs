using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario.Messenger
{
    public class Login
    {
        public tipoLogin Modo { get; set; }
        public Action<dynamic> Resultado { get; set; }
    }

    public enum tipoLogin
    {
        Ninguno = 0,
        Facebook = 1,
        Outlook = 2,
        Hefesoft = 3,
    }
}
