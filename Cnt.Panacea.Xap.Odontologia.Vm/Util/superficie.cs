using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;
using System;
using System.Net;
using System.Windows;
using System.Windows.Input;


public static class superficie
{
    public static DiagnosticoProcedimiento_Extend odontogramaEntityToDiagnosticoProcedimiento_Extend(this OdontogramaEntity elemento)
    {
        var elementosCrear = new DiagnosticoProcedimiento_Extend();

        //Validar caso supernumerario
        validacionSupernumerario(elemento, elementosCrear);
        // Convierte el enumerador de la bd en lo que entiende el odontograma
        string pieza = elemento.nombreBocaPiezaCompleta();
        elementosCrear.Superficie = pieza;
        elementosCrear.Nivel_Severidad = elemento.NivelSeveridad;
        elementosCrear.ConfigurarDiagnosticoProcedimOtraEntity = elemento.OdontogramaEntityToConfigurarDiagnosticoProcedimOtraEntity();
        elementosCrear.Codigo_Pieza_Dental = elemento.Diente.Identificador;
        elementosCrear.Tipo_Odontograma_Actual = Variables_Globales.Tipo_Odontograma_Activo;
        
        //Para no perder caracteristicas cuando se pidan los procedimientos a otros formularios
        elementosCrear.OdontogramaEntity = elemento;

        return elementosCrear;
    }

    private static void validacionSupernumerario(OdontogramaEntity elemento, DiagnosticoProcedimiento_Extend elementosCrear)
    {
        if (elemento.Diente.Identificador == 98)
        {
            elementosCrear.Es_Supernumerario = true;
            if (elemento.DienteReferencia1.Identificador != 0)
            {
                elementosCrear.Referencia_SuperNumerario = new referenciaSupernumerario() { Diente_Referencia = elemento.DienteReferencia1.Identificador, Esta_A_la_Derecha = true };
            }
            else if (elemento.DienteReferencia2.Identificador != 0)
            {
                elementosCrear.Referencia_SuperNumerario = new referenciaSupernumerario() { Diente_Referencia = elemento.DienteReferencia2.Identificador, Esta_A_la_Derecha = false };
            }
        }
    }
}

