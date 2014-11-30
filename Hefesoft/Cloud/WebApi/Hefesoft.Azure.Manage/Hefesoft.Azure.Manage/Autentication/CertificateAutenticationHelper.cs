using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Hefesoft.Azure.Manage.Autentication
{
    internal class CertificateAutenticationHelper
    {
        internal static SubscriptionCloudCredentials getCredentials(string subscriptionId, string base64EncodedCert)
        {
           return new CertificateCloudCredentials(subscriptionId, new X509Certificate2(Convert.FromBase64String(base64EncodedCert)));
        }
    }
}
