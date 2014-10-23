using Cnt.Std;
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
                var item = (IEntidadBase)itemB;
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

        public static async void fillTables<T>(this ObservableCollection<T> lst) where T : class
        {
            try
            {
                foreach (var itemB in lst)
                {
                    var item = (IEntidadBase)itemB;
                    dynamic elementoAdicionar = new Hefesoft.Entities.Odontologia.Util.Odontologia();
                    elementoAdicionar.nombreTabla = item.GetType().Name.eliminarCaracteresEspeciales().ToLower();
                    elementoAdicionar.PartitionKey = item.GetType().FullName.eliminarCaracteresEspeciales().ToLower();
                    elementoAdicionar.RowKey = item.Identificador.ToString();
                    elementoAdicionar.Item = item;

                    await CrudBlob.postBlob(elementoAdicionar);
                }
            }
            catch
            {

            }
        }
    }

