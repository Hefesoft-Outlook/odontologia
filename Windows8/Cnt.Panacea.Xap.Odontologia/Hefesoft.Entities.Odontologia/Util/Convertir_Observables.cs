using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

/// <summary>
/// Convierte una entidad observable a otra
/// </summary>
public static class Convertir_Observables
{
     /// <summary>
     /// Transforma una entidad en otra copiando sus propiedades
     /// </summary>
     /// <typeparam name="P" a la que sedea convertir></typeparam>
     /// <typeparam name="T" Fuente></typeparam>
     /// <param name="source"></param>
     /// <returns></returns>
     public static P ConvertirEntidades<P,T>(this T source) where T : class where P : class
     {
         Mapper.CreateMap<T, P>();
         return Mapper.Map<P>(source);
     }


     public static ObservableCollection<P> ConvertirObservables<P, T>(this ObservableCollection<T> source, ObservableCollection<P> lst)
         where T : class
         where P : class
     {

         foreach (var item in source)
         {
             try
             {
                 Mapper.CreateMap<T, P>();
                 P elemento = Mapper.Map<P>(item);
                 lst.Add(elemento);
             }
             catch
             {

             }
         }

         return lst;
     }

     public static IEnumerable<P> ConvertirIEnumerable<P, T>(this IEnumerable<T> source, IEnumerable<P> lst)
         where T : class
         where P : class
     {

         foreach (var item in source)
         {
             try
             {
                 Mapper.CreateMap<T, P>();
                 P elemento = Mapper.Map<P>(item);
                 lst.ToList().Add(elemento);
             }
             catch
             {

             }
         }

         return lst;
     }
}

