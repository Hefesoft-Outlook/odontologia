using Cnt.Panacea.Xap.Odontologia.Vm.Contexto;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Contexto_Odontologia
{
    public static IContexto_Odontologia obtenerContexto()
    {
        var contexto = SimpleIoc.Default.GetInstance<IContexto_Odontologia>();
        return contexto;
    }
}

