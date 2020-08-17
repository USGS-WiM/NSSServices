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
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using System.Linq;
using NSSAgent.Resources;
using NSSDB.Resources;
using NSSServices.Resources;

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
                return Ok(agent.GetVariablesWithUnits());
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
                return Ok(agent.GetVariableWithUnit(id));
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

                var existingVars = agent.GetVariablesWithUnits();
                if (existingVars.Any(x => x.Name == entity.Name) || existingVars.Any(x => x.Code == entity.Code))
                {
                    throw new Exception("Name or Code aready exists");
                }

                var addedVarType = await shared.Add(new VariableType
                {
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                });

                var newVarWithUnit = new VariableWithUnit
                {
                    ID = addedVarType.ID,
                    Name = addedVarType.Name,
                    Code = addedVarType.Code,
                    Description = addedVarType.Description
                };

                if (entity.UnitTypeID != null)
                {
                    await agent.Add(new Variable
                    {
                        VariableTypeID = addedVarType.ID,
                        UnitTypeID = entity.UnitTypeID ?? -1,
                        Comments = "Default unit"
                    });
                    newVarWithUnit.UnitTypeID = entity.UnitTypeID;
                }

                return Ok(newVarWithUnit);

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
                if (!isValid(entities)) return new BadRequestObjectResult("Object is invalid");

                List<VariableType> newVariableTypeList = new List<VariableType>();
                List<Variable> newVariableList = new List<Variable>();
                List<VariableWithUnit> returnVariableWithUnitList = new List<VariableWithUnit>();
                var existingVars = agent.GetVariablesWithUnits();

                foreach (var item in entities)
                {
                    if (!existingVars.Any(v => v.Code == item.Code) && !existingVars.Any(v => v.Name == item.Name))
                    {
                        var newVar = await shared.Add(new VariableType
                        {
                            Name = item.Name,
                            Code = item.Code,
                            Description = item.Description
                        });

                        var newVarWithUnit = new VariableWithUnit
                        {
                            ID = newVar.ID,
                            Name = item.Name,
                            Code = item.Code,
                            Description = item.Description
                        };

                        if (item.UnitTypeID != null)
                        {
                            await agent.Add(new Variable
                            {
                                VariableTypeID = newVar.ID,
                                UnitTypeID = item.UnitTypeID ?? -1,
                                Comments = "Default unit"
                            });
                            newVarWithUnit.UnitTypeID = item.UnitTypeID;
                        }

                        returnVariableWithUnitList.Add(newVarWithUnit);
                    }
                }

                return Ok(returnVariableWithUnitList);
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

                var updatedVarType = await shared.Update(id, new VariableType
                {
                    ID = id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                });
                var defaultUnit = agent.GetDefaultUnitVariable(id);
                if (entity.UnitTypeID == -1)
                {
                    return Ok(updatedVarType);
                }
                else
                {
                    var newDefaultVar = new Variable
                    {
                        VariableTypeID = updatedVarType.ID,
                        UnitTypeID = entity.UnitTypeID ?? -1,
                        Comments = "Default unit"
                    };

                    if (defaultUnit != null) await agent.Update(defaultUnit.ID, newDefaultVar);
                    else await agent.Add(newDefaultVar);

                    VariableWithUnit returnVariableWithUnit = new VariableWithUnit
                    {
                        ID = updatedVarType.ID,
                        Name = updatedVarType.Name,
                        Code = updatedVarType.Code,
                        Description = updatedVarType.Description,
                        UnitTypeID = newDefaultVar.UnitTypeID
                    };

                    return Ok(returnVariableWithUnit);
                }
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }

        }

        [HttpDelete("{id}", Name = "Delete Variable")]
        [Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Variables/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();

                var returnBool = agent.DeleteVariable(id);
                if (returnBool)
                {
                    shared.DeleteVariableType(id).Wait();
                    return Ok("Deleted ID: " + id);
                }
                else
                {
                    throw new ArgumentException("Variable and VariableType not deleted because of Foreign Key Constraint");
                }
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }


        #endregion
    }
}
