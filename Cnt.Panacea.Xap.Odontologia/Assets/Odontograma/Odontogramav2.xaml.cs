using Cnt.Panacea.Xap.Odontologia.Util.Messenger;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight.Messaging;
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

namespace Cnt.Panacea.Xap.Odontologia.Views
{
    public partial class Odontogramav2 : UserControl
    {

        public Odontogramav2()
        {
            InitializeComponent();     
            oirEventos();
        }

        private void oirEventos()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    // Por codigo deacuerdo a la seleccion del usuario mostramos o ocultamos
                    // la paleta de diagnosticos y procedimientos
                    Grid.SetRow(Mapa_Pieza_Dentales, 1);
                    Grid.SetRowSpan(Mapa_Pieza_Dentales, 1);
                    Paleta.Visibility = System.Windows.Visibility.Visible;
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
                {
                    Grid.SetRow(Mapa_Pieza_Dentales, 1);
                    Grid.SetRowSpan(Mapa_Pieza_Dentales, 1);
                    Paleta.Visibility = System.Windows.Visibility.Visible;
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Evolucion)
                {
                    Grid.SetRow(Mapa_Pieza_Dentales, 0);
                    Grid.SetRowSpan(Mapa_Pieza_Dentales, 2);
                    Paleta.Visibility = System.Windows.Visibility.Collapsed;
                }
            });
        }
    }
}
