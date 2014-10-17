using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Entities.Odontologia;


public static class OdontogramaEntity_Ext
{
    public static ConfigurarDiagnosticoProcedimOtraEntity OdontogramaEntityToConfigurarDiagnosticoProcedimOtraEntity(this OdontogramaEntity item)
    {
        var elemento = new ConfigurarDiagnosticoProcedimOtraEntity();

        if (item.Diagnostico != null && item.Diagnostico.Identificador != 0)
        {
            elemento.Identificador = item.Diagnostico.Identificador;
            elemento.Diagnostico = item.Diagnostico.Diagnostico.Identificador;
            elemento.Color = item.Diagnostico.Color;
            elemento.ColorAdicional = item.Diagnostico.ColorAdicional;
            elemento.Simbolo = item.Diagnostico.Simbolo;
            elemento.Letra = item.Diagnostico.Letra;
            elemento.Fuente = item.Diagnostico.Fuente;
            elemento.TipoPanel = TipoPanel.Diagnostico;
            elemento.IndiceCEO = item.Diagnostico.IndiceCEO;
            elemento.IndiceCOP = item.Diagnostico.IndiceCOP;
            elemento.IndicePlacaBacteriana = item.Diagnostico.IndicePlacaBacteriana;
            elemento.Descripcion = item.Diagnostico.Diagnostico.DescripcionCie;
        }
        else if (item.Procedimiento != null && item.Procedimiento.Identificador != 0)
        {
            elemento.Identificador = item.Procedimiento.Identificador;
            elemento.Procedimiento = Convert.ToInt32(item.Procedimiento.Procedimiento.Identificador);
            elemento.Color = item.Procedimiento.Color;
            elemento.ColorAdicional = item.Procedimiento.ColorAdicional;
            elemento.Simbolo = item.Procedimiento.Simbolo;
            elemento.Letra = item.Procedimiento.Letra;
            elemento.Fuente = item.Diagnostico.Fuente;
            elemento.TipoPanel = TipoPanel.Procedimiento;
            elemento.IndiceCEO = item.Procedimiento.IndiceCEO;
            elemento.IndiceCOP = item.Procedimiento.IndiceCOP;
            elemento.IndicePlacaBacteriana = item.Procedimiento.IndicePlacaBacteriana;
            elemento.Descripcion = item.Procedimiento.NombreProcedimiento;
        }

        return elemento;
    }
}

