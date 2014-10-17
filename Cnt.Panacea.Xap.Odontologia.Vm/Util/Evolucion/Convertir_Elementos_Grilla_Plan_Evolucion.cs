using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util;
using Cnt.Std;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Evolucion
{
    public class Convertir_Elementos_Grilla_Plan_Evolucion
    {
        // Convierte lo que viene de la grilla en un listado que se puede guardar
        internal PlanesTratamientoCollection_Extend convertirGrillaPlanes(ObservableCollection<ProcedimientosGrillaEvolucion> lst)
        {
            var Planes = new PlanesTratamientoCollection_Extend();
            Planes.PlanesTratamientoCollection = new PlanesTratamientoCollection();

            foreach (ProcedimientosGrillaEvolucion pivot in lst)
            {
                if (pivot.PlanTratamientoEntity.EstadoProcedimiento == false)
                {
                    Planes.FinalizaTratamiento = false;
                }

                OdontogramaEntity odontograma = new Entities.Odontologia.OdontogramaEntity() { Identificador = pivot.PlanTratamientoEntity.Identificador };
                pivot.PlanTratamientoEntity.EstadoRegistro = EstadosEntidad.Modificado;
                pivot.PlanTratamientoEntity.Padre = odontograma;


                if (pivot.PlanTratamientoEntity.EstadoProcedimiento && !pivot.Realizado)
                {
                    pivot.PlanTratamientoEntity.FinalidadProcedimiento = pivot.FinalidadesProcedimientoValor.Identificador;

                    if (pivot.OdontologosHigienistasIpsValor != null)
                    {
                        pivot.PlanTratamientoEntity.IdPrestadorRealiza = pivot.OdontologosHigienistasIpsValor.Identificador;
                    }


                    pivot.PlanTratamientoEntity.SesionesPlanTratamiento.FirstOrDefault().SesionRealizada = true;
                    pivot.PlanTratamientoEntity.SesionesPlanTratamiento.FirstOrDefault().EstadoRegistro = EstadosEntidad.Modificado;

                    if (pivot.PlanTratamientoEntity.SesionesPlanTratamiento.Count == 1)
                    {
                        pivot.PlanTratamientoEntity.SesionesPlanTratamiento.FirstOrDefault().FechaRealizacion = DateTime.Now;
                        pivot.PlanTratamientoEntity.SesionesPlanTratamiento.FirstOrDefault().SesionRealizada = true;

                        pivot.PlanTratamientoEntity.SesionesPlanTratamiento.FirstOrDefault().FechaRealizacion = DateTime.Now;
                        pivot.PlanTratamientoEntity.FechaRealizacion = DateTime.Now;
                    }
                    else
                    {
                        for (int i = 0; i <= pivot.PlanTratamientoEntity.SesionesPlanTratamiento.Count() - 1; i++)
                        {
                            if (pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i).SesionRealizada == true)
                            {
                                pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i + 1).SesionRealizada = true;
                                pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i + 1).EstadoRegistro = EstadosEntidad.Modificado;
                                pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i + 1).FechaRealizacion = DateTime.Now;
                            }
                            else
                            {
                                pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i).SesionRealizada = true;
                            }

                            pivot.PlanTratamientoEntity.SesionesPlanTratamiento.GetByIndex(i).EstadoRegistro = EstadosEntidad.Modificado;
                            break;
                        }
                        if (pivot.PlanTratamientoEntity.SesionesPlanTratamiento.Count(p => p.SesionRealizada == false) > 0)
                        {
                            pivot.PlanTratamientoEntity.EstadoProcedimiento = false;
                        }
                        else
                        {
                            pivot.PlanTratamientoEntity.EstadoProcedimiento = true;
                            pivot.PlanTratamientoEntity.FechaRealizacion = DateTime.Now;
                        }
                    }
                    Planes.PlanesTratamientoCollection.Add(pivot.PlanTratamientoEntity);
                }
            }

            return Planes;
        }

        internal List<ProcedimientosGrillaEvolucion> obtenerElementosParaNumeroSesion(List<ProcedimientosGrillaEvolucion> lst, int numeroSesion)
        {
            List<ProcedimientosGrillaEvolucion> lstTemp = new List<ProcedimientosGrillaEvolucion>();

            foreach (var item in lst)
            {
                if (item.Sesion.Any(a => a.IdSesion == numeroSesion))
                {
                    lstTemp.Add(item);
                }
            }

            return lstTemp;
        }
    }

    public class PlanesTratamientoCollection_Extend
    {
        public PlanesTratamientoCollection PlanesTratamientoCollection { get; set; }
        public bool FinalizaTratamiento { get; set; }
    }
}
