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
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Statuses/summary.md")]
    public class StatusesController : NSSControllerBase
    {
        public StatusesController(INSSAgent sa) : base(sa)
        { }
        #region METHODS
        [HttpGet(Name = "Statuses")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Statuses/Get.md")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetStatuses());
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }



        #endregion
    }
}
