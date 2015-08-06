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
using RegressionService.Handlers;
using RegressionService.Resources;

namespace RegressionService
{
    public class Configuration:IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                // Allow codec choice by extension 
                ResourceSpace.Uses.UriDecorator<ContentTypeExtensionUriDecorator>();
                ResourceSpace.Uses.PipelineContributor<ErrorCheckingContributor>();
                ResourceSpace.Uses.PipelineContributor<CrossDomainPipelineContributor>();

                //   
                ResourceSpace.Has.ResourcesOfType<List<RegressionModel>>()
                    .AtUri("/models?state={stateID}").Named("getStateModels")
                    .HandledBy<RegressionHandler>()
                    .TranscodedBy<JsonDotNetCodec>(null).ForMediaType("application/json;q=0.9").ForExtension("json");
 
                ResourceSpace.Has.ResourcesOfType<RegressionModel>()
                    .AtUri("/models/{ModelType}/def?state={stateID}&region={regionID}").Named("getStateModelDefinitions")
                    .And.AtUri("/models/{ModelType}/estimate?state={stateID}").Named("getModelEstimate")
                    .HandledBy<RegressionHandler>()
                    .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json");

                ResourceSpace.Has.ResourcesOfType<List<Report>>()
                    .AtUri("/reports?state={stateID}").Named("getStateReports")
                    .HandledBy<ReportHandler>()
                    .TranscodedBy<JsonDotNetCodec>().ForMediaType("application/json;q=0.5").ForExtension("json");

            }//end using
        }//end Configure

    }//end configuration
}//end namespace