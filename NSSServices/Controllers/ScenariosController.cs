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
using GeoAPI.Geometries;
using WIM.Services.Attributes;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/summary.md")]
    public class ScenariosController : NSSControllerBase
    {
        public ScenariosController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet(Name ="Scenarios")]
        [HttpGet("/Regions/{regions}/[controller]", Name ="Region Scenarios")]
        [HttpPost("[action]", Name ="(Alternative Method) Scenarios")]
        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "(Alternative Method) Region Scenarios")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Get.md")]
        public async Task<IActionResult> Get([FromBody] IGeometry geom = null, string regions="",[FromQuery] string regressionRegions ="", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem=0, [FromQuery] string extensions ="")
        {
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            List<string> regressionregionList = null;
            List<string> extensionList = null;
            List<Scenario> entities = null;
            List<string> RegionList = null;
            try
            {
                if (string.IsNullOrEmpty(regions)&& geom == null ) throw new BadRequestException("region and regression regions or a geometry must be specified");
                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                extensionList = parse(extensions);
                

                if (geom != null)
                {
                    if (!agent.allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', agent.allowableGeometries));
                    entities = agent.GetScenarios(RegionList, geom, null, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();
                }
                else
                {                    
                    regressionregionList = parse(regressionRegions);
                    entities = agent.GetScenarios(RegionList,null, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();
                }                
                sm("Count: " + entities.Count());
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        [HttpGet("/Regions/{regions}/[controller]/[action]", Name ="Estimate Region Scenarios", Order =0)]
        [HttpGet("[action]", Name ="Estimate Scenaros", Order =0)]
        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "Estimate Region Scenarios", Order = 0)]
        [HttpPost("[action]", Name = "Estimate Scenaros", Order = 0)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Estimate.md")]
        public async Task<IActionResult> Estimate(string regions, [FromBody]List<Scenario> scenarioList, [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem = 0, [FromQuery] string extensions = "")
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
                if (!scenarioList.Any()) throw new BadRequestException("scenario must be specified in the body of request");

                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                regressionregionList = parse(regressionRegions);
                extensionList = parse(extensions);

                entities = agent.EstimateScenarios(RegionList, scenarioList, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();
                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name ="Estimate Scenarios")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Estimate.md")]
        public async Task<IActionResult> Execute([FromBody]List<Scenario> scenarioList, [FromQuery] string regressiontypes = "", [FromQuery] Int32 unitsystem = 0)
        {
            List<Scenario> entities = null;
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            List<string> regressionregionList = null;
            List<string> extensionList = null;
            try
            {
                if (!scenarioList.Any()) throw new BadRequestException("scenarios must be specified in the body of request");
                statisticgroupList = scenarioList.Select(s => s.StatisticGroupID.ToString()).ToList();
                regressiontypeList = parse(regressiontypes);
                regressionregionList = scenarioList.SelectMany(s => s.RegressionRegions.Select(rr => rr.ID.ToString())).ToList();
                //extensionList = parse(extensions);

                entities = agent.EstimateScenarios(null, scenarioList, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();

                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

//        [HttpPost(Name = "Edit Scenario")]
//        [Authorize(Policy = "CanModify")]
//        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Edit.md")]
//        public async Task<IActionResult> Put([FromBody]NSSServices.Resources.Scenario entity)
//        {
//            try
//            {
//                //will need to find the equations etc and edit accordingly. 

//#warning check if logged in user allowed to modify based on regionManager
//                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
//                //return Ok(await agent.Add(entity));
//                return NotFound();
//            }
//            catch (Exception ex)
//            {
//                return await HandleExceptionAsync(ex);
//            }
//        }

        [HttpPost(Name ="Add Scenario")]
        [Authorize(Policy = "CanModify")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Add.md")]
        public async Task<IActionResult> Post([FromBody]ScenarioUploadPackage entity)
        {
            try
            {
                if (!IsAuthorizedToEdit(new RegressionRegion() { ID = entity.RegressionRegionID })) return new UnauthorizedResult();       
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                //verify that there isn't a competing scenario already uploaded
                if (agent.GetScenarios(null, null, new List<string>() { entity.RegressionRegionID.ToString() },
                                            new List<string>() { entity.StatisticGroupID.ToString() },
                                            new List<string>() { entity.RegressionTypeID.ToString() },null,0).Any())
                    return new BadRequestObjectResult("The scenario's statistic group and regression type already exists for the given regression region.");


                //process and push to db
                return Ok(await agent.Add(entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }
                
        #endregion
    }
}
