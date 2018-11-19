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
using NSSDB.Resources;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class CitationsController : NSSControllerBase
    {
        public CitationsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet]
        [HttpGet("/Regions/{regions}/[controller]")]
        public async Task<IActionResult> Get(string regions="", [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<Citation> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
                if(String.IsNullOrEmpty(regions)&&String.IsNullOrEmpty(regressionRegions)&& 
                    string.IsNullOrEmpty(statisticgroups)&& string.IsNullOrEmpty(regressiontypes))
                { entities = agent.GetCitations(); }
                else
                {
                    RegionList = parse(regions);
                    regressionRegionList = parse(regressionRegions);
                    statisticgroupList = parse(statisticgroups);
                    regressiontypeList = parse(regressiontypes);

                    entities = agent.GetCitations(RegionList, regressionRegionList, statisticgroupList, regressiontypeList);

                }
                this.sm(agent.Messages);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                this.sm(agent.Messages);
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await agent.GetCitation(id));
            }
            catch (Exception ex)
            {
                this.sm(agent.Messages);
                return await HandleExceptionAsync(ex);
            }
        }

        //
        //POST/Delete should occur at the Regression region level
        //

        [HttpPut("{id}")][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Put(int id, [FromBody]Citation entity)
        {
            try
            {
                if (!IsAuthorizedToEdit<Citation>(await agent.GetCitation(id)))
                    return new UnauthorizedResult();
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                this.sm(agent.Messages);
                return await HandleExceptionAsync(ex);
            }
        }        

        #endregion
        #region HELPER METHODS
        #endregion
    }
}
