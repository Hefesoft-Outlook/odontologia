using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Entities.Odontologia;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;


namespace Cnt.Panacea.Xap.Odontologia.Assets
{
    public partial class Paleta : UserControl, IDisposable
    {        
        public Paleta()
        {
            InitializeComponent();           
            oirCambioOdontograma();

            //Cuando se carga unprocedimiento debe mostrar el odontograma inicial
            oirCargarProcedimientoGuardado();
            oirModoLectura();
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

        private void oirCargarProcedimientoGuardado()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this, elemento => 
            {
                if (elemento.Odontogra_Cargar == "Inicial")
                {
                    VisualStateManager.GoToState(this, "VisualStateDagnostico", true);
                }
            });
        }

        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                //Indica a la paleta como se debe mostrar acorde al odontograma seleccionado
                if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    VisualStateManager.GoToState(this, "VisualStateDagnostico", true);
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
                {
                    VisualStateManager.GoToState(this, "VisualStatePlanTratamiento", true);
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Evolucion)
                {
               
                }

            });
        }


        #region Modo (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public string Modo
        {
            get { return (string)GetValue(ModoProperty); }
            set { SetValue(ModoProperty, value); }
        }
        public static readonly DependencyProperty ModoProperty =
            DependencyProperty.Register("Modo", typeof(string), typeof(Paleta),
            new PropertyMetadata("Diagnostico", new PropertyChangedCallback(OnModoChanged)));

        private static void OnModoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Paleta)d).OnModoChanged(e);
        }

        protected virtual void OnModoChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.ToString() == "Diagnostico")
            {
                VisualStateManager.GoToState(this, "VisualStateDagnostico", false);
            }
            else if (e.NewValue.ToString() == "Procedimiento")
            {
                VisualStateManager.GoToState(this, "VisualStateProcedimiento", false);
            }
        }

        #endregion
       

        public void Dispose()
        {
            Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this);
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Modo_Lectura.Modo_Lectura>(this);
        }
    }
}
