using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cnt.Panacea.Xap.Odontologia.Clases;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using Cnt.Panacea.Xap.Odontologia.Util.Mef;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental
{
	public partial class Mapa_Pieza_Dentales : UserControl, IDisposable
	{
        VM.Vm vm;
		public Mapa_Pieza_Dentales()
		{			
			InitializeComponent();            
            vm = this.DataContext as VM.Vm;
            oirAdjuntar();
            oirModoLectura();
            oirImprimir();

            // Activar el click derecho para este formulario
            //Se usara para mostrar un dialogo que permitira agregar un diente supernumerario
            this.MouseRightButtonDown += Mapa_Pieza_Dentales_MouseRightButtonDown;            
		}

        private void oirImprimir()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Imprimir.Imprimir>(this, imprimir => 
            {
                new Util.Pdf.Pdf().generarPdf(imprimir);
            });
        }

        private void oirModoLectura()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Modo_Lectura.Modo_Lectura>(this, modo => 
            {
                if (modo.Solo_Lectura)
                {
                    this.IsEnabled = false;
                }
                else
                {
                    this.IsEnabled = true;
                }
            });
        }

        //El viewmodel envia el mensaje para cargar las imagenes
        private void oirAdjuntar()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Imagenes.Adjuntar>(this, adjuntar => 
            {
                var Mapa = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
                new Cnt.Panacea.Xap.Odontologia.Util.Adjuntar_Archivos.Adjuntar().AdjuntarImagen(Mapa); 
            });
        }

        void Mapa_Pieza_Dentales_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Imagenes.Adjuntar>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Modo_Lectura.Modo_Lectura>(this);
        }
    }
}