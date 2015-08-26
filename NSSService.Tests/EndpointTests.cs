using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSSService;
using OpenRasta;
using OpenRasta.Hosting.InMemory;
using OpenRasta.Web;
using NSSDB;
using Newtonsoft.Json;


namespace NSSService.Tests
{
    /// <summary>
    /// Summary description for EndpointTests
    /// </summary>
    [TestClass]
    public class EndpointTests
    {

        [TestMethod]
        public void CitationRequest()
        {
            List<Citation> returnedObject = this.GETRequest<List<Citation>>("http://localhost/citations");            
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void EquationRequest()
        {
            List<Equation> returnedObject = this.GETRequest<List<Equation>>("http://localhost/equations");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void EquationTypeDisplayNameRequest()
        {
            List<EquationTypeDisplayName> returnedObject = this.GETRequest<List<EquationTypeDisplayName>>("http://localhost/equationtypedisplaynames");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void EquationTypeRequest()
        {
            List<EquationType> returnedObject = this.GETRequest<List<EquationType>>("http://localhost/equationtypes");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void ErrorTypeRequest()
        {
            List<ErrorType> returnedObject = this.GETRequest<List<ErrorType>>("http://localhost/errortypes");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void PredictionIntervalRequest()
        {
            List<PredictionInterval> returnedObject = this.GETRequest<List<PredictionInterval>>("http://localhost/predictionintervals");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void RegionRequest()
        {
            List<Region> returnedObject = this.GETRequest<List<Region>>("http://localhost/regions");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void StatisticGroupTypeRequest()
        {
            List<StatisticGroupType> returnedObject = this.GETRequest<List<StatisticGroupType>>("http://localhost/statisticgrouptypes");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void SubRegionRequest()
        {
            List<SubRegion> returnedObject = this.GETRequest<List<SubRegion>>("http://localhost/subregions");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void ConversionFactorsRequest()
        {
            List<UnitConversionFactor> returnedObject = this.GETRequest<List<UnitConversionFactor>>("http://localhost/conversionfactors");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void UnitSystemTypeRequest()
        {
            List<UnitSystemType> returnedObject = this.GETRequest<List<UnitSystemType>>("http://localhost/unitsystems");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void UnitTypeRequest()
        {
            List<UnitType> returnedObject = this.GETRequest<List<UnitType>>("http://localhost/unittypes");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void UserTypeRequest()
        {
            List<UserType> returnedObject = this.GETRequest<List<UserType>>("http://localhost/usertypes");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void VariableRequest()
        {
            List<Variable> returnedObject = this.GETRequest<List<Variable>>("http://localhost/variables");
            Assert.IsNotNull(returnedObject);
        }//end method
        [TestMethod]
        public void VariableTypeRequest()
        {
            List<VariableType> returnedObject = this.GETRequest<List<VariableType>>("http://localhost/variabletypes");
            Assert.IsNotNull(returnedObject);
        }//end method

        private T GETRequest<T>(string url) 
        {
            using (InMemoryHost host = new InMemoryHost(new Configuration()))
            {
                var request = new InMemoryRequest()
                {
                    Uri = new Uri(url),
                    HttpMethod = "GET"
                };
                // set up your code formats - I'm using
                // JSON because it's awesome
                request.Entity.ContentType = MediaType.Json;
                request.Entity.Headers["Accept"] = "application/json";

                // send the request and save the resulting response
                var response = host.ProcessRequest(request);
                int statusCode = response.StatusCode;

                // deserialize the content from the response

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
                            return serializer.Deserialize<T>(jsonTextReader);
                        }//end using
                    }//end using
                }//end if
            }//end using  
            return default(T);  
        }
    }//end class
}//end namespace
