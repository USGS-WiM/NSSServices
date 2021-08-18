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
using SharedDB.Resources;
using Shared.Controllers;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using System.Linq;
using NetTopologySuite.Geometries;
using WIM.Exceptions.Services;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using System.Security.Claims;
using NSSDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/StatisticGroups/summary.md")]
    public class StatisticGroupsController : StatisticGroupsControllerBase
    {
        protected INSSAgent agent;
        public StatisticGroupsController(INSSAgent sa, ISharedAgent shared_sa) : base(shared_sa)
        {
            this.agent = sa;
        }

        #region METHOD
        [HttpGet(Name ="Statistic Groups")]
        [HttpGet("/Regions/{regions}/[controller]", Name ="Region Statistic Groups")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/StatisticGroups/Get.md")]
        public async Task<IActionResult> Get(string regions="", [FromQuery] string regressionRegions = "", [FromQuery] string regressions = "", [FromQuery] string defTypes = "")
        {
            IQueryable<StatisticGroupType> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> regressionsList = null;
            List<string> defTypeList = null;
            try
            {
                RegionList = parse(regions);
                regressionRegionList = parse(regressionRegions);
                regressionsList = parse(regressions);
                defTypeList = parse(defTypes);
                entities = agent.GetStatisticGroups(RegionList, null, regressionRegionList, regressionsList, defTypeList, GetApplicableStatus());

                sm($"statistic group count {entities.Count()}");
                return Ok(entities);

            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name = "Statistic Groups By Location")]
        [HttpPost("/Regions/{regions}/[controller]/[action]", Name = "Region Statistic Groups By Location")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/StatisticGroups/Get.md")]
        public async Task<IActionResult> ByLocation([FromBody] Geometry geom= null, string regions = "", [FromQuery] string regressions = "")
        {
            IQueryable<StatisticGroupType> entities = null;
            List<string> RegionList = null;
            List<string> regressionsList = null;
            try
            {
                RegionList = parse(regions);
                regressionsList = parse(regressions);

                if (User.Identity.IsAuthenticated)
                {
                    sm("Is authenticated, will only retrieve managed regression types");
                    entities = agent.GetManagedStatisticGroups(getLoggedInUser(), RegionList, geom, null, regressionsList);
                }
                else
                    entities = agent.GetStatisticGroups(RegionList, geom, null, regressionsList, null, GetApplicableStatus());

                sm($"statistic group count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}", Name ="Statistic Group")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/StatisticGroups/GetDistinct.md")]
        public override async Task<IActionResult> Get(int id)
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
        #endregion
        #region Helper Methods
        private Manager getLoggedInUser()
        {
            try
            {
                return new Manager()
                {
                    ID = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault()),
                    Role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault(),
                    Username = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault()
                };
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IQueryable<Status> GetApplicableStatus()
        {
            var query = agent.GetStatus();
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var manager = getLoggedInUser();
            bool isStreamStats = false;
            if (Request.Headers.FirstOrDefault(h => h.Key.ToUpper() == "X-IS-STREAMSTATS").Value.FirstOrDefault() == "true") isStreamStats = true;

            // set applicable Status for each environment
            var SSProdStatus = new List<string> { "SS Approved" };
            var SSStagingStatus = new List<string> { "SS Approved", "Review" };
            var NSSStagingStatus = new List<string> { "Review", "Approved", "SS Approved" };
            var NSSProdStatus = new List<string> { "Approved", "SS Approved" };

            // if logged in, allow all Status
            if (manager?.Username != null) return query;

            if (isStreamStats)
            {
                if (env.ToUpper() == "PRODUCTION") return query.Where(s => SSProdStatus.Any(stat => stat == s.Name));
                else if (env.ToUpper() == "STAGING" || env.ToUpper() == "DEVELOPMENT") return query.Where(s => SSStagingStatus.Any(stat => stat == s.Name));
            }

            if (env.ToUpper() == "STAGING" || env.ToUpper() == "DEVELOPMENT") return query.Where(s => NSSStagingStatus.Any(stat => stat == s.Name));

            // default is return all SS/NSS approved
            return query.Where(s => NSSProdStatus.Any(stat => stat == s.Name));
        }
        #endregion
    }
}
