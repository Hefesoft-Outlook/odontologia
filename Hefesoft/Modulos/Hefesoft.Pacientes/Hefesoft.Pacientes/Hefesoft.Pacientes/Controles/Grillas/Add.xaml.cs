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

namespace Hefesoft.Pacientes.Controles.Grillas.Pacientes
{
    public sealed partial class Add : UserControl
    {
        public Add()
        {
            this.InitializeComponent();

            //Validar xq no toma el contexto
            if(this.DataContext == null)
            {
                this.DataContext = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Pacientes.Pacientes>();
            }
        }

        private async void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var vmUsuario = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Usuarios>();
            var vmPacientes = ServiceLocator.Current.GetInstance<Hefesoft.Usuario.ViewModel.Pacientes.Pacientes>();

            Image img = sender as Image;
            var item = img.DataContext as Hefesoft.Usuario.ViewModel.Pacientes.Pacientes;

            var foto = await new Hefesoft.Util.W8.UI.Util.WebCam().takePicture(vmUsuario.UsuarioActivo.id);
            item.Paciente.imagenRuta = foto;
            img.Source = new BitmapImage(new Uri(foto));
            await vmPacientes.insert(item.Paciente);
        }
    }
}
