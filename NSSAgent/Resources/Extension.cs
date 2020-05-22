using GeoAPI;
using GeoAPI.Geometries;
using NetTopologySuite;
using NetTopologySuite.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NSSAgent.Resources
{
    public class Extension
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String Code { get; set; }
        public List<ExtensionParameter> Parameters { get; set; }
        public IExtensionResult Result { get; set; }
    }

    public class ExtensionParameter
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public String Code { get; set; }
        public dynamic Value { get; set; }
        public bool ShouldSerializeValue()
        { return !Object.ReferenceEquals(null, Value); }
    }//end ExtensionParameter

    public static class DataReaderExtensions
    {
        public static IEnumerable<T> Select<T>(this IDataReader reader,
                               Func<IDataReader, T> projection)
        {
            while (reader.Read())
            {
                yield return projection(reader);
            }
        }
        public static Boolean HasColumn(this IDataReader reader,
                                       string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }
        public static T? GetValueOrNull<T>(this string valueAsString)
        where T : struct
        {
            if (string.IsNullOrEmpty(valueAsString))
                return null;
            return (T)Convert.ChangeType(valueAsString, typeof(T));
        }

        public static string RemoveWhitespace(this string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public static double Round(this double num)
        {
            if (num == 0)
                return 0;

            decimal scale = (decimal)Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(num))) + 1);
            double temp = (double)(scale * Math.Round((decimal)num / scale, 3));

            return temp;
        }
    }
}