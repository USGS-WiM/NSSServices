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
using NSSDB;
using NSSService.Utilities.ServiceAgent;


namespace NSSService.Handlers
{
    public class StatisticGroupTypeHandler : NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<StatisticGroupType> entities = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.Select<StatisticGroupType>().OrderBy(e => e.ID).ToList();
                    
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
        [HttpOperation(HttpMethod.GET, ForUriName="GetStatisticGroups")]
        public OperationResult get(string region, [Optional]string regressionRegionIDs, [Optional] string regressiontypeIDs)
        {
            //?region={region}&subregions={subregionIDs}&equationtypes={equationtypeIDs}
            List<StatisticGroupType> entities = null;
            List<string> regressionRegionIDList = null;
            List<string> regressiontypeList = null;
            List<string> msg = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(region)) throw new BadRequestException("region must be specified");
                regressionRegionIDList = parse(regressionRegionIDs);
                regressiontypeList = parse(regressiontypeIDs);
                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.GetEquations(region, regressionRegionIDList,null,regressiontypeList)
                                    .Select(e => e.StatisticGroupType).Distinct().ToList();
                    
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
            StatisticGroupType entity = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<StatisticGroupType>().FirstOrDefault(e => e.ID == ID);
                    
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