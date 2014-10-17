using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Entities.Odontologia;

namespace Cnt.Panacea.Xap.Odontologia.Util.Procedimientos
{
    public class Procedimientos
    {
        public ObservableCollection<ConfiguracionProcedimientoOdontologiaEntity> MostrarTodosProcedimientos(ObservableCollection<ConfiguracionProcedimientoOdontologiaEntity> procedimientosConfigurados)
        {
            var ProcedimientosFiltrados = new ObservableCollection<ConfiguracionProcedimientoOdontologiaEntity>();
            foreach (ConfiguracionProcedimientoOdontologiaEntity pivot in procedimientosConfigurados)
            {
                pivot.NombreProcedimiento = pivot.Procedimiento.Nombre;
                pivot.CodigoProcedimiento = pivot.Procedimiento.Codigo;
                pivot.Procedimiento.Identificador = pivot.Procedimiento.Identificador;
                ProcedimientosFiltrados.Add(pivot);
            }

            return ProcedimientosFiltrados;
        }
    }
}
