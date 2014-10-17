using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cnt.Panacea.Entities.Parametrizacion;
using System.Linq;


public static class Especialidades
{
    public static List<EspecialidadEntity> obtenerEspecialidades(this IEnumerable<ProcedimientoEspecialidadSedeEntity> lst)
    {
        List<EspecialidadEntity> temp = new List<EspecialidadEntity>();

        foreach (var item in lst)
        {
            if(!temp.Any(a=>a.Identificador == item.Especialidad.Identificador))
            {
                temp.Add(item.Especialidad);
            }
        }

        return temp;
    }
}

