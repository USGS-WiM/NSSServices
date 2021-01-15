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
using WIM.Resources.TimeSeries;

namespace NSSAgent.ServiceAgents
{
    internal class StationServiceAgent : ServiceAgentBase
    {
        #region Properties
        private Dictionary<string, string> Resources { get; set; }
        #endregion
        #region Constructor
        internal StationServiceAgent(NWISResource resource) : base(resource.baseurl)
        {
            this.Resources = resource.resources;
        }
        #endregion
        #region Methods
        public Station GetNWISStation(string nwisStationID)
        {
            var r = this.GetRequestInfo(stationservicetype.e_nwisstationinfo, new object[] { nwisStationID });
            var siteresult = this.ExecuteAsync<usgs_nwis>(r).Result;
            return new Station(siteresult.site);
        }

        public FlowTimeSeries GetFlowSeries(DateTime sDate, DateTime eDate, string stationID)
        {
            try
            {
                //http://nwis.waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb&site_no=04103500&referred_module=sw&period=&begin_date=1948-08-06&end_date=2014-08-06
                String ds = sDate.ToString("yyy-MM-dd");
                var r = this.GetRequestInfo(stationservicetype.e_nwisdv, new object[] { stationID, sDate.ToString("yyyy-MM-dd"), eDate.ToString("yyyy-MM-dd") });


                FlowTimeSeries fts = new FlowTimeSeries(stationID + " nwis series", "flow series extracted from nwis");
                var result = ExecuteAsync<String>(r).Result;
                fts.LoadNWISDailyValues(stationID,result );
                return fts;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
                    case stationservicetype.e_nwisurl:
                        resulturl = this.Resources["nwisStationurl"];
                        break;
                    case stationservicetype.e_nwisstationinfo:
                        resulturl = this.Resources["nwisStationInfo"];
                        break;
                    case stationservicetype.e_nwisdv:
                        resulturl = this.Resources["dailyvalue"];
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
            e_nwisurl =1,
            e_nwisstationinfo =2,
            e_nwisdv=3

        }

    }
}
