using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.Generic;
using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Recursos;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento;

namespace Cnt.Panacea.Xap.Odontologia
{
    public partial class UserControlGridPlanTratamiento : UserControl
    {
        Listado_Procedimientos vm;
        public UserControlGridPlanTratamiento()
        {
            InitializeComponent();
            vm = this.DataContext as Listado_Procedimientos;
        }

        internal void inicializarElementos()
        {
            vm.inicializar();
        }
    }
}