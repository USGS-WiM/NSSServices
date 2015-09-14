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
        [XmlElement("StatisticGroupType")]
        public SimpleStatisticGroupType StatisticGroupType { get; set; }
        [XmlArrayItem("RegressionRegion")]
        public List<SimpleRegionEquation> RegressionRegions { get; set; }
    }//end Scenario

    public class SimpleStatisticGroupType
    {
        public int  ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }//end subregion

    public class SimpleRegionEquation
    {
        public Int32 ID { get; set; }
        public string Name { get; set; }
        public List<Parameter> Parameters { get; set; }
    }

    public class SimpleEquationType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }//end subregion

}