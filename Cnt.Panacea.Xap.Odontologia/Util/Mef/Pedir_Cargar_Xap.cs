using GalaSoft.MvvmLight.Ioc;
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

namespace Cnt.Panacea.Xap.Odontologia.Util.Mef
{
    public class Pedir_Cargar_Xap : IDisposable
    {
        private string ruta;

        public string Ruta
        {
            get { return ruta; }
            set 
            { 
                ruta = value;
                cargar(ruta);
            }
        }
        
        public Action<double> Porcentaje { get; set; }
        public Action<Pedir_Cargar_Xap> Cargado { get; set; }
        public UserControl Elemento_cargado { get; set; }
        public dynamic Catalogo { get; set; }

        public Pedir_Cargar_Xap()
        {            
            var xap = SimpleIoc.Default.GetInstance<Cargar_Xaps>();           
        }       

        private void cargar(string ruta)
        {
            oirCargando(ruta);
            var xap = SimpleIoc.Default.GetInstance<Cargar_Xaps>();
            xap.Cargado += xap_Cargado;
            xap.cargarXap(ruta);
        }

        void xap_Cargado(object sender, EventArgs e)
        {
            var xap = SimpleIoc.Default.GetInstance<Cargar_Xaps>();
            xap.Cargado -= xap_Cargado;            
            var userControl = sender as UserControl;
            Elemento_cargado = userControl;
            Catalogo = xap;

            if (Cargado != null)
            {
                Cargado(this);
            }
            desregistrar();
        }

        private void oirCargando(string ruta)
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, ruta, resultado =>
            {
                if (resultado == "Cargado")
                {

                }
                else
                {
                    // Si hay alguien oyendo en el otro extremo
                    if (Porcentaje != null)
                    {
                        Porcentaje(double.Parse(resultado));
                    }
                }
            });
        }

        public void Dispose()
        {
            desregistrar();
        }

        private void desregistrar()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<string>(this, ruta);
        }
    }
}
