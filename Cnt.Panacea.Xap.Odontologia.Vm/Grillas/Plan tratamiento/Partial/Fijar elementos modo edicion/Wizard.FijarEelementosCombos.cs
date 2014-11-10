using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento
{
    public partial class GridPlanTratamientoProcedimientosWizard 
    {
        public void pintarProcedimientosColoresPiezadental()
        {

            fijarElementos();

            if (Listado.Any())
            {
                Util.Convertir_Elemento_Grilla_Dibujo_Odontograma.Convertir(Listado);
            }

            RaisePropertyChanged("Listado");
        }

        private void fijarElementos()
        {
            try
            {
                short i = 1;
                foreach (var item in Listado)
                {
                    item.NumeroSesionesProcedimiento = 1;
                    item.numeroSesion = i;
                    i = Convert.ToInt16(i + 1);

                    if (item.PlanTratamientoEntity != null && item.PlanTratamientoEntity.PrestadorOdontologo > 0)
                    {
                        item.OdontologosIpsValor = OdontologosIps.FirstOrDefault(a => a.Identificador == item.PlanTratamientoEntity.PrestadorOdontologo);
                    }

                    if (item.PlanTratamientoEntity != null && item.PlanTratamientoEntity.PrestadorHigienista > 0)
                    {
                        item.HigienistasIpsValor = HigientistasIps.FirstOrDefault(a => a.Identificador == item.PlanTratamientoEntity.PrestadorHigienista);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
