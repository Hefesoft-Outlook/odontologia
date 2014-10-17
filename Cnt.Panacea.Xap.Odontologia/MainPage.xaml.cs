using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using System.Windows.Threading;
using Cnt.Panacea.Xap.Dinamico.Controles;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Util.Mef;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta;
using GalaSoft.MvvmLight.Ioc;
using Cnt.Panacea.Entities.Parametrizacion;

namespace Cnt.Panacea.Xap.Odontologia
{
    //Esta clase tiene una clase parcial Main.dp.Partial donde esta todas las dependency properties
    //Se movieron alla para hacer legible el codigo
    [Export("Odontologia", typeof(IControlesAtencion))]
    public partial class MainPage : UserControl, IDisposable
    {
        #region Constructor

        public MainPage()
        {
            InitializeComponent();

            //Registro el cargador de xap que uso para cargar xap por mef
            SimpleIoc.Default.Register<Cargar_Xaps>(() => new Cargar_Xaps());

            VisualStateManager.GoToState(this, "VisualStateCargando", true);
            
            // Funcion que permite cargar un xap recibir el control
            // Y medir el progreso de descarga
            Messenger.Default.Send(new Pedir_Cargar_Xap()
            {
                Ruta = "Cnt.Panacea.Xap.Odontologia.Mef.Dlls.xap",
                Porcentaje = (m) => { ProgressBar.Value = m; },
                Cargado = xapCargado
            });

            oirBotonGuardadoClick();
            //inicializar();
        }

        private void xapCargado(Pedir_Cargar_Xap obj)
        {
            inicializar();
        }

        private void inicializar()
        {
            //Termina de cargar y se valida que exista una atencion
            if (Variables_Globales.IdAtencion !=null || Variables_Globales.IdAtencion != 0)
            {
                cargarOdontograma();
            }
            else
            {
                VisualStateManager.GoToState(this, "VisualStateAtencion", true);
            }
        }

        private void cargarOdontograma()
        {
            inicializarUi();
            GrdMef.Visibility = System.Windows.Visibility.Collapsed;
            var contenedor = new Contenedor();
            Grid.SetRow(contenedor, 1);
            LayoutRoot.Children.Add(contenedor);
        }

       
        private void oirBotonGuardadoClick()
        {
            //Oye si despus de guardado se dio click en guardar
            Messenger.Default.Register<long>(this,"Atencion", item =>
            {
                if (item > 0)
                {
                    cargarOdontograma();
                    Variables_Globales.IdAtencion = item;
                }

                VisualStateManager.GoToState(this, "VisualState", true);
            });
        }     

        private void inicializarUi()
        {
            //Se inicializa asi para que tome los parametros del client config
            Contexto_Odontologia.inicializarContexto();

            //Saca los paremetros que se necesitan en ejecucción
            cargarParametrosPadre();

            //Aca como al hacer mef no me cargara el Viewmodel locator lo cargo aca
            //Esto con el fin de tener dependency injectionn
            ResourceDictionary diccionario = new ResourceDictionary()
            {
                Source = new Uri("/Cnt.Panacea.Xap.Odontologia;component/ViewLocator.xaml", UriKind.Relative)
            };
            Application.Current.Resources.MergedDictionaries.Add(diccionario);            

            //Prueba de memoria ram y garbage collector
            DispatcherTimer clock = new DispatcherTimer();
            clock.Interval = TimeSpan.FromSeconds(30);
            clock.Tick += (object sender, EventArgs e) =>
            {
                var tamino = bytesToMega(GC.GetTotalMemory(true));
                TxtBlckGarbage.Text = tamino.ToString();
            };
            clock.Start();
        }

        private double bytesToMega(long p)
        {
            return (p / 1024f) / 1024f;
        }

        private static void cargarParametrosPadre()
        {
            Application currentApplication = Application.Current;

            if (((Cnt.Std.Xap.CntApplication)(currentApplication)) != null)
            {
                try
                {
                    dynamic appCurrent = ((Cnt.Std.Xap.CntApplication)(Application.Current));
                    Variables_Globales.IdIps = appCurrent.Token.IdIps;
                    Variables_Globales.IdSede = appCurrent.Token.IdSede;
                    Variables_Globales.IpCliente = appCurrent.Token.IpCliente;
                    Variables_Globales.IdPrestador = appCurrent.Token.IdPrestador;
                    Variables_Globales.UsuarioActual = appCurrent.Token.UsuarioActual;
                    Variables_Globales.IdPaciente = appCurrent.IdPaciente;

                    //Le envia el mensaje al user control paleta para que cargue los diagnosticos
                    //Con la ips diferente de 0
                    Messenger.Default.Send(new Cargar_Diagnosticos());

                    //Le envia un mensaje al contenedor indicandole que debe mostrar la ventana de procedimientos
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana() { Nombre = "Mostrar Procedimientos" });
                }
                catch { }
            }
        }

        #endregion




        public void Dispose()
        {
            Messenger.Default.Unregister<long>(this, "Atencion");
        }
    }
}
