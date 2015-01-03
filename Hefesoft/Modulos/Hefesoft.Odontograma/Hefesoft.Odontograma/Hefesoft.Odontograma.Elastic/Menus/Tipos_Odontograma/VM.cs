using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Odontograma.Elastic.Menus.Tipos_Odontograma
{
    public class VM : ViewModelBase
    {
        public VM()
        {
            if(IsInDesignMode)
            {
                Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 0, Descripcion = "Nuevo" });
                Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 1, Descripcion =  "Inicial" });
            }
            else
            {

            }
        }

        private Entidades.Menu_Odontograma seleccionado;

        public Entidades.Menu_Odontograma Seleccionado
        {
            get { return seleccionado; }
            set 
            { 
                seleccionado = value;
                navergarSeleccionado(value);
            }
        }

        private void navergarSeleccionado(Entidades.Menu_Odontograma value)
        {
            if (value.Codigo == 0)
            {
                // Indicarle a los formularios que deben estar en estado  odontograma inicial
                Variables_Globales.IdTratamientoActivo = 2;
                Variables_Globales.Tipo_Odontograma_Activo = Tipo_Odontograma.Inicial;
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Pedir_Pintar_Datos() { nuevoOdontograma = true });
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new cargar_Diagnosticos_Procedimientos() { tipo = Tipo.todos });
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Activar_Elementos() { valor = "Nuevo" });
                GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cambiar_Tipo_Odontograma() { Tipo_Odontograma = Tipo_Odontograma.Inicial });
            }
            else if(value.Codigo == 1)
            {
                var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
                vm.odontogramaInicial();
            }
            else if (value.Codigo == 2)
            {
                var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
                vm.odontogramaPlanTratamiento();
            }
            else if (value.Codigo == 3)
            {
                var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Contenedor.vm>();
                vm.odontogramaEvolucion();
            }

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<string>("","Mover a mapa dental");
        }
        

        private Entities.Odontologia.Odontograma.Odontograma listadoOdontogramas;

        public Entities.Odontologia.Odontograma.Odontograma ListadoOdontogramas
        {
            get 
            { 
                return listadoOdontogramas; 
            }
            set 
            { 
                listadoOdontogramas = value;
                obtenerValores(value);
            }
        }

        private ObservableCollection<Hefesoft.Odontograma.Elastic.Menus.Entidades.Menu_Odontograma> lst = new ObservableCollection<Entidades.Menu_Odontograma>();

        public ObservableCollection<Hefesoft.Odontograma.Elastic.Menus.Entidades.Menu_Odontograma> Lst
        {
            get { return lst; }
            set { lst = value; RaisePropertyChanged("Lst"); }
        }
        

        private void obtenerValores(Entities.Odontologia.Odontograma.Odontograma value)
        {
            if (!string.IsNullOrEmpty(value.urlPreviewOdontograma))
            {
                UrlImagen = value.urlPreviewOdontograma;
                verTitulosUrlImagen = true;
                RaisePropertyChanged("UrlImagen");
                RaisePropertyChanged("verTitulosUrlImagen");
            }

            Lst.Clear();
            Lst = new ObservableCollection<Entidades.Menu_Odontograma>();
            Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 0, Descripcion = "Nuevo" });
            
            if(value.odontogramaInicial.Any())
            {
                Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 1, Descripcion = "Inicial" });
            }

            if (value.odontogramaPlanTratamiento.Any())
            {
                Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 2, Descripcion = "Plan de tratamiento" });
            }

            if (value.odontogramaPlanTratamiento.Any())
            {
                Lst.Add(new Entidades.Menu_Odontograma() { Codigo = 3, Descripcion = "Evolucion" });
            }

            RaisePropertyChanged("Lst");
        }

        public string UrlImagen { get; set; }

        public bool verTitulosUrlImagen { get; set; }
    }
}
