﻿//------------------------------------------------------------------------------
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
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/GeneralInformations/summary.md")]
    public class GeneralInformationsController : GeneralInformationsControllerBase
    {
        protected INSSAgent agent;
        public GeneralInformationsController(INSSAgent sa, ISharedAgent shared_sa) : base(shared_sa)
        {
            this.agent = sa;
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet("{state}", Name = "GeneralInformations")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/GeneralInformations/Get.md")]
        public override async Task<IActionResult> Get(string state)
        {
            try
            {
                if (string.IsNullOrEmpty(state)) return new BadRequestResult(); // This returns HTTP 404

                var test = shared_agent.GetGeneralInfomation(state);

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