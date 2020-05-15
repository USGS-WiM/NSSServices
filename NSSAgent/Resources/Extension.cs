using GeoAPI;
using GeoAPI.Geometries;
using NetTopologySuite;
using NetTopologySuite.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

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

        public static Geometry ProjectGeometry(this Geometry geom, int srid)
        {
            var FromWKT = getWellKnownText(geom.SRID);
            var ToWKT = getWellKnownText(srid);
            var SourceCoordSystem = new CoordinateSystemFactory().CreateFromWkt(FromWKT);
            var TargetCoordSystem = new CoordinateSystemFactory().CreateFromWkt(ToWKT);

            var trans = new CoordinateTransformationFactory().CreateFromCoordinateSystems(SourceCoordSystem, TargetCoordSystem);

            var projGeom = Transform(geom, trans.MathTransform);
            projGeom.SRID = srid;

            return projGeom;
        }
        static string getWellKnownText(int wkid)
        {
            switch (wkid)
            {
                case 4326:
                    return @"
                        GEOGCS[""WGS 84"", DATUM[""WGS_1984"", SPHEROID[""WGS 84"", 6378137, 298.257223563, AUTHORITY[""EPSG"", ""7030""]], AUTHORITY[""EPSG"", ""6326""]], PRIMEM[""Greenwich"", 0, AUTHORITY[""EPSG"", ""8901""]],UNIT[""degree"", 0.01745329251994328,AUTHORITY[""EPSG"", ""9122""]],AUTHORITY[""EPSG"", ""4326""]]
                    ";
                case 3857:
                    return @"
                        PROJCS[""WGS 84 / Pseudo - Mercator"",GEOGCS[""Popular Visualisation CRS"",DATUM[""Popular_Visualisation_Datum"", SPHEROID[""Popular Visualisation Sphere"", 6378137, 0, AUTHORITY[""EPSG"", ""7059""]], TOWGS84[0, 0, 0, 0, 0, 0, 0], AUTHORITY[""EPSG"", ""6055""]],
                        PRIMEM[""Greenwich"", 0, AUTHORITY[""EPSG"", ""8901""]],UNIT[""degree"", 0.01745329251994328, AUTHORITY[""EPSG"", ""9122""]], AUTHORITY[""EPSG"", ""4055""]], UNIT[""metre"", 1, AUTHORITY[""EPSG"", ""9001""]], PROJECTION[""Mercator_1SP""], PARAMETER[""central_meridian"", 0],
                        PARAMETER[""scale_factor"", 1], PARAMETER[""false_easting"", 0], PARAMETER[""false_northing"", 0], AUTHORITY[""EPSG"", ""3785""], AXIS[""X"", EAST], AXIS[""Y"", NORTH]]
                    ";
                default:
                    //1020008
                    return @"
                        PROJCS[""North_America_Albers_Equal_Area_Conic"",GEOGCS[""GCS_North_American_1983"",DATUM[""North_American_Datum_1983"",SPHEROID[""GRS_1980"",6378137,298.257222101]],PRIMEM[""Greenwich"",0],UNIT[""Degree"",0.017453292519943295]],PROJECTION[""Albers_Conic_Equal_Area""],PARAMETER[""False_Easting"",0],PARAMETER[""False_Northing"",0],PARAMETER[""longitude_of_center"",-96],PARAMETER[""Standard_Parallel_1"",20],PARAMETER[""Standard_Parallel_2"",60],PARAMETER[""latitude_of_center"",40],UNIT[""Meter"",1],AUTHORITY[""EPSG"",""102008""]]
                    ";
            }
        }

        static Geometry Transform(Geometry geom, MathTransform transform)
        {
            geom = geom.Copy();
            geom.Apply(new MTF(transform));
            return geom;
        }

        sealed class MTF : NetTopologySuite.Geometries.ICoordinateSequenceFilter
        {
            private readonly MathTransform _mathTransform;

            public MTF(MathTransform mathTransform) => _mathTransform = mathTransform;

            public bool Done => false;
            public bool GeometryChanged => true;
            public void Filter(CoordinateSequence seq, int i)
            {
                double x = seq.GetX(i);
                double y = seq.GetY(i);
                double z = seq.GetZ(i);
                _mathTransform.Transform(ref x, ref y, ref z);
                seq.SetX(i, x);
                seq.SetY(i, y);
                seq.SetZ(i, z);
            }
        }
    }
}