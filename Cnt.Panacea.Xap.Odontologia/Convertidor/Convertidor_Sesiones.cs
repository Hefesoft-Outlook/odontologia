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
using System.Linq;
using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Xap.Odontologia.Vm.Estaticas;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertidor_Sesiones : IValueConverter
    {
        /// <summary>
        /// Identifica la session a mostrar de acuerdo al array de sessiones que venga en el binding y a la session actual a la cual se hace referencia
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var Sesion = value as SesionesPlanTratamientosCollection;
            if (Sesion != null)
            {
                if (Sesion.FirstOrDefault(a => a.IdSesion == Variables_Globales.SesionActual) != null)
                {
                    // Si tiene dos sesiones para un procedimiento muestra la que corresponda a esta vez
                    return Sesion.FirstOrDefault(a => a.IdSesion == Variables_Globales.SesionActual).IdSesion.ToString();
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
