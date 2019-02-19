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
using RestSharp;
using WiM.TimeSeries;
using WiM.Utilities.ServiceAgent;

using NSSService.Resources;
using System.Net;

namespace NSSService.Utilities.ServiceAgent
{
    public class StationServiceAgent: ServiceAgentBase   {
        #region "Properties"
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public StationServiceAgent(String BaseURL)
            : base(BaseURL)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                     | SecurityProtocolType.Tls11
                                                     | SecurityProtocolType.Tls12;
        }
        #endregion
        #endregion
        #region "Methods"

        public Station GetNWISStation(String stationID)
        {
            try
            {
                //
                RestRequest r = new RestRequest(String.Format(getURI(serviceType.e_nwis_info), stationID));


                site s = Execute<usgs_nwis>(r).Data.site;
                return new Station(s);
            }
            catch (Exception)
            {
                return new Station(stationID);
            }
        }

        public FlowTimeSeries GetFlowSeries(DateTime sDate, DateTime eDate, String stationID)
        {
            try
            {
                //http://nwis.waterdata.usgs.gov/nwis/dv?cb_00060=on&format=rdb&site_no=04103500&referred_module=sw&period=&begin_date=1948-08-06&end_date=2014-08-06
                String ds = sDate.ToString("yyy-MM-dd");
                RestRequest r = new RestRequest(String.Format(getURI(serviceType.e_nwis_ts), stationID, sDate.ToString("yyyy-MM-dd"), eDate.ToString("yyyy-MM-dd")));
                FlowTimeSeries fts = new FlowTimeSeries(stationID + " nwis series", "flow series extracted from nwis");

                fts.LoadNWISDailyValues(stationID, Convert.ToString(Execute(r)));
                return fts;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        #endregion
        #region "Helper Methods"
        private String getURI(serviceType sType)
        {
            string uri = string.Empty;
            switch (sType)
            {
                case serviceType.e_nwis_info:
                   //inventory?search_site_no={stationID}&search_site_no_match_type=exact&format=sitefile_output&sitefile_output_format=xml
                    uri = ConfigurationManager.AppSettings["nwisStationInfo"];
                    break;
                case serviceType.e_nwis_ts:
                    uri = ConfigurationManager.AppSettings["nwisDailyValue"];
                    break;
            }

            return uri;
        }//end getURL
        
        #endregion 
        #region Enumerations
        public enum serviceType
        {
            e_nwis_info,
            e_nwis_ts
        }

        #endregion
    }//end class
}//end namespace