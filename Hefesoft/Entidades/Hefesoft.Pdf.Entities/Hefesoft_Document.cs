using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Pdf.Entities
{
    public class Hefesoft_Document : IHefesoftDocumenType
    {        
        public bool scale { get; set; }

        public string Path { get; set; }

        public string Text { get; set; }

        public TypeElement TypeElement { get; set; }
    }
}
