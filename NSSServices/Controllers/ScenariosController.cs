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
using WIM.Security.Authorization;

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
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Get.md")]
        public async Task<IActionResult> Get(string regions="",[FromQuery] string regressionRegions ="", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                                 [FromQuery] Int32 unitsystem=0, [FromQuery] string extensions ="")
        {
            try
            {                  
                return GetScenario(null, regions, regressionRegions, statisticgroups, regressiontypes, unitsystem, extensions);
            }
            catch (Exception ex)
            {

                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpPost("[action]", Name = "Scenarios By Location")]
        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "Region Scenarios By Location")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Get.md")]
        public async Task<IActionResult> ByLocation([FromBody] IGeometry geom = null, string regions = "", [FromQuery] string statisticgroups = "", [FromQuery] string regressiontypes = "",
                                                            [FromQuery] Int32 unitsystem = 0, [FromQuery] string extensions = "")
        {
            try
            {
                return GetScenario(geom, regions, null, statisticgroups, regressiontypes, unitsystem, extensions);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "Estimate Region Scenarios")]
        [HttpPost("[action]", Name ="Estimate Scenarios")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Estimate.md")]
        public async Task<IActionResult> Estimate([FromBody]List<Scenario> scenarioList, [FromQuery] string regressiontypes = "", [FromQuery] Int32 unitsystem = 0, [FromQuery] string extensions = "")
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
                extensionList = parse(extensions);

                entities = agent.EstimateScenarios(null, scenarioList, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem).ToList();

                sm("Count: " + entities.Count());

                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut(Name = "Edit Scenario")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Edit.md")]
        public async Task<IActionResult> Put([FromBody]Scenario entity, [FromQuery]string existingstatisticgroup ="")
        {
            try
            {
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                if (!entity.RegressionRegions.Select(rr => new RegressionRegion() { ID = rr.ID, Code = rr.Code.ToLower() }).Distinct().ToList()
                    .All(rr => IsAuthorizedToEdit(rr))) return new UnauthorizedResult();

                return Ok(await agent.Update(entity,existingstatisticgroup));

            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }        

        [HttpPost(Name ="Add Scenario")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Add.md")]
        public async Task<IActionResult> Post([FromBody]Scenario entity, string statisticgroupIDorCode)
        {
            List<string> regressiontypeList = null;
            List<RegressionRegion> regressionregionList = null;
            try
            {
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                regressionregionList = entity.RegressionRegions.Select(rr => new RegressionRegion() { ID = rr.ID, Code = rr.Code.ToLower() }).Distinct().ToList();
                if (!regressionregionList.All(rr => IsAuthorizedToEdit(rr))) return new UnauthorizedResult();       
          
                regressiontypeList = entity.RegressionRegions.SelectMany(s=>s.Regressions.Select(x=>x.code.ToLower())).ToList();


                //verify that there isn't a competing scenario already uploaded
                if (agent.GetScenarios(null, null, regressionregionList.Select(s=>s.Code).ToList(),
                                            new List<string>() { entity.StatisticGroupID.ToString() },
                                            regressiontypeList, null,0).Any())
                    return new BadRequestObjectResult("The scenario's statistic group and regression type already exists for the given regression region.");


                //process and push to db
                return Ok(await agent.Add(entity));
                
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }            
        }

        [HttpDelete(Name = "Delete Scenario")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Scenarios/Edit.md")]
        public async Task<IActionResult> Delete([FromQuery] Int32 regressionregionID, [FromQuery]Int32 statisticgroupID, [FromQuery]Int32 regressiontypeID)
        {
            try
            {
                if (regressionregionID < 1 || statisticgroupID < 1 || regressiontypeID < 1)
                    return new BadRequestResult();

                RegressionRegion rr = agent.GetRegressionRegion(regressionregionID).FirstOrDefault();
                if (!IsAuthorizedToEdit(rr)) return new UnauthorizedResult();


                await agent.DeleteScenario(regressionregionID, statisticgroupID, regressiontypeID);
                return Ok();

            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        #endregion

        #region HelperMethod
        private IActionResult GetScenario(IGeometry geom = null, string regions = "", string regressionRegions = "", string statisticgroups = "", string regressiontypes = "",
                                                             Int32 unitsystem = 0, string extensions = "")
        {
            List<string> statisticgroupList = null;
            List<string> regressiontypeList = null;
            List<string> regressionregionList = null;
            List<string> extensionList = null;
            List<Scenario> entities = null;
            List<string> RegionList = null;
            try
            {                
                RegionList = parse(regions);
                statisticgroupList = parse(statisticgroups);
                regressiontypeList = parse(regressiontypes);
                extensionList = parse(extensions);

                if (geom != null)
                {
                    if (!agent.allowableGeometries.Contains(geom.GeometryType)) throw new BadRequestException("Geometry is not of type: " + String.Join(',', agent.allowableGeometries));
                    entities = agent.GetScenarios(RegionList, geom, null, statisticgroupList, regressiontypeList, extensionList, unitsystem, LoggedInUser()).ToList();
                }
                else
                {
                    regressionregionList = parse(regressionRegions);
                    entities = agent.GetScenarios(RegionList, null, regressionregionList, statisticgroupList, regressiontypeList, extensionList, unitsystem, LoggedInUser()).ToList();
                }
                sm("Count: " + entities.Count());
                return Ok(entities);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
