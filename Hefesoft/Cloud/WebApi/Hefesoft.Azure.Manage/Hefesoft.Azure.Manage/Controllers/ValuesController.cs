using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Hefesoft.Azure.Manage.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            Autentication.CreateStorageAccount store = new Autentication.CreateStorageAccount(
                "hefesofttest",
                "75e1f58d-6858-46e5-b080-19acb0070f47",
                "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");
            await store.delete();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
