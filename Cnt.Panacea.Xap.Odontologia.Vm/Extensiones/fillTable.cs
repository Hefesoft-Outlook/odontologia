using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;



    public static class fillTable
    {
        public static async void fillEntity<T>(this T itemB) where T : class
        {
            try
            {
                var item = (Cnt.Std.IEntidadBase)itemB;
                dynamic elementoAdicionar = new Hefesoft.Entities.Odontologia.Util.Odontologia();
                elementoAdicionar.nombreTabla = item.GetType().Name.eliminarCaracteresEspeciales().ToLower();
                elementoAdicionar.PartitionKey = item.GetType().FullName.eliminarCaracteresEspeciales().ToLower();
                elementoAdicionar.RowKey = item.Identificador.ToString();
                elementoAdicionar.Item = item;

                await CrudBlob.postBlob(elementoAdicionar);
            }
            catch
            { 
            }
        }

        public static async void fillTables<T,P>(this ObservableCollection<T> lst, P otherClas)             
            where T : class 
            where P : Dto.IEntidadBase
        {
            try
            {
                foreach (var itemB in lst)
                {
                    var item = (Cnt.Std.IEntidadBase)itemB;
                    Mapper.CreateMap<T, P>();
                    P ElementoInsertar = Mapper.Map<P>(item);
                    ElementoInsertar.nombreTabla = item.GetType().Name.eliminarCaracteresEspeciales().ToLower();
                    ElementoInsertar.PartitionKey = item.GetType().FullName.eliminarCaracteresEspeciales().ToLower();
                    ElementoInsertar.RowKey = item.Identificador.ToString();
                    

                    await CrudBlob.postBlob(ElementoInsertar);
                }
            }
            catch
            {

            }
        }
    }

