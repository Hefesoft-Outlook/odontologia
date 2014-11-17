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

        internal void login()
        {
            var usuario = App2.Util.Storage.Usuario.obtenerUsuario();

            if(!string.IsNullOrEmpty(usuario.id))
            {
                var UsuarioVm = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
                UsuarioVm.UsuarioActivo = usuario;
            }

            oirUsuarioCreado();
        }


        private void oirUsuarioCreado()
        {
            //Ocurre cuando el usuario a sido creado
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Usuario.Messenger.Usuario_Cargado>(this, item => 
            {
                var valorUsuario = item.Usuario;
                App2.Util.Storage.Usuario.guardarUsuario(item.Usuario);
            });
        }
    }
}
