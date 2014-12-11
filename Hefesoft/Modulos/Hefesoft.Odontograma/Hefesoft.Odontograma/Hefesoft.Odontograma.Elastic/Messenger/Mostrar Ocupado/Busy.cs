using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


public static class Busy
{
    public static void UserControlCargando(bool cargando = true, string mensaje = "Cargando")
    {
        GalaSoft.MvvmLight.Messaging.Messenger.Default.Send((new Mostrar_Cargando() { mostrar_Cargando = cargando, texto = mensaje })); 
    }
}

