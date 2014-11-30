using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.Storage;
using Microsoft.WindowsAzure.Management.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Azure.Manage.Autentication
{
    public class CreateStorageAccount
    {
        private string _name;        
        private SubscriptionCloudCredentials _credential;
        private StorageManagementClient _storage;
        public CreateStorageAccount(string name, string id, string cert)
        {
            _name = name;            
            _credential = Autentication.CertificateAutenticationHelper.getCredentials(id, cert);
            _storage = CloudContext.Clients.CreateStorageManagementClient(_credential);
        }

        public async Task create()
        {
            try
            {
                var result = await _storage.StorageAccounts.CreateAsync(new StorageAccountCreateParameters
                {
                    AccountType = StorageAccountTypes.StandardGRS,
                    Name = _name,
                    Location = GeoRegionNames.NorthCentralUS
                });


                var status = result.StatusCode;
            }
            catch
            {

            }
        }

        public async Task delete()
        {
            try
            {
                var result = await _storage.StorageAccounts.DeleteAsync(_name);
            }
            catch
            {

            }
        }

    }
}
