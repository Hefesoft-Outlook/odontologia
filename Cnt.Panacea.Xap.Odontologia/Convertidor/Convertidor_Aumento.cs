using System;
using System.Windows;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Aumento : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ( value.ToString() == Visibility.Visible.ToString())
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
