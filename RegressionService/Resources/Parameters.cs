using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace RegressionService.Resources
{
    [JsonObject("parameters")]
    [XmlRoot("parameters")]
    public class Parameter
    {
        public String name { get; set; }
        public String description { get; set; }
        public String code { get; set; }
        public String unit { get; set; }
        public Double? value { get; set; }
        public bool ShouldSerializevalue()
        { return value.HasValue; }
        public Limit limits { get; set; }        

        #region Structures
        public struct Limit
        {
            public Double max { get; set; }
            public Double min { get; set; }
        }
    #endregion

    }//end PARAMETER
}