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

namespace NSSAgent.Resources
{
    [XmlInclude(typeof(RegressionResult))]
    [Serializable]
    public abstract class RegressionBase
    {
        public Int32 ID { get; set; }
        public string Name { get; set; }
        [Required]
        public string code { get; set; }
        public string Description { get; set; }
        public Double? Value { get; set; }
        public Double? SEP { get; set; }
        public List<Error> Errors { get; set; }
        [XmlElement("UnitType")]
        [Required]
        public SimpleUnitType Unit { get; set; }
        [Required]
        public String Equation { get; set; }     
        public abstract RegressionBase Clone();
        public IntervalBounds IntervalBounds { get; set; }

    }
    [Serializable]
    public class RegressionResult : RegressionBase
    {
        public Double? EquivalentYears { get; set; }        
        public override RegressionBase Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (RegressionResult)formatter.Deserialize(ms);
            }//end using
        }
    }//end class
    [Serializable]
    public class Error
    {
        [Required]
        public Int32 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [Required]
        public Double Value { get; set; }
    }//end class
    [Serializable]
    public class IntervalBounds
    {
        public double Lower { get; set; }
        public double Upper { get; set; }
    }
    [Serializable]
    public class SimpleUnitType
    {
        [Required]
        public Int32 ID { get; set; }
        public string Unit { get; set; }
        public string Abbr { get; set; }       
        
        [XmlIgnore]
        [JsonIgnore]
        public double factor { get; set; }
    }//end class
}//end namespace
