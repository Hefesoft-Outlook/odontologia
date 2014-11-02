using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Azure.Helpers
{
    public static class AzureStorageConstants
    {
        public static string Account = "hefesoft";
        public static string SharedKeyAuthorizationScheme = "SharedKey";
        public static string BlobEndPoint = "https://hefesoft.blob.core.windows.net/";
        public static string Key = "dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
        public static string ContainerName = "imagenes/";
        public static string FileLocation = BlobEndPoint + ContainerName;
    }

}
