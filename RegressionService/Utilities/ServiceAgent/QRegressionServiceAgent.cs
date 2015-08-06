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
    public class QRegressionServiceAgent : ServiceAgent
    {
        #region "Properties"
        public String Region { get; set; }
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public QRegressionServiceAgent(string region)
        { 
            this.Region = region; 
        }
        #endregion
        #endregion
        #region "Methods"
        public QRegressionStruc GetRegionEquations(String stateID, string modelType)
        {
            JObject flObj = base.GetJsonFromFile<JObject>(stateID, modelType);
            return parseRegionEquations(flObj);
        }
        #endregion
        #region "Helper Methods"
        private QRegressionStruc parseRegionEquations(JObject json)
        {
            try
            {
                QRegressionStruc qReg = new QRegressionStruc();
                if(json["regions"] != null)
                    json = (JObject)json["regions"].Where(r => String.Equals(r.Value<String>("region"), this.Region)).First();

                qReg.regressions = json["regressions"].ToList().Select(i => new regionEquation()
                {
                    Probability = (Double)i.SelectToken("interval"),
                    Equation = (String)i.SelectToken("equation")
                }).ToList();

                return qReg;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        #region Structures
        public struct QRegressionStruc
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