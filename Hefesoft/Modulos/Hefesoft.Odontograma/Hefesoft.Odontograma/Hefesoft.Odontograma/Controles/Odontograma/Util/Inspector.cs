using Hefesoft.Portable.xml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;

namespace App2.Util
{
    class Inspector : IClientMessageInspector 
    {
     
        Message TraceMessage(Message reply)
        {
            MessageBuffer buffer = reply.CreateBufferedCopy(int.MaxValue);
            Message newMsg = buffer.CreateMessage();
            var result = xml(newMsg);            
            return buffer.CreateMessage();
        }
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            reply = TraceMessage(reply);
        }

        public string xml(Message message)
        {
            MemoryStream ms = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(ms);
            message.WriteMessage(writer);
            writer.Flush();
            ms.Position = 0;

            var myStr = ms.streamToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(myStr);

            var b = xmlDoc.GetElementsByTagName("s:Body");
            var c = b.First().LastChild;
            var d = xmlDoc.GetElementsByTagName(c.NodeName);

            foreach (var item in d)
            {
                if (item.NodeValue != null)
                {
                    var e = item.NodeValue.ToString();
                }
            }

            string result = "";
            try
            {   
                XElement xdoc = XElement.Parse(myStr);
                List<string> listNodes = new List<string>() { "s:Body", "Body" };
                dynamic xmlContent = new ExpandoObject();
                ExpandoObjectHelperWithoutNamespace.Parse(xmlContent, xdoc, listNodes);

                foreach (var item in xmlContent.Envelope.Body)
                {
                    
                }
            }
            catch { }
            return result;
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            //request = TraceMessage(request);
            return null;
        }


        private static string streamToString(MemoryStream ms)
        {
            string myStr = "";
            using (MemoryStream stream = new MemoryStream())
            {
                ms.Position = 0;
                var sr = new StreamReader(ms);
                myStr = sr.ReadToEnd();
            }
            return myStr;
        }
    }

    public class MyBehavior : IEndpointBehavior
    {

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
          
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Inspector inspector = new Inspector();
            clientRuntime.ClientMessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            
        }       
    }
}
