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

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Methodss/summary.md")]
    public class MethodsController : NSSControllerBase
    {
        public MethodsController(INSSAgent sa) : base(sa)
        { }
        #region MethodsS
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

        #endregion
    }
}
