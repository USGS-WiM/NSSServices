//------------------------------------------------------------------------------
//----- Configuration -----------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

//    authors:  Jeremy Newson          
//  
//   purpose:   Configuration implements the IConfiurationSource interface. OpenRasta
//              will call the Configure method and use it to configure the application 
//              through a fluent interface using the Resource space as root objects. 
//
//discussion:   The ResourceSpace is where you can define the resources in the application and what
//              handles them and how thy are represented. 
//              https://github.com/openrasta/openrasta/wiki/Configuration
//
//     
#region Comments
// 08.01.14 - JKN - Created
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using OpenRasta.Authentication;
using OpenRasta.Authentication.Basic;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.IO;
using OpenRasta.Pipeline.Contributors;
using OpenRasta.Web.UriDecorators;

using WiM.Codecs;
using WiM.PipeLineContributors;
using WiM.Codecs.json;
using WiM.Codecs.xml;
using NSSService.Handlers;
using NSSDB;
using NSSService.Resources;

namespace NSSService
{
    public class Configuration:IConfigurationSource
    {
        #region Private Field Properties
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

        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                // Allow codec choice by extension 
                ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
                ResourceSpace.Uses.PipelineContributor<ErrorCheckingContributor>();
                ResourceSpace.Uses.PipelineContributor<CrossDomainPipelineContributor>();

                AddCitationEndpoints();
                AddEquationTypeEndpoints();
                AddEquationTypeDisplayNamesEndpoints();
                AddErrorTypeEndpoints();
                AddPredictionIntervalEndpoints();
                AddRegionEndpoints();
                AddScenarioEndpoints();
                AddStatisticGroupTypeEndpoints();
                AddRegionEquationEndpoints();
                AddUnitConversionFactorEndpoints();
                AddUnitSystemTypeEndpoints();
                AddUnitTypeEndpoints();
                AddUserTypeEndpoints();
                AddVariableEndpoints();
                AddVariableTypeEndpoints();
                

            }//end using
        }//end Configure

        #region Helper methods  
        private void AddCitationEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Citation>>()
                .AtUri(citationResource)
                .And.AtUri(citationResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + equationTypeResource + "={equationtypeIDs}").Named("GetCitations")
                .And.AtUri(regionResource + "/{region}/" + citationResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("GetCitations")
                .HandledBy<CitationHandler>()
                .TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml")
                .And.TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json");
                


            ResourceSpace.Has.ResourcesOfType<Citation>()
                .AtUri(citationResource + "/{ID}")
                .HandledBy<CitationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8XmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddEquationTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<EquationType>>()
                .AtUri(equationTypeResource)
                .And.AtUri(equationTypeResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("GetEquationTypes")
                .And.AtUri(regionResource + "/{region}/" + equationTypeResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("GetEquationTypes")
                .HandledBy<EquationTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<EquationType>()
                .AtUri(equationTypeResource + "/{ID}")
                .HandledBy<EquationTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddEquationTypeDisplayNamesEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<EquationTypeDisplayName>>()
                .AtUri(equationTypeDisplayNamesResource)
                .HandledBy<EquationTypeDisplayNameHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<EquationTypeDisplayName>()
                .AtUri(equationTypeDisplayNamesResource + "/{ID}")
                .HandledBy<EquationTypeDisplayNameHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddErrorTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<ErrorType>>()
                .AtUri(errorTypeResource)
                .HandledBy<ErrorTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<ErrorType>()
                .AtUri(errorTypeResource + "/{ID}")
                .HandledBy<ErrorTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddPredictionIntervalEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<PredictionInterval>>()
                .AtUri(predictionIntervalResource)
                .HandledBy<PredictionIntervalHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<PredictionInterval>()
                .AtUri(predictionIntervalResource + "/{ID}")
                .HandledBy<PredictionIntervalHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddRegionEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Region>>()
                .AtUri(regionResource)
                .HandledBy<RegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Region>()
                .AtUri(regionResource + "/{region}")
                .HandledBy<RegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddScenarioEndpoints() 
                {
                    ResourceSpace.Has.ResourcesOfType<List<Scenario>>()
                        .AtUri(scenarioResource + "?region={region}&"+RegressionRegionResource+"={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + equationTypeResource + "={equationtypeIDs}").Named("GetScenarios")
                        .And.AtUri(regionResource + "/{region}/" + scenarioResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + equationTypeResource + "={equationtypeIDs}").Named("GetScenarios")

                        .And.AtUri(scenarioResource + "/estimate?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("EstimatesScenarios")
                        .And.AtUri(regionResource + "/{region}/" + scenarioResource + "/estimate?subregion={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("EstimateScenarios")
                        .HandledBy< ScenarioHandler>()
                        .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json")
                        .And.TranscodedBy<UTF8XmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");
                }
        private void AddStatisticGroupTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<StatisticGroupType>>()
                .AtUri(statisticGroupTypeResource)
                .And.AtUri(statisticGroupTypeResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + equationTypeResource + "={equationtypeIDs}").Named("GetStatisticGroups")
                .And.AtUri(regionResource + "/{region}/" + statisticGroupTypeResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + equationTypeResource + "={equationtypeIDs}").Named("GetStatisticGroups")
                .HandledBy<StatisticGroupTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<StatisticGroupType>()
                .AtUri(statisticGroupTypeResource + "/{ID}")
                .HandledBy<StatisticGroupTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddRegionEquationEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<RegressionRegion>>()
                .AtUri(RegressionRegionResource)
                .And.AtUri(regionResource + "/{region}/" + RegressionRegionResource).Named("GetRegionSubregions")
                .And.AtUri(RegressionRegionResource + "?region={region}").Named("GetRegionSubregions")
                .HandledBy<RegressionRegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")            
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<RegressionRegion>()
                .AtUri(RegressionRegionResource + "/{ID}")
                .HandledBy<RegressionRegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddUnitConversionFactorEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitConversionFactor>>()
                .AtUri(unitConversionFactorResource)
                .HandledBy<UnitConversionFactorHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitConversionFactor>()
                .AtUri(unitConversionFactorResource + "/{ID}")
                .HandledBy<UnitConversionFactorHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddUnitSystemTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitSystemType>>()
                .AtUri(unitSystemTypeResource)
                .HandledBy<UnitSystemTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitSystemType>()
                .AtUri(unitSystemTypeResource + "/{ID}")
                .HandledBy<UnitSystemTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddUnitTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitType>>()
                .AtUri(unitTypeResource)
                .HandledBy<UnitTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitType>()
                .AtUri(unitTypeResource + "/{ID}")
                .HandledBy<UnitTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddUserTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UserType>>()
                .AtUri(userTypeResource)
                .HandledBy<UserTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UserType>()
                .AtUri(userTypeResource + "/{ID}")
                .HandledBy<UserTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddVariableEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Variable>>()
                .AtUri(variableResource)
                .HandledBy<VariableHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Variable>()
                .AtUri(variableResource + "/{ID}")
                .HandledBy<VariableHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddVariableTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<VariableType>>()
                .AtUri(variableTypeResource)
                .HandledBy<VariableTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<VariableType>()
                .AtUri(variableTypeResource + "/{ID}")
                .HandledBy<VariableTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        
        #endregion
    }//end configuration
}//end namespace