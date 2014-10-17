using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Mensajes;
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

namespace Cnt.Panacea.Xap.Odontologia.Assets.Mensajes
{
    public partial class Mensaje : UserControl, IDisposable
    {
        public Mensaje()
        {
            InitializeComponent();
            oirMensaje();
        }

        private void oirMensaje()
        {
            Messenger.Default.Register<Mostrar_Mensaje_Usuario>(this, item =>
            {
                MessageBox.Show(item.Mensaje);
            });
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Mostrar_Mensaje_Usuario>(this);
        }
    }
}
