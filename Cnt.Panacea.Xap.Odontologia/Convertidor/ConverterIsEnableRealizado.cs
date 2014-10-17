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
using Cnt.Panacea.Xap.Odontologia.Clases;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    public class ConverterIsEnableRealizado : IValueConverter
    {
        /// <summary>
        /// Activa o inactiva una celda de acuerdo al parametro enviado en el binding de la celda
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }
            else if (((Cnt.Panacea.Entities.Odontologia.PlanTratamientoEntity)(value)).EstadoProcedimiento == true)
            {
                return false;
            }
            else if (((Cnt.Panacea.Entities.Odontologia.PlanTratamientoEntity)(value)).Legalizacion == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
