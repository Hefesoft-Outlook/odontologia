using Cnt.Panacea.Entities.Odontologia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Datos_de_prueba
{
    public class Paleta
    {
        public ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> datos { get; set; }

        public Paleta()
        {
            datos = new ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity>();
            //Add Diagnostico
            datos.Add(new ConfigurarDiagnosticoProcedimOtraEntity()
            {
                Identificador = 2,
                AplicaTratamiento = 1,//AplicaTratamiento.Diente,
                Codigo = "K022",//Codigo Diag
                Descripcion = "Caries del cemento",
                Diagnostico = 3777,
                //Config Simbolo Aplica tambien para Px,Otros
                Simbolo = "E",
                ColorAdicional = 6750207,
                Fuente = "Wingdings 3",
                //
                //Solo para Dx
                IndiceCOP = true,
                IndiceCEO = true,
                IndicePlacaBacteriana = true,
                Severidad = true,//Si es true debe exitir el comob de Severidad, si es false no es obligatorio
            });


            //Add Procedimiento
            datos.Add(new ConfigurarDiagnosticoProcedimOtraEntity()
            {
                AplicaTratamiento = 2,//AplicaTratamiento.Diente,
                Codigo = "232101",
                Descripcion = "OBTURACION DENTAL CON AMALGAMA",
                Procedimiento = 782,
                //Config Letra Aplica para Dx, Otros
                Letra = "A",
                ColorAdicional = 10040064,
                //
            });

            //Add Otra Caracteristica
            datos.Add(new ConfigurarDiagnosticoProcedimOtraEntity()
            {
                AplicaTratamiento = 1, //AplicaTratamiento.Diente,
                Descripcion = "Diente ausente",//Codigo Diag
                OtraCaracteristica = -1,
                //Config Color Aplica Px, Dx                
                Color = 13421772,
                //
            });

        }
    }
}
