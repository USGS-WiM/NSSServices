using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSSDB;
using Newtonsoft.Json;


namespace FU_NSSDB.Resources
{
    public class FURegion : Region
    {
        public int oldID { get; set; }
        public List<FURegressionRegion> regressionregions { get; set;}

        public static FURegion FromDataReader(System.Data.IDataReader r)
        {
            return new FURegion()
            {
                oldID = r["ID"] is DBNull ? 0 : Convert.ToInt32(r["ID"]),
                Code = r["Code"] is DBNull ? "" : (r["Code"]).ToString(),
                Name = r["Name"] is DBNull ? "" : (r["Name"]).ToString(),
            };

        }

    }
    public class FURegressionRegion : RegressionRegion
    {
        public int oldID { get; set; }
        public List<FUEquation> FUequations { get; set; }
        public static FURegressionRegion FromDataReader(System.Data.IDataReader r)
        {
            return new FURegressionRegion()
            {
                oldID = r["RegionID"] is DBNull ? 0 : Convert.ToInt32(r["RegionID"]),
                Code = r["RegionID"] is DBNull ? "" : "GC" + (r["RegionID"]).ToString(),
                Name = r["RegionName"] is DBNull ? "" : (r["RegionName"]).ToString(),
                Description ="",
                Citation = FUCitation.FromDataReader(r)

            };
        }
    }
    public class FUCitation : Citation
    {
        public int oldID { get; set; }

        public static FUCitation FromDataReader(System.Data.IDataReader r)
        {
            return new FUCitation()
            {
                oldID = r["DataSourceID"] is DBNull ? 0 : Convert.ToInt32(r["DataSourceID"]),
                Title = r["Citation"] is DBNull ? "" : splitTitle((r["Citation"]).ToString(),true),
                Author = r["Citation"] is DBNull ? "" : splitTitle((r["Citation"]).ToString()),
                CitationURL = r["CitationURL"] is DBNull ? "" : (r["CitationURL"]).ToString()
            };

        }
        private static string splitTitle(string authertitle, bool getTitle = false)
        {
            Int32 splitlocation = authertitle.IndexOfAny("0123456789".ToCharArray());
            if (!getTitle)
                //auther
                return authertitle.Substring(0, splitlocation - 1);
            else
                //title
                return authertitle.Substring(splitlocation);
        }
    }
    public class FUEquation : Equation
    {
        public int oldID { get; set; }
        public UnitType otherunit { get; set; }        
        public static FUEquation FromDataReader(System.Data.IDataReader r)
        {
            return new FUEquation()
            {
                oldID = r["DepVarID"] is DBNull ? 0 : Convert.ToInt32(r["DepVarID"]),
                Equation1 = r["Equation"] is DBNull ? "" : (r["Equation"]).ToString().RemoveWhitespace(),
                DA_Exponent = r["ExpDA"] is DBNull ? (double?)null : Convert.ToDouble(r["ExpDA"]),
                EquivalentYears = r["EquivYears"] is DBNull ? (double?)null : Convert.ToDouble(r["EquivYears"]),
                OrderIndex = r["OrderIndex"] is DBNull ? (Int32?)null : Convert.ToInt32(r["OrderIndex"]),
                UnitType = FUUnitType.FromDataReader(r),
                PredictionInterval = FUPredictionInterval.FromDataReader(r),
                RegressionType = FURegressionType.FromDataReader(r),
                StatisticGroupType = FUStatisticGroupType.FromDataReader(r),
                EquationErrors = getEquationErrors(r),
                otherunit = new UnitType()
                {
                    Abbr = r["MetricAbbrev"].ToString(),
                    UnitSystemTypeID = 1
                }
            };
        }
        private static List<EquationError> getEquationErrors(System.Data.IDataReader r) {
            List<EquationError> eqErr = new List<EquationError>();
            if (!(r["StdErr"] is DBNull) && Convert.ToDouble(r["StdErr"]) > 1) eqErr.Add(new EquationError() { Value = Convert.ToDouble(r["StdErr"]), ErrorTypeID = 1 });
            if (!(r["EstErr"] is DBNull) && Convert.ToDouble(r["EstErr"]) > 1) eqErr.Add(new EquationError() { Value = Convert.ToDouble(r["EstErr"]), ErrorTypeID = 2 });
            if (!(r["PreErr"] is DBNull) && Convert.ToDouble(r["PreErr"]) > 1) eqErr.Add(new EquationError() { Value = Convert.ToDouble(r["PreErr"]), ErrorTypeID = 3 });
            return eqErr;
        }
    }    
    public class FUPredictionInterval : PredictionInterval
    {
        public int oldID { get; set; }

