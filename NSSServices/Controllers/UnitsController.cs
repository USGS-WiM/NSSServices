﻿//------------------------------------------------------------------------------
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
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using Shared;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using Shared.Controllers;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/summary.md")]
    public class UnitsController : UnitsControllerBase
    {
        protected INSSAgent agent;
        public UnitsController(INSSAgent sa, ISharedAgent shared_sa) : base(shared_sa)
        {
            this.agent = sa;
        }

        #region METHOD
        [HttpGet(Name ="Units")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/Get.md")]
        public override async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetUnits());  
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }      
        }

        [HttpGet("{id}", Name ="Unit")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/GetDistinct.md")]
        public override async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.GetUnit(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        
 
        #endregion
    }
}
