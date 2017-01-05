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
            Assert.Inconclusive("Deserializing object not yet implemented");
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
            Assert.Inconclusive("Deserializing object not yet implemented");
        }//end method
        [TestMethod]
        public void ScenarioEvaluateRequest()
        {
            //List<Scenario> content = null;
            //List<Scenario> returnedObject = this.POSTRequest<List<Scenario>>(host + Configuration.regionResource + "/IN/" + Configuration.scenarioResource, content);
            
            //Assert.IsNotNull(returnedObject);
            Assert.Inconclusive("Not yet implemented");
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
