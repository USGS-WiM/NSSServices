//------------------------------------------------------------------------------
//----- HttpController ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2020 WIM - USGS

//    authors:  Katrin E. Jacobsen USGS Web Informatics and Mapping
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
using WIM.Services.Attributes;
using WIM.Security.Authorization;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methodss/summary.md")]
    public class MethodsController : NSSControllerBase
    {
        public MethodsController(INSSAgent sa) : base(sa)
        { }
        #region Methods
        [HttpGet(Name = "Methods")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methods/Get.md")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetMethods());
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}", Name = "Method")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methods/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 0) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.GetMethod(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name = "Add Method")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methods/Add.md")]
        public async Task<IActionResult> Post([FromBody] Method entity)
        {
            try
            {
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                var result = await agent.Add(entity);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name = "Edit Method")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methods/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody] Method entity)
        {
            try
            {
                if (!IsAuthorizedToEdit(await agent.GetMethod(id))) return new UnauthorizedResult();

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.Update(id, entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpDelete("{id}", Name = "Delete Method")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methods/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!IsAuthorizedToEdit(await agent.GetMethod(id))) return new UnauthorizedResult();
                await agent.DeleteMethod(id);
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
