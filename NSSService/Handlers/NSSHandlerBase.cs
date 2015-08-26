using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using System.Collections.ObjectModel;
using System.Configuration;

using OpenRasta.Web;
using NSSDB;
using WiM.Resources.Hypermedia;
using WiM.Handlers;
using WiM.Authentication;
using WiM.Exceptions;

namespace NSSService.Handlers
{
    public abstract class NSSHandlerBase : HandlerBase
    {
        #region Constants
        protected const string AdminRole = "Admin";
        protected const string ManagerRole = "RegionManager";
        protected const string PublicRole = "Public";

        #endregion

        #region "Base Properties"
        protected String connectionString = ConfigurationManager.ConnectionStrings["nssEntities"].ConnectionString;

        public abstract string entityName { get; }
        #endregion
        #region Base Routed Methods

        #endregion

        #region "Base Methods"
        public bool IsAuthorizedToEdit(string OwnerUserName)
        {
            if (string.Equals(OwnerUserName.ToUpper(), username.ToUpper()))
                return true;

            if (IsAuthorized(AdminRole))
                return true;

            return false;
        }

        protected nssEntities GetRDBContext(EasySecureString password)
        {
            return new nssEntities(string.Format(connectionString, Context.User.Identity.Name, password.decryptString()));
        }

        protected nssEntities GetRDBContext()
        {
            var rdb = new nssEntities(string.Format(connectionString, "**username**", "***REMOVED***"));
            //rdb.Configuration.ProxyCreationEnabled = false;
            rdb.Configuration.ProxyCreationEnabled = false; 
            return rdb;
        }

        protected override EasySecureString GetSecuredPassword()
        {
            return new EasySecureString("***REMOVED***");
            //return new EasySecureString(STNBasicAuthentication.ExtractBasicHeader(Context.Request.Headers["Authorization"]).Password);
        }

        protected OperationResult HandleException(Exception ex)
        {
            if (ex is BadRequestException)
                return new OperationResult.NotFound { ResponseResource = ex.Message.ToString() };
            if (ex is NotFoundRequestException)
                return new OperationResult.NotFound { ResponseResource = ex.Message.ToString() };
            else
                return new OperationResult.InternalServerError { ResponseResource = ex.Message.ToString() };
        }

        protected List<string> parse(string items)
        {
            if (items == null) items = string.Empty;
            char[] delimiterChars = { ';', ',' };
            return items.ToLower().Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        #endregion


    }//end class HandlerBase
}
