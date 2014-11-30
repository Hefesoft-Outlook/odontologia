using Hefesoft.Standard.Interfaces;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2.Assets.Menu
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {            
            this.InitializeComponent();
            oirClick();
        }

        private void oirClick()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<string>(this, "Menu", item => 
            {
                var vm = ServiceLocator.Current.GetInstance<Hefesoft.Odontologia.Menu.ViewModel.Menu>();

                switch (vm.ElementoSeleccionado.Pagina)
                {
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Odontograma:
                        this.Frame.Navigate(typeof(HubPage));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Periodontograma:
                        this.Frame.Navigate(typeof(Assets.Periodontograma.Periodontograma));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Parametrizacion_Diagnosticos_Procedimientos:
                        this.Frame.Navigate(typeof(Assets.Diagnosticos_procedimientos.Diagnosticos_Procedimientos));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Parametrizacion_Niveles_Severidad:
                        this.Frame.Navigate(typeof(Assets.Niveles_de_severidad.Niveles_Severidad));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Parametrizacion_Odontologos:
                        this.Frame.Navigate(typeof(Assets.Tercero.Odontologo.Odontologo));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Parametrizacion_Higienista:
                        this.Frame.Navigate(typeof(Assets.Tercero.Higienista.Higienista));
                        break;
                    case Hefesoft.Odontologia.Menu.Enumeradores.Paginas.Adicionar_Paciente:
                        this.Frame.Navigate(typeof(App2.Pacientes.Pacientes));
                        break;
                    default:
                        break;
                }                
            });
        }
    }
}
