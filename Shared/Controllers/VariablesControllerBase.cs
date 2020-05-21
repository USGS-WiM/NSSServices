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
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using Shared.Resources;

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
        
        [HttpPost(Name ="Add Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Add.md")]
        public async Task<IActionResult> Post([FromBody] VariableWithUnit entity)
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

        [HttpPost("[action]", Name ="Variable Batch Upload")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Batch.md")]
        public async Task<IActionResult> Batch([FromBody]List<VariableWithUnit> entities)
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

        [HttpPut("{id}", Name ="Edit Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody] VariableWithUnit entity)
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

        [HttpDelete("{id}", Name ="Delete Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                await shared_agent.DeleteVariable(id);
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
