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
using WIM.Exceptions.Services;
using WIM.Services.Attributes;
using System.ComponentModel;
using WIM.Security.Authorization;
using NetTopologySuite.Geometries;
using System.Security.Claims;
using SharedDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/summary.md")]
    public class CitationsController : NSSControllerBase
    {
        public CitationsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD        
        [HttpGet(Name = "Citations")]
        [HttpGet("/Regions/{regions}/[controller]", Name = "Region Citations")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/Get.md")]
        public async Task<IActionResult> Get(string regions = "", [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<Citation> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
               
                RegionList = parse(regions);
                regressionRegionList = parse(regressionRegions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);

                if (IsAuthenticated)
                {
                    sm("Is authenticated, will only retrieve managed citations");
                    entities = agent.GetManagedCitations(LoggedInUser(), RegionList, null, regressionRegionList, statisticgroupList, regressiontypeList);
                }
                else
                    entities = agent.GetCitations(RegionList, null, regressionRegionList, statisticgroupList, regressiontypeList, GetApplicableStatus());


                sm($"citation count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name = "Citations By Location")]
        [HttpPost("/Regions/{regions}/[controller]/[[action]]", Name = "Region Citations By Location")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/Add.md")]
        public async Task<IActionResult> ByLocation([FromBody] Geometry geom, string regions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<Citation> entities = null;
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
                    sm("Is authenticated, will only retrieve managed citations");
                    entities = agent.GetManagedCitations(LoggedInUser(), RegionList, geom, null, statisticgroupList, regressiontypeList);
                }
                else
                    entities = agent.GetCitations(RegionList, geom, null, statisticgroupList, regressiontypeList, GetApplicableStatus());


                sm($"citation count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
                
        [HttpGet("{id}", Name = "Citation")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404
                
                return Ok(await agent.GetCitation(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name = "Add Citation")][Authorize(Policy = Policy.Managed)]
        public async Task<IActionResult> Post([FromBody]Citation entity)
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

        [HttpPost("/RegressionRegions/{regressionRegionID}/[controller]", Name = "Add RegressionRegion Citation")]        
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/Add.md")]
        public async Task<IActionResult> Post([FromBody]Citation entity, Int32 regressionRegionID = -1)
        {
            try
            {
                RegressionRegion regRegion = agent.GetRegressionRegion(regressionRegionID).FirstOrDefault();

                if (regRegion == null) return new BadRequestResult();
                if (!IsAuthorizedToEdit(regRegion)) return new UnauthorizedResult();
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                regRegion.Citation = entity;
                var result = await agent.Update(regressionRegionID, regRegion);
                return Ok(result.Citation);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name = "Edit Citation")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]Citation entity)
        {
            try
            {
                if (!IsAuthorizedToEdit(await agent.GetCitation(id)))return new UnauthorizedResult();

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpDelete("{id}", Name = "Delete Citation")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Citations/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (!IsAuthorizedToEdit(await agent.GetCitation(id))) return new UnauthorizedResult();
                await agent.DeleteCitation(id);
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
