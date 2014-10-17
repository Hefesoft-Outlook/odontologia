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
using System.Linq;

namespace Cnt.Panacea.Xap.Odontologia
{
    public static class Diagnostico_Procedimiento
    {
        public static bool tieneDiagnosticos(this ObservableCollection<ConfigurarDiagnosticoProcedimOtraEntity> lst)
        {
            if(lst.Any(a=>a.TipoPanel == TipoPanel.Diagnostico))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
