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
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;


namespace Cnt.Panacea.Xap.Odontologia.Assets.Grillas.Evolucion
{
    public partial class UserControlEvolucionProcedimientos : UserControl
    {        
        public UserControlEvolucionProcedimientos()
        {
            InitializeComponent();            
        }


        internal void inicializar()
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Grid_Evolucion>();
            vm.InicializarEvolucion();            
        }
    }
}