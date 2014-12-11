using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public static class Numero_Sessiones
    {
        public static void numeroSessiones(this OdontogramaEntity elemento)
        {
            if (elemento.PlanTratamiento == null)
            {
                elemento.PlanTratamiento = new PlanTratamientoEntity();
            }
            if (elemento.PlanTratamiento.SesionesPlanTratamiento == null)
            {
                elemento.PlanTratamiento.SesionesPlanTratamiento = new SesionesPlanTratamientosCollection();
            }

            SesionesPlanTratamientosCollection Sesiones = new SesionesPlanTratamientosCollection();

            for (int i = 0; i <= elemento.PlanTratamiento.NumeroSesionesProcedimiento - 1; i++)
            {
                SesionesPlanTratamientoEntity sesion = new SesionesPlanTratamientoEntity();

                if (elemento.PlanTratamiento.NumeroSesion == null)
                {
                    elemento.PlanTratamiento.NumeroSesion = 0;
                }

                sesion.IdSesion = (short)(elemento.PlanTratamiento.NumeroSesion + i);
                sesion.Estado = true;
                sesion.IdIps = Variables_Globales.IdIps;

                if (elemento.PlanTratamiento.Procedimiento != null)
                {
                    sesion.Procedimiento = (int)elemento.PlanTratamiento.Procedimiento;
                }

                if (elemento.PlanTratamiento.SesionesPlanTratamiento != null && elemento.PlanTratamiento.Procedimiento != null)
                {
                    if ((elemento.PlanTratamiento.SesionesPlanTratamiento.Where(p => p.IdSesion == (short)(elemento.PlanTratamiento.NumeroSesion + i)).Where(p => p.Procedimiento == (int)elemento.PlanTratamiento.Procedimiento).Any()))
                    {
                        sesion.SesionRealizada = (elemento.PlanTratamiento.SesionesPlanTratamiento.Where(p => p.IdSesion == (short)(elemento.PlanTratamiento.NumeroSesion + i)).Where(p => p.Procedimiento == (int)elemento.PlanTratamiento.Procedimiento).FirstOrDefault().SesionRealizada);
                    }
                    else
                    {
                        sesion.SesionRealizada = false;
                    }
                }
                else
                {
                    sesion.SesionRealizada = false;
                }

                sesion.Identificador = i;
                Sesiones.Add(sesion);

                elemento.PlanTratamiento.SesionesPlanTratamiento = Sesiones;
            }
        }
    }

