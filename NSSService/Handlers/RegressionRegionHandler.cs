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
using WiM.Resources;
using NSSDB;
using NSSService.Utilities.ServiceAgent;
using System.Data.Entity;


namespace NSSService.Handlers
{
    public class RegressionRegionHandler : NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<RegressionRegion> entities = null;
            
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.Select<RegressionRegion>().OrderBy(e => e.ID).ToList();
                    
                    sm(MessageType.info,"Count: " + entities.Count());
                    sm(sa.Messages);
                    
                }//end using

                //hypermedia
                //entities.CreateUri();
                var msg = Messages.GroupBy(g => g.type).Select(gr => gr.Key.ToString() + ": " + string.Join(",", gr.Select(c => c.msg))).ToList();

                return new OperationResult.OK { ResponseResource = entities, Description = string.Join(";",msg) };
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
            finally
            {

            }//end try
        }//end Get
        [HttpOperation(HttpMethod.GET, ForUriName = "GetRegressionRegion")]
        public OperationResult GetRegressionRegions(string region, [Optional]string statisticgroups, [Optional] string regressiontypeIDs)
        {
            List<RegressionRegion> entities = null;
            List<string> statisticGroupList = null;
            List<string> regressiontypeList = null;

            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                statisticGroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypeIDs);

                using (NSSAgent sa = new NSSAgent())
                {
                    
                    entities = sa.GetEquations(region, null, statisticGroupList, regressiontypeList)
                                    .Select(e => e.RegressionRegion).Distinct().ToList();

                    
                    
                    sm(MessageType.info,"Count: " + entities.Count());
                    sm(sa.Messages);
                    
                }//end using

                //hypermedia
                //entities.CreateUri();               

                var msg = Messages.GroupBy(g => g.type).Select(gr => gr.Key.ToString()+ ": " + string.Join(",", gr.Select(c => c.msg))).ToList();

                return new OperationResult.OK { ResponseResource = entities, Description = string.Join(";",msg) };
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
            RegressionRegion entity = null;
            
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<RegressionRegion>().FirstOrDefault(e => e.ID == ID);
                    
                    sm(sa.Messages);                    
                }//end using

                //hypermedia
                //entities.CreateUri();

                var msg = Messages.GroupBy(g => g.type).Select(gr => gr.Key.ToString() + ": " + string.Join(",", gr.Select(c => c.msg))).ToList();

                return new OperationResult.OK { ResponseResource = entity, Description = string.Join(";",msg) };
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