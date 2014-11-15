using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hefesoft.Pdf.Endpoint.Controllers
{
    public class ValuesController : ApiController
    {
        Util.Pdf pdf;

        public ValuesController()
        {
            pdf = new Util.Pdf();
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            var entidad = new Hefesoft.Pdf.Entities.Test.test();
            return pdf.generarPdf(entidad.document);
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
