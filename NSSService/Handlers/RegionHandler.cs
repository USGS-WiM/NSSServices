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
using WiM.Exceptions;
using WiM.Resources;
using NSSDB;
using NSSService.Utilities.ServiceAgent;


namespace NSSService.Handlers
{
    public class RegionHandler:NSSHandlerBase
    {
        #region Properties
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<Region> entities = null;
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entities = sa.Select<Region>().OrderBy(e => e.ID).ToList();
                    
                    sm(MessageType.info,"Count: " + entities.Count());
                    sm(sa.Messages);
                    
                }//end using

                //hypermedia
                //entities.CreateUri();
                var msg = Messages.GroupBy(g => g.type).Select(gr => gr.Key.ToString() + ": " + string.Join(",", gr.Select(c => c.msg))).ToList();

                return new OperationResult.OK{ ResponseResource = entities, Description = string.Join(";",msg) };
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
        public OperationResult get(string region)
        {
            Region entity = null;
            
            try
            {
                using (NSSAgent sa = new NSSAgent())
                {
                    entity = sa.Select<Region>().FirstOrDefault(e => String.Equals(e.ID.ToString().Trim().ToLower(), 
                                                        region.Trim().ToLower()) || String.Equals(e.Code.Trim().ToLower(), 
                                                        region.Trim().ToLower()));
                   
                    sm(sa.Messages);
                }//end using

                //hypermedia
                //entities.CreateUri();
                var msg = Messages.GroupBy(g => g.type).Select(gr => gr.Key.ToString() + ": " + string.Join(",", gr.Select(c => c.msg))).ToList();

                return new OperationResult.OK { ResponseResource = entity , Description = string.Join(";",msg) };
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