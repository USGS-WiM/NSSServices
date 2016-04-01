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
using System.Runtime.InteropServices;
using NSSService.Utilities.ServiceAgent;
using NSSDB;
using WiM.Exceptions;
using WiM.Resources;


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
                using (NSSAgent sa = new NSSAgent())
                {                    
                    entities = sa.Select<Citation>().OrderBy(e => e.ID).ToList();
                    
                    sm(MessageType.info,"Count: " + entities.Count());
                    sm(sa.Messages);
                    
                }//end using
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
        [HttpOperation(HttpMethod.GET, ForUriName = "GetCitations")]
        public OperationResult GetCitations(string region, [Optional] string regressionRegionIDs, [Optional] string statisticgroups, [Optional] string regressiontypeIDs)
        {
            //?region={region}&subregions={subregionIDs}&statisticgroups={statisticgroups}&equationtypes={equationtypeIDs}"
            List<Citation> entities = null;
            List<string> regressionRegionIDList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;

            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                regressionRegionIDList = parse(regressionRegionIDs);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypeIDs);

                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.GetEquations(region, regressionRegionIDList, statisticgroupList,regressiontypeList)
                        .Select(e => e.RegressionRegion.Citation).Distinct().OrderBy(e => e.ID).ToList();

                }//end using

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
        [HttpOperation(HttpMethod.GET, ForUriName = "GetRegressionRegionCitations")]
        public OperationResult GetRegressionRegionCitations(string regressionRegionIDs)
        {
            //?region={region}&subregions={subregionIDs}&statisticgroups={statisticgroups}&equationtypes={equationtypeIDs}"
            List<Citation> entities = null;
            List<string> regressionRegionIDList = null;
            try
            {
                if (string.IsNullOrEmpty(regressionRegionIDs)) throw new BadRequestException("regression regions must be specified");
                regressionRegionIDList = parse(regressionRegionIDs);

                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.Select<RegressionRegion>().Where(rr=>regressionRegionIDList.Contains(rr.ID.ToString())).Select(rr=>rr.Citation).Distinct().ToList();

                }//end using

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
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<Citation>().FirstOrDefault(e => e.ID == ID);                    
                    sm(sa.Messages);                    
                }//end using
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