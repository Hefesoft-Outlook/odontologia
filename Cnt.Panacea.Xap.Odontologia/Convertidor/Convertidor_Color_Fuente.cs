using System;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Color_Fuente :IValueConverter 
    {
        /// <summary>
        /// Da color a la fuente de simbolo o letra de cada superficie
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
                return "#FF000000";
            }
            else if (value.ToString().Length < 10)
            {
                return "#FF000000";
            }
            else
            {
                if (value.ToString().Contains(","))
                {
                    if (value.ToString().Split(',')[2].ToString().Trim() == "")
                    {
                        return "#FF000000";
                    }
                    else
                    {
                        return value.ToString().Split(',')[2].ToString();
                    }
                }
                else
                {
                    return "#FF000000";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
