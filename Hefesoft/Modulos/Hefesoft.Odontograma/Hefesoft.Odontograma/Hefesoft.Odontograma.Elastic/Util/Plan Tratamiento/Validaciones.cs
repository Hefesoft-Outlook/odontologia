using Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento;
using Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento
{
    public class Grilla_Plan_Tratamiento
    {
        internal fallo validarAntesGuardar(UserControlGuardarPlanTratamiento vm)
        {
            var valido = new fallo() { valido = true };

            if (vm.FormaPagoSeleccionado == null)
            {
                valido.mensaje += "Seleccione una forma de pago" + System.Environment.NewLine;
                valido.valido = false;
            }

            foreach (ProcedimientosGrillaPlanTratamiento pivot in vm.ListadoGrillaPlanTratamiento)
            {
                if (pivot.NumeroSesionesValor == 0)
                {
                    valido.valido = false;
                    valido.mensaje += "Realice configuracion sesiones" + System.Environment.NewLine;
                    break;
                }
            }

            return valido;
        }


    }

    public class fallo
    {
        public bool valido { get; set; }
        public string mensaje { get; set; }
    }
}
