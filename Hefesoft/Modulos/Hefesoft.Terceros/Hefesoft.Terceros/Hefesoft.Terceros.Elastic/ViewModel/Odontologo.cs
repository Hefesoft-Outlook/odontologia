using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.Util.Collection.Observables;
using System.Collections.ObjectModel;
using Hefesoft.Standard.BusyBox;
using Hefesoft.Terceros.Elastic.Entidades;
using Hefesoft.Standard.Util.table;
using Hefesoft.Standard.Util.Blob;

namespace Hefesoft.Terceros.Elastic.ViewModel
{
    public class Odontologo : ViewModelBase
    {
        public Odontologo()
        {
            Listado = new ObservableCollection<TerceroEntity>();

            if(IsInDesignMode)
            {
                Listado.Add(new TerceroEntity() 
                {
                    NombreCompleto = "odontologo"
                });
            }
            else
            {
                load();
                insertCommand = new RelayCommand(insert);
                newCommand = new RelayCommand(nuevo);
                deleteCommand = new RelayCommand<TerceroEntity>(delete);
            }
        }

        private async void delete(TerceroEntity elemento)
        {
            BusyBox.UserControlCargando(true);
            if (elemento != null)
            {
                elemento.Activo = false;
                await elemento.postBlob();
                Listado.Remove(elemento);
            }
            BusyBox.UserControlCargando(false);
        }

        private void nuevo()
        {
            Seleccionado = null;
            Seleccionado = new TerceroEntity();
        }

        private async void insert()
        {
            BusyBox.UserControlCargando(true);
            if (!string.IsNullOrEmpty(Seleccionado.NombreCompleto))
            {
                if (string.IsNullOrEmpty(Seleccionado.RowKey))
                {
                    Seleccionado.Activo = true;
                    Seleccionado.nombreTabla = "odontologo";
                    Seleccionado.PartitionKey = "cnt.panacea.entities.parametrizacion.terceroentity";
                    Seleccionado.RowKey = new Random().Next().ToString();
                    await Seleccionado.postBlob();
                    Listado.Add(Seleccionado);
                }
                else
                {
                    await Seleccionado.postBlob();
                    Listado.UpdateElementCollection(Seleccionado);
                }
            }
            BusyBox.UserControlCargando(false);
        }

        private async void load()
        {
            BusyBox.UserControlCargando(true);
            List<TerceroEntity> result = await CrudBlob.getBlobByPartition(new TerceroEntity(), 
                "odontologo", "cnt.panacea.entities.parametrizacion.terceroentity");
            Listado = result.Where(a=> (a.Activo == null || a.Activo == true)).ToObservableCollection();
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private TerceroEntity seleccionado = new TerceroEntity();

        public TerceroEntity Seleccionado
        {
            get { return seleccionado; }
            set 
            { 
                seleccionado = value;
                RaisePropertyChanged("Seleccionado");
            }
        }

        public ObservableCollection<TerceroEntity> Listado { get; set; }

        public RelayCommand insertCommand { get; set; }

        public RelayCommand newCommand { get; set; }

        public RelayCommand<TerceroEntity> deleteCommand { get; set; }
    }
}
