using System;
using System.Windows.Data;
using System.Collections.Generic;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Superficie_Sana : IValueConverter
    {
        /// <summary>
        /// Si la superficie no tiene procedimientos o diagnosticos asociados la muestra como superficie sana
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Text.StringBuilder Texto = new System.Text.StringBuilder();
            List<DiagnosticoProcedimiento> lstDiagnosticos = new List<DiagnosticoProcedimiento>();

            if (value == null)
            {
                return "Sano";
            }
            else
            {
                bool sano = true;
                lstDiagnosticos = value as List<DiagnosticoProcedimiento>;
                lstDiagnosticos.ForEach(a =>
                    {
                        //if (a.EsDiagnostico && Odontologia.Clases.Variables_Globales.OdontogramaInicial == true)
                        //{
                        //    Texto.AppendLine("- " + a.DiagnosticoEntity.Diagnostico.DescripcionCie);
                        //    //Texto += " -"+a.DiagnosticoEntity.Diagnostico.DescripcionCie;
                        //    sano = false;
                        //}
                        //else if (a.EsProcedimiento && Odontologia.Clases.Variables_Globales.OdontogramaInicial == true)
                        //{
                        //    Texto.AppendLine("- " + a.ProcedimientoEntity.Procedimiento.Nombre);
                        //    //Texto += " -" + a.ProcedimientoEntity.Procedimiento.Nombre;
                        //    sano = false;
                        //}
                        //else if (a.EsProcedimiento && Odontologia.Clases.Variables_Globales.Plantratamiento == true)
                        //{
                        //    Texto.AppendLine("- " + a.ProcedimientoEntity.Procedimiento.Nombre);
                        //    //Texto += " -" + a.ProcedimientoEntity.Procedimiento.Nombre;
                        //    sano = false;
                        //}
                        //else
                        //{
                        //    Texto.AppendLine("Sano");
                        //}
                    });
                if(sano)
                    Texto.AppendLine("Sano");

                return Texto.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
