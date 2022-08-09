//------------------------------------------------------------------------------
//----- HttpController ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2022 WIM - USGS

//    authors:  Chad Fanguy USGS Web Informatics and Mapping
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
using Shared.Controllers;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using System.Linq;
using NSSAgent.Resources;
using NSSDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/ApplicationInfos/summary.md")]
    public class ApplicationInfosController : ApplicationInfosControllerBase
    {
        protected INSSAgent agent;
        public ApplicationInfosController(INSSAgent sa, ISharedAgent shared_sa) : base(shared_sa)
        {
            this.agent = sa;
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet("{application}", Name = "ApplicationInfos")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/ApplicationInfos/Get.md")]
        public override async Task<IActionResult> Get(string application)
        {
            try
            {
                if (string.IsNullOrEmpty(application)) return new BadRequestResult(); // This returns HTTP 404

                var test = shared_agent.GetApplicationInfo(application);

                return Ok(test);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        #endregion
    }
}
