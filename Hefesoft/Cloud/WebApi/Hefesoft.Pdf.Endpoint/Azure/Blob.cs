using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hefesoft.Pdf.Endpoint.Azure
{
    public class Blob
    {
        private CloudTable table;
        private CloudBlobContainer container;
        public CloudStorageAccount storageAccount { get; set; }
        

        public Blob()
        {        
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        private void inicializarContenedor(dynamic entidad)
        {
            var blobClient = storageAccount.CreateCloudBlobClient();

            //El nombre de la tabla debe estar en minuscula
            this.container = blobClient.GetContainerReference(entidad.nombreTabla);
            this.container.CreateIfNotExists();
        }

    }
}