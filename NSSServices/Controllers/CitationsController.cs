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
    public class CitationsController : NSSControllerBase
    {
        public CitationsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet]
        [HttpGet("/Regions/{regions}/[controller]")]
        [HttpPost("[action]")]
        [HttpPost("/Regions/{regions}/[controller]")]
        public async Task<IActionResult> Get(string regions="", [FromBody] IGeometry geom = null, [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "")
        {
            IQueryable<Citation> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            try
            {
                if (geom != null && !agent.allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', agent.allowableGeometries));

                RegionList = parse(regions);
                regressionRegionList = parse(regressionRegions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);

                entities = agent.GetCitations(RegionList, geom, regressionRegionList, statisticgroupList, regressiontypeList);
                
                sm("test from citations handler");
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
                
                return Ok(await agent.GetCitation(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost]
        [HttpPost("/RegressionRegions/{regressionRegionID}/[controller]")]
        [Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromBody]Citation entity, Int32 regressionRegionID = -1)
        {
            try
            {
                entity.ID = 0;
                RegressionRegion regRegion = await agent.GetRegressionRegion(regressionRegionID);

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

        [HttpPut("{id}")][Authorize(Policy = "CanModify")]
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

        //
        //Delete should occur at the Regression region level
        //
        #endregion

    }
}
