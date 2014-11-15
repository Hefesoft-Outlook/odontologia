using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hefesoft.Pdf.Endpoint.Controllers
{
    public class PdfController : ApiController
    {
        Util.Pdf pdf;

        public PdfController()
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
            return "";
        }

        // POST api/values
        public string Post(Hefesoft.Pdf.Entities.Document document)
        {
            try
            {
                return pdf.generarPdf(document);
            }
            catch (Exception ex)
            {
                return string.Format("error {0}", ex.Message);
            }
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
