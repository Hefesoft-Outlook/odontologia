using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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

namespace App2.Assets.PopUp
{
    public sealed partial class Generico : UserControl,IDisposable
    {
        public Generico()
        {
            this.InitializeComponent();
            oirMostrarVentana();
            flyout = new Flyout();

        }

        private void oirMostrarVentana()
        {            
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this, generarVentana);
        }

        private void generarVentana(Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana obj)
        {
            if (obj.Nombre == "Convenio")
            {
             
            }
            else if (obj.Nombre == "Supernumerario")
            {
                if (!SimpleIoc.Default.IsRegistered<Layouts.Agregar_Supernumerario>())
                {
                    SimpleIoc.Default.Register(() => new Layouts.Agregar_Supernumerario());
                }
                var supernumerario = SimpleIoc.Default.GetInstance<Layouts.Agregar_Supernumerario>();
                supernumerario.Cerrar = (p) =>
                {   
                    flyout.Hide();
                };
                var ventana = mostrarVentana(supernumerario);
            }
            else if (obj.Nombre == "Evolucion")
            {
             
            }                        
            else if (obj.Nombre == "Listado odontograma inicial")
            {
             
            }
            else if (obj.Nombre == "Listado imagenes")
            {
             
            }
            else if (obj.Nombre == "Observaciones Evolucion")
            {
             
            }
            else if (obj.Nombre == "Aprueba plan tratamiento")
            {
             
            }
            else if (obj.Nombre == "Generar plan tratamiento")
            {
             
            }
            else if (obj.Nombre == "Cotizacion")
            {
             
            }
            else if (obj.Nombre == "Bodega")
            {
             
            }
            else if (obj.Nombre == "Descargar imagenes")
            {
             
            }
            else if (obj.Nombre == "Sobreescribir Procedimiento")
            {
                if (!SimpleIoc.Default.IsRegistered<Layouts.SobreEscribirAdicionar>())
                {
                    SimpleIoc.Default.Register(() => new Layouts.SobreEscribirAdicionar());
                }
                var confirmacion = SimpleIoc.Default.GetInstance<Layouts.SobreEscribirAdicionar>();
                confirmacion.Cerrar = (p) => 
                {
                    obj.Resultado(confirmacion.EstadoSobreEscribirAdicionar);
                    flyout.Hide();
                };                
                var ventana = mostrarVentana(confirmacion);                
            }
            else if (obj.Nombre == "Descargar imagen")
            {
             
            }
            else if (obj.Nombre == "Reporte")
            {
             
            }
            else if (obj.Nombre == "Procedimientos evolucion")
            {
             
            }
            else if (obj.Nombre == "Requiere clasificador")
            {
                
            }
            else if (obj.Nombre == "Mostrar Procedimientos")
            {
                
            }
            else if (obj.Nombre == "Finalizar tratamiento")
            {
                
            }
            else if (obj.Nombre == "Autorizacion")
            {
                
            }
            else if (obj.Nombre == "Finaliza Plan Tratamiento")
            {
                
            }
            else if (obj.Nombre == "Ver imagen")
            {
                
            }
        }        

        private Flyout mostrarVentana(UIElement elemento)
        {            
            flyout.Content = elemento;
            flyout.ShowAt(padre);
            return flyout;
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
            DependencyProperty.Register("padre", typeof(FrameworkElement), typeof(Generico),
            new PropertyMetadata(null, new PropertyChangedCallback(OnpadreChanged)));

        private static void OnpadreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Generico)d).OnpadreChanged(e);
        }

        private void OnpadreChanged(DependencyPropertyChangedEventArgs e)
        {
            
        }

        

        #endregion



        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Pop_Up.Mostrar_Ventana>(this);
        }

        public Flyout flyout { get; set; }
    }
}
