using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Hefesoft.Standard.Util.Pdf;
using Hefesoft.Util.W8.UI.Util;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Hefesoft.Odontograma.Util.Reportes.Templates
{
    public sealed partial class Plan_Tratamiento : UserControl
    {
        public Plan_Tratamiento()
        {
            this.InitializeComponent();
            //Primero se casa la imagen del contenedo padre
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Messenger.Capturar_Imagen() { Imagen = imagenCargada }, "Capturar mapa dental");
        }

        private void imagenCargada(Windows.UI.Xaml.Media.Imaging.RenderTargetBitmap obj)
        {
            generarReporte(obj);
        }

        private void generarReporte(RenderTargetBitmap img)
        {            
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.GridPlanTratamientoProcedimientosWizard>();
            Report.ItemsSource = vm.Listado;

            Report.ImagenOdontograma = new Image();
            Report.ImagenOdontograma.Source = img;
            Report.Imagen = true;

            RowDefinition Titulo = new RowDefinition();
            Titulo.Height = GridLength.Auto;
            TextBlock Texto = new TextBlock() { Text = "Plan de tratamiento" };
            Grid Grid = new Grid() { Height = 50 };
            Grid.RowDefinitions.Add(Titulo);
            Grid.SetRow(Texto, 0);
            Grid.Children.Add(Texto);
            Report.Grid = Grid;
            Report.Texto = true;            

            //Tamanio Carta
            Report.BuildReport(new Size(1275, 1650));

            if (Report._pageTrees.Any())
            {                
                Contenedor.Children.Clear();
                document = new Hefesoft.Pdf.Entities.Document() { pdfName = string.Format("{0}.pdf", Variables_Globales.IdTratamientoActivo.ToString()) };

                foreach (var item in Report._pageTrees)
                {
                    Contenedor.Children.Add(item);                          
                }
            }

        }

        public Hefesoft.Pdf.Entities.Document document { get; set; }

        private async void HyprlnkBttnExportarPdf_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(true, "Exportando... tomara un tiempo...");
            Snapshot snap = new Snapshot();
            int i = 0;
            foreach (var item in Report._pageTrees)
            {
                var url = await guardarImagen(snap, item, Variables_Globales.IdTratamientoActivo.ToString());
                document.lst.Add(i, new Hefesoft.Pdf.Entities.Image(url) { scale = true, TypeElement = Hefesoft.Pdf.Entities.TypeElement.Image });
                i = i + 1;
            }

            await exportarPdf();
            Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(false);
        }

        private async Task<string> guardarImagen(Snapshot snap, UIElement item, string nombre)
        {
            nombre = string.Format("{0}.png", nombre);
            var result = await snap.snapShot((FrameworkElement)item, nombre);

            return result;
        }

        public async Task exportarPdf()
        {   
            var pathPdf = await Hefesoft.Standard.Util.Pdf.Pdf.postPdf(document);
            await Launcher.LaunchUriAsync(new Uri(pathPdf, UriKind.Absolute));
        }
    }
}
