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


namespace NSSService.Handlers
{
    public class EquationTypeHandler:NSSHandlerBase
    {
        #region Properties
        public override string entityName
        {
            get { return "entitys"; }
        }
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<EquationType> entities = null;
            try
            {
                using (nssEntities c = GetRDBContext())
                {
                    entities = c.EquationTypes.OrderBy(e => e.ID).ToList();
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
        [HttpOperation(HttpMethod.GET, ForUriName = "GetEquationTypes")]
        public OperationResult GetEquationTypes([Optional]string regions, [Optional] string subregions, [Optional]string statisticgroups)
        {
            //?regions={regions}&subregions={subregions}&statisticgroups={statisticgroups}
            List<string> regionList = null;
            List<string> subRegionList=null;
            List<string> statisticgroupList = null;

            List<EquationType> entities = null;
            try
            {
                regionList = parse(regions);
                subRegionList = parse(subregions);
                statisticgroupList = parse(statisticgroups);

                using (nssEntities c = GetRDBContext())
                {
                    IQueryable<EquationType> qentities = c.Equations.Include("EquationTypes,Regions").Where(e => regionList.Contains(e.Region.Code) || regionList.Contains(e.RegionID.ToString())).Select(e => e.EquationType).Distinct();

                    entities = qentities.OrderBy(e => e.ID).ToList();
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
            EquationType entity = null;
            try
            {
                using (nssEntities c = GetRDBContext())
                {
                    entity = c.EquationTypes.FirstOrDefault(e => e.ID == ID);
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