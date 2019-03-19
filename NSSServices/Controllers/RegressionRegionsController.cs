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
using GeoAPI.Geometries;
using WIM.Exceptions.Services;

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
        [HttpPost("[action]")]
        [HttpPost("/Regions/{regions}/[controller]")]
        public async Task<IActionResult> Get(string regions = "", [FromBody] IGeometry geom = null, [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            String[] allowableGeometries = new String[] { "Polygon", "MuliPolygon" };
            IQueryable<RegressionRegion> entities = null;
            List<string> RegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
                if (geom != null && !allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', allowableGeometries));
                                
                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);                
                
                entities = agent.GetRegressionRegions(RegionList,geom, statisticgroupList,regressiontypeList);                

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
        public async Task<IActionResult> Post(string region, [FromBody]RegressionRegion entity)
        {
            try
            {
                entity.ID = 0;
                //entity must include citation
                if (!isValid(entity)) return BadRequest(); // This returns HTTP 404
                Region regionEntity = agent.GetRegionByIDOrCode(region);
                if (regionEntity == null) return BadRequest($"No region exists with {region} identifier.");              
                if (!IsAuthorizedToEdit(regionEntity)) return Unauthorized();

                entity.StatusID = (entity.CitationID != null || entity.Citation != null) ? (int?)2 : (int?)1;

                entity.RegionRegressionRegions = new List<RegionRegressionRegion>(){new RegionRegressionRegion
                {
                    RegionID = regionEntity.ID,
                    RegressionRegion = entity
                } };

                RegressionRegion Addeditem = await agent.Add(entity);

                return Ok(Addeditem);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost("[action]")]
        [Authorize(Policy = "CanModify")]
        [HttpPost("/Regions/{region}/[controller]/[action]")]
        public async Task<IActionResult> Batch(string region,[FromBody]List<RegressionRegion> entities)
        {
            try
            {
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");
                Region regionEntity = agent.GetRegionByIDOrCode(region);
                if (regionEntity == null) return BadRequest($"No region exists with {region} identifier.");
                if (!IsAuthorizedToEdit(regionEntity)) return Unauthorized();
                                
                entities.ForEach(rr=> 
                {
                    rr.ID = 0;
                    rr.RegionRegressionRegions = new List<RegionRegressionRegion>() { new RegionRegressionRegion { RegionID = regionEntity.ID, RegressionRegion = rr } };
                    rr.StatusID = (rr.CitationID != null || rr.Citation != null)?(int?)2:(int?)1;
                });

                var results = await agent.Add(entities);

                return Ok( results);
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
