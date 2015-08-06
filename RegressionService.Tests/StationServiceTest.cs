using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegressionService.Utilities.ServiceAgent;

namespace RegressionService.Tests
{
    [TestClass]
    public class RegressionServiceTests
    {
        [TestMethod]
        public void StationServiceTest()
        {
            StationServiceAgent sa = null;
            try
            {
                string NWIS_Station_ID = "05471050";
                DateTime sd = new DateTime(2014, 11, 01);
                DateTime ed = new DateTime(2015,3,2);
                if (String.IsNullOrEmpty(NWIS_Station_ID)) throw new Exception("No nwis gage ID");
                sa = new StationServiceAgent(ConfigurationManager.AppSettings["nwis"]);

                var stationgage = sa.GetNWISStation(NWIS_Station_ID);

                stationgage.LoadRecord(sd,ed);

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                sa = null;
            }

        }
    }
}
