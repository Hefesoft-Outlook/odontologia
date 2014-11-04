using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Hefesoft.Azure.Helpers
{
    public class Azure_Helper
    {

        public static async Task<string> PutBlob_async(String containerName, String blobName, Byte[] blobContent)
        {
            String requestMethod = "PUT";

            //String content = "The Name of This Band is Talking Heads";
            //UTF8Encoding utf8Encoding = new UTF8Encoding();
            //Byte[] blobContent = utf8Encoding.GetBytes(content);
            Int32 blobLength = blobContent.Length;

            const String blobType = "BlockBlob";

            String urlPath = String.Format("{0}/{1}", containerName, blobName);
            String msVersion = "2009-09-19";
            String dateInRfc1123Format = DateTime.UtcNow.ToString("R", CultureInfo.InvariantCulture);

            String canonicalizedHeaders = String.Format("x-ms-blob-type:{0}\nx-ms-date:{1}\nx-ms-version:{2}", blobType, dateInRfc1123Format, msVersion);
            String canonicalizedResource = String.Format("/{0}/{1}", AzureStorageConstants.Account, urlPath);
            String stringToSign = String.Format("{0}\n\n\n{1}\n\n\n\n\n\n\n\n\n{2}\n{3}", requestMethod, blobLength, canonicalizedHeaders, canonicalizedResource);
         
            String authorizationHeader = CreateAuthorizationHeader(stringToSign);
         

            string uri = AzureStorageConstants.BlobEndPoint + urlPath;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-ms-blob-type", blobType);
            client.DefaultRequestHeaders.Add("x-ms-date", dateInRfc1123Format);
            client.DefaultRequestHeaders.Add("x-ms-version", msVersion);
            
            client.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
            
            //logRequest(requestContent, uri);

            
            HttpContent requestContent = new ByteArrayContent(blobContent);
            HttpResponseMessage response = await client.PutAsync(uri, requestContent);
            
            //if (response.IsSuccessStatusCode == true)
            //{
            //    foreach (var aHeader in response.Headers)
            //    {
            //        if (aHeader.Key == "ETag") return aHeader.Value.ElementAt(0);
            //    }
            //}


            var result = uri;
            return result;
        }

        private static String CreateAuthorizationHeader(String canonicalizedString)
        {
            if (String.IsNullOrEmpty(canonicalizedString))
            {
                throw new ArgumentNullException("canonicalizedString");
            }

            String signature = CreateHmacSignature(canonicalizedString, Convert.FromBase64String(AzureStorageConstants.Key));
            String authorizationHeader = String.Format(CultureInfo.InvariantCulture, "{0} {1}:{2}", AzureStorageConstants.SharedKeyAuthorizationScheme, AzureStorageConstants.Account, signature);

            return authorizationHeader;
        }

        private static String CreateHmacSignature(String unsignedString, Byte[] key)
        {
            if (String.IsNullOrEmpty(unsignedString))
            {
                throw new ArgumentNullException("unsignedString");
            }

            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            Byte[] dataToHmac = System.Text.Encoding.UTF8.GetBytes(unsignedString);
            using (HMACSHA256 hmacSha256 = new HMACSHA256(key))
            {
                return Convert.ToBase64String(hmacSha256.ComputeHash(dataToHmac));
            }
        }
    }
}
