﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hefesoft.Entities.Odontologia.Util;
using GalaSoft.MvvmLight.Command;
using Hefesoft.Standard.BusyBox;

namespace Hefesoft.Entities.Odontologia.ViewModel.Niveles_Severidad
{
    public class Niveles_Severidad : ViewModelBase
    {
        public Niveles_Severidad()
        {   
            Listado = new System.Collections.ObjectModel.ObservableCollection<Diagnostico.NivelSeveridadDXEntity>();

            if(IsInDesignMode)
            {
                Listado.Add(new Diagnostico.NivelSeveridadDXEntity() 
                {
                     Descripcion = "Nombre nivel"
                });
            }
            else
            {
                load();
                insertCommand = new RelayCommand(insert);
                newCommand = new RelayCommand(nuevo);
                deleteCommand = new RelayCommand<Diagnostico.NivelSeveridadDXEntity>(delete);
            }
        }

        private async void delete(Diagnostico.NivelSeveridadDXEntity elemento)
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
            Seleccionado = new Diagnostico.NivelSeveridadDXEntity();
        }

        private async void insert()
        {
            BusyBox.UserControlCargando(true);
            if (!string.IsNullOrEmpty(Seleccionado.Descripcion))
            {
                if (string.IsNullOrEmpty(Seleccionado.RowKey))
                {
                    Seleccionado.Activo = true;
                    Seleccionado.nombreTabla = "nivelseveridaddxentity";
                    Seleccionado.PartitionKey = "cnt.panacea.entities.parametrizacion.nivelseveridaddxentity";
                    Seleccionado.RowKey = new Random().Next().ToString();
                    await Seleccionado.postBlob();
                    Listado.Add(Seleccionado);
                }
                else
                {
                    var item = Listado.FirstOrDefault(a => a == Seleccionado);
                    var index = Listado.IndexOf(item);
                    await Seleccionado.postBlob();
                    Listado.Remove(item);
                    Listado.Insert(index, item);
                }
            }
            
            BusyBox.UserControlCargando(false);
        }

        private async void load()
        {
            BusyBox.UserControlCargando(true);
            List<Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity> blob = await new Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity().getBlobByPartition(
                "nivelseveridaddxentity", "cnt.panacea.entities.parametrizacion.nivelseveridaddxentity");
            Listado = blob.Where(a => (a.Activo == null || a.Activo == true)).ToObservableCollection();
            RaisePropertyChanged("Listado");
            BusyBox.UserControlCargando(false);
        }

        private Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity seleccionado = new Diagnostico.NivelSeveridadDXEntity();

        public Hefesoft.Entities.Odontologia.Diagnostico.NivelSeveridadDXEntity Seleccionado
        {
            get { return seleccionado; }
            set 
            { 
                seleccionado = value;
                RaisePropertyChanged("Seleccionado");
            }
        }        

        public System.Collections.ObjectModel.ObservableCollection<Diagnostico.NivelSeveridadDXEntity> Listado { get; set; }

        public RelayCommand insertCommand { get; set; }

        public RelayCommand newCommand { get; set; }

        public RelayCommand<Diagnostico.NivelSeveridadDXEntity> deleteCommand { get; set; }
    }
}
