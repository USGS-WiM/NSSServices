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
using NSSServices.Resources;
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
        public override async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetVariablesWithUnits().ToList());  
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
                return Ok(agent.GetVariableWithUnits(id));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost(Name = "Add Variable")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Add.md")]
        public async Task<IActionResult> Post([FromBody] VariableWithUnit entity)
        {
            try
            {
                if (!isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                Variable tempV = new Variable
                {
                    UnitTypeID = entity.UnitTypeID,
                    Comments = "Default unit"
                };

                VariableType tempVT = new VariableType
                {
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                };

                var temp = await shared.Add(tempVT);
                tempV.VariableTypeID = temp.ID;
                await agent.Add(tempV);

                return Ok(temp);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPost("[action]", Name = "Variable Batch Upload")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Batch.md")]
        public async Task<IActionResult> Batch([FromBody] List<VariableWithUnit> entities)
        {
            try
            {
                entities.ForEach(e => e.ID = 0);
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                List<VariableType> tempVTList = new List<VariableType>();
                List<Variable> tempVList = new List<Variable>();

                foreach (var item in entities)
                {
                    Variable tempV = new Variable
                    {
                        UnitTypeID = item.UnitTypeID,
                        Comments = "Default unit"
                    };

                    VariableType tempVT = new VariableType
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Code = item.Code,
                        Description = item.Description
                    };

                    tempVTList.Add(tempVT);
                    tempVList.Add(tempV);
                }

                var temp = await shared.Add(tempVTList);
                var tempList = temp.ToList();

                for (int i = 0; i < tempList.Count(); i++)
                {
                    tempVList[i].VariableTypeID = tempList[i].ID;
                }

                await agent.Add(tempVList);

                return Ok(temp);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}", Name = "Edit Variable")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody] VariableWithUnit entity)
        {
            try
            {
                if (id < 0 || !isValid(entity)) return new BadRequestResult(); // This returns HTTP 404

                Variable tempV = new Variable
                {
                    VariableTypeID = id,
                    UnitTypeID = entity.UnitTypeID,
                    Comments = "Default unit"
                };

                VariableType tempVT = new VariableType
                {
                    ID = id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                };

                var temp = await shared.Update(id, tempVT);
                var varID = agent.GetVar(id);
                await agent.Update(varID.ID, tempV);

                return Ok(temp);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }


        #endregion
    }
}
