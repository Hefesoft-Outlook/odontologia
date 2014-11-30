using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hefesoft.Management.Autentication
{
    internal class AutenticationHelper
    {
        internal static SubscriptionCloudCredentials GetCredentials(string subscriptionId)
        {   
            return new CertificateCloudCredentials(subscriptionId);
        }
      
    }
}
