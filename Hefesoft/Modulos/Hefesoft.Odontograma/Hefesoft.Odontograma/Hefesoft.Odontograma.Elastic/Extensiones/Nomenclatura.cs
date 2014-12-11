using Cnt.Panacea.Entities.Odontologia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Cambiar_Nomenclatura_Superficies
{
    public static string nomenclaturaSuperficie(this Superficie item)
    {
        if (item == Superficie.IncisalOclusal)
        {
            return "Superficie1";
        }
        else if (item == Superficie.Mesial)
        {
            return "Superficie2";
        }
        else if (item == Superficie.Distal)
        {
            return "Superficie3";
        }
        else if (item == Superficie.LingualPalatinaInferior)
        {
            return "Superficie4";
        }
        else if (item == Superficie.LingualPalatinaSuperior)
        {
            return "Superficie5";
        }
        else if (item == Superficie.VestibularSuperior)
        {
            return "Superficie6";
        }
        else if (item == Superficie.VestibularInferior)
        {
            return "Superficie7";
        }
        else if (item == Superficie.SuperficieTotal)
        {
            return "Pieza_Completa || Boca";
        }
        else if (item == Superficie.SuperficieTotal)
        {
            return "Boca";
        }
        else
        {
            return "No encontrado";
        }
    }

    public static Superficie superficieNomenclatura(this string item)
    {

        if (item.Equals("Superficie1"))
        {
            return Superficie.IncisalOclusal;
        }

        else if (item.Equals("Superficie2"))
        {
            return Superficie.Mesial;
        }

        else if (item.Equals("Superficie3"))
        {
            return Superficie.Distal;
        }

        else if (item.Equals("Superficie4"))
        {
            return Superficie.LingualPalatinaInferior;
        }

        else if (item.Equals("Superficie5"))
        {
            return Superficie.LingualPalatinaSuperior;
        }

        else if (item.Equals("Superficie6"))
        {
            return Superficie.VestibularSuperior;
        }
        else if (item.Equals("Superficie7"))
        {
            return Superficie.VestibularInferior;
        }
        else if (item.Equals("Pieza_Completa"))
        {
            return Superficie.SuperficieTotal;
        }
        else if (item.Equals("Boca"))
        {
            return Superficie.SuperficieTotal;
        }
        else
        {
            return Superficie.Ninguno;
        }
    }

    public static string nombreBocaPiezaCompleta(this OdontogramaEntity pivote)
    {
        string conversion = pivote.Superficie.nomenclaturaSuperficie();
        string pieza;
        if (conversion == "Pieza_Completa || Boca")
        {
            if (pivote.Diente.Identificador == 99)
            {
                pieza = "Boca";
            }
            else
            {
                pieza = "Pieza_Completa";
            }
        }
        else
        {
            pieza = conversion;
        }
        return pieza;
    }
}