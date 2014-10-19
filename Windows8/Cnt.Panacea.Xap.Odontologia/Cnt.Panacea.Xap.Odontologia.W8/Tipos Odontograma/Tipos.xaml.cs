using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
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

namespace App2.Tipos_Odontograma
{
    public sealed partial class Tipos : UserControl
    {
        public Tipos()
        {
            this.InitializeComponent();
            oirCambioOdontograma();
        }

        private void oirCambioOdontograma()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                //Indica a la paleta como se debe mostrar acorde al odontograma seleccionado
                if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    VisualStateManager.GoToState(this, "VisualStateOdontogramaInicial", true);
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

    }
}
