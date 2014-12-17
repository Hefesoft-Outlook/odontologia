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

namespace Hefesoft.Periodontograma.Assets.Control
{
    public sealed partial class Periodontograma : UserControl, IDisposable
    {
        private static bool isRegistered;
        public Periodontograma()
        {
            this.InitializeComponent();

            if (!isRegistered)
            {
                isRegistered = true;
                oirPeriodontogramaSeleccionado();
            }
        }

        private void oirPeriodontogramaSeleccionado()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Hefesoft.Periodontograma.Elastic.Entidades.PeriodontogramaEntity>(this, "Pieza Seleccionada", elemento =>
            {
                //Provoca que el elemento seleccionado cuando se salta de un listado a otro no se muestre
                //desSeleccionar(elemento);
            });
        }

        private void desSeleccionar(Elastic.Entidades.PeriodontogramaEntity elemento)
        {
            if (elemento.Parte == Elastic.Enumeradores.Parte.parte1)
            {
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte2)
            {
                listBox1.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte3)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte4)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte5)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte6)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte7)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox8.SelectedIndex = -1;
            }
            else if (elemento.Parte == Elastic.Enumeradores.Parte.parte8)
            {
                listBox1.SelectedIndex = -1;
                listBox2.SelectedIndex = -1;
                listBox3.SelectedIndex = -1;
                listBox4.SelectedIndex = -1;
                listBox5.SelectedIndex = -1;
                listBox6.SelectedIndex = -1;
                listBox7.SelectedIndex = -1;
            }
        }

        public void Dispose()
        {
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Unregister<Hefesoft.Periodontograma.Elastic.Entidades.PeriodontogramaEntity>(this, "Pieza Seleccionada");
        }
    }
}
