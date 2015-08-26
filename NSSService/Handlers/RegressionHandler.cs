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
// 08.01.14 - JKN - Created
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
using WiM.Handlers;
using WiM.Resources;

using System.Configuration;

namespace NSSService.Handlers
{
    public class RegressionHandler: HandlerBase
    {
        #region Properties

        #endregion
        #region CRUD Methods
        #region GET Methods
        [HttpOperation(HttpMethod.GET, ForUriName = "getStateModels")]
        public OperationResult getStateModels(String stateID)
        {
            ServiceAgent sa = null;
            List<RegressionModel> response;
            try
            {
                sa = new ServiceAgent();
                response = sa.GetJsonFromFile<List<RegressionModel>>(stateID,"Models");

                if (response != null)
                    response.ForEach(x => x.LoadLinks(Context.ApplicationBaseUri.AbsoluteUri, linkType.e_group));
                    

                return new OperationResult.OK { ResponseResource = response};
            }
            catch (Exception ex)
            {
                return new OperationResult.InternalServerError { ResponseResource = ex.Message.ToString() };
            }
            finally
            {

            }//end try
        }//end Get

        [HttpOperation(HttpMethod.GET, ForUriName = "getStateModelDefinitions")]
        public OperationResult getStateModelDefinitions(String ModelType, String stateID, [Optional]String regionID)
        {
            ServiceAgent sa = null;
            RegressionModel response;
            try
            {
                sa = new ServiceAgent();
                response = sa.GetJsonFromFile<RegressionModel>(stateID, ModelType);

                if(!String.IsNullOrEmpty(regionID))
                    response.Regions.RemoveAll(r => !String.Equals(r.Region.Trim().ToUpper(), regionID.Trim().ToUpper()));

                if (response != null)
                    response.LoadLinks(Context.ApplicationBaseUri.AbsoluteUri, linkType.e_individual);

                return new OperationResult.OK { ResponseResource = response };
            }
            catch (Exception ex)
            {
                return new OperationResult.InternalServerError { ResponseResource = ex.Message.ToString() };
            }
            finally
            {

            }//end try
        }//end Get

        [HttpOperation(HttpMethod.POST, ForUriName = "getModelEstimate")]
        public OperationResult getModelEstimate(String ModelType, String stateID, RegressionModel model)
        {
            RegressionModel myRegmodel = null;
            try
            {
                //Get model type based on model.type
                switch (ModelType)
                {
                    case "FDCTM":
                        myRegmodel = new FDCTM(model, stateID);
                        break;
                    case "FLA":
                        myRegmodel = new FlowAnywhere(model, stateID);
                        break;
                    case "QHigh":
                        model.ModelType = ModelType;
                        myRegmodel = new QRegression(model, stateID);
                        break;
                    default:
                        break;
                }

                //test if model is valid
                if (!myRegmodel.CanLoad) return new OperationResult.BadRequest { ResponseResource = myRegmodel.Message };
                if (!myRegmodel.Load()) return new OperationResult.BadRequest { ResponseResource = myRegmodel.Message };

                if (!myRegmodel.Execute()) return new OperationResult.BadRequest { ResponseResource = myRegmodel.Message };
              

                if (myRegmodel != null)
                    myRegmodel.LoadLinks(Context.ApplicationBaseUri.AbsoluteUri, linkType.e_individual);

                return new OperationResult.OK { ResponseResource = myRegmodel };
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