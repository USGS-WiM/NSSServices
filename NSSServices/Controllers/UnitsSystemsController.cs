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
using WIM.Services.Attributes;
using WIM.Security.Authorization;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/summary.md")]
    public class UnitsSystemsController : NSSControllerBase
    {
        protected ISharedAgent shared_agent;
        public UnitsSystemsController(INSSAgent sa, ISharedAgent shared_sa) : base(sa)
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet(Name ="Unit Systems")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/Get.md")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetUnitSystems());  
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }      
        }

        [HttpGet("{id}", Name ="Unit System")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id<0) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.GetUnitSystem(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        
        [HttpPost(Name ="Add Unit System")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/Add.md")]
        public async Task<IActionResult> Post([FromBody]UnitSystemType entity)
        {
            try
            {
                 if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await shared_agent.Add(entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPut("{id}", Name ="Edit Unit System")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]UnitSystemType entity)
        {
            try
            {
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await shared_agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }        

        [HttpDelete("{id}", Name ="Delete Unit System")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/UnitSystems/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                await shared_agent.DeleteUnitSystem(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        #endregion
    }
}
