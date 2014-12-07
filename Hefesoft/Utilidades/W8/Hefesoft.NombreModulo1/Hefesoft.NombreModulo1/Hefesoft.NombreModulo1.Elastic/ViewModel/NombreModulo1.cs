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

namespace Hefesoft.NombreModulo1.Elastic.ViewModel
{
    public class NombreModulo1 : ViewModelBase
    {
        public NombreModulo1()
        {
            Listado = new ObservableCollection<Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1>();
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
            Listado.Add(new Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1()
            {
                Codigo = 1,
                Descripcion = "Test"
            });
        }

        private async void listar()
        {
            BusyBox.UserControlCargando(true);
            var query = new Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1()
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
        private Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1 seleccionado = new Entidades.NombreModulo1() { nombreTabla = "PruebaElastic" };

        public Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1 Seleccionado
        {
            get { return seleccionado; }
            set { seleccionado = value; RaisePropertyChanged("Seleccionado"); }
        }


        public RelayCommand insertCommand { get; set; }

        public ObservableCollection<Hefesoft.NombreModulo1.Elastic.Entidades.NombreModulo1> Listado { get; set; }
    }
}
