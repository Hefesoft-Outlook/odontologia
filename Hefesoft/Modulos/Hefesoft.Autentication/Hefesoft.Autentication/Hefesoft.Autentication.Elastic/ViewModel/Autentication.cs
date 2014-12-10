using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.BusyBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Standard.Util.Collection.Observables;
using Hefesoft.Standard.Util.Collection.IEnumerable;
using System.Collections.ObjectModel;

namespace Hefesoft.Autentication.Elastic.ViewModel
{
    public class Autentication : ViewModelBase
    {
        public Autentication()
        {
            Listado = new ObservableCollection<Hefesoft.Autentication.Elastic.Entidades.Autentication>();
            if (IsInDesignMode)
            {
                datosDisenio();
            }
            else
            {
                data = new Data.Crud();
                listar();
                insertCommand = new RelayCommand(insert);
            }
        }

        private void datosDisenio()
        {
            Listado.Add(new Hefesoft.Autentication.Elastic.Entidades.Autentication()
            {
                Codigo = 1,
                Descripcion = "Test"
            });
        }

        private async void listar()
        {
            BusyBox.UserControlCargando(true);
            var query = new Hefesoft.Autentication.Elastic.Entidades.Autentication()
            {
                nombreTabla = "PruebaElastic"
            };

            var result = await data.getAllTableStorage(query);
            Listado = result.ToObservableCollection();
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private async void insert()
        {
            BusyBox.UserControlCargando(true);
            //Este es el identificador en table storage
            Seleccionado.RowKey = new Random().Next().ToString();
            await data.insert(Seleccionado);
            Listado.Add(Seleccionado);
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }


        private Data.Crud data;
        private Hefesoft.Autentication.Elastic.Entidades.Autentication seleccionado = new Entidades.Autentication() { nombreTabla = "PruebaElastic" };

        public Hefesoft.Autentication.Elastic.Entidades.Autentication Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; RaisePropertyChanged("Seleccionado"); }
        }


        public RelayCommand insertCommand { get; set; }

        public ObservableCollection<Hefesoft.Autentication.Elastic.Entidades.Autentication> Listado { get; set; }
    }
}
