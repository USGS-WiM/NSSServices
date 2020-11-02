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
        public string Description { get; set; }
        public int? StatusID { get; set; }
        public int? MethodID { get; set; }
        public int? CitationID { get; set; }
        public double? PercentWeight { get; set; }
        public Double? AreaSqMile { get; set; }
        public bool ShouldSerializePercentWeight()
        { return PercentWeight.HasValue; }
        public List<Parameter> Parameters { get; set; }
        [XmlArrayItem("RegressionResult")]
        public List<RegressionBase> Results { get; set; }
        public List<Regression> Regressions { get; set; }
        public List<Extension> Extensions { get; set; }
        public bool ShouldSerializeExtensions()
        { return Extensions?.Any() == true; }

        public string Disclaimer { get; set; }
        public bool ShouldSerializeDisclaimer()
        { return !string.IsNullOrEmpty(Disclaimer) ; }
    }
   
}