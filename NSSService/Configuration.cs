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

namespace NSSService
{
    public class Configuration:IConfigurationSource
    {
        #region Private Field Properties
        private string citationResource = "/citations";
        private string EquationResource = "/equations";
        private string EquationTypeResource = "/equationtypes";
        private string EquationTypeDisplayNamesResource = "/equationtypedisplaynames";
        private string ErrorTypeResource = "/errortypes";
        private string PredictionIntervalResource = "/predictionintervals";
        private string RegionResource = "/regions";
        private string StatisticGroupTypeResource = "/statisticgrouptypes";
        private string SubRegionResource = "/subregions";
        private string UnitConversionFactorResource = "/conversionfactors";
        private string UnitSystemTypeResource = "/unitsystems";
        private string UnitTypeResource = "/unittypes";
        private string UserTypeResource = "/usertypes";
        private string VariableResource = "/variables";
        private string VariableTypeResource = "/variabletypes";
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
                AddEquationEndpoints();
                AddEquationTypeEndpoints();
                AddEquationTypeDisplayNamesEndpoints();
                AddErrorTypeEndpoints();
                AddPredictionIntervalEndpoints();
                AddRegionEndpoints();
                AddStatisticGroupTypeEndpoints();
                AddSubRegionEndpoints();
                AddUnitConversionFactorEndpoints();
                AddUnitSystemTypeEndpoints();
                AddUnitTypeEndpoints();
                AddUserTypeEndpoints();
                AddVariableEndpoints();
                AddVariableTypeEndpoints();
                

                //ResourceSpace.Has.ResourcesOfType<RegressionModel>()
                //    .AtUri("/models/{ModelType}/def?state={stateID}&region={regionID}").Named("getStateModelDefinitions")
                //    .And.AtUri("/models/{ModelType}/estimate?state={stateID}").Named("getModelEstimate")
                //    .HandledBy<RegressionHandler>()
                //    .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json");

                //ResourceSpace.Has.ResourcesOfType<List<Report>>()
                //    .AtUri("/reports?state={stateID}").Named("getStateReports")
                //    .HandledBy<ReportHandler>()
                //    .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json");

            }//end using
        }//end Configure

        #region Helper methods  
        private void AddCitationEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Citation>>()
                .AtUri(citationResource)
                .HandledBy<CitationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Citation>()
                .AtUri(citationResource + "/{ID}")
                .HandledBy<CitationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddEquationEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Equation>>()
                .AtUri(EquationResource)
                .And.AtUri(EquationResource + "?region={regionid}&subregion={subregionid}&statisticgroup={statisticgroupid}&")
                .HandledBy<EquationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Equation>()
                .AtUri(EquationResource + "/{ID}")
                .HandledBy<EquationHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddEquationTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<EquationType>>()
                .AtUri(EquationTypeResource)
                .And.AtUri(EquationTypeResource + "?regions={regions}&subregions={subregions}&statisticgroups={statisticgroups}").Named("GetEquationTypes")
                .HandledBy<EquationTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<EquationType>()
                .AtUri(EquationTypeResource + "/{ID}")
                .HandledBy<EquationTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddEquationTypeDisplayNamesEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<EquationTypeDisplayName>>()
                .AtUri(EquationTypeDisplayNamesResource)
                .HandledBy<EquationTypeDisplayNameHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<EquationTypeDisplayName>()
                .AtUri(EquationTypeDisplayNamesResource + "/{ID}")
                .HandledBy<EquationTypeDisplayNameHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddErrorTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<ErrorType>>()
                .AtUri(ErrorTypeResource)
                .HandledBy<ErrorTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<ErrorType>()
                .AtUri(ErrorTypeResource + "/{ID}")
                .HandledBy<ErrorTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddPredictionIntervalEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<PredictionInterval>>()
                .AtUri(PredictionIntervalResource)
                .HandledBy<PredictionIntervalHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<PredictionInterval>()
                .AtUri(PredictionIntervalResource + "/{ID}")
                .HandledBy<PredictionIntervalHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddRegionEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Region>>()
                .AtUri(RegionResource)
                .HandledBy<RegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Region>()
                .AtUri(RegionResource + "/{ID}")
                .And.AtUri(RegionResource + "?rcode={regionCode}").Named("GetRegionByCode")
                .HandledBy<RegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddStatisticGroupTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<StatisticGroupType>>()
                .AtUri(StatisticGroupTypeResource)
                .HandledBy<StatisticGroupTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<StatisticGroupType>()
                .AtUri(StatisticGroupTypeResource + "/{ID}")
                .HandledBy<StatisticGroupTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddSubRegionEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<SubRegion>>()
                .AtUri(SubRegionResource)
                .HandledBy<SubRegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<SubRegion>()
                .AtUri(SubRegionResource + "/{ID}")
                .HandledBy<SubRegionHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddUnitConversionFactorEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitConversionFactor>>()
                .AtUri(UnitConversionFactorResource)
                .HandledBy<UnitConversionFactorHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitConversionFactor>()
                .AtUri(UnitConversionFactorResource + "/{ID}")
                .HandledBy<UnitConversionFactorHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddUnitSystemTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitSystemType>>()
                .AtUri(UnitSystemTypeResource)
                .HandledBy<UnitSystemTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitSystemType>()
                .AtUri(UnitSystemTypeResource + "/{ID}")
                .HandledBy<UnitSystemTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddUnitTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UnitType>>()
                .AtUri(UnitTypeResource)
                .HandledBy<UnitTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UnitType>()
                .AtUri(UnitTypeResource + "/{ID}")
                .HandledBy<UnitTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddUserTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<UserType>>()
                .AtUri(UserTypeResource)
                .HandledBy<UserTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<UserType>()
                .AtUri(UserTypeResource + "/{ID}")
                .HandledBy<UserTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddVariableEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<Variable>>()
                .AtUri(VariableResource)
                .HandledBy<VariableHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<Variable>()
                .AtUri(VariableResource + "/{ID}")
                .HandledBy<VariableHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        private void AddVariableTypeEndpoints()
        {
            ResourceSpace.Has.ResourcesOfType<List<VariableType>>()
                .AtUri(VariableTypeResource)
                .HandledBy<VariableTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");


            ResourceSpace.Has.ResourcesOfType<VariableType>()
                .AtUri(VariableTypeResource + "/{ID}")
                .HandledBy<VariableTypeHandler>()
                .TranscodedBy<JsonEntityDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json")
                .And.TranscodedBy<UTF8EntityXmlSerializerCodec>(null).ForMediaType("application/xml;q=0.4").ForExtension("xml");

        }
        #endregion
    }//end configuration
}//end namespace