using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Hefesoft.Odontograma.Assets.PopUp
{
    public sealed partial class Modal
    {
        private void oirCerraVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp.Cerrar_Pop_Up_Generico>(this, elemento =>
            {
                if (modal != null)
                {
                    try
                    {
                        modal.ocultarModal(true);
                    }
                    catch
                    { }
                }
            });
        }

        private void oirMostrarVentana()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this, generarVentana);
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

        private void Grid_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (modal != null)
            {
                modal.ocultarModal(false);
            }
        }

        private Action<object> ventanaCerrada { get; set; }

        public Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana elementoOtraVentana { get; set; }
    }
}
