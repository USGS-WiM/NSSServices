using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSSDB;
using WiMServices.Test;
using NSSService.Resources;


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
        public string citationResource = "citations";
        private string equationTypeResource = "equationtypes";
        private string equationTypeDisplayNamesResource = "equationnames";
        private string errorTypeResource = "errors";
        private string predictionIntervalResource = "predictionintervals";
        private string regionResource = "regions";
        private string statisticGroupTypeResource = "statisticgroups";
        private string scenarioResource = "scenarios";
        private string RegressionRegionResource = "regressionregions";
        private string unitConversionFactorResource = "conversionfactors";
        private string unitSystemTypeResource = "unitsystems";
        private string unitTypeResource = "units";
        private string userTypeResource = "users";
        private string variableResource = "variables";
        private string variableTypeResource = "variabletypes";

        #endregion
        #region Constructor
        public EndpointTests():base(new Configuration()){}
        #endregion
        #region Test Methods        
        [TestMethod]
        public void CitationRequest()
        {            
            List<Citation> returnedObject = this.GETRequest<List<Citation>>(host+citationResource);
            Assert.IsTrue(returnedObject.Count > 0, returnedObject.Count.ToString());
            //Assert.IsFalse(true);

        }//end method
        [TestMethod]
        public void EquationTypeDisplayNameRequest()
        {
            List<EquationTypeDisplayName> returnedObject = this.GETRequest<List<EquationTypeDisplayName>>(host+equationTypeDisplayNamesResource);
            Assert.IsTrue(returnedObject.Count > 0, returnedObject.Count.ToString());
        }//end method
        [TestMethod]
        public void EquationTypeRequest()
        {
            List<EquationType> returnedObject = this.GETRequest<List<EquationType>>(host+equationTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ErrorTypeRequest()
        {
            List<ErrorType> returnedObject = this.GETRequest<List<ErrorType>>(host+errorTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void PredictionIntervalRequest()
        {
            List<PredictionInterval> returnedObject = this.GETRequest<List<PredictionInterval>>(host+predictionIntervalResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void RegionRequest()
        {
            List<Region> returnedObject = this.GETRequest<List<Region>>(host+regionResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ScenarioRequest()
        {
            Scenario returnedObject = this.GETRequest<Scenario>(host + regionResource+"/IN/"+scenarioResource);
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void ScenarioEvaluateRequest()
        {
            List<Scenario> content = null;
            List<Scenario> returnedObject = this.POSTRequest<List<Scenario>>(host + regionResource + "/IN/" + scenarioResource,content);

            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void StatisticGroupTypeRequest()
        {
            List<StatisticGroupType> returnedObject = this.GETRequest<List<StatisticGroupType>>(host+statisticGroupTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void RegressionRegionRequest()
        {
            List<RegressionRegion> returnedObject = this.GETRequest<List<RegressionRegion>>(host + RegressionRegionResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void ConversionFactorsRequest()
        {
            List<UnitConversionFactor> returnedObject = this.GETRequest<List<UnitConversionFactor>>(host+unitConversionFactorResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UnitSystemTypeRequest()
        {
            List<UnitSystemType> returnedObject = this.GETRequest<List<UnitSystemType>>(host+unitSystemTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UnitTypeRequest()
        {
            List<UnitType> returnedObject = this.GETRequest<List<UnitType>>(host+unitTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void UserTypeRequest()
        {
            List<UserType> returnedObject = this.GETRequest<List<UserType>>(host+userTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void VariableRequest()
        {
            List<Variable> returnedObject = this.GETRequest<List<Variable>>(host+variableResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method
        [TestMethod]
        public void VariableTypeRequest()
        {
            List<VariableType> returnedObject = this.GETRequest<List<VariableType>>(host+variableTypeResource);
            Assert.IsTrue(returnedObject.Count > 0);
        }//end method

        #endregion

    }//end class
}//end namespace
