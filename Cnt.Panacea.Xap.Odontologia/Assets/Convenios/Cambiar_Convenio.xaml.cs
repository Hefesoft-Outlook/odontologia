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

namespace Cnt.Panacea.Xap.Odontologia.Assets.Convenios
{
    public partial class Cambiar_Convenio : UserControl
    {
        public Cnt.Panacea.Xap.Odontologia.Assets.Convenios.Vm.vm vm { get; set; }
        public Cambiar_Convenio()
        {
            InitializeComponent();
            vm = this.DataContext as Cnt.Panacea.Xap.Odontologia.Assets.Convenios.Vm.vm;
        }
    }
}
