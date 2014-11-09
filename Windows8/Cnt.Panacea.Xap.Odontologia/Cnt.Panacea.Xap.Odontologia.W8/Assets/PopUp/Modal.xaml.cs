using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using Microsoft.Practices.ServiceLocation;

namespace App2.Assets.PopUp
{
    public sealed partial class Modal : UserControl, IDisposable
    {
        public Modal()
        {            
            this.InitializeComponent();
            
            oirMostrarVentana();
            oirCerraVentana();

            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.Titulo.Margin = new Thickness(20, 20, 20, 0);
            this.LayoutRoot.Margin = new Thickness(20, 0, 20, 20);
        }

        private void oirCerraVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp.Cerrar_Pop_Up_Generico>(this, elemento=>
            {
                ocultarModal(true);
            });
        }

        private void oirMostrarVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this, generarVentana);
        }

        private void generarVentana(Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            if (obj.Nombre == "Plan tratamiento")
            {
                elementoOtraVentana = obj;
                var wizard = new App2.Grillas.Plan_tratamiento.UserControlGuardarPlanTratamiento() {HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch};
                var vCerrada = MostrarModal(wizard, "Plan de tratamiento");
                vCerrada = cerrar;
            }
            else if (obj.Nombre == "Evolucion")
            {
                elementoOtraVentana = obj;
                var wizard = new App2.Grillas.Evolucion.SplitEvolucion() { HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch, VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch };
                var vCerrada = MostrarModal(wizard, "Evolucion");
                vCerrada = cerrar;
            }
            else if (obj.Nombre == "Tratamientos")
            {
                elementoOtraVentana = obj;
                var reporte = new App2.Util.Reportes.Templates.Plan_Tratamiento();
                MostrarModal(reporte, "Reporte plan de tratamiento");                
            }
            else if (obj.Nombre == "Listado imagenes")
            {
                elementoOtraVentana = obj;
                var reporte = new App2.Fotos.SplitFotos() { DataContext = obj.Propiedad_Adicional };
                MostrarModal(reporte, "Imagenes");
            }

             
        }

        private void cerrar(object obj)
        {
            if (elementoOtraVentana != null && elementoOtraVentana.Nombre == "Plan tratamiento")
            {
                var datacontext = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental.UserControlGuardarPlanTratamiento>();
                if (datacontext.lstOdontogramaEntity != null && datacontext.lstOdontogramaEntity.Any())
                {
                    elementoOtraVentana.Resultado(datacontext);
                }
            }
            else if (elementoOtraVentana != null && elementoOtraVentana.Nombre == "Evolucion")
            {
                Messenger.Default.Send(new Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Guardar.Guardar() { }, "Evolucion");
            }
        }

        private Action<object> MostrarModal(UIElement elementoMostrar, string titulo = "")
        {
            TxtBlckTitulo.Text = titulo;
            Contenedor.Children.Clear();
            Contenedor.Children.Add(elementoMostrar);
            this.Visibility = Windows.UI.Xaml.Visibility.Visible;


            return ventanaCerrada;
        }

        private void ocultarModal(bool dialogResult)
        {
            TxtBlckTitulo.Text = "";
            Contenedor.Children.Clear();            
            this.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (dialogResult)
            {
                //Callback
                ventanaCerrada = cerrar;
                ventanaCerrada(null);
            }
        }


        #region padre (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public FrameworkElement padre
        {
            get { return (FrameworkElement)GetValue(padreProperty); }
            set { SetValue(padreProperty, value); }
        }
        public static readonly DependencyProperty padreProperty =
            DependencyProperty.Register("padre", typeof(FrameworkElement), typeof(Modal),
            new PropertyMetadata(null, new PropertyChangedCallback(OnpadreChanged)));

        private static void OnpadreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Modal)d).OnpadreChanged(e);
        }

        private void OnpadreChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        #endregion


        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp.Cerrar_Pop_Up_Generico>(this);
        }

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            ocultarModal(false);
        }

        private Action<object> ventanaCerrada { get; set; }

        public Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana elementoOtraVentana { get; set; }
    }
}
