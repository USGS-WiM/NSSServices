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
        public string Description { get; set; }
        public Double? Value { get; set; }
        [NonSerialized]
        private List<Error> _errors;
        public List<Error> Errors { get { return _errors; } set { _errors = value; } }

        [NonSerialized]
        private SimpleUnitType _unit;
        [XmlElement("UnitType")]
        public SimpleUnitType Unit { get { return _unit; } set { _unit = value; } }
        public String Equation { get; set; }
        public abstract RegressionResultBase Clone();

    }
    [Serializable]
    public class RegressionResult : RegressionResultBase
    {
        public Double? EquivalentYears { get; set; }
        [NonSerialized]
        private IntervalBounds _intervalBounds;
        public IntervalBounds IntervalBounds { get { return _intervalBounds; } set { _intervalBounds = value; } }
        public override RegressionResultBase Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (RegressionResult)formatter.Deserialize(ms);
            }
        }
    }//end class
    public class Error
    {
        public string Name { get; set; }
        public Double? Value { get; set; }
    }//end class
    public class IntervalBounds
    {
        public double Lower { get; set; }
        public double Upper { get; set; }
    }
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
