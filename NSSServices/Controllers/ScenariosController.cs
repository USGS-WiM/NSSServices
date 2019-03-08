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
using NSSAgent.Resources;
using System.Threading.Tasks;
using System.Collections.Generic;
using WIM.Exceptions.Services;
using System.Linq;
using NetTopologySuite.Geometries;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    public class ScenariosController : NSSControllerBase
    {
        public ScenariosController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet("[action]")]
        [HttpGet("/Regions/{regions}/[controller]/[action]")]
        [HttpPost("[action]")]
        [HttpPost("/Regions/{regions}/[controller]/[action]")]
        public async Task<IActionResult> GetScenariosConfigurations([FromBody] Geometry geom = null, string regions="",[FromQuery] string regressionRegions ="", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem=0, [FromQuery] Int32? config =1, [FromQuery] string extensions ="")
        {
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            List<string> regressionregionList = null;
            List<string> extensionList = null;
            List<Scenario> entities = null;
            List<string> RegionList = null;
            try
            {
                if (string.IsNullOrEmpty(regions)) throw new BadRequestException("region must be specified");
                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                regressionregionList = parse(regressionRegions);
                extensionList = parse(extensions);
                if (!config.HasValue) config = 1;

                entities = agent.GetScenarios(RegionList, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();
                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        [HttpGet("/Regions/{regions}/[controller]/[action]")]
        [HttpGet("[action]")]
        [HttpPost("/Regions/{regions}/[controller]/[action]")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Estimate(string regions, [FromBody]List<Scenario> scenarioList, [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem = 0, [FromQuery] Int32? config = 1, [FromQuery] string extensions = "")
        {
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            List<string> regressionregionList = null;
            List<string> extensionList = null;
            List<Scenario> entities = null;
            List<string> RegionList = null;
            try
            {
                if (string.IsNullOrEmpty(regions)) throw new BadRequestException("region must be specified");                
                if (scenarioList == null || scenarioList.Count() < 1) throw new BadRequestException("scenario must be specified in the body of request");

                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                regressionregionList = parse(regressionRegions);
                extensionList = parse(extensions);
                if (!config.HasValue) config = 1;

                entities = agent.EstimateScenarios(RegionList, scenarioList, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();
                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        

        public async Task<IActionResult> Execute([FromBody]List<Scenario> scenarioList, [FromQuery] Int32 unitsystem = 0, [FromQuery] Int32? config = 1)
        {
            List<Scenario> entities = null;
            try
            {
                if (scenarioList == null || scenarioList.Count() < 1) throw new BadRequestException("scenario must be specified in the body of request");

                if (!config.HasValue) config = 1;
#warning refactor to perform actions on all scenarios passed in.
                //entities = agent.EstimateScenarios( scenarioList, unitsystem).ToList();
                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("/Regions/{regions}/[controller]")]
        [HttpPost]
        [Authorize(Policy = "CanModify")]
        public async Task<IActionResult> Post([FromBody]Scenario entity)
        {
            try
            {
#warning check if logged in user allowed to modify based on regionManager
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                //return Ok(await agent.Add(entity));
                return NotFound();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }
        #endregion

    }
}
