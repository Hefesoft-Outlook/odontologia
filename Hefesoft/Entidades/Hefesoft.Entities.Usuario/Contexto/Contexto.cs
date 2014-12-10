using GalaSoft.MvvmLight.Ioc;
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
            if (!SimpleIoc.Default.IsRegistered<Hefesoft.Usuario.ViewModel.Usuarios>())
            {
                new Hefesoft.Usuario.Locator();
            }

            var contexto = ServiceLocator.Current.GetInstance<IUsuarios>();
            return contexto;
        }
    }
}
