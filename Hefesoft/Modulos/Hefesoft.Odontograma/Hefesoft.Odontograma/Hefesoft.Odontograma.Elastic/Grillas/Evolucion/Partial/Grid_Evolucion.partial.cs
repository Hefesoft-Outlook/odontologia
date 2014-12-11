using Cnt.Panacea.Entities.Parametrizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Grillas.Evolucion
{
    public partial class Grid_Evolucion
    {
        private void datosPruebaFinalidadProcedimiento()
        {
            List<FinalidadProcedimientoEntity> lst = new List<FinalidadProcedimientoEntity>();

            lst.Add(new FinalidadProcedimientoEntity() 
            { 
                  Codigo = 1,
                  Descripcion = "Finalidad 1",
                  Estado = true,
                  Identificador = 1              
            });

            lst.Add(new FinalidadProcedimientoEntity()
            {
                Codigo = 2,
                Descripcion = "Finalidad 2",
                Estado = true,
                Identificador = 2
            });

            lst.Add(new FinalidadProcedimientoEntity()
            {
                Codigo = 3,
                Descripcion = "Finalidad 3",
                Estado = true,
                Identificador = 3
            });

            lst.Add(new FinalidadProcedimientoEntity()
            {
                Codigo = 4,
                Descripcion = "Finalidad 4",
                Estado = true,
                Identificador = 4
            });

            //lst.ToObservableCollection().fillTables(new Hefesoft.Entities.Odontologia.Finalidad.FinalidadProcedimientoEntity());
        }
    }
}
