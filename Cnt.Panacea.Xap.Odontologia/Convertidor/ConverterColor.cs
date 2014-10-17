using System;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    /// <summary>
    /// Clase que se usa para colerear las piezas dentales deacuerdo a lo que se encuentre en la base de datos
    /// </summary>
    public class ConverterColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            if (value != null)
            {
                if ((bool)value == false)
                {
                    mySolidColorBrush.Color = Colors.Red;


                }
                else
                {
                    mySolidColorBrush.Color = Colors.Black;
                }

            }

            return mySolidColorBrush;



        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("PropertyViewer does not use ConvertBack.");
        }

    }
}
