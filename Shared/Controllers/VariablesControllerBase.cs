//------------------------------------------------------------------------------
//----- HttpController ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2019 WIM - USGS

//    authors:  Jeremy K. Newson USGS Web Informatics and Mapping
//              
//  
//   purpose:   Handles resources through the HTTP uniform interface.
//
//discussion:   Controllers are objects which handle all interaction with resources. 
//              
//
// 

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using SharedDB.Resources;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using WIM.Services.Attributes;
using WIM.Security.Authorization;

namespace Shared.Controllers
{
    public abstract class VariablesControllerBase : WIM.Services.Controllers.ControllerBase
    {
        protected ISharedAgent shared_agent;
        public VariablesControllerBase(ISharedAgent shared_sa) : base()
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        public abstract Task<IActionResult> Get();
        public abstract Task<IActionResult> Get(int id);
        #endregion
    }
}
