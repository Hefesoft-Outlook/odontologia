using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Convenios.Vm
{
    public class vm : ViewModelBase
    {
        public vm()
        {
            ConveniosPaciente = new ObservableCollection<Entities.Parametrizacion.PacienteConvenioEntity>();
            if (IsInDesignMode)
            {
                ConveniosPaciente.Add(new Entities.Parametrizacion.PacienteConvenioEntity()
                {
                    Convenio = new Entities.Parametrizacion.ConvenioEntity() { Nombre = "Prueba" }
                });
            }
            else
            {
                
                CambiarConvenio();
            }
        }

        /// <summary>
        ///Lista los convenios.
        /// </summary>
        /// <param name="ProcedimientoSeleccionado">The procedimiento seleccionado.</param>
        public async void CambiarConvenio()
        {
            ConveniosPaciente = await Contexto_Odontologia.ListarConveniosPaciente(Variables_Globales.IdIps, Variables_Globales.IdPaciente);
            RaisePropertyChanged("ConveniosPaciente");
        }

        

        public ObservableCollection<Entities.Parametrizacion.PacienteConvenioEntity> ConveniosPaciente { get; set; }

        private Entities.Parametrizacion.PacienteConvenioEntity convenioSeleccionado;

        public Entities.Parametrizacion.PacienteConvenioEntity ConvenioSeleccionado
        {
            get { return convenioSeleccionado; }
            set { convenioSeleccionado = value; RaisePropertyChanged("ConvenioSeleccionado"); }
        }
        
    }
}
