using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia.Vm
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
                
                CambiarConvenio(Variables_Globales.IdIps, Convert.ToInt16(Variables_Globales.IdPaciente));
            }
        }

        /// <summary>
        ///Lista los convenios.
        /// </summary>
        /// <param name="ProcedimientoSeleccionado">The procedimiento seleccionado.</param>
        public async void CambiarConvenio(short idIps, short idPaciente)
        {
            ConveniosPaciente = await Contexto_Odontologia.obtenerContexto().ListarConveniosPaciente(idIps, idPaciente);
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
