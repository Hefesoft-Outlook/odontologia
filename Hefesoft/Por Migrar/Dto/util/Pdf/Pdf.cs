using Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


public static partial class Pdf
{
    public static async Task<string> postPdf(Hefesoft.Pdf.Entities.Document document)
    {
        try
        {   
            string json = JsonConvert.SerializeObject(document);
            var resultadoString = await doPost(json);
            return resultadoString;
        }
        catch
        {
            return "error";
        }
    }

    private static async Task<string> doPost(string json)
    {
        HttpClientHandler handler = new HttpClientHandler();
        var httpClient = new HttpClient(handler);
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrlServicioPdf() + "pdf");
        request.Content = new StringContent(json);
        MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
        contentType.MediaType = "application/json";

        request.Content.Headers.ContentType = contentType;

        if (handler.SupportsTransferEncodingChunked())
        {
            request.Headers.TransferEncodingChunked = true;
        }
        
        HttpResponseMessage response = await httpClient.SendAsync(request);
        var resultadoString = response.Content.ReadAsStringAsync().Result;
        resultadoString = JsonConvert.DeserializeObject<string>(resultadoString);

        return resultadoString;
    }
}

