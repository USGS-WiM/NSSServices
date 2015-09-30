using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WiM.Resources;
using Newtonsoft.Json;

namespace NSSService.Resources
{
    [XmlInclude(typeof(RegressionResult))]
    public abstract class RegressionResultBase
    {
        public string Name { get; set; }
        public Double? Value { get; set; }
        public List<Error> Errors { get; set; }
        [XmlElement("UnitType")]
        public SimpleUnitType Unit { get; set; }
        public String Equation { get; set; }
    }

    public class RegressionResult : RegressionResultBase
    {
        public string Description { get; set; }
        public Double? EquivalentYears { get; set; }
        public IntervalBounds IntervalBounds { get; set; }
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
