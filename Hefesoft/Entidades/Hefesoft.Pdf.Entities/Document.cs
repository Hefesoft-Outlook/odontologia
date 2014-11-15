using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Pdf.Entities
{
    public class Document
    {
        public Dictionary<int, object> lst { get; set; }

        public Document()
        {
            lst = new Dictionary<int, object>();
        }

        public string pdfName { get; set; }
    }
}
