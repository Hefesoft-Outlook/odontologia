using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace testJsonDynamic.storage
{
    public class azureStorage
    {
        public azureStorage()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            storageAccount = CloudStorageAccount.Parse(connectionString);

            //var generator = new SnowMaker.UniqueIdGenerator(storageAccount);
            //var orderNumber = generator.NextId("orderNumbers");
        }

        public dynamic delete(string nombreTabla, string partitionKey, string rowKey)
        {
            try
            {
                var client = storageAccount.CreateCloudTableClient();
                var table = client.GetTableReference(nombreTabla);


                TableOperation retrieveOperation = TableOperation.Retrieve(partitionKey, rowKey);
                TableResult retrievedResult = table.Execute(retrieveOperation);
                var deleteEntity = (DynamicObjectTableEntity)retrievedResult.Result;

                if (deleteEntity != null)
                {
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                    // Execute the operation.
                    table.Execute(deleteOperation);

                    Console.WriteLine("Entity deleted.");
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);                
                return false;
            }
        }

        public dynamic getAllByPartitionKey(string nombreTabla,string partitionKey)
        {
            try
            {
                var client = storageAccount.CreateCloudTableClient();
                var table = client.GetTableReference(nombreTabla);

                TableQuery query = new TableQuery().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                var result = table.ExecuteQuery(query);
                dynamic resultado = result.ToList();

                List<object> lst = new List<object>();

                foreach (var item in resultado)
                {
                    var elemento = new Expando();
                    elemento["PartitionKey"] = item.PartitionKey;
                    elemento["RowKey"] = item.RowKey;

                    foreach (var itemB in item.Properties)
                    {   
                        var propiedad = ConvertToEntityProperty(itemB.Key, itemB.Value);
                        elemento[itemB.Key] = propiedad;                        
                    }

                    lst.Add(elemento.Properties);
                }

                return lst;
                
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public dynamic getASingle(string nombreTabla, string partitionKey, string rowKey)
        {
            try
            {
                var client = storageAccount.CreateCloudTableClient();
                var table = client.GetTableReference(nombreTabla);

                TableOperation retrieveOperation = TableOperation.Retrieve(partitionKey, rowKey);
                TableResult retrievedResult = table.Execute(retrieveOperation);
                dynamic result = retrievedResult.Result;

                var elemento = new Expando();
                elemento["PartitionKey"] = result.PartitionKey;
                elemento["RowKey"] = result.RowKey;

                foreach (var item in result.Properties)
                {   
                    var propiedad =ConvertToEntityProperty(item.Key, item.Value);
                    elemento[item.Key] = propiedad;
                }

                return elemento.Properties;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public object ConvertToEntityProperty(string key, EntityProperty value)
        {           
            switch (value.PropertyType)
            {
                case EdmType.Binary:
                    return value.BinaryValue;
                    
                case EdmType.Boolean:
                    return value.BooleanValue;
                    
                case EdmType.DateTime:
                    return value.DateTimeOffsetValue;
                    
                case EdmType.Double:
                    return value.DoubleValue;
                    
                case EdmType.Guid:
                    return value.GuidValue;
                    
                case EdmType.Int32:
                    return value.Int32Value;
                    
                case EdmType.Int64:
                    return value.Int64Value;
                    
                case EdmType.String:
                    return value.StringValue;
                    
                default:
                    return null;
                    
            } 
        }

        public void insert(dynamic entidad)
        {
            try
            {
                var client = storageAccount.CreateCloudTableClient();
                var table = client.GetTableReference(entidad.nombreTabla);
                table.CreateIfNotExists();

                if (entidad.generarIdentificador)
                {
                    //Generando ids consecutivos
                    entidad.RowKey = Variables_Globales.GetUniqueId().ToString();
                    entidad.generarIdentificador = false;
                }

                dynamic entity = new DynamicObjectTableEntity(entidad.PartitionKey, entidad.RowKey);
                
                ReflectPropertyValue(entidad, ref entity);
                table.Execute(TableOperation.InsertOrReplace(entity));                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Inserta elementos desde esta solucion
        public void insert(DynamicObjectTableEntity entidad, string nombreTabla)
        {
            try
            {
                var client = storageAccount.CreateCloudTableClient();
                var table = client.GetTableReference(nombreTabla);
                table.CreateIfNotExists();
                table.Execute(TableOperation.InsertOrReplace(entidad));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReflectPropertyValue(dynamic source, ref dynamic entidadGuardar)
        {
            var propiedadesElemento = source as DynamicJsonObject;
            var dynamicTableEntity = new DynamicObjectTableEntity();

            if (propiedadesElemento != null)
            {
                foreach (var item in propiedadesElemento.GetDynamicMemberNames())
                {
                    if (!(item == "PartitionKey" || item == "RowKey"))
                    {
                        try
                        {
                            var valor = ConvertToEntityProperty(item, (object)source[item]);

                            if (valor != null)
                            {
                                if (!((DynamicObjectTableEntity)entidadGuardar).properties.Any(a => a.Key == item))
                                {
                                    ((DynamicObjectTableEntity)entidadGuardar).properties.Add(item, valor);

                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }

        }

        public EntityProperty ConvertToEntityProperty(string key, object value)
        {
            if (value == null) return new EntityProperty((string)null);
            if (value.GetType() == typeof(byte[]))
                return new EntityProperty((byte[])value);
            if (value.GetType() == typeof(bool))
                return new EntityProperty((bool)value);
            if (value.GetType() == typeof(DateTimeOffset))
                return new EntityProperty((DateTimeOffset)value);
            if (value.GetType() == typeof(DateTime))
                return new EntityProperty((DateTime)value);
            if (value.GetType() == typeof(double))
                return new EntityProperty((double)value);
            if (value.GetType() == typeof(Guid))
                return new EntityProperty((Guid)value);
            if (value.GetType() == typeof(int))
                return new EntityProperty((int)value);
            if (value.GetType() == typeof(long))
                return new EntityProperty((long)value);
            if (value.GetType() == typeof(string))
                return new EntityProperty((string)value);
            else
            {
                return null;
            }

        }

        public CloudStorageAccount storageAccount { get; set; }
    }
}
