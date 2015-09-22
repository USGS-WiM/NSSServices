using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WiM.Resources;
using NSSDB;


using Newtonsoft.Json;

namespace NSSService.Resources
{
    public class Scenario
    {
        public int StatisticGroupID { get; set; }
        public string StatisticGroupName { get; set; }
        [XmlArrayItem("RegressionRegion")]
        public List<SimpleRegionEquation> RegressionRegions { get; set; }
        
    }//end Scenario

    public class SimpleRegionEquation
    {
        public Int32 ID { get; set; }
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
        [XmlArrayItem("RegressionResult")]
        public List<RegressionResultBase> Results { get; set; }
    }
}