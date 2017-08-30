using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using NSSDB;
using WiM.Test;
using NSSService.Resources;
using NSSService;
using Newtonsoft.Json;
using System.IO;
using System.Text;


namespace NSSService.Tests
{
    /// <summary>
    /// Summary description for EndpointTests
    /// </summary>
    [TestClass]
    public class EndpointTests:EndpointTestBase
    {
        #region Private Fields
        private string host = "http://localhost/";
        #endregion
        #region Constructor
        public EndpointTests():base(new Configuration()){}
        #endregion
        #region Test Methods        
        [TestMethod]
        public void CitationRequest()
        {            
            List<Citation> returnedObject = this.GETRequest<List<Citation>>(host+ Configuration.citationResource);
            Assert.IsTrue(returnedObject.Count > 0, returnedObject.Count.ToString());
            //Assert.IsFalse(true);

        }//end method
        [TestMethod]
        public void EquationTypeDisplayNameRequest()
        {
            List<RegressionTypeDisplayName> returnedObject = this.GETRequest<List<RegressionTypeDisplayName>>(host + Configuration.regressionTypeDisplayNamesResource);
            Assert.IsTrue(returnedObject.Count > 0, returnedObject.Count.ToString());
        }//end method
        [TestMethod]
        public void EquationTypeRequest()
        {
            List<RegressionType> returnedObject = this.GETRequest<List<RegressionType>>(host + Configuration.regressionTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ErrorTypeRequest()
        {
            List<ErrorType> returnedObject = this.GETRequest<List<ErrorType>>(host + Configuration.errorTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void PredictionIntervalRequest()
        {
            List<PredictionInterval> returnedObject = this.GETRequest<List<PredictionInterval>>(host + Configuration.predictionIntervalResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void RegionRequest()
        {
            List<Region> returnedObject = this.GETRequest<List<Region>>(host + Configuration.regionResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ScenarioRequest()
        {
            List<Scenario> returnedObject = this.GETRequest<List<Scenario>>(host + Configuration.regionResource + "/IN/" + Configuration.scenarioResource);
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void ScenarioExtensionRequest()
        {
            var resourceurl = host + Configuration.regionResource + "/IA/" + Configuration.scenarioResource;
            List<Scenario> returnedObject = this.GETRequest<List<Scenario>>(resourceurl+"?"+Configuration.statisticGroupTypeResource+"=fds&"+Configuration.extensionResource+"=qppq&"+Configuration.unitSystemTypeResource+"=2");
            Assert.IsNotNull(returnedObject);

            //load scenario object for post
            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr=>{rr.Extensions.ForEach(e => e.Parameters.ForEach(p =>{ switch (p.Code.ToUpper())
                                                                    {case "SID":  p.Value = "05465000"; break;
                                                                     case "SDATE": p.Value = "2015-01-01T00:00:00"; break;
                                                                     case "EDATE": p.Value = "2016-01-01T00:00:00"; break;
                                                                    }}));
                                                                    rr.Parameters.ForEach(p => { switch (p.Code.ToUpper())
                                                                            {
                                                                                case "DRNAREA": p.Value = 69.3; break;
                                                                                case "PRECIP": p.Value = 35.46; break;
                                                                                case "RSD": p.Value = 0.3; break;
                                                                                case "HYSEP": p.Value = 55.22; break;
                                                                                case "STREAM_VARG": p.Value = 0.49; break;
                                                                                case "SSURGOB": p.Value = 79.9; break;
                                                                                case "SSURGOC": p.Value = 0.53; break;
                                                                                case "SSURGOD": p.Value = 0.87; break;
                                                                            }
                                                                    });
            }));

            List<Scenario> resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + Configuration.statisticGroupTypeResource + "=fds&" + Configuration.extensionResource + "=qppq&" + Configuration.unitSystemTypeResource + "=2", returnedObject);
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void ScenarioLimitationExtensionRequest()
        {
            var resourceurl = host + Configuration.regionResource + "/IA/" + Configuration.scenarioResource;
            var queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + "=gc0,gc1560,gc0,gc1526,gc1561,gc1564,gc15620,gc1201,gc1202,gc667&" + Configuration.unitSystemTypeResource + "=2";
            List<Scenario> returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            //load scenario object for post
            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 126; break;
                        case "I24H10Y": p.Value = 4.23; break;
                        case "CCM": p.Value = 1.22; break;
                        case "STRMTOT": p.Value = 103.251; break;
                    }
                });
            }));

            List<Scenario> resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
        }//end method
        [TestMethod]
        public void ScenarioEvaluateRequest()
        {
            string resourceurl;
            string queryParams;
            List<Scenario> returnedObject = null;
            List<Scenario> resultObject = null;
            //https://test.streamstats.usgs.gov/nssservices/scenarios/estimate.json?region=CO&statisticgroups=5&regressionregions=GC1222,GC1221,GC1219,GC1220,GC1207&configs=2
            resourceurl = host + Configuration.regionResource + "/CO/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=5&" + Configuration.RegressionRegionResource + @"=GC1222,GC1221,GC1219,GC1220,GC1207&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 22.7; break;
                        case "ELEV": p.Value = 1459; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1222": rr.PercentWeight = 99.96301812706554; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            Assert.IsNotNull(resultObject.First().RegressionRegions.First().Results);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_



            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
            //https://test.streamstats.usgs.gov/nssservices/scenarios/estimate.json?region=AZ&statisticgroups=2&regressionregions=GC1621,GC1618,GC1623&configs=2
            //Tests prediction interval and average standard error weight
            resourceurl = host + Configuration.regionResource + "/AZ/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + @"=GC1621,GC1618,GC1623&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 326.37; break;
                        case "PRECIP": p.Value = 26.5; break;
                        case "ELEV": p.Value = 5245.768; break;
                        case "CONTDA": p.Value = 326.37; break;
                        case "FD_Region": p.Value = 326.37; break;
                    }
                });
                switch (rr.Code)
                {
                    default: rr.PercentWeight = 100; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            Assert.IsNotNull(resultObject.First().RegressionRegions.First().Results);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
            // https://test.streamstats.usgs.gov/nssservices/scenarios/estimate.json?region=MT&statisticgroups=2&regressionregions=GC1679&configs=2
            //Tests prediction interval and average standard error weight
            resourceurl = host + Configuration.regionResource + "/MT/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + @"=GC1679&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "CONTDA": p.Value = 34.9; break;
                        case "ET0306MOD": p.Value = 0.97; break;
                        case "SLOP30_30M": p.Value = 0; break;
                    }
                });
                switch (rr.Code)
                {
                    default: rr.PercentWeight = 100; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            Assert.IsNotNull(resultObject.First().RegressionRegions.First().Results);
            //Returns count =0 +_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            //https://streamstatstest.wim.usgs.gov/nssservices/scenarios/estimate.json?region=VA&statisticgroups=4&regressionregions=GC1545,GC1546,GC1549,GC1551,GC1552,GC1553&configs=2
            //Tests prediction interval and average standard error weight
            resourceurl = host + Configuration.regionResource + "/VA/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=31&" + Configuration.RegressionRegionResource + @"=GC1614&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 104; break;
                        case "LC01DEV": p.Value = 6.23; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1545": rr.PercentWeight = 3.6757487063050256; break;
                    case "GC1546": rr.PercentWeight = 94.546113853685171; break;
                    case "GC1549": rr.PercentWeight = 1.7781374348535617; break;
                    case "GC1551": rr.PercentWeight = 3.6757487063050256; break;
                    case "GC1552": rr.PercentWeight = 1.7781374348535617; break;
                    case "GC1553": rr.PercentWeight = 94.546113853685171; break;
                    default: rr.PercentWeight = 100;break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            //https://streamstatstest.wim.usgs.gov/nssservices/scenarios/estimate.json?region=IA&statisticgroups=4&regressionregions=GC1560,GC1699,GC1701,GC1700,GC1724,GC1525,GC1526,GC1564,GC1561&configs=2
            //Tests prediction interval and average standard error weight
            resourceurl = host + Configuration.regionResource + "/IA/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=8&" + Configuration.RegressionRegionResource + @"=GC1560,GC1699,GC1701,GC1700,GC1724,GC1525,GC1526,GC1564,GC1561&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 225; break;
                        case "TAU_ANN_G": p.Value = 21.49; break;
                        case "SSURGOKSAT": p.Value = 10.12; break;
                        case "BFI": p.Value = 0.550591; break;
                        case "SSURGOA": p.Value = 1.05; break;
                        case "DRNFREQ": p.Value = 0.6; break;
                        case "LC11CRPHAY": p.Value = 88.4; break;
                        case "PRJULDEC10": p.Value = 2.89; break;
                        case "SSURGOD": p.Value = 0; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1700": rr.PercentWeight = 8.17634983236648; break;
                    case "GC1699": rr.PercentWeight = 8.17634983236648; break;
                    case "GC1701": rr.PercentWeight = 91.81417149453831; break;
                    case "GC1724": rr.PercentWeight = 91.81417149453831; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            //https://streamstatstest.wim.usgs.gov/nssservices/scenarios/estimate.json?region=TN&statisticgroups=4&regressionregions=GC344,GC1418,GC1419&configs=2
            resourceurl = host + Configuration.regionResource + "/TN/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=4&" + Configuration.RegressionRegionResource + @"=GC344,GC1418,GC1419&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 18.77; break;
                        case "RECESS": p.Value = 64; break;
                        case "PERMGTE2IN": p.Value = 40.317; break;
                        case "CLIMFAC2YR": p.Value = 2.4; break;
                        case "SOILPERM": p.Value = 1.106; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1418": rr.PercentWeight = 92.17488438336163;break;
                    case "GC1419": rr.PercentWeight = 7.825115617612058; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //returned count = 0 +_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            //https://streamstatstest.wim.usgs.gov/nssservices/scenarios/estimate.json?region=GA&statisticgroups=31&regressionregions=GC1250,GC1250,GC1539,GC1540,GC1572,GC1250,GC1541,GC1573,GC1250,GC1542,GC1574&configs=2
           resourceurl = host + Configuration.regionResource + "/GA/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=31&" + Configuration.RegressionRegionResource + @"=GC1250,GC1250,GC1539,GC1540,GC1572,GC1250,GC1541,GC1573,GC1250,GC1542,GC1574&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 157; break;
                        case "LC06IMP": p.Value = 0.9; break;
                        case "I24H50Y": p.Value = 6.94; break;
                        case "LC06DEV": p.Value = 5.615; break;
                        case "PCTREG1": p.Value = 24.343; break;
                        case "PCTREG2": p.Value = 0; break;
                        case "PCTREG3": p.Value = 74.801; break;
                        case "PCTREG4": p.Value = 0.856; break;
                        case "PCTREG5": p.Value = 0; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1540": rr.PercentWeight = 24.351169108102276; break;
                    case "GC1541": rr.PercentWeight = 74.79479013931387; break;
                    case "GC1542": rr.PercentWeight = 0.8540407491242334; break;
                    default: rr.PercentWeight = 100; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //No result returned +_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            resourceurl = host + Configuration.regionResource + "/OR/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=PFS&" + Configuration.RegressionRegionResource + @"=gc730,gc731&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 1.64; break;
                        case "BSLOPD": p.Value = 24.4; break;
                        case "BSLDEM30M": p.Value = 18.2; break;
                        case "I24H2Y": p.Value = 3.19; break;
                        case "JANMAXT2K": p.Value = 45.9; break;
                        case "JANMINT2K": p.Value = 31; break;
                        case "ELEV": p.Value = 3070; break;
                        case "ORREG2": p.Value = 10001; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);


            //No results returned +_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            resourceurl = host + Configuration.regionResource + "/OH/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=&"+Configuration.regressionTypeResource+"=Q10&" + Configuration.RegressionRegionResource + @"=gc1523,gc10004,gc1450,gc1449,gc1234,gc1524&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 36.4; break;
                        case "STREAM_VARG": p.Value = 0.63; break;
                        case "LAT_CENT": p.Value = 41.6373; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);


            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            resourceurl = host + Configuration.regionResource + "/MN/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=5&" + Configuration.RegressionRegionResource + @"=gc1653,gc1648,gc1201,gc667&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 3.82; break;
                        case "PMPE": p.Value = -19.9; break;
                        case "PFLATLOW": p.Value = 4.92; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);


            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            resourceurl = host + Configuration.regionResource + "/NY/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + @"=gc1430,gc1431,gc1075,gc1076,gc738,gc741,gc909&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                switch(rr.Code){
                    case "GC1075": rr.PercentWeight = 57.35349086187217; break;
                    case "GC1076": rr.PercentWeight = 42.6457965599144; break;
                }
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 2490; break;
                        case "CSL10_85": p.Value = 7.68; break;
                        case "EL1200": p.Value = 58.7; break;
                        case "MAR": p.Value = 14.6; break;
                        case "PRECIP": p.Value = 33.9; break;
                        case "SLOPERATIO": p.Value = 0.0192; break;
                        case "STORAGE": p.Value = 1.3; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_
            resourceurl = host + Configuration.regionResource + "/ME/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + @"=gc1595,gc1435,gc1632,gc632,gc1633,gc1637,gc1641,gc1645,gc1635,gc1634,gc1640,gc1639,gc1636,gc1638,gc1644,gc1643,gc1642&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 15.3; break;
                        case "STORNWI": p.Value = 3.14; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_

            resourceurl = host + Configuration.regionResource + "/OR/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + @"=gc729,gc730,gc731&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 4990; break;
                        case "ELEV": p.Value = 2960; break;
                        case "I24H2Y": p.Value = 2.63; break;
                        case "WATCAPORC": p.Value = 0.11; break;
                        case "SOILPERM": p.Value = 1.75; break;
                        case "ORREG2": p.Value = 10003; break;
                        case "BSLDEM30M": p.Value = 16.7; break;
                        case "JANMINT2K": p.Value = 21.3; break;
                        case "JANMAXT2K": p.Value = 40; break;

                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_

            resourceurl = host + Configuration.regionResource + "/NC/" + Configuration.scenarioResource;
             queryParams = Configuration.statisticGroupTypeResource + "=2&" + Configuration.RegressionRegionResource + "=gc1254,gc1,gc1576,gc1254,gc1580,gc1577&" + Configuration.unitSystemTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 0.4; break;
                        case "PCTREG1": p.Value = 100; break;
                        case "PCTREG2": p.Value = 0; break;
                        case "PCTREG3": p.Value = 0; break;
                        case "PCTREG4": p.Value = 0; break;
                        case "PCTREG5": p.Value = 0; break;
                        case "LC06IMP": p.Value = 3.35; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_++_+_+_

            resourceurl = host + Configuration.regionResource + "/MA/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=&" + Configuration.RegressionRegionResource + "=251&" + Configuration.unitSystemTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 0.73; break;
                        case "PCTSNDGRV": p.Value = 0; break;
                        case "FOREST": p.Value = 76.93; break;
                        case "MAREGION": p.Value = 0.0; break;
                    }
                });
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
        }//end method
        [TestMethod]
        public void ScenarioWeightedEvaluateRequest()
        {
            var resourceurl = host + Configuration.regionResource + "/NY/" + Configuration.scenarioResource;
            var queryParams = Configuration.statisticGroupTypeResource + "=24&" + Configuration.RegressionRegionResource + "=gc1425& " + Configuration.unitSystemTypeResource + "=2";
            List<Scenario> returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 101; break;
                    }
                });
                
            }));

            List<Scenario> resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(returnedObject);


            resourceurl = host + Configuration.regionResource + "/CO/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=4&" + Configuration.RegressionRegionResource + "=gc1214,gc1213,gc1211,gc1212,gc1204,gc1698,gc5999,gc1488& " + Configuration.unitSystemTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);
            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 0.91; break;
                        case "PRECIP": p.Value = 19.83; break;
                        case "ELEV": p.Value = 6850; break;
                    }
                });
                rr.PercentWeight = 3.647444727402222;
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void StatisticGroupTypeRequest()
        {
            List<StatisticGroupType> returnedObject = this.GETRequest<List<StatisticGroupType>>(host + Configuration.statisticGroupTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void RegressionRegionRequest()
        {
            List<RegressionRegion> returnedlimitObject = this.GETRequest<List<RegressionRegion>>(host + Configuration.RegressionRegionResource+"?region=IL&"+Configuration.statisticGroupTypeResource+"=&"+Configuration.regressionTypeResource+"=1118");
            Assert.IsTrue(returnedlimitObject.Count == 7);
            List<RegressionRegion> returnedObject = this.GETRequest<List<RegressionRegion>>(host + Configuration.RegressionRegionResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ConversionFactorsRequest()
        {
            List<UnitConversionFactor> returnedObject = this.GETRequest<List<UnitConversionFactor>>(host + Configuration.unitConversionFactorResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UnitSystemTypeRequest()
        {
            List<UnitSystemType> returnedObject = this.GETRequest<List<UnitSystemType>>(host + Configuration.unitSystemTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UnitTypeRequest()
        {
            List<UnitType> returnedObject = this.GETRequest<List<UnitType>>(host + Configuration.unitTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UnitTypeUpdateRequest()
        {
            UnitType returnedObject = this.GETRequest<List<UnitType>>(host + Configuration.unitTypeResource).First();
            returnedObject.Unit += "test";
            var updatedObject = this.PUTRequest<UnitType>(host + Configuration.unitTypeResource+"/"+returnedObject.ID, returnedObject);
            Assert.IsTrue(returnedObject !=null);
        }//end method
        [TestMethod]
        public void UserTypeRequest()
        {
            List<UserType> returnedObject = this.GETRequest<List<UserType>>(host + Configuration.userTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void VariableRequest()
        {
            List<Variable> returnedObject = this.GETRequest<List<Variable>>(host + Configuration.variableResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void VariableTypeRequest()
        {
            List<VariableType> returnedObject = this.GETRequest<List<VariableType>>(host + Configuration.variableTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method

        #endregion
        protected override T deserialize<T>(OpenRasta.Web.IResponse response)
        {
            if (response.Entity.ContentLength > 0)
            {
                // you must rewind the stream, as OpenRasta
                // won't do this for you
                response.Entity.Stream.Seek(0, SeekOrigin.Begin);

                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader streamReader = new StreamReader(response.Entity.Stream, new UTF8Encoding(false, true)))
                {
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        serializer.TypeNameHandling = TypeNameHandling.Objects;
                        return serializer.Deserialize<T>(jsonTextReader);
                    }//end using
                }//end using
            }//end if
            return default(T);
        }
    }//end class
}//end namespace
