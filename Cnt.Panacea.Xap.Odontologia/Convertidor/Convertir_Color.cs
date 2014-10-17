using System;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia
{
    public class Convertir_Color : IValueConverter
    {
        /// <summary>
        /// Identifica el color a mostrar en cada superficie de acuerdo al binding establecido
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
                return "Transparent";
            }
            else if (value.ToString().Length > 10)
            {
                return "Transparent";
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
