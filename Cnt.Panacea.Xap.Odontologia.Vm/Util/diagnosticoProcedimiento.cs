using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Util
{
    public static class diagnosticoProcedimiento
    {
        public static void inhabilitarSuperficies(this Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.DiagnosticoProcedimiento elemento)
        {
            elemento.Habilitado_Superficie1 = elemento.Superficie1.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie2 = elemento.Superficie2.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie3 = elemento.Superficie3.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie4 = elemento.Superficie4.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie5 = elemento.Superficie5.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie6 = elemento.Superficie6.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie7 = elemento.Superficie7.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Superficie8 = elemento.Superficie8.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Pieza_Completa = elemento.PiezaCompleta.Any(a => a.Diagnostico != null);
            elemento.Habilitado_Boca = elemento.Boca.Any(a => a.Diagnostico != null);

            // Cuando es pieza completa habilite toda la superficie
            if (elemento.Habilitado_Pieza_Completa)
            {
                elemento.Habilitado_Superficie1 = true;
                elemento.Habilitado_Superficie2 = true;
                elemento.Habilitado_Superficie3 = true;
                elemento.Habilitado_Superficie4 = true;
                elemento.Habilitado_Superficie5 = true;
                elemento.Habilitado_Superficie6 = true;
                elemento.Habilitado_Superficie7 = true;
                elemento.Habilitado_Superficie8 = true;                
            }

        }

        public static void habilitarSuperficies(this Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.DiagnosticoProcedimiento elemento)
        {
            elemento.Habilitado_Superficie1 = true;
            elemento.Habilitado_Superficie2 = true;
            elemento.Habilitado_Superficie3 = true;
            elemento.Habilitado_Superficie4 = true;
            elemento.Habilitado_Superficie5 = true;
            elemento.Habilitado_Superficie6 = true;
            elemento.Habilitado_Superficie7 = true;
            elemento.Habilitado_Superficie8 = true;
            elemento.Habilitado_Boca = true;
            elemento.Habilitado_Pieza_Completa = true;
        }
    }
}
