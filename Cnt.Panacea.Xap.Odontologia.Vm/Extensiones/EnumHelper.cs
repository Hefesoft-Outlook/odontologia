using System;
using System.Collections.Generic;
using System.Reflection;
public static class EnumHelper
{
    /// <summary>
    /// Clase generada para encontrar los ancestros al interior de una grilla
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T[] GetValues<T>()
    {

        Type enumType = typeof(T);

        if (!enumType.IsEnum)

            throw new ArgumentException("Type ‘" + enumType.Name + "’ is not an enum");


        List<T> values = new List<T>();

        FieldInfo[] fields = enumType.GetFields();



        foreach (FieldInfo field in fields)
        {

            if (field.IsLiteral)
            {

                object value = field.GetValue(enumType);

                values.Add((T)value);

            }

        }
        return values.ToArray();

    }

}
