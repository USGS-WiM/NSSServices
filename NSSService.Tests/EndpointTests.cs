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
            string expectedResultString = "";

            resourceurl = host + Configuration.regionResource + "/CO/" + Configuration.scenarioResource;
            queryParams = Configuration.statisticGroupTypeResource + "=7&" + Configuration.RegressionRegionResource + @"=GC1222,GC1221,GC1219,GC1220,GC1207&" + Configuration.userTypeResource + "=2";
            returnedObject = this.GETRequest<List<Scenario>>(resourceurl + "?" + queryParams);

            Assert.IsNotNull(returnedObject);

            returnedObject.ForEach(s => s.RegressionRegions.ForEach(rr => {
                rr.Parameters.ForEach(p => {
                    switch (p.Code.ToUpper())
                    {
                        case "DRNAREA": p.Value = 6.2; break;
                        case "ELEV": p.Value = 11129; break;
                    }
                });
                switch (rr.Code)
                {
                    case "GC1219": rr.PercentWeight = 100; break;
                }
            }));

            resultObject = this.POSTRequest<List<Scenario>>(resourceurl + "/estimate?" + queryParams, returnedObject);
            Assert.IsNotNull(resultObject);
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = @"[{'GC1219':{'Q1':1.53255283209625," +
                                              "'Q2':1.37131101906397," +
                                              "'Q3':1.28243688279756," +
                                              "'Q4':2.98896095707572," +
                                              "'Q5':12.5084492857449," +
                                              "'Q6':18.2685493685393," +
                                              "'Q7':6.91331428775481," +
                                              "'Q8':4.65729758089565," +
                                              "'Q9':3.3717574380219," +
                                              "'Q10':2.54847609662851," +
                                              "'Q11':1.91786693517863," +
                                              "'Q12':1.61941812279903}" +
                                           "}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));
            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_

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
            expectedResultString = @"[{'GC1222':{'D10':4.18132728094425E-06," +
                                              "'D25':2.14304064262411E-08," +
                                              "'D50':4.20482645603736E-08," +
                                              "'D75':5.2504078006353E-09," +
                                              "'D90':6.22914892060179E-11}" +
                                           "}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            expectedResultString = "[{\"GC1621\":{\"PK2\":2553.23576946212,\"PK5\":8715.72737999659,\"PK10\":15084.1991605652,\"PK25\":26933.4059638614,\"PK50\":38762.8713617922,\"PK100\":52760.1594295127,\"PK200\":68953.4398111781,\"PK500\":95732.1796665734}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            expectedResultString = "[{\"GC1679\":{\"PK1_5\":119.284949669654,\"PK2\":199.377355463888,\"PK2_33\":258.205014560047,\"PK5\":646.3651392416,\"PK10\":1138.81157432261,\"PK25\":1985.8908308646,\"PK50\":2772.25705474955,\"PK100\":3680.88945974997,\"PK200\":4710.13217207196,\"PK500\":6236.06984416227}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));


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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = "[{\"GC1614\":{\"PK1_005U\":665.10715543239,\"PK1_01U\":754.925603168872,\"PK1_05U\":1068.52926939028,\"PK1_11U\":1362.75305247259,\"PK1_25U\":1778.90741186528,\"PK1_5U\":2212.10320814829,\"PK2U\":2804.83918845105,\"PK2_33U\":3155.67130789551,\"PK5U\":4957.87230229318,\"PK10U\":6895.03671754016,\"PK25U\":10007.61286043,\"PK50U\":12997.6150490891,\"PK100U\":16025.4606870367,\"PK200U\":20686.6514986757,\"PK500U\":24904.9346711008}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 5);
            expectedResultString = "[{\"GC1699\":{\"M1D10Y0406\":19.3750553579722,\"M7D10Y0406\":22.1179702007597,\"M30D10Y46\":34.3652730521304},\"GC1700\":{\"M1D10Y1012\":3.7825930045043,\"M7D10Y1012\":4.54523643988549,\"M30D10YOD\":7.03581227690121},\"GC1701\":{\"M1D10Y0406\":4.06890536857436,\"M7D10Y0406\":5.10786777368609,\"M30D10Y46\":9.73605614089722},\"GC1724\":{\"M1D10Y1012\":0.426784734532477,\"M7D10Y1012\":0.584852684360611,\"M30D10YOD\":1.17561902113688},\"areaave\":{\"M1D10Y0406\":5.3200040593358118,\"M7D10Y0406\":6.4981890968790861,\"M30D10Y46\":11.74890422759805,\"M1D10Y1012\":0.701126904859052,\"M7D10Y1012\":0.9086120786424996,\"M30D10YOD\":1.65465748949706}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));


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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 3);
            expectedResultString = "[{\"GC1418\":{\"M7D10Y\":0.231954068113531,\"M30D5Y\":0.478860518714785},\"GC1419\":{\"M7D10Y\":0.776429295570855,\"M30D5Y\":1.29980107998381},\"areaave\":{\"M7D10Y\":0.27455988417358135,\"M30D5Y\":0.54310006679062217}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 4);
            expectedResultString = "[{\"GC1540\":{\"PK2U\":3673.69959952281,\"PK5U\":6202.34905493982,\"PK10U\":8064.06795956639,\"PK25U\":10540.5307466281,\"PK50U\":12447.5773519122,\"PK100U\":14415.2167269424,\"PK200U\":16471.2461772891,\"PK500U\":19326.782586218},\"GC1541\":{\"PK2U\":990.868392434281,\"PK5U\":1611.33945705614,\"PK10U\":2095.08190320299,\"PK25U\":2782.34127750491,\"PK50U\":3342.06365215035,\"PK100U\":3982.27789327804,\"PK200U\":4639.39181007062,\"PK500U\":5607.03252640772},\"GC1542\":{\"PK2U\":1231.04311380204,\"PK5U\":2293.26924347009,\"PK10U\":3191.84586537523,\"PK25U\":4540.21939189143,\"PK50U\":5670.51286742078,\"PK100U\":6929.52116048095,\"PK200U\":8354.38719159281,\"PK500U\":10368.1610317158},\"areaave\":{\"PK2U\":1646.2203465126463,\"PK5U\":2735.127926206646,\"PK10U\":3557.966602913265,\"PK25U\":4686.56411017862,\"PK50U\":5579.2485963462286,\"PK100U\":6547.9911299484838,\"PK200U\":7552.3142498821535,\"PK500U\":8988.6140420406027}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 3);
            expectedResultString = "[{\"GC730\":{\"PK2\":112.606850734354,\"PK5\":169.221053390627,\"PK10\":206.980553897539,\"PK25\":255.305513008102,\"PK50\":292.336555204171,\"PK100\":329.966801778842,\"PK500\":422.499449529942},\"GC731\":{\"PK2\":170.940863501283,\"PK5\":242.266488253646,\"PK10\":289.440515133048,\"PK25\":348.333883127765,\"PK50\":391.945463244617,\"PK100\":434.862757738605,\"PK500\":534.042100388147},\"transave\":{\"PK2\":125.44033354307838,\"PK5\":185.29104906049116,\"PK10\":225.121745369351,\"PK25\":275.77175443442786,\"PK50\":314.25051497306913,\"PK100\":353.04391208998993,\"PK500\":447.03883271874713}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = "[{\"GC1449\":{\"Q10\":19.7352558617966}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));


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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = "[{\"GC1648\":{\"D0_01\":386.934690254537,\"D0_1\":172.935409649244,\"D2\":21.0398646578699,\"D5\":8.35501165573636,\"D10\":3.89044412068671,\"D25\":1.45725898883949,\"D50\":0.469526786500179,\"D75\":0.0860121620131727,\"D90\":0.0462719924119639,\"D95\":0.0471119046483946,\"D99\":0.00917185535514543,\"D99_9\":0.00102299659187749,\"D99_99\":0.00126783605940649}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));


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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 3);
            expectedResultString = "[{\"GC1075\":{\"PK1_25\":26247.9416075157,\"PK1_5\":31033.3976781684,\"PK2\":37576.1044439658,\"PK5\":56059.1913667439,\"PK10\":69451.8436218103,\"PK25\":88014.5584966329,\"PK50\":103579.489516239,\"PK100\":118909.42670987,\"PK200\":135240.420792791,\"PK500\":158347.059375138},\"GC1076\":{\"PK1_25\":13978.8652669598,\"PK1_5\":16493.1320184586,\"PK2\":19776.3248068867,\"PK5\":28414.681119952,\"PK10\":34612.1798134541,\"PK25\":43109.2424903938,\"PK50\":49496.420651631,\"PK100\":56175.4270706974,\"PK200\":63548.7465517186,\"PK500\":73360.3197027621},\"areaave\":{\"PK1_25\":21015.509234428268,\"PK1_5\":24832.364428426721,\"PK2\":29984.978872690255,\"PK5\":44269.570301328029,\"PK10\":54593.69657323412,\"PK25\":68863.701615457,\"PK50\":80514.595910003991,\"PK100\":92155.165527182558,\"PK200\":104665.97155177238,\"PK500\":122102.6589249058}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = "[{\"GC1435\":{\"PK2\":709.379632679408,\"PK5\":1143.66363135795,\"PK10\":1475.1630431185,\"PK25\":1928.47655647647,\"PK50\":2291.33160491155,\"PK100\":2679.26812582662,\"PK500\":3666.15430345523}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 1);
            expectedResultString = "[{\"GC729\":{\"PK2\":166038.490822227,\"PK5\":216764.702837038,\"PK10\":249302.976960551,\"PK25\":290084.331381748,\"PK50\":319127.771350028,\"PK100\":347447.649338759,\"PK500\":409374.346522032}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

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
            Assert.IsTrue(resultObject.First().RegressionRegions.Count == 2);
            expectedResultString = "[{\"GC1254\":{\"PK2\":87.4453930923411,\"PK5\":166.14665975557,\"PK10\":226.188757355877,\"PK25\":308.211156262781,\"PK50\":381.272950109629,\"PK100\":450.426190257644,\"PK200\":519.533234890715,\"PK500\":628.059768738079},\"GC1580\":{\"PK2\":94.328500838218,\"PK5\":155.938244105175,\"PK10\":202.533741235969,\"PK25\":265.217187353446,\"PK50\":315.224590653835,\"PK100\":367.257604582405,\"PK200\":421.291351631458,\"PK500\":494.763033225917}}]";
            Assert.IsTrue(isValidResult(resultObject, expectedResultString));

            //+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_+_
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
        private bool isValidResult(List<Scenario> resultObject, string expectedResultString)
        {
            List<Dictionary<string, Dictionary<string, double?>>> results, expectedResults = null;
            bool match = true;
            results = resultObject.Select(i => i.RegressionRegions.ToDictionary(key => key.Code, val => val.Results.ToDictionary(key => key.code, v => v.Value))).ToList();
            expectedResults = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, double?>>>>(expectedResultString);

            match = true;
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Keys.Count != expectedResults[i].Keys.Count ||
                !results[i].Keys.All(k => expectedResults[i].ContainsKey(k) || !object.Equals(expectedResults[i][k], results[i][k])))
                {
                    match = false;
                }
            }//next i
            return match;
        }
    }//end class
}//end namespace
