using NSSAgent.ServiceAgents;
using System;
using System.Collections.Generic;
using WIM.Resources.TimeSeries;

namespace NSSAgent.Resources
{
    public class Station
    {
        #region "Properties"
        public String StationID { get; set; }
        public String Name { get; set; }
        public Double? DrainageArea_sqMI { get; set; }
        public Double Latitude_DD { get; set; }
        public Double Longitude_DD { get; set; }
        public FlowTimeSeries Discharge { get; private set; }
        public SortedDictionary<Double, Double> ExceedanceProbabilities { get; set; }
        public String URL { get; set; }
        
        #endregion
        #region "Collections & Dictionaries"
        #endregion
        #region "Constructor and IDisposable Support"
        #region Constructors
        public Station() 
        { }
        public Station(String gageID)
        {
            StationID = gageID;
        }

        public Station(site s):this(s.site_no)
        {
            try
            {
                this.Name = s.station_nm;
                this.DrainageArea_sqMI = Convert.ToDouble(s.drain_area_va);
                this.Latitude_DD = s.Lat_DD;
                this.Longitude_DD = s.Long_DD;

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion
        #region "Methods"
        public Boolean LoadFullRecord()
        {
            return LoadRecord(new DateTime(1900,10,1), DateTime.Today);
        }
        public Boolean LoadRecord(DateTime sDate, DateTime eDate)
        {
            StationServiceAgent sa = null;
            try
            {
                //sa = new StationServiceAgent(ConfigurationManager.AppSettings["nwis"]);
                //Discharge = sa.GetFlowSeries(sDate, eDate, StationID);
                //if (Discharge != null)
                //{
                //    URL = String.Format(ConfigurationManager.AppSettings["nwisStationurl"],StationID);
                //}
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean LimitDischarge(DateTime sDate, DateTime eDate)
        {
           return Discharge.SetTimeSeriesRange(sDate, eDate);
        }
        public IDictionary<Double, TimeSeriesObservation> GetExceedanceProbability()
        {
            try
            {
                if (Discharge == null) LoadFullRecord();
                return Discharge.GetProbabilityOfExceedance();
            }
            catch (Exception)
            {                
                throw;
            }
        }
        #endregion
        #region "Static Methods"
        public static Station NWISStation(string stationID)
        {
            throw new NotImplementedException();
            //StationServiceAgent sa = new StationServiceAgent(ConfigurationManager.AppSettings["nwis"]);
            //return sa.GetNWISStation(stationID);
        }
        
        #endregion
    }

    public class site
    {
        public String agency_cd { get; set; }
        public String site_no { get; set; }
        public String station_nm { get; set; }
        public String lat_va { get; set; }
        public String long_va { get; set; }
        public String lat_long_datum_cd { get; set; }
        public String drain_area_va { get; set; }

        public Double Lat_DD
        {
            get 
            {
                try
                {
                    Int32 d = Convert.ToInt32(lat_va.Substring(0, 2));
                    Int32 m = Convert.ToInt32(lat_va.Substring(2, 2));
                    Int32 s = Convert.ToInt32(lat_va.Substring(4, 2));

                    return ddmmssToDD(d,m,s); 
                }
                catch (Exception)
                {
                    return -999;
                }
                
            }
        }
        public Double Long_DD
        {
            get
            {
                try
                {
                    //this.Latitude_DD = ddmmssToDD(x,y,z);
                    Int32 d = Convert.ToInt32(long_va.Substring(0, 3));
                    Int32 m = Convert.ToInt32(long_va.Substring(3, 2));
                    Int32 s = Convert.ToInt32(long_va.Substring(5, 2));

                    return ddmmssToDD(d, m, s); 
                }
                catch (Exception)
                {
                    return -999;
                }

            }
        }

        private Double ddmmssToDD(Int32 degr, Int32 min, Int32 sec)
        {
            //converts ddmmss to dd
            //30º 15' 50" = 30º + 15'/60 + 50"/3600 = 30.263888889º
            Double dd = Convert.ToDouble(degr) + Convert.ToDouble(min) / 60 + Convert.ToDouble(sec) / 3600;

            return Math.Round(dd, 6);
        }
    }//end class
    public class usgs_nwis
    {
        public site site { get; set; }
    }
}