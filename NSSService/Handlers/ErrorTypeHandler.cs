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
using NSSDB;


namespace NSSService.Handlers
{
    public class ErrorTypeHandler:NSSHandlerBase
    {
        #region Properties
        public override string entityName
        {
            get { return "ErrorType"; }
        }
        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get()
        {
            List<ErrorType> entities = null;
            try
            {
                using (nssEntities c = GetRDBContext())
                {
                    entities = c.ErrorTypes.OrderBy(e => e.ID).ToList();
                }//end using

                //hypermedia
                //entities.CreateUri();

                return new OperationResult.OK { ResponseResource = entities };
            }
            catch (BadRequestException ex)
            {
                return new OperationResult.BadRequest { ResponseResource = ex.Message.ToString() };
            }
            catch (Exception ex)
            {
                return new OperationResult.InternalServerError { ResponseResource = ex.Message.ToString() };
            }
            finally
            {

            }//end try
        }//end Get
        [HttpOperation(HttpMethod.GET)]
        public OperationResult get(Int32 ID)
        {
            ErrorType entity = null;
            try
            {
                using (nssEntities c = GetRDBContext())
                {
                    entity = c.ErrorTypes.FirstOrDefault(e => e.ID == ID);
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