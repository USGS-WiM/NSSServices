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
using RegressionService.Utilities.ServiceAgent;
using RegressionService.Resources;

using System.Configuration;

namespace RegressionService.Handlers
{
    public class ReportHandler
    {
        #region Properties

        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET, ForUriName = "getStateReports")]
        public OperationResult getStateReports(String stateID)
        {
            ServiceAgent sa = null;
            try
            {
                sa = new ServiceAgent();

                return new OperationResult.OK { ResponseResource = sa.GetJsonFromFile<List<Report>>(stateID, "Reports") };
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