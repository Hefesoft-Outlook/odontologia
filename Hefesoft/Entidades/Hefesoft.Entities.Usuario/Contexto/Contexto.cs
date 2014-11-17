using Hefesoft.Usuario.Interfaces;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Usuario.Contexto
{
    public class Contexto
    {
        public static Interfaces.IUsuarios obtenerContexto() 
        {
            var contexto = ServiceLocator.Current.GetInstance<IUsuarios>();
            return contexto;
        }
    }
}
