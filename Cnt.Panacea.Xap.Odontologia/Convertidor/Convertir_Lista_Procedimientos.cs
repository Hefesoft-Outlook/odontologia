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
using Cnt.Panacea.Xap.Odontologia.Clases;
using System.Linq;
using Cnt.Panacea.Xap.Odontologia.Vm.Odontograma;

namespace Cnt.Panacea.Xap.Odontologia.Convertidor
{
    public class Convertir_Lista_Procedimientos : IValueConverter
    {
        /// <summary>
        /// Muestra la lista de procedimientos para la superficie seleccionada
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<ProcedimientoPlanTratamiento> Texto = new List<ProcedimientoPlanTratamiento>();
            if (value!= null)
            {
                Texto = value as List<ProcedimientoPlanTratamiento>;

                foreach (var pivote in Texto)
                {
                    if (pivote.ProcedimientoEntity.Procedimiento.Nombre.Split('*').Any())
                    {
                        pivote.ProcedimientoEntity.Procedimiento.Nombre = "* " + pivote.ProcedimientoEntity.Procedimiento.Nombre.Replace("*", " ").Trim();
                    }
                    if (pivote.ProcedimientoEntity.Procedimiento.Nombre.Contains("\n")) 
                    {
                        pivote.ProcedimientoEntity.Procedimiento.Nombre = pivote.ProcedimientoEntity.Procedimiento.Nombre.Replace("\n", " ").Trim();
                    }
                    if (pivote.ProcedimientoEntity.Procedimiento.Nombre.Contains("\r"))
                    {
                        pivote.ProcedimientoEntity.Procedimiento.Nombre = pivote.ProcedimientoEntity.Procedimiento.Nombre.Replace("\r", " ").Trim();
                    }
                }

                string Procedimientos = "";
                if (Texto != null && Texto.Any())
                {                    
                    Texto.ForEach(a =>
                        {
                            if (a.ProcedimientoEntity.Procedimiento.Nombre != null)
                            {
                                Procedimientos += a.ProcedimientoEntity.Procedimiento.Nombre.ToString() + ";";
                            }                            
                        }
                        );
                }
                return Procedimientos;
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
