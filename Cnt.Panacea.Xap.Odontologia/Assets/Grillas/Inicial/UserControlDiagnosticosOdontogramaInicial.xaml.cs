using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Grillas.Inicial.vm
{
	public partial class UserControlDiagnosticosOdontogramaInicial : UserControl
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="UserControlDiagnosticosOdontogramaInicial"/> class.
        /// </summary>
        Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial.vm vm;
		public UserControlDiagnosticosOdontogramaInicial()
		{	
			InitializeComponent();
            vm = this.DataContext as Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Inicial.vm;
		}

        internal void inicializarElementos()
        {
            vm.pedirDatosDiagnosticos();
        }
    }
}