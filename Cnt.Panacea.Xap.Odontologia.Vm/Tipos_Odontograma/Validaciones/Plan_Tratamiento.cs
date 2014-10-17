using System;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Validaciones
{
    public class Plan_Tratamiento
    {
        public falla validarDatos(Cnt.Panacea.Xap.Odontologia.Assets.Tipos_Odontogramas.Vm.Plan_Tratamiento vm)
        {
            var falla = new falla() { valido = true };

            if (vm.TiposTratamientoSeleccionado == null || vm.TiposTratamientoSeleccionado.Identificador == 0)
            {
                falla.valido = false;
                falla.mensaje += "Seleccione un tipo de tratamiento por favor" + System.Environment.NewLine;
            }

            return falla;
        }
    }

    public class falla
    {
        public string mensaje { get; set; }
        public bool valido { get; set; }
    }
}
