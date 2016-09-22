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
using System.Collections.Generic;

using OpenRasta.Configuration;
using OpenRasta.Web.UriDecorators;

using WiM.PipeLineContributors;
using WiM.Codecs.json;
using WiM.Codecs.xml;

using NSSDB;

using NSSService.Handlers;
using NSSService.Resources;
using NSSService.PipeLineContributors;

namespace NSSService
{
    public class Configuration:IConfigurationSource
    {
        #region Private Field Properties
        public static string citationResource = "citations";
        public static string extensionResource = "extensions";
        public static string regressionTypeResource = "regressiontypes";
        public static string regressionTypeDisplayNamesResource = "regressionnames";
        public static string errorTypeResource = "errors";
        public static string predictionIntervalResource = "predictionintervals";
        public static string regionResource = "regions";
        public static string statisticGroupTypeResource = "statisticgroups";
        public static string scenarioResource = "scenarios";
        public static string RegressionRegionResource = "regressionregions";
        public static string unitConversionFactorResource = "conversionfactors";
        public static string unitSystemTypeResource = "unitsystems";
        public static string unitTypeResource = "units";
        public static string userTypeResource = "users";
        public static string variableResource = "variables";
        public static string variableTypeResource = "variabletypes";
        #endregion

        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                // Allow codec choice by extension 
                ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
                ResourceSpace.Uses.PipelineContributor<ErrorCheckingContributor>();
                ResourceSpace.Uses.PipelineContributor<CrossDomainPipelineContributor>();
                ResourceSpace.Uses.PipelineContributor<MessagePipelineContributor>();
                ResourceSpace.Uses.PipelineContributor<NSSHyperMediaPipelineContributor>();

                AddCitationEndpoints();
                AddRegressionTypeEndpoints();
                AddRegressionTypeDisplayNamesEndpoints();
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
                .And.AtUri(citationResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}").Named("GetCitations")
                .And.AtUri(regionResource + "/{region}/" + citationResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}").Named("GetCitations")
                .And.AtUri(citationResource + "?" + RegressionRegionResource + "={regressionRegionIDs}").Named("GetRegressionRegionCitations")
                .HandledBy<CitationHandler>()
                .TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml")
                .And.TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json");
                


            ResourceSpace.Has.ResourcesOfType<Citation>()
                .AtUri(citationResource + "/{ID}")
                .HandledBy<CitationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8XmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddRegressionTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<RegressionType>>()
                .AtUri(regressionTypeResource)
                .And.AtUri(regressionTypeResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("GetRegressionTypes")
                .And.AtUri(regionResource + "/{region}/" + regressionTypeResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}").Named("GetRegressionTypes")
                .HandledBy<RegressionTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<RegressionType>()
                .AtUri(regressionTypeResource + "/{ID}")
                .HandledBy<RegressionTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");

        }
        private void AddRegressionTypeDisplayNamesEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<RegressionTypeDisplayName>>()
                .AtUri(regressionTypeDisplayNamesResource)
                .HandledBy<RegressionTypeDisplayNameHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>().ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<RegressionTypeDisplayName>()
                .AtUri(regressionTypeDisplayNamesResource + "/{ID}")
                .HandledBy<RegressionTypeDisplayNameHandler>()
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
                        .AtUri(scenarioResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}&" + unitSystemTypeResource + "={systemtypeID}&" +extensionResource+"={extensionmethods}").Named("GetScenarios")
                        .And.AtUri(regionResource + "/{region}/" + scenarioResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}&" + unitSystemTypeResource + "={systemtypeID}&" + extensionResource + "={extensionmethods}").Named("GetScenarios")

                        .And.AtUri(scenarioResource + "/estimate?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}&" + unitSystemTypeResource + "={systemtypeID}&" + extensionResource + "={extensionmethods}").Named("EstimatesScenarios")
                        .And.AtUri(regionResource + "/{region}/" + scenarioResource + "/estimate?" + RegressionRegionResource + "={regressionRegionIDs}&" + statisticGroupTypeResource + "={statisticgroups}&" + regressionTypeResource + "={regressiontypeIDs}&" + unitSystemTypeResource + "={systemtypeID}&" + extensionResource + "={extensionmethods}").Named("EstimateScenarios")
                        .HandledBy< ScenarioHandler>()
                        .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json")
                        .And.TranscodedBy<UTF8XmlSerializerCodec>().ForMediaType("application/xml;q=0.9").ForExtension("xml");
                }
        private void AddStatisticGroupTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<StatisticGroupType>>()
                .AtUri(statisticGroupTypeResource)
                .And.AtUri(statisticGroupTypeResource + "?region={region}&" + RegressionRegionResource + "={regressionRegionIDs}&" + regressionTypeResource + "={regressiontypeIDs}").Named("GetStatisticGroups")
                .And.AtUri(regionResource + "/{region}/" + statisticGroupTypeResource + "?" + RegressionRegionResource + "={regressionRegionIDs}&" + regressionTypeResource + "={regressiontypeIDs}").Named("GetStatisticGroups")
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
                .AtUri(unitTypeResource + "?" + unitSystemTypeResource + "={unitsystem}")
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