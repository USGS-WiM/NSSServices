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
using NSSDB.Resources;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WIM.Exceptions.Services;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using NetTopologySuite.Geometries;
using SharedDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/summary.md")]
    public class RegressionRegionsController : NSSControllerBase
    {
        public RegressionRegionsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet(Name ="Regression Regions")]
        [HttpGet("/Regions/{regions}/[controller]",Name ="Region Regression Regions")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Get.md")]
        public async Task<IActionResult> Get(string regions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<RegressionRegion> entities = null;
            List<string> RegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {

                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                if (IsAuthenticated)
                {
                    sm("Is authenticated, will only retrieve managed regression regions");
                    entities = agent.GetManagedRegressionRegions(LoggedInUser(), RegionList, null, statisticgroupList, regressiontypeList);
                }
                else
                    entities = agent.GetRegressionRegions(RegionList,null, statisticgroupList,regressiontypeList);

                IQueryable<Status> applicableStatus = GetApplicableStatus();
                if (applicableStatus != null) entities = entities.Where(e => e.StatusID != null && applicableStatus.Any(s => s.ID == e.StatusID));
                sm($"regression region count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name = "Regression Regions By Location")]
        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "Region Regression Regions By Location")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Get.md")]
        public async Task<IActionResult> ByLocation(string regions = "", [FromBody] Geometry geom = null, [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<RegressionRegion> entities = null;
            List<string> RegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
                if (geom != null && !agent.allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', agent.allowableGeometries));

                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);

                if (IsAuthenticated)
                {
                    sm("Is authenticated, will only retrieve managed regression regions");
                    entities = agent.GetManagedRegressionRegions(LoggedInUser(), RegionList, geom, statisticgroupList, regressiontypeList);
                }
                else
                    entities = agent.GetRegressionRegions(RegionList, geom, statisticgroupList, regressiontypeList);

                var applicableStatus = GetApplicableStatus();
                entities = entities.Where(e => applicableStatus.Any(s => s.ID == e.StatusID));
                sm($"regression region count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}", Name ="Regression Region")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/GetDistinct.md")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool includeGeometry = false)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404
                RegressionRegion entity = agent.GetRegressionRegion(id, includeGeometry).FirstOrDefault();

                // reproject for web clients
                if (entity.Location != null && entity.Location.Geometry.SRID != 4326)
                {
                    entity.Location.Geometry = agent.ReprojectGeometry(entity.Location.Geometry, 4326);
                }
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name ="Add Regression Region")]
        [HttpPost("/Regions/{region}/[controller]", Name ="Add Region Regression Region")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Add.md")]
        public async Task<IActionResult> Post(string region, [FromBody]RegressionRegion entity)
        {
            try
            {
                //entity must include citation
                if (!isValid(entity)) return BadRequest(); // This returns HTTP 404
                Region regionEntity = agent.GetRegionByIDOrCode(region);
                if (regionEntity == null) return BadRequest($"No region exists with {region} identifier.");
                if (!IsAuthorizedToEdit(regionEntity)) return Unauthorized();

                if (entity.StatusID == null || ((entity.StatusID == 3 || entity.StatusID == 4) && entity.CitationID == null && entity.Citation == null))
                {
                    // if no status ID given, or if no citation attached and user selcted a public status ID, set to 2
                    entity.StatusID = (int?)2;
                    sm("Status set to Review, disabled for public users due to a missing citation or status");
                }

                entity.RegionRegressionRegions = new List<RegionRegressionRegion>(){new RegionRegressionRegion
                {
                    RegionID = regionEntity.ID,
                    RegressionRegion = entity
                } };

                RegressionRegion Addeditem = await agent.Add(entity);
                Addeditem.RegionRegressionRegions = null;

                return Ok(Addeditem);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name ="Regression Region Batch Upload")]
        [Authorize(Policy = Policy.Managed)]
        [HttpPost("/Regions/{region}/[controller]/[action]", Name ="Region Regression Region Batch Upload")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Edit.md")]
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

                    if (rr.StatusID == null || ((rr.StatusID == 3 || rr.StatusID == 4) && rr.CitationID == null && rr.Citation == null))
                    {
                        // if no status ID given, or if no citation attached and user selcted a public status ID, set to 2
                        rr.StatusID = (int?)2;
                        sm(rr.Name + " status set to Review, disabled for public users due to a missing citation or status");
                    }
                });

                var results = await agent.Add(entities);

                return Ok( results);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name ="Edit Regression Region")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]RegressionRegion entity)
        {
            try
            {

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                RegressionRegion rr = agent.GetRegressionRegion(id).FirstOrDefault();
                if (rr == null) return BadRequest($"No regression region exists with {id} identifier.");
                if (!IsAuthorizedToEdit(rr)) return Unauthorized();

                if (entity.StatusID == null || ((entity.StatusID == 3 || entity.StatusID == 4) && entity.CitationID == null && entity.Citation == null))
                {
                    // if no status ID given, or if no citation attached and user selcted a public status ID, set to 2
                    entity.StatusID = (int?)2;
                    sm("Status set to Review, disabled for public users due to a missing citation or status");
                }
                // remove extra limitation variables
                if (entity.Limitations != null && entity.Limitations.Count > 0)
                {
                    foreach (var lim in entity.Limitations)
                    {
                        agent.RemoveLimitationVariables(lim.ID, lim.Variables.ToList());
                    }
                }

                return Ok(await agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpDelete("{id}", Name ="Delete Regression Region")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionRegions/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                RegressionRegion rr = agent.GetRegressionRegion(id).FirstOrDefault();
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
