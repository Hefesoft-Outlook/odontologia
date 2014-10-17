using System;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Caracteres : IValueConverter
    {
        /// <summary>
        /// Muestra el caracter en cada superficie de la pieza dental
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
                return "";
            }
            else if (value.ToString().Length < 10)
            {
                return "";
            }
            else
            {
                return value.ToString().Split(',')[0].ToString();
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
