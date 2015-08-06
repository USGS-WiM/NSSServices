//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

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

using RegressionService.Resources;

namespace RegressionService.Utilities.ServiceAgent
{
    public class FDCTMServiceAgent : ServiceAgent
    {
        #region "Properties"
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public FDCTMServiceAgent()
        { }
        #endregion
        #endregion
        #region "Methods"
        public FDCTMStruc GetRegionEquations(String stateID)
        {
            JObject flObj = base.GetJsonFromFile<JObject>(stateID, "FDCTM");
            return parseRegionEquations(flObj);
        }
        #endregion
        #region "Helper Methods"
        private FDCTMStruc parseRegionEquations(JObject json)
        {

            try
            {
                FDCTMStruc fdctm = new FDCTMStruc();

                fdctm.regressions = json["regressions"].ToList().Select(i => new regionEquation()
                {
                    Probability = (Double)i.SelectToken("probability"),
                    Equation = (String)i.SelectToken("equation")
                }).ToList();
               
                return fdctm;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        #region Structures
        public struct FDCTMStruc
        {
            public List<regionEquation> regressions { get; set; }
        }
        public struct regionEquation
        {
            public Double Probability { get; set; }
            public String Equation { get; set; }

        }
        
        #endregion
    }//end class
    
}//end namespace