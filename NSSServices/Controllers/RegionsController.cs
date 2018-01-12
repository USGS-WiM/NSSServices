//------------------------------------------------------------------------------
//----- HttpController ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2017 WiM - USGS

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
using NSSDB.Resources;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class RegionsController : NSSControllerBase
    {
        public RegionsController(INSSAgent sa) : base(sa)
        {}
        #region METHODS
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var query = agent.Select<Region>();
                if (User.Identity.IsAuthenticated && !User.IsInRole("Administrator"))
                {
                    query = query.Include(r => "RegionManagers.Regions").Where(r => r.Managers
                                    .Any(rm => rm.ID == LoggedInUser().ID)).Select(r => new Region()
                                                                                        {
                                                                                            Description = r.Description,
                                                                                            ID =r.ID,
                                                                                            Name = r.Name,
                                                                                            Code = r.Code
                                                                                        });
                }//end if
                return Ok(query);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
            
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                if (String.IsNullOrEmpty(id)) return new BadRequestResult();
                var item = agent.GetRegionByIDOrCode(id);
                if (item == null) return new BadRequestObjectResult(new Error(errorEnum.e_badRequest));
                return Ok(item);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        
        [HttpPost][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post([FromBody]Region entity)
        {
            try
            {
                if (!isValid(entity)) return new BadRequestResult();
                return Ok(await agent.Add<Region>(entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost][Authorize(Policy = "AdminOnly")]
        [Route("Batch")]
        public async Task<IActionResult> Batch([FromBody]List<Region> entities)
        {
            try
            {
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                return Ok(await agent.Add<Region>(entities));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Put(int id, [FromBody]Region entity)
        {
            try
            {
                if (id < 1 || !isValid(entity)) return new BadRequestResult();
                return Ok(await agent.Update<Region>(id, entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }
        
        [HttpDelete("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (id < 1) return new BadRequestResult();
                var entity = await agent.Find<Region>(id);
                if (entity == null) return new NotFoundResult();
                await agent.Delete<Region>(entity);

                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        #endregion
        #region HELPER METHODS
        #endregion
    }
}
