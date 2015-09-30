//------------------------------------------------------------------------------
//----- HttpHandler ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2014 WiM - USGS

//    authors:  Jeremy K. Newson USGS Wisconsin Internet Mapping
//              
//  
//   purpose:   Handles Site resources through the HTTP uniform interface.
//              Equivalent to the controller in MVC.
//
//discussion:   Handlers are objects which handle all interaction with resources. 
//              https://github.com/openrasta/openrasta/wiki/Handlers
//
//     

#region Comments
// 08.14.14 - JKN - Created
#endregion
using OpenRasta.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSSService.Utilities.ServiceAgent;
using NSSService.Resources;
using WiM.Exceptions;

using System.Configuration;

namespace NSSService.Handlers
{
    public class ScenarioHandler:NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET, ForUriName = "GetScenarios")]
        public OperationResult GetScenarios(string region, [Optional] string regressionRegionIDs, [Optional] string statisticgroups, [Optional] string equationtypeIDs, [Optional] string systemtypeID)
        {
            List<string> statisticgroupList = null;
            List<string> equationtypeList = null;
            List<string> subregionList = null;
            List<Scenario> entities = null;
            Int32 unitsystemID = 0;
            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                statisticgroupList = parse(statisticgroups);
                equationtypeList = parse(equationtypeIDs);
                subregionList = parse(regressionRegionIDs);
                unitsystemID = Convert.ToInt32(systemtypeID);
                if (unitsystemID < 1) unitsystemID = 1;

                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.GetScenarios(region, unitsystemID, subregionList, statisticgroupList, equationtypeList).ToList();
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entities };
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {

            }//end try
        }//end Get

        [HttpOperation(HttpMethod.POST, ForUriName = "EstimateScenarios")]
        public OperationResult EstimateScenarios(string region, List<Scenario> scenarioList, [Optional] string regressionRegionIDs, [Optional] string statisticgroups, [Optional] string equationtypeIDs, [Optional] string systemtypeID)
        {
            List<string> statisticgroupList = null;
            List<string> equationtypeList = null;
            List<string> subregionList = null;
            List<Scenario> entities = null;
            Int32 unitsystemID = 0;
            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                if (scenarioList == null || scenarioList.Count() < 1) throw new BadRequestException("scenario must be specified");
                unitsystemID = Convert.ToInt32(systemtypeID);
                if (unitsystemID < 1) unitsystemID = 1;
                statisticgroupList = parse(statisticgroups);
                equationtypeList = parse(equationtypeIDs);
                subregionList = parse(regressionRegionIDs);

                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.EstimateScenarios(region,unitsystemID, scenarioList, subregionList, statisticgroupList, equationtypeList).ToList();
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entities };
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {

            }//end try
        }//end Get
        
        #endregion
        #region PUT Methods

        #endregion
        #region POST Methods

        #endregion
        #region DELETEMethods

        #endregion
        #endregion
        #region Helper Methods

        #endregion
        #region Enumerations

        #endregion
    }//end class
}//end namespace