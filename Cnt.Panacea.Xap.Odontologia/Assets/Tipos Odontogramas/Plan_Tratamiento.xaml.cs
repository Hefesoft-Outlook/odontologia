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
using GalaSoft.MvvmLight.Messaging;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas
{
    public partial class Plan_Tratamiento : UserControl
    {
        Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento vm;
        public Plan_Tratamiento()
        {
            InitializeComponent();
            vm = this.DataContext as Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento;            
        }        
    }
}
