//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
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
using System.Collections.Generic;
using System.Text;
using WIM.Utilities.ServiceAgent;
using NSSAgent.Resources;
using System.Linq;
using System.Collections;
using WIM.Utilities.Resources;

namespace NSSAgent.ServiceAgents
{
    internal class StationServiceAgent : ServiceAgentBase
    {
        #region Properties
        private Dictionary<string, string> Resources { get; set; }
        #endregion
        #region Constructor
        internal StationServiceAgent(Resource resource) : base(resource.baseurl)
        {
            this.Resources = resource.resources;
        }
        #endregion
        #region Methods
       
        #endregion
        #region HelperMethods
        private RequestInfo GetRequestInfo(stationservicetype requestType, object[] args=null) {
            RequestInfo requestInfo = null;
            try
            {
                requestInfo = new RequestInfo(string.Format(GetResourcrUrl(requestType),args));
                
                return requestInfo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private String GetResourcrUrl(stationservicetype filetype)
        {
            try
            {
                String resulturl = string.Empty;
                switch (filetype)
                {
                    case stationservicetype.e_nwis:
                        resulturl = this.Resources["nwisStationurl"];
                        break;
                   
                }//end switch
                return resulturl;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion
        private enum stationservicetype
        {
            e_nwis =1
        }

    }
}
