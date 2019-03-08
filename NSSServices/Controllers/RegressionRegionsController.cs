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
    public class RegressionRegionsController : NSSControllerBase
    {
        public RegressionRegionsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet]
        [HttpGet("/Regions/{regions}/[controller]")]
        public async Task<IActionResult> GetRegressionRegions(string regions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<RegressionRegion> entities = null;
            List<string> RegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
                if (String.IsNullOrEmpty(regions) &&
                    string.IsNullOrEmpty(statisticgroups) && string.IsNullOrEmpty(regressiontypes))
                { entities = agent.GetRegressionRegions(); }
                else
                {
                    RegionList = parse(regions);
                    statisticgroupList = parse(statisticgroups);
                    regressiontypeList = parse(regressiontypes);

                    entities = agent.GetRegressionRegions(RegionList, statisticgroupList, regressiontypeList);
                }

                return Ok(entities);
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

                return Ok(await agent.GetRegressionRegion(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost]
        [HttpPost("/Regions/{region}/[controller]")]
        [Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromQuery]string region, [FromBody]RegressionRegion entity)
        {
            try
            {
                //entity must include citation
                if (!isValid(entity)) return BadRequest(); // This returns HTTP 404
                Region regionEntity = agent.GetRegionByIDOrCode(region);
                if (regionEntity == null) return BadRequest($"No region exists with {region} identifier.");              
                if (!IsAuthorizedToEdit(regionEntity)) return Unauthorized();

                RegionRegressionRegion Addeditem = await agent.Add(new RegionRegressionRegion
                                                                        {
                                                                            RegionID = regionEntity.ID,
                                                                            RegressionRegion = entity
                                                                        });
                return Ok(Addeditem.RegressionRegion);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "CanModify")]
        [HttpPost("/Regions/{region}/[controller]/[action]")]
        public async Task<IActionResult> Batch([FromQuery]string region,[FromBody]List<RegressionRegion> entities)
        {
            List<RegionRegressionRegion> rrr = new List<RegionRegressionRegion>();
            try
            {
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");
                Region regionEntity = agent.GetRegionByIDOrCode(region);
                if (regionEntity == null) return BadRequest($"No region exists with {region} identifier.");
                if (!IsAuthorizedToEdit(regionEntity)) return Unauthorized();
                                
                entities.ForEach(rr=>rrr.Add(new RegionRegressionRegion() { RegionID = regionEntity.ID, RegressionRegion = rr }));
                var results = await agent.Add(rrr);

                return Ok( results.Select(r=>r.RegressionRegion));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Put(int id, [FromBody]RegressionRegion entity)
        {
            try
            {

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                RegressionRegion rr = await agent.GetRegressionRegion(id);
                if (rr == null) return BadRequest($"No regression region exists with {id} identifier.");
                if (!IsAuthorizedToEdit(rr)) return Unauthorized();

                return Ok(await agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }        

        [HttpDelete("{id}")][Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                RegressionRegion rr = await agent.GetRegressionRegion(id);
                if (rr == null) return BadRequest($"No regression region exists with {id} identifier.");
                if (!IsAuthorizedToEdit(rr)) return Unauthorized();

                await agent.DeleteRegressionRegion(id);

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
