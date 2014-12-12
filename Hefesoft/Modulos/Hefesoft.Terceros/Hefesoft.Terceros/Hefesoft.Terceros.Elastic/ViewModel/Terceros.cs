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

namespace Hefesoft.Terceros.Elastic.ViewModel
{
    public class Terceros : ViewModelBase
    {
        public Terceros()
        {
            Listado = new ObservableCollection<Hefesoft.Terceros.Elastic.Entidades.Terceros>();
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
            Listado.Add(new Hefesoft.Terceros.Elastic.Entidades.Terceros()
            {
                Codigo = 1,
                Descripcion = "Test"
            });
        }

        private async void listar()
        {
            BusyBox.UserControlCargando(true);
            var query = new Hefesoft.Terceros.Elastic.Entidades.Terceros()
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
        private Hefesoft.Terceros.Elastic.Entidades.Terceros seleccionado = new Entidades.Terceros() { nombreTabla = "PruebaElastic" };

        public Hefesoft.Terceros.Elastic.Entidades.Terceros Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; RaisePropertyChanged("Seleccionado"); }
        }


        public RelayCommand insertCommand { get; set; }

        public ObservableCollection<Hefesoft.Terceros.Elastic.Entidades.Terceros> Listado { get; set; }
    }
}
