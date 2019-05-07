using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NSSDB.Resources;
using WIM.Hypermedia;
using WIM.Resources;


namespace NSSAgent.Resources
{
    public class Scenario:IHypermedia
    {
        [Required]
        public int StatisticGroupID { get; set; }
        public string StatisticGroupCode { get; set; }
        public string StatisticGroupName { get; set; }
        [XmlArrayItem("RegressionRegion")]
        [Required]
        public List<SimpleRegressionRegion> RegressionRegions { get; set; }
        public List<Link> Links { get; set; }

    }//end Scenario

  
    public class SimpleRegressionRegion
    {
        public SimpleRegressionRegion() { }

        [JsonConstructor]
        public SimpleRegressionRegion(List<RegressionResult> obj) {
            if (obj != null)
                this.Results = obj.Cast<RegressionBase>().ToList();

        }
        [Required]
        public Int32 ID { get; set; } //x
        public string Name { get; set; }
        public string Code { get; set; }//x
        public double? PercentWeight { get; set; }
        public Double? AreaSqMile { get; set; }
        public bool ShouldSerializePercentWeight()
        { return PercentWeight.HasValue; }
        public List<Parameter> Parameters { get; set; }
        [XmlArrayItem("RegressionResult")]
        public List<RegressionBase> Results { get; set; }
        public List<Regression> Regressions { get; set; }
        public List<Extension> Extensions { get; set; }
        public bool ShouldSerializeExtension()
        { return Extensions != null || Extensions.Count > 1; }

        public string Disclaimer { get; set; }
        public bool ShouldSerializeDisclaimer()
        { return !string.IsNullOrEmpty(Disclaimer) ; }
    }
   
}