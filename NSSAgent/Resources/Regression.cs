using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WIM.Resources;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel.DataAnnotations;
using NSSDB.Resources;

namespace NSSAgent.Resources
{
    [Serializable]
    public class Regression : RegressionBase
    {
        public double? EquivalentYears { get; set; }

        [Required]
        public ExpectedValue Expected { get; set; }
        public bool ShouldSerializeExpected()
        { return Expected != null; }

        public PredictionInterval PredictionInterval { get; set; }
        public List<Parameter> Parameters { get; set; }
        public override RegressionBase Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (Regression)formatter.Deserialize(ms);
            }//end using
        }
    }
    public class ExpectedValue
    {
        [Required]
        public double Value { get; set; }
        [Required]
        public Dictionary<string, double> Parameters { get; set; }
        public IntervalBounds IntervalBounds { get; set; }
    }

}//end namespace
