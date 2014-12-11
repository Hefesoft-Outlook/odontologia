using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Plan_tratamiento.Util
{
    class Convertir_Elemento_Grilla_Dibujo_Odontograma
    {
        public static void Convertir(ObservableCollection<ProcedimientosGrillaPlanTratamiento> Listado)
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
