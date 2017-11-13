using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WiM.Resources;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace NSSService.Resources
{
    [XmlInclude(typeof(RegressionResult))]
    [Serializable]
    public abstract class RegressionResultBase
    {
        public string Name { get; set; }
        public string code { get; set; }
        public string Description { get; set; }
        public Double? Value { get; set; }
        private List<Error> _errors;
        public List<Error> Errors { get { return _errors; } set { _errors = value; } }
        private SimpleUnitType _unit;
        [XmlElement("UnitType")]
        public SimpleUnitType Unit { get { return _unit; } set { _unit = value; } }
        public String Equation { get; set; }     
        public abstract RegressionResultBase Clone();
        public IntervalBounds IntervalBounds { get; set; }

    }
    [Serializable]
    public class RegressionResult : RegressionResultBase
    {
        public Double? EquivalentYears { get; set; }        
        public override RegressionResultBase Clone()
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
        public string Name { get; set; }
        public string Code { get; set; }
        public Double? Value { get; set; }
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
        public Int32 ID { get; set; }
        public string Unit { get; set; }
        public string Abbr { get; set; }       
        
        [XmlIgnore]
        [JsonIgnore]
        public double factor { get; set; }
    }//end class
}//end namespace
