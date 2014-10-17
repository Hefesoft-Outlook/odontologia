using Cnt.Panacea.Xap.Odontologia.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Odontograma.Tipo;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Cnt.Panacea.Xap.Odontologia
{
	public partial class Tipo_Odontogramas : UserControl, IDisposable
	{
		public Tipo_Odontogramas()
		{
			// Required to initialize variables
			InitializeComponent();
            oirEventos();

            //Cuando se carga unprocedimiento debe mostrar el odontograma inicial
            oirCargarProcedimientoGuardado();
		}

        private void oirCargarProcedimientoGuardado()
        {
            Messenger.Default.Register<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this, elemento =>
            {
                if (elemento.Odontogra_Cargar == "Inicial")
                {
                    VisualStateManager.GoToState(this, "VisualStateInicial", true);
                }
            });
        }

        private void oirEventos()
        {
            Messenger.Default.Register<Cambiar_Tipo_Odontograma>(this, item =>
            {
                Variables_Globales.Tipo_Odontograma_Activo = item.Tipo_Odontograma;

                //Validar volverlo un state manager
                if (item.Tipo_Odontograma == Tipo_Odontograma.Inicial)
                {
                    VisualStateManager.GoToState(this, "VisualStateInicial", true);
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Plan_Tratamiento)
                {
                    VisualStateManager.GoToState(this, "VisualStatePlanTratamiento", true);
                }
                else if (item.Tipo_Odontograma == Tipo_Odontograma.Evolucion)
                {
                    VisualStateManager.GoToState(this, "VisualStateEvolucion", true);
                }
            });
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Util.Messenger.Cargar_Odontograma>(this);
            Messenger.Default.Unregister<Cambiar_Tipo_Odontograma>(this);
        }
    }
}