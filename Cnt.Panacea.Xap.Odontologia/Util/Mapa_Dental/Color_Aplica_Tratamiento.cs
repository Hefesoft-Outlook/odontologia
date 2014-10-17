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

namespace Cnt.Panacea.Xap.Odontologia.Util.Mapa_Dental
{
    public class Color_Aplica_Tratamiento
    {
        public ConfigurarDiagnosticoProcedimOtraEntity obtenerColorFalsoParaPintarSuperficiesTratamiento()
        {
            //Add Otra Caracteristica
            var elemento = (new ConfigurarDiagnosticoProcedimOtraEntity()
            {
                AplicaTratamiento = 1, //AplicaTratamiento.Diente,
                Descripcion = "Diente ausente",//Codigo Diag
                OtraCaracteristica = -1,
                //Config Color Aplica Px, Dx                
                Color = 13421772,
                //
            });

            return elemento;
        }
    }
}