        public static FUPredictionInterval FromDataReader(System.Data.IDataReader r)
        {
            return new FUPredictionInterval()
            {
                oldID = r["DepVarID"] is DBNull ? 0 : Convert.ToInt32(r["DepVarID"]),
                BiasCorrectionFactor = r["BCF"] is DBNull ? (double?)null : Convert.ToDouble(r["BCF"]),
                Student_T_Statistic = r["t"] is DBNull ? (double?)null : Convert.ToDouble(r["t"]),
                Variance = r["Variance"] is DBNull ? (double?)null : Convert.ToDouble(r["Variance"]),
                CovarianceMatrix = "",
                XIRowVector = getrowVectorString(r["XiVector"].ToString())
            };

        }
        private static string getrowVectorString(string rowvector) {
            if (string.IsNullOrEmpty(rowvector)) return string.Empty;
            var str = rowvector.Split(':').Select(s=>s.Trim());
            return JsonConvert.SerializeObject(str);
        }
    }
    public class FUVariable : Variable
    {
        public int oldID { get; set; }
        public UnitType otherunit { get; set; }

        public static FUVariable FromDataReader(System.Data.IDataReader r)
        {
            return new FUVariable()
            {
                oldID = r["ParmID"] is DBNull ? 0 : Convert.ToInt32(r["ParmID"]),
                MinValue = r["Min"] is DBNull ? (double?)null : Convert.ToDouble(r["Min"]),
                MaxValue = r["Max"] is DBNull ? (double?)null : Convert.ToDouble(r["Max"]),
                VariableType = FUVariableType.FromDataReader(r),
                UnitType = FUUnitType.FromDataReader(r),
                otherunit = new UnitType() {
                     Abbr = r["MetricAbbrev"].ToString(),
                     UnitSystemTypeID =1
                }
                
            };

        }
    }

    public class FUVariableType:VariableType
    {
        public int oldID { get; set; }

        public static FUVariableType FromDataReader(System.Data.IDataReader r)
        {
            return new FUVariableType()
            {
                oldID = r["ParmID"] is DBNull ? 0 : Convert.ToInt32(r["ParmID"]),
                Code = r["StatLabel"] is DBNull ? "" : (r["StatLabel"]).ToString()
            };

        }
    }
    public class FURegressionType : RegressionType
    {
        public int oldID { get; set; }

        public static FURegressionType FromDataReader(System.Data.IDataReader r)
        {
            return new FURegressionType()
            {
                oldID = r["StatisticTypeID"] is DBNull ? 0 : Convert.ToInt32(r["StatisticTypeID"]),
                Code = r["StatLabel"] is DBNull ? "" : (r["StatLabel"]).ToString()
            };

        }
    }
    public class FUStatisticGroupType : StatisticGroupType
    {
        public int oldID { get; set; }

        public static FUStatisticGroupType FromDataReader(System.Data.IDataReader r)
        {
            return new FUStatisticGroupType()
            {
                oldID = r["DepVarID"] is DBNull ? 0 : Convert.ToInt32(r["DepVarID"]),
                Code = r["StatisticTypeCode"].ToString()
            };

        }
    }
    public class FUUnitType : UnitType
    {
        public int oldID { get; set; }

        public static FUUnitType FromDataReader(System.Data.IDataReader r)
        {
            return new FUUnitType()
            {
                oldID = r["UnitID"] is DBNull ? 0 : Convert.ToInt32(r["UnitID"]),
                Abbr = r["EnglishAbbrev"] is DBNull ? "" : (r["EnglishAbbrev"]).ToString(),
                UnitSystemTypeID = 2
            };

        }
    }
    public class FUCovariance {
        public int oldID { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public double Value { get; set; }

        public static FUCovariance FromDataReader(System.Data.IDataReader r)
        {
            return new FUCovariance()
            {
                oldID = r["DepVarID"] is DBNull ? 0 : Convert.ToInt32(r["DepVarID"]),
                Row = r["Row"] is DBNull ? 0 : Convert.ToInt32(r["Row"]),
                Column = r["Col"] is DBNull ? 0 : Convert.ToInt32(r["Col"]),
                Value = r["Value"] is DBNull ? 0 : Convert.ToDouble(r["Value"]),
            };

        }
    }
}
