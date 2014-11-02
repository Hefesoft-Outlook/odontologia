using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion.Util
{
    public class Convertir_Elemento_Grilla_Dibujo_Odontograma
    {
        public static void Convertir(ObservableCollection<ProcedimientosGrillaEvolucion> Listado)
        {
            foreach (var item in Listado)
            {
                var diagnosticoExtend = item.OdontogramaEntity.odontogramaEntityToDiagnosticoProcedimiento_Extend();
                item.Odontograma.DiagnosticoProcedimiento.lst.Add(diagnosticoExtend);
                item.Odontograma.DiagnosticoProcedimiento.pintarDiagnosticos(diagnosticoExtend.ConfigurarDiagnosticoProcedimOtraEntity, diagnosticoExtend.Superficie);
                item.ConfigurarDiagnosticoProcedimOtraEntity = diagnosticoExtend.ConfigurarDiagnosticoProcedimOtraEntity;
            }
        }
    }
}
