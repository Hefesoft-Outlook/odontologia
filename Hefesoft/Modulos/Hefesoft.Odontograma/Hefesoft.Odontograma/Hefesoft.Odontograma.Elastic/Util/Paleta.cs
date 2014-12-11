using Cnt.Panacea.Entities.Odontologia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.Util
{
    public class Aplicar_Tratamiento_A
    {
        internal Aplica_A aplicarA(ConfigurarDiagnosticoProcedimOtraEntity item)
        {
            Aplica_A Aplicar_A = Aplica_A.Ninguno;

            if (item == null)
            {
                Aplicar_A = Aplica_A.Ninguno;
            }
            else if (item.AplicaTratamiento == 1)
            {
                Aplicar_A = Aplica_A.Diente;
            }
            else if (item.AplicaTratamiento == 2)
            {
                Aplicar_A = Aplica_A.Superficie;
            }
            else if (item.AplicaTratamiento == 3)
            {
                Aplicar_A = Aplica_A.Boca;
            }

            return Aplicar_A;
        }

        internal Tipo_Elemento tipoElemento(ConfigurarDiagnosticoProcedimOtraEntity item)
        {
            if (!string.IsNullOrEmpty(item.Simbolo))
            {
                return Tipo_Elemento.Simbolo;
            }
            else if (!string.IsNullOrEmpty(item.Letra))
            {
                return Tipo_Elemento.Letra;
            }
            else
            {
                return Tipo_Elemento.Color;
            }
        }
    }

    public enum Aplica_A
    {
        Ninguno = 0,
        Diente = 1,
        Superficie = 2,
        Boca = 3
    }

    public enum Tipo_Elemento
    {
        Simbolo = 0,
        Letra = 1,
        Color = 2
    }
}
