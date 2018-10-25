using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using NSSDB;
using Newtonsoft.Json;
using WiM.Hypermedia;

namespace NSSAgent.Resources
{
    public class Scenario:IHypermedia
    {
        public int StatisticGroupID { get; set; }
        public string StatisticGroupName { get; set; }
        [XmlArrayItem("RegressionRegion")]
        public List<SimpleRegionRegion> RegressionRegions { get; set; }
        public List<Link> Links { get; set; }
    }//end Scenario

  
    public class SimpleRegionRegion
    {
        public SimpleRegionRegion() { }

        [JsonConstructor]
        public SimpleRegionRegion(List<RegressionResult> results) {
            if (results != null)
                this.Results = results.Cast<RegressionResultBase>().ToList();
        }
        public Int32 ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double? PercentWeight { get; set; }
        public bool ShouldSerializePercentWeight()
        { return PercentWeight.HasValue; }
        public List<Parameter> Parameters { get; set; }
        [XmlArrayItem("RegressionResult")]
        public List<RegressionResultBase> Results { get; set; }
        public List<Extension> Extensions { get; set; }
        public bool ShouldSerializeExtension()
        { return Extensions != null || Extensions.Count > 1; }

        public string Disclaimer { get; set; }
        public bool ShouldSerializeDisclaimer()
        { return !string.IsNullOrEmpty(Disclaimer) ; }
    }
}