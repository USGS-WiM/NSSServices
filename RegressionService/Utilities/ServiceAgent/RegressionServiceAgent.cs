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

using RegressionService.Resources;

namespace RegressionService.Utilities.ServiceAgent
{
    public class RegressionServiceAgent:ServiceAgent
    {
        #region "Properties"        
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public RegressionServiceAgent()
        {

        }
        #endregion
        #endregion
        #region "Methods"

        public List<RegressionService.Resources.Parameter> GetParameterList(string stateID, string modelType, string region = null)
        {
            try
            {
                String parameters = string.Empty;
                JObject flObj = base.GetJsonFromFile<JObject>(stateID, modelType);
            
                if (string.IsNullOrEmpty(region)) parameters = flObj["parameters"].ToString();
            
                else {
                   var regionObj = flObj["regions"].Where(r => String.Equals(r.Value<String>("region"), region)).First();
                    parameters = regionObj["parameters"].ToString();
                }

                return JsonConvert.DeserializeObject<List<RegressionService.Resources.Parameter>>(parameters);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
        #region "Helper Methods"

        #endregion 
    }
    public class ServiceAgent
    {
        #region "Properties"        
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public ServiceAgent()
        {

        }
        #endregion
        #endregion
        #region "Methods"
        
        public T GetJsonFromFile<T>(String stateID, String model)
        {
            T modelList;
            try
            {
                //get file
                using (StreamReader r = new StreamReader(this.GetJsonFile(stateID, model)))
                {
                    string json = r.ReadToEnd();

                    modelList = JsonConvert.DeserializeObject<T>(json);
                    
                }//end using

                return modelList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
        #region "Helper Methods"

        private String GetJsonFile(string stateID, string fileName)
        {
            String result = Path.Combine(new String[] { AppDomain.CurrentDomain.BaseDirectory, "Assets", "Data", stateID, fileName + ".json" });
            return result;
        }
        #endregion 
        
    }//end class

    public class StationServiceAgent:ServiceAgentBase
    {
        #region "Properties"
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public StationServiceAgent(String BaseURL)
            : base(BaseURL)
        {
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
            catch (Exception e)
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

    public abstract class ServiceAgentBase
    {
        #region "Events"

        #endregion

        #region Properties & Fields

        readonly string _accountSid;
        readonly string _secretKey;

        private RestClient client = new RestClient();
        #endregion

        #region Constructors
        public ServiceAgentBase(string BaseUrl)
        {
            client.BaseUrl = BaseUrl;
        }

        public ServiceAgentBase(string accountSid, string secretKey, string baseUrl)
            : this(baseUrl)
        {
            _accountSid = accountSid;
            _secretKey = secretKey;
        }
        #endregion

        #region Methods
        public void ExecuteAsync<T>(RestRequest request, Action<T> CallBackOnSuccess, Action<string> CallBackOnFail) where T : new()
        {
            // request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request

            client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    CallBackOnFail(response.ErrorMessage);
                }
                else
                {
                    CallBackOnSuccess(response.Data);
                }
            });


        }//end ExecuteAsync<T>

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : new()
        {
            IRestResponse<T> result = null;
            if (request == null) throw new ArgumentNullException("request");

            AutoResetEvent waitHandle = new AutoResetEvent(false);
            Exception exception = null;

            client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ResponseStatus == ResponseStatus.Error)
                {
                    exception = new Exception(response.ResponseStatus.ToString());
                    //release the Event
                    waitHandle.Set();
                }
                else
                {
                    result = response;
                    //release the Event
                    waitHandle.Set();
                }
            });

            //wait until the thread returns
            waitHandle.WaitOne();

            return result;
        }//end Execute<T>

        public Object Execute(IRestRequest request)
        {
            IRestResponse response = null;
            if (request == null) throw new ArgumentNullException("request");

            response = client.Execute(request) as IRestResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                switch(response.ContentType)
                {
                    case "text/html":
                        throw new Exception(response.Content);
                        break;
                    case "text/plain":
                        return response.Content;
                    default:
                        return JsonConvert.DeserializeObject(response.Content);
                }
                
            }//else
            else
            {
                throw new Exception(response.ErrorMessage);
            }
        }//endExecute

        protected RestRequest getRestRequest(string URI, object Body)
        {
            RestRequest request = new RestRequest(URI);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", Body, ParameterType.RequestBody);
            request.Method = Method.POST;
            request.Timeout = 600000;

            return request;
        }//end BuildRestRequest

        #endregion

    }//end class ServiceAgentBase
}//end namespace