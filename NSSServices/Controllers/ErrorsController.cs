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
using SharedAgent;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class ErrorsController : NSSControllerBase
    {
        protected ISharedAgent shared_agent;
        public ErrorsController(INSSAgent sa, ISharedAgent shared_sa) : base(sa)
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetErrors());  
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
                return Ok(await agent.GetError(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post([FromBody]ErrorType entity)
        {
            try
            {
                entity.ID = 0;
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await shared_agent.Add(entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Batch([FromBody]List<ErrorType> entities)
        {
            try
            {

                entities.ForEach(e => e.ID = 0);
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");
                return Ok(await shared_agent.Add(entities));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Put(int id, [FromBody]ErrorType entity)
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

        [HttpDelete("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await shared_agent.DeleteError(id);
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
