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
using NSSAgent.Resources;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class ScenariosController : NSSControllerBase
    {
        public ScenariosController(INSSAgent sa) : base(sa)
        { }

        #region METHOD

        [HttpGet("Regions/{region}/[controller]")]
        [HttpGet("[controller]?region={region}")]
        public async Task<IActionResult> GetScenarios(int region,[FromQuery] string regressionRegions ="", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem=1, [FromQuery] Int32 config =1, [FromQuery] string extensions ="")
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
        [HttpGet("Regions/{region}/[controller]/[action]")]
        [HttpGet("[controller]/[action]?region={region}")]
        public async Task<IActionResult> Estimate([FromQuery] string regressionRegions)
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


        //[HttpPost]
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] Int32 year, [FromQuery]Int32? endyear = null, [FromBody] object basin = null,
        //                                        [FromQuery]string sources = "", [FromQuery]bool includePermits = false,
        //                                        [FromQuery]bool computeDomestic = false)
        //{
        //    try
        //    {
        //        if (year < 1950 || (basin == null && string.IsNullOrEmpty(sources))) return new BadRequestResult(); //return HTTP 404

        //        if (includePermits) agent.IncludePermittedWithdrawals = includePermits;

        //        if (computeDomestic)
        //            agent.ComputeDomesticWateruse();

        //        if (!string.IsNullOrEmpty(sources))
        //        {
        //            if (!User.Identity.IsAuthenticated) return new UnauthorizedResult();// return HTTP 401
        //            return Ok(agent.GetWateruse(parse(sources), year, endyear));
        //        }//end if

        //        if (basin == null) return new BadRequestResult(); //return HTTP 404
        //        return Ok(agent.GetWateruse(basin, year, endyear));
        //    }
        //    catch (Exception ex)
        //    {
        //        return await HandleExceptionAsync(ex);
        //    }
        //}

        //[HttpPost]
        //[HttpGet]
        //[Authorize(Policy = "Restricted")]
        //[Route("BySource")]
        //public async Task<IActionResult> BySource([FromQuery] Int32 year, [FromQuery]Int32? endyear = null, [FromBody] object basin = null,
        //                                            [FromQuery]string sources = "", [FromQuery]bool includePermits = false,
        //                                            [FromQuery]bool computeDomestic = false)
        //{
        //    try
        //    {
        //        if (year < 1950 || (basin == null && string.IsNullOrEmpty(sources))) return new BadRequestResult(); //return HTTP 404

        //        if (includePermits) agent.IncludePermittedWithdrawals = includePermits;

        //        if (computeDomestic)
        //            agent.ComputeDomesticWateruse();

        //        if (!string.IsNullOrEmpty(sources))
        //        {
        //            if (!User.Identity.IsAuthenticated) return new UnauthorizedResult(); //return HTTP 404                   
        //            return Ok(agent.GetWaterusebySource(parse(sources), year, endyear));
        //        }//end if

        //        if (basin == null) return new BadRequestResult(); //return HTTP 401
        //        return Ok(agent.GetWaterusebySource(basin, year, endyear));
        //    }
        //    catch (Exception ex)
        //    {
        //        return await HandleExceptionAsync(ex);
        //    }
        //}

        [HttpPost][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromBody]Scenario entity)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await agent.Add<Scenario>(entity));
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
