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
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WiM.Exceptions;
using NSSService.Utilities.ServiceAgent;
using NSSDB;


namespace NSSService.Handlers
{
    public class RegressionTypeHandler:NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<RegressionType> entities = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.Select<RegressionType>().OrderBy(e => e.ID).ToList();
                    msg.Add("Count: " + entities.Count());
                    msg.AddRange(sa.Messages);
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entities, Description = string.Join(";", msg) };
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {

            }//end try
        }//end Get
        [HttpOperation(HttpMethod.GET, ForUriName = "GetRegressionTypes")]
        public OperationResult GetRegressionTypes(string region, [Optional] string regressionRegionIDs, [Optional]string statisticgroups)
        {
            //?region={region}&subregions={subregionIDs}&statisticgroups={statisticgroups}
            List<string> regressionRegionIDList=null;
            List<string> statisticgroupList = null;
            List<string> msg = new List<string>();
            List<RegressionType> entities = null;
            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");

                regressionRegionIDList = parse(regressionRegionIDs);
                statisticgroupList = parse(statisticgroups);

                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.GetEquations(region, regressionRegionIDList, statisticgroupList)
                        .Select(e => e.RegressionType).Distinct().OrderBy(e => e.ID).ToList();
                    msg.Add("Count: " + entities.Count());
                    msg.AddRange(sa.Messages);
                    
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entities, Description = string.Join(";", msg) };
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
            RegressionType entity = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<RegressionType>().FirstOrDefault(e => e.ID == ID);
                    msg.AddRange(sa.Messages);
                    
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entity, Description = string.Join(";", msg) };
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