using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.BusyBox;
using Hefesoft.Usuario.Messenger;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Usuario.ViewModel.Pacientes
{
    public class Pacientes : ViewModelBase
    {
        public Pacientes()
        {
            if(IsInDesignMode)
            {

            }
            else
            {
                data = Hefesoft.Usuario.Contexto.Contexto.obtenerContexto();
                vmUsuario = ServiceLocator.Current.GetInstance<ViewModel.Usuarios>();
                listar();

                add = new GalaSoft.MvvmLight.Command.RelayCommand(addElement, validateAdd);
                createNew = new GalaSoft.MvvmLight.Command.RelayCommand(() => 
                {
                    Paciente = null;
                    Paciente = new Entidades.Usuario() { imagenRuta = "http://www.flaticon.com/png/256/37943.png" };
                });

                lostFocus = new RelayCommand(() => 
                {
                    add.RaiseCanExecuteChanged();
                });

                search = new GalaSoft.MvvmLight.Command.RelayCommand<string>(searchElement);
            }
        }

        private void searchElement(string obj)
        {
            Listado = null;
            if(!string.IsNullOrEmpty(obj))
            {                
                Listado = ListadoTodos.Where(a => a.nombre.Contains(obj));                
            }
            else
            {
                Listado = ListadoTodos;
            }

            RaisePropertyChanged("Listado");
        }

        private bool validateAdd()
        {
            var valido = true;
            if(string.IsNullOrEmpty(Paciente.nombre))
            {
                valido = false;
            }
            if (string.IsNullOrEmpty(Paciente.telefono))
            {
                valido = false;
            }
            return valido;
        }

        private async void addElement()
        {
            if (!string.IsNullOrEmpty(Paciente.RowKey) && ListadoTodos.Any(a => a.RowKey == Paciente.RowKey))
            {
                var item = ListadoTodos.FirstOrDefault(a => a.RowKey == Paciente.RowKey);
                item = Paciente;
                Listado = null;
                Listado = ListadoTodos;
                RaisePropertyChanged("Listado");
            }

            if (Paciente != null)
            {
                await insert(Paciente);
            }
        }

        private async void listar()
        {
           ListadoTodos = await data.listarUsuarios(vmUsuario.UsuarioActivo.id, "", "Pacientes");
           Listado = ListadoTodos;
           RaisePropertyChanged("Listado");
        }

        public async Task<Hefesoft.Usuario.Entidades.IUsuario> insert(Hefesoft.Usuario.Entidades.IUsuario usuario)
        {
            BusyBox.UserControlCargando(true);            
            usuario.nombreTabla = "Pacientes";
            usuario.PartitionKey = vmUsuario.UsuarioActivo.id;


            var usuarioCreado = await data.crearUsuario(usuario);
            BusyBox.UserControlCargando(false);
            return usuarioCreado;
        }

        public void seleccionar(Hefesoft.Usuario.Entidades.Usuario seleccionado)
        {
            Paciente = seleccionado;
        }
        
        private Usuario.Entidades.Usuario paciente = new Entidades.Usuario() { imagenRuta = "http://www.flaticon.com/png/256/37943.png" };

        public Usuario.Entidades.Usuario Paciente
        {
            get { return paciente; }
            set 
            {                
                paciente = value;
                RaisePropertyChanged("Paciente"); 
            }
        }


        public GalaSoft.MvvmLight.Command.RelayCommand add { get; set; }

        public RelayCommand lostFocus { get; set; }

        public Interfaces.IUsuarios data { get; set; }

        public Usuarios vmUsuario { get; set; }

        public IEnumerable<Entidades.Usuario> Listado { get; set; }

        public RelayCommand createNew { get; set; }

        public IEnumerable<Entidades.Usuario> ListadoTodos { get; set; }

        public RelayCommand<string> search { get; set; }
    }
}
