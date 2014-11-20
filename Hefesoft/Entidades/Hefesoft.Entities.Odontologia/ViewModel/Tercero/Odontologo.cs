using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.Util.Collection.Observables;
using System.Collections.ObjectModel;

namespace Hefesoft.Entities.Odontologia.ViewModel.Tercero
{
    public class Odontologo : ViewModelBase
    {
        public Odontologo()
        {
            Listado = new ObservableCollection<Convenio.TerceroEntity>();

            if(IsInDesignMode)
            {
                Listado.Add(new Convenio.TerceroEntity() 
                {
                    NombreCompleto = "odontologo"
                });
            }
            else
            {
                load();
                insertCommand = new RelayCommand(insert);
                newCommand = new RelayCommand(nuevo);
            }
        }

        private void nuevo()
        {
            Seleccionado = null;
            Seleccionado = new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity();
        }

        private async void insert()
        {
            if (!string.IsNullOrEmpty(Seleccionado.NombreCompleto))
            {
                if (string.IsNullOrEmpty(Seleccionado.RowKey))
                {
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
        }

        private async void load()
        {
            List<Hefesoft.Entities.Odontologia.Convenio.TerceroEntity> result = await CrudBlob.getBlobByPartition(new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity(), 
                "odontologo", "cnt.panacea.entities.parametrizacion.terceroentity");
            Listado = result.ToObservableCollection();
            RaisePropertyChanged("Listado");
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
    }
}
