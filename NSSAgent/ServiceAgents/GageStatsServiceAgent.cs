//------------------------------------------------------------------------------
//----- ServiceAgent -------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2021 WIM - USGS

//    authors:  Katrin E. Jacobsen USGS Web Informatics and Mapping
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
using WIM.Resources.TimeSeries;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NSSAgent.ServiceAgents
{
    internal class GageStatsServiceAgent : ServiceAgentBase
    {
        #region Properties
        private GageStatsResource Resource { get; set; }
        #endregion
        #region Constructor
        internal GageStatsServiceAgent(GageStatsResource resource) : base(resource.baseurl)
        {
            this.Resource = resource;
        }
        #endregion
        #region Methods
        public async Task<GageStatsStation> GetGageStatsStationAsync(string stationCode)
        {
            var r = this.GetRequestInfo(gagestatsservicetype.e_stationinfo, new object[] { stationCode });
            var siteresult = await ExecuteAsync<string>(r);
            var station = JObject.Parse(siteresult);
            GageStatsStation stat_object = station.ToObject<GageStatsStation>();
            return stat_object;
        }

        #endregion
        #region HelperMethods
        private RequestInfo GetRequestInfo(gagestatsservicetype requestType, object[] args = null)
        {
            RequestInfo requestInfo = null;
            try
            {
                requestInfo = new RequestInfo(this.Resource.baseurl + string.Format(GetResourceUrl(requestType), args));

                return requestInfo;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private String GetResourceUrl(gagestatsservicetype filetype)
        {
            try
            {
                String resulturl = string.Empty;
                switch (filetype)
                {
                    case gagestatsservicetype.e_stationinfo:
                        resulturl = this.Resource.resources["gageStatsStationInfo"];
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
        private enum gagestatsservicetype
        {
            e_stationinfo = 1

        }

    }
}
