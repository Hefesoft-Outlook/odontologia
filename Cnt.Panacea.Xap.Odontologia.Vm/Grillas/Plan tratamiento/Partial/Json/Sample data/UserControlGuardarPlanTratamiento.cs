using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Cnt.Panacea.Entities.Parametrizacion;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Mapa_Dental
{
    public partial class UserControlGuardarPlanTratamiento
    {
        public void datosPruebaComprobantes()
        {
           if (Comprobantes == null)
           {
               Comprobantes = new ObservableCollection<ComprobanteEntity>();
           }

           Comprobantes.Add(new ComprobanteEntity()
           {
               Identificador = 1,
               Descripcion = "Comprobante numero 1",
               Modulo =  new ModuloFuncionalEntity(),
               Detalles = new ComprobanteDetallesCollection()
           });

           Comprobantes.Add(new ComprobanteEntity()
           {
               Identificador = 2,
               Descripcion = "Comprobante numero 2",
               Modulo = new ModuloFuncionalEntity(),
               Detalles = new ComprobanteDetallesCollection()
           });

           Comprobantes.Add(new ComprobanteEntity()
           {
               Identificador = 3,
               Descripcion = "Comprobante numero 3",
               Modulo = new ModuloFuncionalEntity(),
               Detalles = new ComprobanteDetallesCollection()
           });

           Comprobantes.Add(new ComprobanteEntity()
           {
               Identificador = 4,
               Descripcion = "Comprobante numero 4",
               Modulo = new ModuloFuncionalEntity(),
               Detalles = new ComprobanteDetallesCollection()
           });

           Comprobantes.ToObservableCollection().fillTables(new Hefesoft.Entities.Odontologia.Comprobantes.ComprobanteEntity());
        }
    }
}
