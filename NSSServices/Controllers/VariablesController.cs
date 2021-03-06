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
using Shared.Controllers;
using NSSAgent;
using System.Threading.Tasks;
using System.Collections.Generic;
using SharedAgent;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using System.Linq;
using NSSAgent.Resources;
using NSSDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/summary.md")]
    public class VariablesController : VariablesControllerBase
    {
        protected INSSAgent agent;
        protected ISharedAgent shared;
        public VariablesController(INSSAgent sa, ISharedAgent shared_sa) : base(shared_sa)
        {
            this.agent = sa;
            this.shared = shared_agent;
        }

        #region METHOD
        [HttpGet(Name ="Variables")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Get.md")]
        public async Task<IActionResult> GetVariables([FromQuery] string statisticgroups = "")
        {
            List<string> statisticgroupList = null;
            try
            {
                statisticgroupList = parse(statisticgroups);
                return Ok(agent.GetVariableTypes(statisticgroupList));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }      
        }

        [HttpGet("{id}", Name ="Variable")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/GetDistinct.md")]
        public override async Task<IActionResult> Get(int id)
        {
            try
            {
                if(id<0) return new BadRequestResult(); // This returns HTTP 404
                return Ok(agent.GetVariableType(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name = "Add Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Add.md")]
        public async Task<IActionResult> Post([FromBody] VariableType entity)
        {
            try
            {
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                return Ok(await shared.Add(entity));

            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name = "Variable Batch Upload")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Batch.md")]
        public async Task<IActionResult> Batch([FromBody] List<VariableType> entities)
        {
            try
            {
                entities.ForEach(e => e.ID = 0);
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                return Ok(await shared.Add(entities));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name = "Edit Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody] VariableType entity)
        {
            try
            {
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                // If unit types are the same, including the full object results in DB error
                if (entity.MetricUnitTypeID != null && entity.MetricUnitType != null) entity.MetricUnitType = null;
                if (entity.EnglishUnitTypeID != null && entity.EnglishUnitType != null) entity.EnglishUnitType = null;

                return Ok(await shared.Update(id, entity));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }

        [HttpDelete("{id}", Name = "Delete Variable")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();

                shared.DeleteVariableType(id).Wait();
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
