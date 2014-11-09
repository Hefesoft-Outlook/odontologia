using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cnt.Panacea.Entities.Parametrizacion;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento
{
    public partial class GridPlanTratamientoProcedimientosWizard 
    {
        private void datosPruebaOdontologosHigienistas()
        {
            List<TerceroEntity> tercero = new List<TerceroEntity>();

            tercero.Add(new TerceroEntity()
            {
                Identificador = 1,
                NombreCompleto = "Medico 1"
            });

            tercero.Add(new TerceroEntity()
            {
                Identificador = 2,
                NombreCompleto = "Medico 2"
            });

            tercero.Add(new TerceroEntity()
            {
                Identificador = 3,
                NombreCompleto = "Farmacia 1"
            });

            tercero.Add(new TerceroEntity()
            {
                Identificador = 4,
                NombreCompleto = "Farmacia 2"
            });
            
            //tercero.ToObservableCollection().fillTables(new Hefesoft.Entities.Odontologia.Convenio.TerceroEntity());
        }

        //Toma un elemento como referencia y llena los demas igual
        public void tomarElementoComoBase(Vm.Util.Plan_Tratamiento.ProcedimientosGrillaPlanTratamiento elementoReferencia)
        {
            foreach (var item in Listado)
            {
                item.OpcionesTratamientoValor = elementoReferencia.OpcionesTratamientoValor;
                item.OdontologosIpsValor = elementoReferencia.OdontologosIpsValor;
                item.HigienistasIpsValor = elementoReferencia.HigienistasIpsValor;
                item.PlanTratamientoEntity.Cobra = elementoReferencia.PlanTratamientoEntity.Cobra;

                item.ProcedimientosEspecialidadValor = elementoReferencia.ProcedimientosEspecialidadValor;
                item.OdontogramaEntity.PlanTratamiento.ValorServicio = elementoReferencia.OdontogramaEntity.PlanTratamiento.ValorServicio;
                item.OdontogramaEntity.PlanTratamiento.ValorPaciente = elementoReferencia.OdontogramaEntity.PlanTratamiento.ValorPaciente;
            }

            RaisePropertyChanged("Listado");
        }
    }
}
