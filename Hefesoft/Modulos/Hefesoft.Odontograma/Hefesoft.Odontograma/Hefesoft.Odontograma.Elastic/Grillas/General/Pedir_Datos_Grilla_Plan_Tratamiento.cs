using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.General
{
    public class Pedir_Datos_Grilla_Plan_Tratamiento
    {
        public Action<falla> fallido { get; set; }
        public Action<ObservableCollection<ProcedimientosGrillaPlanTratamiento>> lst { get; set; }
    }

    public class falla
    {

        public List<string> mensaje { get; set; }
        public bool valido { get; set; }
    }


    public static class GrillaPlanTratamiento
    {
        public static falla validaListadoGrilla(this IEnumerable<ProcedimientosGrillaPlanTratamiento> lst)
        {
            falla falla = new falla() { valido = true, mensaje = new List<string>() };

            var valido = true;

            foreach (var item in lst)
            {
                if (item.ProcedimientosEspecialidadValor == null)
                {
                    valido = false;
                }
                if (item.OpcionesTratamientoValor == null)
                {
                    valido = false;
                }
                if (item.OdontogramaEntity.PlanTratamiento.NumeroSesion == null)
                {
                    valido = false;
                }
                if (item.NumeroSesionesProcedimiento == 0)
                {
                    valido = false;
                }
                if (
                    (item.OdontologosIpsValor == null) &&
                    (item.HigienistasIpsValor == null)
                    )
                {
                    valido = false;
                }

                if (!valido)
                {
                    falla.mensaje.Add(string.Format("Error en pieza dental {0}", item.OdontogramaEntity.Diente.Identificador));
                    falla.valido = false;
                }

            }

            return falla;
        }

    }
}
