//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2013 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   The service agent is responsible for initiating the service call, 
//              capturing the data that's returned and forwarding the data back to 
//              the requestor.
//
//discussion:   delegated hunting and gathering responsibilities.   
//
//    

using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp.Serializers;
using RestSharp;
using WiM.TimeSeries;
using WiM.Utilities.ServiceAgent;

using NSSService.Resources;

namespace NSSService.Utilities.ServiceAgent
{
    public class FlowAnywhereServiceAgent : ServiceAgentBase
    {
        #region "Properties"
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FlowAnywhereServiceAgent()
        { }
        #endregion
        #endregion
        #region "Methods"
        public regionEquation GetRegionEquations(String stateID, Int32 region)
        {
            JObject flaObj = base.GetJsonFromFile<JObject>(stateID, "FLA");
            List<regionEquation> regEqList = parseRegionEquations(flaObj);

            return regEqList.FirstOrDefault(r => r.region == region);
        }
        #endregion
        #region "Helper Methods"
        private List<regionEquation> parseRegionEquations(JObject json)
        {

            try
            {
                regionEquation regionEq = new regionEquation();

                return json["regions"].Where(f => isRegionEquation(f, out regionEq)).Select(f => regionEq).ToList<regionEquation>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Boolean isRegionEquation(JToken jobj, out regionEquation req)
        {

            try
            {
                req = new regionEquation()
                {
                    region = (Int32)jobj.SelectToken("region"),
                    constant1 = (Double)jobj.SelectToken("constant1"),
                    constant2 = (Double)jobj.SelectToken("constant2"),
                    constant3 = (Double)jobj.SelectToken("constant3"),
                };

                return true;
            }
            catch (Exception)
            {
                req = new regionEquation();
                return false;
            }
        }

        #endregion
        #region Structures
        public struct regionEquation
        {
            public Int32 region { get; set; }
            public Double constant1 { get; set; }
            public Double constant2 { get; set; }
            public Double constant3 { get; set; }
            public Double maxarea { get; set; }
            public Double minarea { get; set; }

        }
        #endregion
    }
}