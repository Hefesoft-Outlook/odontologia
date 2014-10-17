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
using System.Windows.Data;
using System.Collections.Generic;
using Cnt.Panacea.Entities.Odontologia;
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertir_Lista_Diagnosticos : IValueConverter
    {
        /// <summary>
        /// Muestra la lista de diagnosticos para la superficie seleccionada
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<ConfiguracionDiagnosticoOdontologiaEntity> Texto = new List<ConfiguracionDiagnosticoOdontologiaEntity>();

            if (value!= null)
            {                
                Texto = value as List<ConfiguracionDiagnosticoOdontologiaEntity>;
                string Diagnosticos = "";
                if (Texto != null && Texto.Any())
                {                    
                    Texto.ForEach(a =>
                            {
                                if (a != null && !string.IsNullOrEmpty(a.Diagnostico.DescripcionCie))
                                {
                                    Diagnosticos += a.Diagnostico.DescripcionCie.ToString() + ";";
                                }
                            }
                        );
                }
                return Diagnosticos;
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
