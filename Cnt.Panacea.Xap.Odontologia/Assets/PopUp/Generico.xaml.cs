using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
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

namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class Generico : ChildWindow, IDisposable
    {
        public Generico()
        {
            InitializeComponent();
            Messenger.Default.Register<Cerrar_Pop_Up_Generico>(this, elemento =>             
            {
                this.DialogResult = true;
            });
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cerrar_Pop_Up_Generico>(this);
        }
    }
}

