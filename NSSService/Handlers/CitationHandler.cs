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
using NSSDB;
using WiM.Exceptions;

using System.Configuration;

namespace NSSService.Handlers
{
    public class CitationHandler:NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<Citation> entities = null;

            try
            {
                using (NSSDBAgent sa = new NSSDBAgent())
                {                    
                    entities = sa.Select<Citation>().OrderBy(e => e.ID).ToList();
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
        [HttpOperation(HttpMethod.GET, ForUriName = "GetCitations")]
        public OperationResult GetCitations(string region, [Optional] string regressionRegionIDs, [Optional] string statisticgroups, [Optional] string equationtypeIDs )
        {
            //?region={region}&subregions={subregionIDs}&statisticgroups={statisticgroups}&equationtypes={equationtypeIDs}"
            List<Citation> entities = null;
            List<string> regressionRegionIDList = null;
            List<string> statisticgroupList = null;
            List<string> equationtypeList = null;

            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                regressionRegionIDList = parse(regressionRegionIDs);
                statisticgroupList = parse(statisticgroups);
                equationtypeList = parse(equationtypeIDs);

                using (NSSDBAgent sa = new NSSDBAgent())
                {
                    entities = sa.GetEquations(region, regressionRegionIDList, statisticgroupList,equationtypeList)
                        .Select(e => e.RegressionRegion.Citation).Distinct().OrderBy(e => e.ID).ToList();

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
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get(Int32 ID)
        {
            Citation entity = null;
            try
            {
                using (NSSDBAgent sa = new NSSDBAgent())
                {
                    entity = sa.Select<Citation>().FirstOrDefault(e => e.ID == ID);
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entity };
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {

            }//end try
        }//end Get
        [HttpOperation(HttpMethod.GET, ForUriName = "getStateReports")]
        public OperationResult getStateReports(String stateID)
        {
            ServiceAgent sa = null;
            try
            {
                sa = new ServiceAgent();

                return new OperationResult.OK { ResponseResource = sa.GetJsonFromFile<List<Citation>>(stateID, "Reports") };
            }
            catch (Exception ex)
            {
                return new OperationResult.InternalServerError { ResponseResource = ex.Message.ToString() };
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