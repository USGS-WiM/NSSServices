﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WiM.Resources;
using NSSDB;
using WiM.Hypermedia;
using Newtonsoft.Json;

namespace NSSService.Resources
{
    public class Scenario:IHypermedia
    {
        public int StatisticGroupID { get; set; }
        public string StatisticGroupName { get; set; }
        [XmlArrayItem("RegressionRegion")]
        public List<SimpleRegionEquation> RegressionRegions { get; set; }
        public List<Link> Links { get; set; }
    }//end Scenario

  
    public class SimpleRegionEquation
    {
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
    }
}