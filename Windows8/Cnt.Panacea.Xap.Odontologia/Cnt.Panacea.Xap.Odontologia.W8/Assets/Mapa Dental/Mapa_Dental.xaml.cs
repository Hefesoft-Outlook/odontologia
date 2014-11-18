using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Cnt.Panacea.Xap.Odontologia.W8.Mapa_Dental
{
    public sealed partial class Mapa_Dental : UserControl, IDisposable
    {
        public Mapa_Dental()
        {
            this.InitializeComponent();
            oirOdontogramaSeleccionado();

            oirCapturarImagen();
        }

        //Captura la imagen del contenedor para los reportes
        private void oirCapturarImagen()
        {            
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<App2.Util.Messenger.Capturar_Imagen>(this, "Capturar mapa dental", elemento => 
            {
                sacarImagen(elemento);                        
            });
        }

        private async void sacarImagen(App2.Util.Messenger.Capturar_Imagen item)
        {            
            var snap = new App2.Hub_Partial.Snapshot();
            RenderTargetBitmap result = await snap.snapShot(this.LayoutRoot);
            item.Imagen(result);
        }

        

        private void oirOdontogramaSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Odontograma>(this, "Pieza Seleccionada", elemento => 
            { 
                if(elemento.UbicacionOdontograma == 1)
                {                    
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 2)
                {
                    listBox1.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 3)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 4)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 5)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 6)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 7)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox8.SelectedIndex = -1;
                }
                else if (elemento.UbicacionOdontograma == 8)
                {
                    listBox1.SelectedIndex = -1;
                    listBox2.SelectedIndex = -1;
                    listBox3.SelectedIndex = -1;
                    listBox4.SelectedIndex = -1;
                    listBox5.SelectedIndex = -1;
                    listBox6.SelectedIndex = -1;
                    listBox7.SelectedIndex = -1;
                }
            });
        }
        
        private void StackPanel_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            StackPanel st = sender as StackPanel;
            var item = st.DataContext as Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma;
            var Vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Assets.Mapa_Dental.VM.Vm>();
            Vm.clickDerechoContenedorPiezaDental(item);
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<App2.Util.Messenger.Capturar_Imagen>(this, "Capturar mapa dental");
        }
    }
}
