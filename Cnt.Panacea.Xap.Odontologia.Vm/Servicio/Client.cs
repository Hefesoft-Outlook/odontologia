using Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Servicio
{
    internal static class ServiceClass
    {
        internal static Object GetWCFSvc(string siteUrl)
        {
            Uri serviceUri = new Uri(siteUrl, UriKind.Absolute);
            EndpointAddress endpointAddress = new EndpointAddress(serviceUri);

            //Create the binding here
            Binding binding = new MyCustomBinding();

            OdontologiaServicioClient client = new OdontologiaServicioClient(binding, endpointAddress);
            return client;
        }


    }

    internal static class BindingFactory
    {
        internal static Binding CreateInstance()
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            return binding;
        }

    }

    public class MyCustomBinding : Binding
    {
        private HttpTransportBindingElement transport;
        private BinaryMessageEncodingBindingElement encoding;

        public MyCustomBinding()
            : base()
        {
            this.InitializeValue();
        }
        public override BindingElementCollection CreateBindingElements()
        {
            BindingElementCollection elements = new BindingElementCollection();
            elements.Add(this.encoding);
            elements.Add(this.transport);
            return elements;
        }
        public override string Scheme
        {
            get { return this.transport.Scheme; }
        }
        private void InitializeValue()
        {  
            this.transport = new HttpTransportBindingElement();
            this.encoding = new BinaryMessageEncodingBindingElement();
        }
    }
}
