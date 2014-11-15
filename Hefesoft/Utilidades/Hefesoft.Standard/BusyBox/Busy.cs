using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.BusyBox
{
    public static class BusyBox
    {
        public static void UserControlCargando(bool cargando = true, string mensaje = "Cargando")
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send((new Mostrar_Cargando() { mostrar_Cargando = cargando, texto = mensaje }));
        }
    }
}
