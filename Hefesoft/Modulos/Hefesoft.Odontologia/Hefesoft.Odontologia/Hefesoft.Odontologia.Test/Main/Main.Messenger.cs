using Hefesoft.Standard.BusyBox;
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
using Windows.UI.Xaml.Navigation;


namespace Hefesoft.Odontologia.Test
{    
    public sealed partial class MainPage : Page
    {
        private void oirCambioUsuario()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Cambio Usuario", item =>
            {
                this.Host.Navigate(typeof(Hefesoft.Autentication.Controles.Autentication));
            });
        }

        private void oirMenuSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Menu", item =>
            {
                Hefesoft.Standard.BusyBox.BusyBox.UserControlCargando(true);
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.MenuOdontologia.Elastic.ViewModel.Menu>();

                switch (vm.ElementoSeleccionado.Pagina)
                {
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Odontograma:
                        this.Host.Navigate(typeof(Hefesoft.Odontograma.Odontograma));
                        break;
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Periodontograma:
                        this.Host.Navigate(typeof(Hefesoft.Periodontograma.Assets.Periodontograma));
                        break;
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Parametrizacion_Diagnosticos_Procedimientos:
                        this.Host.Navigate(typeof(Hefesoft.ParamDiagnosticos.Paginas.Diagnosticos_Procedimientos));
                        break;
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Parametrizacion_Niveles_Severidad:
                        this.Host.Navigate(typeof(Hefesoft.NivelesSeveridad.Paginas.Niveles_Severidad));
                        break;
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Parametrizacion_Odontologos:
                        this.Host.Navigate(typeof(Hefesoft.Terceros.Controles.Odontologo.Odontologo));
                        break;                    
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Parametrizacion_Higienista:
                        this.Host.Navigate(typeof(Hefesoft.Terceros.Controles.Higienista.Higienista));
                        break;                    
                    case Hefesoft.MenuOdontologia.Elastic.Enumeradores.Paginas.Adicionar_Paciente:
                        this.Host.Navigate(typeof(Hefesoft.Pacientes.Controles.Pacientes));
                        break;
                    default:
                        break;
                }
            });
        }

        private void oirUsuarioIngreso()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Usuario logueado", item =>
            {
                this.Host.Navigate(typeof(Hefesoft.Paginas.Menu));
            });
        }
    }
}
