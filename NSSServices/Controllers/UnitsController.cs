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
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class UnitsController : NSSControllerBase
    {
        public UnitsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet]
        public async Task<IActionResult> Get()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
        
        [HttpPost][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromBody]UnitType entity)
        {
            try
            {
                 if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                //return Ok(await agent.Add<UnitType>(entity));
                return NotFound();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost][Authorize(Policy = "AdminOnly")]
        [Route("Batch")]
        public async Task<IActionResult> Batch([FromBody]List<UnitType> entities)
        {
            try
            {
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                //return Ok(await agent.Add<UnitType>(entities));
                return NotFound();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Put(int id, [FromBody]UnitType entity)
        {
            try
            {
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                //return Ok(await agent.Update<UnitType>(id,entity));
                return NotFound();
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
                //var entity = await agent.Find<UnitType>(id);
                //if (entity == null) return new NotFoundResult();
                //await agent.Delete<UnitType>(entity);

                //return Ok();
                return NotFound();
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
