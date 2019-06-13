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
using System.Linq;
using WIM.Services.Attributes;
using WIM.Security.Authorization;

namespace Shared.Controllers
{
    public abstract class RegressionTypesControllerBase : WIM.Services.Controllers.ControllerBase
    {
        protected ISharedAgent shared_agent;
        public RegressionTypesControllerBase(ISharedAgent shared_sa) : base()
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        public abstract Task<IActionResult> Get(int id);

        [HttpPost(Name ="Add Regression Type")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Add.md")]
        public virtual async Task<IActionResult> Post([FromBody]RegressionType entity)
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

        [HttpPost("[action]", Name ="Regression Type Batch Upload")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Batch.md")]
        public virtual async Task<IActionResult> Batch([FromBody]List<RegressionType> entities)
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

        [HttpPut("{id}", Name ="Edit Regression Type")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Edit.md")]
        public virtual async Task<IActionResult> Put(int id, [FromBody]RegressionType entity)
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

        [HttpDelete("{id}", Name ="Delete Regression Type")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Delete.md")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await shared_agent.DeleteRegressionType(id);

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
