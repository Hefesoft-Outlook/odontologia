using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.Util.Collection.Observables;
using System.Collections.ObjectModel;
using Hefesoft.Standard.BusyBox;
using Hefesoft.Standard.Util.Blob;
using Hefesoft.Standard.Util.table;


namespace Hefesoft.Entities.Odontologia.ViewModel.Tercero
{
    public class Higienista : ViewModelBase
    {
        public Higienista()
        {
            Listado = new ObservableCollection<Convenio.TerceroEntity>();

            if(IsInDesignMode)
            {
                Listado.Add(new Convenio.TerceroEntity() 
                {
                    NombreCompleto = "higienista"
                });
            }
            else
            {
                load();
                insertCommand = new RelayCommand(insert);
                newCommand = new RelayCommand(nuevo);
                deleteCommand = new RelayCommand<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity>(delete);
            }
        }

        private async void delete(Hefesoft.Entities.Odontologia.Convenio.TerceroEntity elemento)
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
            Seleccionado = new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity();
        }

        private async void insert()
        {
            BusyBox.UserControlCargando(true);
            if (!string.IsNullOrEmpty(Seleccionado.NombreCompleto))
            {
                if (string.IsNullOrEmpty(Seleccionado.RowKey))
                {
                    Seleccionado.nombreTabla = "higienista";
                    Seleccionado.Activo = true;
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
            List<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity(), 
                "higienista", "cnt.panacea.entities.parametrizacion.terceroentity");
            Listado = result.Where(a=> (a.Activo == null || a.Activo == true)).ToObservableCollection();
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private Hefesoft.Entities.Odontologia.Convenio.TerceroEntity seleccionado = new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity();

        public Hefesoft.Entities.Odontologia.Convenio.TerceroEntity Seleccionado
        {
            get { return seleccionado; }
            set 
            { 
                seleccionado = value;
                RaisePropertyChanged("Seleccionado");
            }
        }

        public ObservableCollection<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> Listado { get; set; }

        public RelayCommand insertCommand { get; set; }

        public RelayCommand newCommand { get; set; }

        public RelayCommand<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> deleteCommand { get; set; }
    }
}
