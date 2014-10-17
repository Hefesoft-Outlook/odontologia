using System.Windows;
using System.Windows.Controls;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Clases;
using GalaSoft.MvvmLight.Messaging;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.PopUp;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente;
using System;
using Microsoft.Practices.ServiceLocation;

namespace Cnt.Panacea.Xap.Odontologia.Views
{

    public partial class PacienteTratamientos : UserControl, IDisposable
    {
        #region Constructor
        public PacienteTratamientos()
        {
            InitializeComponent();
            Variables_Globales.mostrar_Diagnosticos_plan_tratamiento = true;
            oirCargarElementosGraficos();
        }

        private void oirCargarElementosGraficos()
        {
            Messenger.Default.Register<Cargar_Recursos_Ui>(this, elemento => 
            {
                try
                {
                    resourcedictionary = new ResourceDictionary();
                    resourcedictionary.Source = new Uri("/Cnt.Panacea.Xap.Estilos;component/Cnt.Xap.Estilos.xaml", UriKind.RelativeOrAbsolute);                 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }
        
        #endregion

        public ResourceDictionary resourcedictionary { get; set; }

        public void Dispose()
        {
            Messenger.Default.Unregister<Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paciente.Inicializar_Valor_Paciente>(this);
            Messenger.Default.Unregister<Cargar_Recursos_Ui>(this);
        }

        internal void inicializar()
        {
            var vm = ServiceLocator.Current.GetInstance<Cnt.Panacea.Xap.ViewModels.PacienteTratamientosViewModel>();
            vm.cargarDatosPaciente();
        }
    }
}
