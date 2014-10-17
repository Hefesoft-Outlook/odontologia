using System;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Fuentes : IValueConverter
    {
        /// <summary>
        /// Identifica la fuente que se debe retornar para la superficie a la cual se haga referencia en el binding
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
                return "Portable User Interface";
            }
            else if (value.ToString().Length < 10)
            {
                return "Portable User Interface";
            }
            else
            {
                if (value.ToString().Contains(","))
                {
                    return value.ToString().Split(',')[1].ToString();
                }
                else
                {
                    return "Portable User Interface";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
