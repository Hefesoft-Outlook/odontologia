using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace Hefesoft.Pdf.Endpoint.Util
{
    public class Pdf
    {
        private CloudTable table;
        private CloudBlobContainer container;
        public CloudStorageAccount storageAccount { get; set; }
        public Pdf()
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=hefesoft;AccountKey=dodn17DT7hBi3lXrWlvXihLS9J7xuItHLIpWLBZn2QEMdBHm02Lqxr055rNCpP5z3FhfcjjX3MhPy1Npk3VF3Q==";
            storageAccount = CloudStorageAccount.Parse(connectionString);
            blobClient = storageAccount.CreateCloudBlobClient();

            //El nombre de la tabla debe estar en minuscula
            container = blobClient.GetContainerReference("pdf");
            container.CreateIfNotExists();
        }

        public string generarPdf(Hefesoft.Pdf.Entities.Document documentSource)
        {
            if (valido(documentSource))
            {
                string result = "";
                using (MemoryStream ms = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    var margenHorizontal = 50;
                    var margenVertical = 55;

                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    foreach (var item in documentSource.lst.OrderBy(a => a.Key))
                    {
                        var serializer = new JavaScriptSerializer();
                        var elemento = serializer.Deserialize<Hefesoft.Pdf.Entities.Hefesoft_Document>(item.Value.ToString());
                        
                        if (elemento.TypeElement == Entities.TypeElement.Image)
                        {
                            Image jpg = Image.GetInstance(new Uri(elemento.Path));
                            jpg.ScalePercent(24f);

                            if (elemento.scale)
                            {
                                jpg.ScaleToFit(document.PageSize.Width - margenHorizontal, document.PageSize.Height - margenVertical);
                            }
                            document.Add(jpg);
                        }
                        else if (elemento.TypeElement == Entities.TypeElement.Paragraph)
                        {
                            document.Add(new Paragraph(elemento.Text));
                        }
                    }

                    document.Close();
                    writer.Close();


                    try
                    {
                        CloudBlockBlob blockBlob = container.GetBlockBlobReference(documentSource.pdfName);
                        blockBlob.UploadFromByteArray(ms.GetBuffer(), 0, ms.GetBuffer().Length);

                        result = blockBlob.Uri.AbsoluteUri;
                    }
                    catch
                    {

                    }
                }

                return result;
            }
            else
            {
                return msgError;
            }
        }

        private bool valido(Entities.Document documentSource)
        {
            var valido = true;
            msgError = "";

            if(documentSource.pdfName == null)
            {
                valido = false;
                msgError += "Nombre vacio";
            }
            if (!documentSource.lst.Any())
            {
                valido = false;
                msgError += "sin elementos";
            }
            return valido;
        }

        public CloudBlobClient blobClient { get; set; }

        public string msgError { get; set; }
    }
}
