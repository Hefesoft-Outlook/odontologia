using System;
using System.Windows;
using System.Windows.Data;

namespace Cnt.Panacea.Xap.Odontologia.Clases
{
    /// <summary>
    /// Clase que maneja los valores nulos al interior de los campos de los Datagrids de silverlight.
    /// </summary>
    public class Convertir_Valores_Nulos : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }
            else if (bool.Parse(value.ToString()) == true)
            {
                return Visibility.Visible;
            }
            else if (bool.Parse(value.ToString()) == false)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
