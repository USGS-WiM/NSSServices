﻿//------------------------------------------------------------------------------
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
using System.Data.Entity;
using WiM.Exceptions;
using NSSDB;
using NSSService.Utilities.ServiceAgent;


namespace NSSService.Handlers
{
    public class UnitTypeHandler : NSSHandlerBase
    {
        #region Properties

        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get([Optional]string unitsystem)
        {
            IQueryable<UnitType> usquery = null;
            List<UnitType> entities = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    usquery = sa.Select<UnitType>();
                    if (!string.IsNullOrEmpty(unitsystem))
                        usquery = usquery.Where(e => String.Compare(unitsystem,e.UnitSystemType.ID.ToString(),true) == 0
                                            || String.Compare(unitsystem, e.UnitSystemType.UnitSystem.Trim(),true) == 0 
                                            || e.UnitSystemTypeID == 3);

                    entities = usquery.ToList();
                    
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
            UnitType entity = null;
            List<string> msg = new List<string>();
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<UnitType>().Include(p => p.UnitConversionFactorsIn).FirstOrDefault(e => e.ID == ID);                    
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