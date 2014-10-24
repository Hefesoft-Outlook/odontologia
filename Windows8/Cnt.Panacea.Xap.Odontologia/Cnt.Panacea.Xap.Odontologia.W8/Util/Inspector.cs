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

            string myStr = "";
            using (MemoryStream stream = new MemoryStream())
            {
                ms.Position = 0;
                var sr = new StreamReader(ms);
                myStr = sr.ReadToEnd();
            }

            try
            {
                XmlSerializer serializer1 = new XmlSerializer(typeof(object));
                object deserializedStudents1 = serializer1.Deserialize(ms) as object;
            }
            catch { }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(myStr);

            

            string result = "";
            try
            {                   
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(myStr);

                string xpath = "ListarParametrosConvenioResponse";
                var nodes = xmlDoc.SelectNodes(xpath);


                XElement xdoc = XElement.Parse(myStr);
                List<string> listNodes = new List<string>() { "owners" };
                dynamic xmlContent = new ExpandoObject();
                ExpandoObjectHelper.Parse(xmlContent, xdoc, listNodes);

                var a = xmlContent["{http://www.w3.org/2003/05/soap-envelope}Envelope"];

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

   
    public class ExpandoObjectHelper
    {
        private static List<string> KnownLists;
        public static void Parse(dynamic parent, XElement node, List<string> knownLists = null)
        {
            if (knownLists != null)
            {
                KnownLists = knownLists;
            }
            IEnumerable<XElement> sorted = from XElement elt in node.Elements() orderby node.Elements(elt.Name.LocalName).Count() descending select elt;

            if (node.HasElements)
            {
                int nodeCount = node.Elements(sorted.First().Name.LocalName).Count();
                bool foundNode = false;
                if (KnownLists != null && KnownLists.Count > 0)
                {
                    foundNode = (from XElement el in node.Elements() where KnownLists.Contains(el.Name.LocalName) select el).Count() > 0;
                }

                if (nodeCount > 1 || foundNode == true)
                {
                    // At least one of the child elements is a list
                    var item = new ExpandoObject();
                    List<dynamic> list = null;
                    string elementName = string.Empty;
                    foreach (var element in sorted)
                    {
                        if (element.Name.LocalName != elementName)
                        {
                            list = new List<dynamic>();
                            elementName = element.Name.LocalName;
                        }

                        if (element.HasElements ||
                            (KnownLists != null && KnownLists.Contains(element.Name.LocalName)))
                        {
                            Parse(list, element);
                            AddProperty(item, element.Name.LocalName, list);
                        }
                        else
                        {
                            Parse(item, element);
                        }
                    }

                    foreach (var attribute in node.Attributes())
                    {
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }

                    AddProperty(parent, node.Name.ToString(), item);
                }
                else
                {
                    var item = new ExpandoObject();

                    foreach (var attribute in node.Attributes())
                    {
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }

                    //element
                    foreach (var element in sorted)
                    {
                        Parse(item, element);
                    }
                    AddProperty(parent, node.Name.ToString(), item);
                }
            }
            else
            {
                AddProperty(parent, node.Name.ToString(), node.Value.Trim());
            }
        }

        private static void AddProperty(dynamic parent, string name, object value)
        {
            if (parent is List<dynamic>)
            {
                (parent as List<dynamic>).Add(value);
            }
            else
            {
                (parent as IDictionary<String, object>)[name] = value;
            }
        }
    }
}
