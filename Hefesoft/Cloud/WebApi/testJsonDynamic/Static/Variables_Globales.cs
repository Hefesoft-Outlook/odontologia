using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using SnowMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class Variables_Globales
{
    public static long GetUniqueId()
    {
        if (_instance != null)
        {
            return _instance.NextId("identificador");
        }

        var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
        var storageAccount = CloudStorageAccount.Parse(connectionString);
        IOptimisticDataStore blobDataStore = new BlobOptimisticDataStore(storageAccount, "Consecutivos");
        _instance = new UniqueIdGenerator(blobDataStore);
        return _instance.NextId("identificador");
    }

    public static UniqueIdGenerator _instance { get; set; }
}

