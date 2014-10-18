using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace App2.Util
{
    public class Inicializar_Servicio
    {
        internal CustomBinding CreateCustomBinding()
        {
            TimeSpan oneMinute = new TimeSpan(0, 1, 0);
            TimeSpan tenMinutes = new TimeSpan(0, 10, 0);


            BinaryMessageEncodingBindingElement binaryEncoding = new BinaryMessageEncodingBindingElement();
            binaryEncoding.ReaderQuotas.MaxDepth = 32;
            binaryEncoding.ReaderQuotas.MaxStringContentLength = 8192;
            binaryEncoding.ReaderQuotas.MaxArrayLength = 10487560;
            binaryEncoding.ReaderQuotas.MaxBytesPerRead = 4096;
            binaryEncoding.ReaderQuotas.MaxNameTableCharCount = 16384;

            TcpTransportBindingElement tcpTransport = new TcpTransportBindingElement();
            tcpTransport.MaxReceivedMessageSize = 2147483646;
            tcpTransport.MaxBufferSize = 10487560;         

            BindingElementCollection elements = new BindingElementCollection();
            elements.Add(binaryEncoding);
            elements.Add(tcpTransport);

            CustomBinding binding = new CustomBinding(elements)
            {
                Name = "TcpUserNameBinding",
                CloseTimeout = oneMinute,
                OpenTimeout = oneMinute,
                ReceiveTimeout = tenMinutes,
                SendTimeout = oneMinute,
            };

            return binding;
        }

        internal Binding obtenerbinding()
        {
            BinaryMessageEncodingBindingElement encodingElement = new BinaryMessageEncodingBindingElement();

            XmlDictionaryReaderQuotas quotas= XmlDictionaryReaderQuotas.Max;       


            CustomBinding binding = new CustomBinding(new NetTcpBinding()
            {
                
                //TransferMode = System.ServiceModel.TransferMode.Buffered,
                //ReaderQuotas = quotas,
                //MaxBufferPoolSize = long.MaxValue,            
                //MaxBufferSize = 2147483647,
                //MaxReceivedMessageSize = 2147483647,
                //CloseTimeout = new TimeSpan(0,0,60,0,0),
                //ReceiveTimeout = new TimeSpan(0, 0, 60, 0, 0),
                //SendTimeout = new TimeSpan(0, 0, 60, 0, 0),
                Security = new System.ServiceModel.NetTcpSecurity()
                {
                    Mode = SecurityMode.None
                }
            });

            //binding.OpenTimeout = new TimeSpan(0, 0, 60, 0, 0);
            //binding.CloseTimeout = new TimeSpan(0, 0, 60, 0, 0);
            //binding.ReceiveTimeout = new TimeSpan(0, 0, 60, 0, 0);
            //binding.SendTimeout = new TimeSpan(0, 0, 60, 0, 0);

            return binding;
        }

        public Binding basico()
        {
            var binding = new BasicHttpBinding()
            {
                Name = "BindingName",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            return binding;
        }
    }
}
