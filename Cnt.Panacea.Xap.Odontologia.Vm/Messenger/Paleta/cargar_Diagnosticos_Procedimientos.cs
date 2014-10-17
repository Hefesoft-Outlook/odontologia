using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Input;

namespace Cnt.Panacea.Xap.Odontologia.Vm.Messenger.Paleta
{
    public class cargar_Diagnosticos_Procedimientos
    {
        public List<int> listadoProcedimientos { get; set; }
        public Tipo tipo { get; set; }
    }
    public enum Tipo
    {
        todos = 0,
        todosIncluyendoNoCubreConvenio = 1,
        listado = 2
    }
}
