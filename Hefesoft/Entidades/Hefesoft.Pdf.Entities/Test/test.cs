using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Pdf.Entities.Test
{
    public class test
    {
        public Document document { get; set; }

        public test()
        {
            document = new Document() { pdfName = "planTratamiento.pdf" };
            document.lst.Add(0, new Image("https://hefesoft.blob.core.windows.net/imagenes/0.png") { scale = true });
        }
    }
}
