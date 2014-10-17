using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Util.Pdf
{
    public class Pdf
    {
        public void generarPdf(Vm.Messenger.Imprimir.Imprimir imprimir) 
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "PDF file format|*.pdf";

            // Save the document...
             if (d.ShowDialog() == true)
            {                
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();                
                
                XGraphics gfx = XGraphics.FromPdfPage(page);

                XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);

                // Create a font
                XFont font = new XFont("Huxtable", 10, XFontStyle.Bold, options);

                string text = "";

                foreach (var item in imprimir.Listado)
                {
                    text += texto(item);                   
                }

                var formatter = new XTextFormatter(gfx);
                var layoutRectangle = new XRect(10, 10, page.Width, page.Height);
                formatter.DrawString(text, font, XBrushes.Black, layoutRectangle);                   

                document.Save(d.OpenFile());
            }        
        }

        private string texto(Entities.Odontologia.OdontogramaEntity item)
        {
            string text = item.Diente.Identificador.ToString().PadRight(5, ' ');

            if (item.Diagnostico != null)
            {
                text += item.Diagnostico.Diagnostico.NombreAlterno.PadRight(5, ' ');
            }

            if (item.Procedimiento != null)
            {
                text += item.Procedimiento.Procedimiento.NombreAlterno.PadRight(5, ' ');
            }

            if (item.Superficie != null)
            {
                text += item.Superficie.ToString();
            }

            text += "\r\n";
            return text;
        }
        

    }
}
