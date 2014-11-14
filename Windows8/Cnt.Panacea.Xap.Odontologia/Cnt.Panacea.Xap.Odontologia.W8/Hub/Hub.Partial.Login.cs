using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight.Messaging;
using Hefesoft.Entities.Odontologia.Odontograma;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace App2
{
    public sealed partial class HubPage : Page, IDisposable
    {

        internal async void login()
        {
            string login = (string)ApplicationData.Current.LocalSettings.Values["login"];

            if(!string.IsNullOrEmpty(login))
            {  
                if (login.Equals("Facebook"))
                {
                    item = new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.Login() { Modo = Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.tipoLogin.Facebook, Resultado = loginCallBack };
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(item, "Facebook");
                }
                else if (login.Equals("Outlook"))
                {
                    item = new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.Login() { Modo = Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.tipoLogin.Outlook, Resultado = loginCallBack };
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(item, "Outlook");
                }
            }
            else
            {

            }
        }

        private void loginCallBack(dynamic obj)
        {
            if(item.Modo == Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.tipoLogin.Facebook)
            {

            }
            else if (item.Modo == Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.tipoLogin.Outlook)            
            {

            }
            Busy.UserControlCargando(false);
        }


        public Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Login.Login item { get; set; }
    }
}
