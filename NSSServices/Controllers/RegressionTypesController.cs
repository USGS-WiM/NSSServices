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
using SharedDB.Resources;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using System.Linq;
using WIM.Services.Attributes;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/summary.md")]
    public class RegressionTypesController : NSSControllerBase
    {
        protected ISharedAgent shared_agent;
        public RegressionTypesController(INSSAgent sa, ISharedAgent shared_sa) : base(sa)
        {
            this.shared_agent = shared_sa;
        }

        #region METHOD
        [HttpGet(Name = "Regression Types")]
        [HttpGet("/Regions/{regions}/[controller]", Name = "Region Regression Types")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Get.md")]
        public async Task<IActionResult> GetRegressionTypes(string regions = "", [FromQuery] string regressionRegions = "", [FromQuery] string statisticgroups = "")
        {
            IQueryable<RegressionType> entities = null;
            List<string> RegionList = null;
            List<string> regressionRegionList = null;
            List<string> statisticgroupList = null;
            try
            {
                
                RegionList = parse(regions);
                regressionRegionList = parse(regressionRegions);
                statisticgroupList = parse(statisticgroups);

                if (IsAuthenticated)
                {
                    sm("Is authenticated, will only retrieve managed regression types");
                    entities = agent.GetManagedRegressions(LoggedInUser(), RegionList, null, regressionRegionList, statisticgroupList);
                }
                else
                    entities = agent.GetRegressions(RegionList, null, regressionRegionList, statisticgroupList);

                sm($"regressiontype count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        [HttpGet("{id}", Name ="Regression Type")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await agent.GetRegression(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name ="Add Regression Type")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Add.md")]
        public async Task<IActionResult> Post([FromBody]RegressionType entity)
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

        [HttpPost("[action]", Name ="Regression Type Batch Upload")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Batch.md")]
        public async Task<IActionResult> Batch([FromBody]List<RegressionType> entities)
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

        [HttpPut("{id}", Name ="Edit Regression Type")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]RegressionType entity)
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

        [HttpDelete("{id}", Name ="Delete Regression Type")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/RegressionTypes/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await shared_agent.DeleteRegressionType(id);

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
