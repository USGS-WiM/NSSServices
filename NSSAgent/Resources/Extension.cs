using GeoAPI;
using GeoAPI.Geometries;
using NetTopologySuite;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
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
    }

    public static class GeometryExtensions
    {
        static readonly IGeometryServices _geometryServices = NtsGeometryServices.Instance;
        static readonly ICoordinateSystemServices _coordinateSystemServices
            = new CoordinateSystemServices(
                new CoordinateSystemFactory(),
                new CoordinateTransformationFactory(),
                new Dictionary<int, string>
                {
                // Coordinate systems:
                // (3857 and 4326 included automatically)

                // This coordinate system covers the area of our data.
                [4269] =
                    @"
                    GEOGCS[""NAD83"",DATUM[""North_American_Datum_1983"",SPHEROID[""GRS 1980"",6378137,298.257222101,AUTHORITY[""EPSG"",""7019""]],TOWGS84[0,0,0,0,0,0,0],AUTHORITY[""EPSG"",""6269""]],PRIMEM[""Greenwich"",0,AUTHORITY[""EPSG"",""8901""]],UNIT[""degree"",0.0174532925199433,AUTHORITY[""EPSG"",""9122""]],AUTHORITY[""EPSG"",""4269""]]
                    ",
                [102008] = @"
                    PROJCS[""North_America_Albers_Equal_Area_Conic"",GEOGCS[""GCS_North_American_1983"",DATUM[""North_American_Datum_1983"",SPHEROID[""GRS_1980"",6378137,298.257222101]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]],PROJECTION[""Albers_Conic_Equal_Area""],PARAMETER[""False_Easting"",0],PARAMETER[""False_Northing"",0],PARAMETER[""longitude_of_center"",-96],PARAMETER[""Standard_Parallel_1"",20],PARAMETER[""Standard_Parallel_2"",60],PARAMETER[""latitude_of_center"",40],UNIT[""Meter"",1],AUTHORITY[""EPSG"",""102008""]]
                    "
                });

        public static IGeometry ProjectTo(this IGeometry geometry, int srid)
        {
            var geometryFactory = _geometryServices.CreateGeometryFactory(srid);
            var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

            return GeometryTransform.TransformGeometry(
                geometryFactory,
                geometry,
                transformation.MathTransform);
        }
    }
}