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

namespace App2.Assets.Periodontograma.Sangrado_Supuracion
{
    public sealed partial class Sangrado_Supuracion_Elemento : UserControl
    {
        public Sangrado_Supuracion_Elemento()
        {
            this.InitializeComponent();
        }


        #region Sangrado_Supuracion (DependencyProperty)

        /// <summary>
        /// A description of the property.
        /// </summary>
        public Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion Sangrado_Supuracion
        {
            get { return (Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion)GetValue(Sangrado_SupuracionProperty); }
            set { SetValue(Sangrado_SupuracionProperty, value); }
        }
        public static readonly DependencyProperty Sangrado_SupuracionProperty =
            DependencyProperty.Register("Sangrado_Supuracion", typeof(Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion), typeof(Sangrado_Supuracion_Elemento),
            new PropertyMetadata(Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion.ninguno, new PropertyChangedCallback(OnSangrado_SupuracionChanged)));

        private static void OnSangrado_SupuracionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Sangrado_Supuracion_Elemento)d).OnSangrado_SupuracionChanged(e);
        }

        private void OnSangrado_SupuracionChanged(DependencyPropertyChangedEventArgs e)
        {
            var item = (Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion)e.NewValue;

            if(item == Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion.ninguno)
            {
                VisualStateManager.GoToState(this, "VisualState", true);
            }
            else if (item == Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion.red)
            {
                VisualStateManager.GoToState(this, "VisualStateRed", true);
            }
            else if (item == Hefesoft.Odontologia.Periodontograma.Enumeradores.Sangrado_Supuracion.red_yellow)
            {
                VisualStateManager.GoToState(this, "VisualStateRed_Yellow", true);
            }
        }        

        #endregion
        
    }
}
