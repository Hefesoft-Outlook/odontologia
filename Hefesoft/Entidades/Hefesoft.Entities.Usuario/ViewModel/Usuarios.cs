using GalaSoft.MvvmLight;
using Hefesoft.Usuario.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hefesoft.Standard.BusyBox;
using GalaSoft.MvvmLight.Command;

namespace Hefesoft.Usuario.ViewModel
{
    public class Usuarios : ViewModelBase
    {
        public Usuarios()
        {
            if(IsInDesignMode)
            {

            }
            else
            {
                changeUserCommand = new RelayCommand(changeUser);
            }
        }

        private void changeUser()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("Cambio Usuario", "Cambio Usuario");
        }        

        // Obtiene un string de lo que esta guardado en el storage y pide que se loguee de esa manera
        // Luego en el callback hace el proceso con los datos del login
        public void login(string login)
        {
            BusyBox.UserControlCargando(true, "Validando usuario");
            if (!string.IsNullOrEmpty(login))
            {
                if (login.Equals("Facebook"))
                {
                    item = new Login() { Modo =  tipoLogin.Facebook , Resultado = loginCallBack };
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(item, "Facebook");
                }
                else if (login.Equals("Outlook"))
                {
                    item = new Login() { Modo =  tipoLogin.Outlook, Resultado = loginCallBack };
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(item, "Outlook");
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// Cuando la app va y valida contra el servicio correspondiente
        /// devuelve la informacion para ser procesada
        /// </summary>
        /// <param name="obj"></param>
        private async void loginCallBack(dynamic obj)
        {
            if (item.Modo == tipoLogin.Facebook)
            {
                var fb = new Hefesoft.Usuario.Entidades.Facebook();
                fb.nombre = obj.name;
                fb.id = obj.id;
                fb.RowKey = obj.id;
                await insert(fb);
            }
            else if (item.Modo == tipoLogin.Outlook)
            {
                var outlook = new Hefesoft.Usuario.Entidades.Outlook();
                outlook.nombre = obj.name;
                outlook.id = obj.id;
                outlook.RowKey = obj.id;
                await insert(outlook);
            }
        }


        public async Task<Hefesoft.Usuario.Entidades.IUsuario> insert(Hefesoft.Usuario.Entidades.IUsuario usuario)
        {
            var data = new Hefesoft.Usuario.Data.Usuarios();
            BusyBox.UserControlCargando(false);
            var usuarioCreado = await data.crearUsuario(usuario);
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Messenger.Usuario_Cargado() { Usuario = usuarioCreado });
            return usuarioCreado;
        }

        public Login item { get; set; }

        private Usuario.Entidades.Usuario usuarioActivo = new Entidades.Usuario() { imagenRuta = "http://www.flaticon.com/png/256/37943.png", nombre = "-----------" };

        public Usuario.Entidades.Usuario UsuarioActivo
        {
            get { return usuarioActivo; }
            set 
            { 
                usuarioActivo = value; 
                RaisePropertyChanged("UsuarioActivo"); 
            }
        }


        public RelayCommand changeUserCommand { get; set; }
    }
}
