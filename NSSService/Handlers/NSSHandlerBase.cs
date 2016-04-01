﻿using System;
using System.Collections.Generic;
using System.Linq;

using WiM.Handlers;

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
        

        #endregion
        #region Base Routed Methods

        #endregion

        #region "Base Methods"
        public bool IsAuthorizedToEdit(string OwnerUserName)
        {
            if (string.Equals(OwnerUserName.ToLower(), username.ToLower()))
                return true;

            if (IsAuthorized(AdminRole))
                return true;

            return false;
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
