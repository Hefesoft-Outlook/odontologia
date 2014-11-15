using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Pdf.Entities
{
    public class Paragraph : IHefesoftDocumenType
    {        
        public Paragraph(string text)
        {
            Text = text;
        }

        public bool scale { get; set; }

        public string Path { get; set; }

        public string Text { get; set; }

        public TypeElement TypeElement { get; set; }
    }
}
