using Cnt.Panacea.Entities.Odontologia;
using Cnt.Panacea.Entities.Parametrizacion;
using Cnt.Panacea.Xap.Odontologia.Vm.Extensiones.Clases;
using Cnt.Panacea.Xap.Odontologia.Vm.Util.Plan_Tratamiento;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


public static class Collection
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
    {
        if (enumerableList != null)
        {

            var observableCollection = new ObservableCollection<T>();

            foreach (var item in enumerableList)
                observableCollection.Add(item);


            return observableCollection;
        }
        return null;
    }

    public static ObservableCollection<T> inicializarListaSiEsVacia<T>(this ObservableCollection<T> enumerableList)
    {
        if (enumerableList == null)
        {
            enumerableList = new ObservableCollection<T>();
        }

        return enumerableList;
    }

    public static bool validaCadaSuperficieConDiagnosticoTieneProcedimiento(this IEnumerable<Cnt.Panacea.Xap.Odontologia.Vm.Odontograma.Odontograma> lst)
    {
        var valido = true;
        foreach (var item in lst)
        {
            valido = item.DiagnosticoProcedimiento.lst.validarDiagnosticoConSuperficieTieneProcedimientoExtend();

            if (!valido)
            {
                break;
            }
        }
        return valido;
    }

    public static bool validarDiagnosticoConSuperficieTieneProcedimientoExtend(this IEnumerable<DiagnosticoProcedimiento_Extend> item)
    {
        var valido = true;
        // Se pregunta si hay algo que validar
        if (item.Any())
        {
            //Por cada superficie con diagnostico
            var diagnosticos = item.Where(a => a.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Diagnostico);
            if (diagnosticos != null && diagnosticos.Any())
            {
                foreach (var diag in diagnosticos.ToList())
                {
                    //i igual tiene un procedimiento pero para la superficie completa
                    valido = item.Any(a => (a.Superficie == diag.Superficie || a.Superficie == "Pieza_Completa") && a.ConfigurarDiagnosticoProcedimOtraEntity.TipoPanel == TipoPanel.Procedimiento);

                    if (!valido)
                    {
                        break;
                    }
                }
            }
            else
            {
                valido = true;
                return valido;
            }
        }
        else
        {
            valido = true;
        }
        return valido;
    }

    public static bool validarDiagnosticoConSuperficieTieneProcedimientoSuperficie(this IEnumerable<ConfigurarDiagnosticoProcedimOtraEntity> item)
    {
        // Se pregunta si hay algo que validar
        if (item.Any())
        {
            // Si tiene una superficie con diagnostico y procedimiento
            var result = item.Any(a => a.TipoPanel == Cnt.Panacea.Entities.Odontologia.TipoPanel.Diagnostico) &&
                         item.Any(a => a.TipoPanel == Cnt.Panacea.Entities.Odontologia.TipoPanel.Procedimiento);
            return result;
        }
        else
        {
            return true;
        }
    }

    public static ObservableCollection<T> inicializarListaYLimpiar<T>(this ObservableCollection<T> enumerableList)
    {
        if (enumerableList == null)
        {
            enumerableList = new ObservableCollection<T>();
        }

        if (enumerableList.Any())
        {
            enumerableList.Clear();
        }

        return enumerableList;
    }

    public static List<EspecialidadEntity> obtenerEspecialidades(this IEnumerable<ProcedimientoEspecialidadSedeEntity> lst)
    {
        List<EspecialidadEntity> temp = new List<EspecialidadEntity>();

        foreach (var item in lst)
        {
            if (!temp.Any(a => a.Identificador == item.Especialidad.Identificador))
            {
                temp.Add(item.Especialidad);
            }
        }

        return temp;
    }

    public static bool validarSesionesConfiguradas(this IEnumerable<ProcedimientosGrillaPlanTratamiento> lst)
    {
        var valido = true;
        foreach (var item in lst)
        {
            if (item.NumeroSesionesValor == 0)
            {
                valido = false;
                break;
            }
        }

        return valido;
    }

    public static List<T> Unique<KEY, T>(this List<T> InputList, Func<T, KEY> func)
    {
        if (func == null)
            throw new ArgumentNullException("Indique la funcion de comparación");

        if (InputList == null)
        { return null; }

        if (InputList.Count == 0)
        { return InputList; }


        Dictionary<KEY, T> uniqueDictionary = new Dictionary<KEY, T>();

        foreach (var item in InputList)
        {
            KEY k = func.Invoke(item);

            if (!uniqueDictionary.ContainsKey(k))
            {
                uniqueDictionary.Add(k, item);
            }    
        }

        Dictionary<KEY, T>.Enumerator e = uniqueDictionary.GetEnumerator();

        List<T> uniqueList = new List<T>();
        while (e.MoveNext())
        {
            uniqueList.Add(e.Current.Value);
        }

        return uniqueList;
    } 
}

