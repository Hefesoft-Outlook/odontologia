using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Hefesoft.Standard.BusyBox;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


public static class Busy
{
    public static void UserControlCargando(bool cargando = true, string mensaje = "Cargando")
    {
        BusyBox.UserControlCargando(cargando, mensaje);
    }
}

