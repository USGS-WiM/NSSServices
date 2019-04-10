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
using SharedDB.Resources;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using System.Linq;
using NetTopologySuite.Geometries;
using WIM.Exceptions.Services;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class StatisticGroupsController : NSSControllerBase
    {
        protected ISharedAgent shared_agent;
        public StatisticGroupsController(INSSAgent sa, ISharedAgent shared_sa) : base(sa)
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet]
        [HttpGet("/Regions/{regions}/[controller]")]
        [HttpPost("[action]")]
        [HttpPost("/Regions/{regions}/[controller]")]
        public async Task<IActionResult> Get(string regions="", [FromBody] Geometry geom = null, [FromQuery] string regressionRegions = "", [FromQuery] string regressions = "")
        {
            IQueryable<StatisticGroupType> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> regressionsList = null;
            try
            {
                if (String.IsNullOrEmpty(regions) && String.IsNullOrEmpty(regressionRegions) &&
                    string.IsNullOrEmpty(regressions) && geom == null)
                { entities = agent.GetStatisticGroups(); }

                RegionList = parse(regions);
                regressionsList = parse(regressions);
                if (geom == null)
                {                    
                    regressionRegionList = parse(regressionRegions);
                    entities = agent.GetStatisticGroups(RegionList, regressionRegionList, regressionsList);
                }
                else {
                    if (!agent.allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', agent.allowableGeometries));
                    entities = agent.GetStatisticGroups(RegionList, geom, regressionsList);
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

                return Ok(await agent.GetStatisticGroup(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post([FromBody]StatisticGroupType entity)
        {
            try
            {
                entity.ID = 0;
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await shared_agent.Add(entity));
             
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost("[action]")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Batch([FromBody]List<StatisticGroupType> entities)
        {
            try
            {
                entities.ForEach(e => e.ID = 0);
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");
                return Ok(await shared_agent.Add(entities));

            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Put(int id, [FromBody]StatisticGroupType entity)
        {
            try
            {
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                return Ok(await shared_agent.Update(id,entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }        

        [HttpDelete("{id}")][Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await shared_agent.DeleteStatisticGroup(id);
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
