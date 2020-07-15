//------------------------------------------------------------------------------
//----- HttpController ---------------------------------------------------------
//------------------------------------------------------------------------------

//-------1---------2---------3---------4---------5---------6---------7---------8
//       01234567890123456789012345678901234567890123456789012345678901234567890
//-------+---------+---------+---------+---------+---------+---------+---------+

// copyright:   2020 WIM - USGS

//    authors: Katrin E. Jacobsen USGS Web Informatics and Mapping
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

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Limitations/summary.md")]
    public class LimitationsController : NSSControllerBase
    {
        public LimitationsController(INSSAgent sa) : base(sa)
        { }

        #region METHOD
        [HttpGet("/RegressionRegions/{rr}/[controller]", Name = "Regression Region Limitations")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Limitations/Get.md")]
        public async Task<IActionResult> Get(int rr)
        {
            IQueryable<Limitation> entities = null;
            try
            {
                RegressionRegion regRegionEntity = agent.GetRegressionRegion(rr).First();
                if (regRegionEntity == null) return BadRequest($"No regression region exists with {rr} identifier.");

                entities = agent.GetRegressionRegionLimitations(rr);

                sm($"limitation count {entities.Count()}");
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name = "Add Limitations")]
        [HttpPost("/RegressionRegions/{rr}/[controller]", Name = "Add Regression Region Limitations")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Limitations/Add.md")]
        public async Task<IActionResult> Post(int rr, [FromBody] List<Limitation> entities)
        {
            try
            {
                if (!isValid(entities)) return BadRequest(); // This returns HTTP 404
                var AddedItems = await agent.AddRegressionRegionLimitations(rr, entities);

                // get all limitations, including newly added
                var rrLimitations = agent.GetRegressionRegionLimitations(rr);
                return Ok(rrLimitations);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name = "Edit Limitation")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Limitations/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]Limitation entity)
        {
            try
            {

                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                Limitation l = await agent.GetLimitation(id);
                if (l == null) return BadRequest($"No limitation exists with {id} identifier.");
                RegressionRegion rr = agent.GetRegressionRegion(l.RegressionRegionID).FirstOrDefault();
                if (!IsAuthorizedToEdit(rr)) return Unauthorized();

                return Ok(await agent.Update(id, entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpDelete("{id}", Name = "Delete Limitation")]
        [Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Limitations/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                Limitation l = await agent.GetLimitation(id);
                if (l == null) return BadRequest($"No limitation exists with {id} identifier.");
                RegressionRegion rr = agent.GetRegressionRegion(l.RegressionRegionID).FirstOrDefault();
                if (!IsAuthorizedToEdit(rr)) return Unauthorized();

                foreach (var variable in l.Variables)
                {
                    await agent.DeleteVariable(variable.ID);
                }
                await agent.DeleteLimitation(id);

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
