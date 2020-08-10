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

namespace NSSServices.Controllers
{
    public abstract class UnitsControllerBase : WIM.Services.Controllers.ControllerBase
    {
        protected ISharedAgent shared_agent;
        public UnitsControllerBase(ISharedAgent shared_sa) : base()
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        public abstract Task<IActionResult> Get();
        public abstract Task<IActionResult> Get(int id);
        
        [HttpPost(Name ="Add Unit")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/Add.md")]
        public virtual async Task<IActionResult> Post([FromBody]UnitType entity)
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

        [HttpPost("[action]", Name ="Unit Batch Upload")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/Batch.md")]
        public virtual async Task<IActionResult> Batch([FromBody]List<UnitType> entities)
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

        [HttpPut("{id}", Name ="Edit Unit")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/Edit.md")]
        public virtual async Task<IActionResult> Put(int id, [FromBody]UnitType entity)
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

        [HttpDelete("{id}", Name ="Delete Unit")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Units/Delete.md")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (id < 1) return new BadRequestResult();
                await shared_agent.DeleteUnit(id);
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
