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
using Hefesoft.Standard.Util.Collection.IEnumerable;
using Hefesoft.Standard.Util.Collection.Observables;
using System.Collections.ObjectModel;
using Hefesoft.Usuario.ViewModel.Pacientes;

namespace Hefesoft.Pacientes.Elastic.ViewModel
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
                try
                {
                    data = Hefesoft.Usuario.Contexto.Contexto.obtenerContexto();
                    vmUsuario = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
                    listar();
                }
                catch { }

                add = new GalaSoft.MvvmLight.Command.RelayCommand(addElement, validateAdd);
                createNew = new GalaSoft.MvvmLight.Command.RelayCommand(() => 
                {
                    Paciente = null;
                    Paciente = new Hefesoft.Usuario.Entidades.Usuario() { imagenRuta = "http://www.flaticon.com/png/256/37943.png" };
                });

                lostFocus = new RelayCommand(() => 
                {
                    try
                    {
                        add.RaiseCanExecuteChanged();
                    }
                    catch
                    {
                    }
                });

                search = new GalaSoft.MvvmLight.Command.RelayCommand<string>(searchElement);
            }
        }

        private void searchElement(string obj)
        {
            Listado = null;
            if(!string.IsNullOrEmpty(obj))
            {                
                Listado = ListadoTodos.Where(a => a.nombre.Contains(obj)).ToObservableCollection();
            }
            else
            {
                Listado = ListadoTodos.ToObservableCollection();
            }

            RaisePropertyChanged("Listado");
        }

        private bool validateAdd()
        {
            var valido = true;
            if (Paciente != null)
            {
                if (string.IsNullOrEmpty(Paciente.nombre))
                {
                    valido = false;
                }
                if (string.IsNullOrEmpty(Paciente.telefono))
                {
                    valido = false;
                }
            }
            else
            {
                valido = false;
            }
            return valido;
        }

        private async void addElement()
        {
            BusyBox.UserControlCargando(true);
            if (!string.IsNullOrEmpty(Paciente.RowKey) && ListadoTodos.Any(a => a.RowKey == Paciente.RowKey))
            {
                var item = ListadoTodos.FirstOrDefault(a => a.RowKey == Paciente.RowKey);
                item = Paciente;
                await insert(Paciente);
                Listado.UpdateElementCollection(Paciente);
            }
            else if (Paciente != null)
            {
                await insert(Paciente);
                Listado.Add(Paciente);
            }

            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private async void listar()
        {
           BusyBox.UserControlCargando(true);
           var result = await data.listarUsuarios(vmUsuario.UsuarioActivo.id, "", "Pacientes");
           ListadoTodos = result.ToList();
           Listado = ListadoTodos.ToObservableCollection();
           RaisePropertyChanged("Listado");
           BusyBox.UserControlCargando(false);
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

        private Usuario.Entidades.Usuario paciente = new Hefesoft.Usuario.Entidades.Usuario() { imagenRuta = "http://www.flaticon.com/png/256/37943.png" };

        public Usuario.Entidades.Usuario Paciente
        {
            get { return paciente; }
            set 
            {                
                paciente = value;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send("Paciente seleccionado", "Paciente seleccionado");
                RaisePropertyChanged("Paciente"); 
            }
        }


        public GalaSoft.MvvmLight.Command.RelayCommand add { get; set; }

        public RelayCommand lostFocus { get; set; }

        public Hefesoft.Usuario.Interfaces.IUsuarios data { get; set; }

        public Hefesoft.Usuario.ViewModel.Usuarios vmUsuario { get; set; }

        public ObservableCollection<Hefesoft.Usuario.Entidades.Usuario> Listado { get; set; }

        public RelayCommand createNew { get; set; }


        public List<Hefesoft.Usuario.Entidades.Usuario> ListadoTodos { get; set; }

        public RelayCommand<string> search { get; set; }
    }
}
