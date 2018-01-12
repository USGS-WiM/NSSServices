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

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class CitationsController : NSSControllerBase
    {
        public CitationsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.Select<Citation>());  
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }      
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.Find<Citation>(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("Regions/{region}/[controller]")]
        [HttpGet("[controller]?region={region}")]
        public async Task<IActionResult> GetCitations(int region,[FromQuery] string regressionRegions ="", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            try
            {
                //if (id < 0) return new BadRequestResult(); // This returns HTTP 404

                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("[controller]")]
        public async Task<IActionResult> GetRegressionRegionCitations([FromQuery] string regressionRegions)
        {
            try
            {
                //if (id < 0) return new BadRequestResult(); // This returns HTTP 404

                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromBody]Citation entity)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await agent.Add<Citation>(entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost][Authorize(Policy = "CanModify")]
        [Route("Batch")]
        public async Task<IActionResult> Batch([FromBody]List<Citation> entities)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager

                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                return Ok(await agent.Add<Citation>(entities));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Put(int id, [FromBody]Citation entity)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await agent.Update<UnitType>(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }        

        [HttpDelete("{id}")][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager
                if (id < 1) return new BadRequestResult();
                var entity = await agent.Find<UnitType>(id);
                if (entity == null) return new NotFoundResult();
                await agent.Delete<UnitType>(entity);

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
