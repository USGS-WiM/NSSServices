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

                Variable newVariable = new Variable
                {
                    UnitTypeID = entity.UnitTypeID,
                    Comments = "Default unit"
                };

                VariableType newVariableType = new VariableType
                {
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                };

                var newVarTypeToGrabID = await shared.Add(newVariableType);
                newVariable.VariableTypeID = newVarTypeToGrabID.ID;
                agent.Add(newVariable).Wait();

                VariableWithUnit returnVariableWithUnit = new VariableWithUnit
                {
                    ID = newVarTypeToGrabID.ID,
                    Name = newVariableType.Name,
                    Code = newVariableType.Code,
                    Description = newVariableType.Description,
                    UnitTypeID = newVariable.UnitTypeID
                };

                return Ok(returnVariableWithUnit);
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

                List<VariableType> newVariableTypeList = new List<VariableType>();
                List<Variable> newVariableList = new List<Variable>();
                List<VariableWithUnit> returnVariableWithUnitList = new List<VariableWithUnit>();

                foreach (var item in entities)
                {
                    Variable newVariable = new Variable
                    {
                        UnitTypeID = item.UnitTypeID,
                        Comments = "Default unit"
                    };

                    VariableType newVariableType = new VariableType
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Code = item.Code,
                        Description = item.Description
                    };

                    VariableWithUnit returnVariableWithUnit = new VariableWithUnit
                    {
                        Name = item.Name,
                        Code = item.Code,
                        Description = item.Description,
                        UnitTypeID = item.UnitTypeID
                    };

                    newVariableTypeList.Add(newVariableType);
                    newVariableList.Add(newVariable);
                    returnVariableWithUnitList.Add(returnVariableWithUnit);
                }

                var newVarTypeToGrabIDIEnum = await shared.Add(newVariableTypeList);
                var newVarTypeToGrabIDList = newVarTypeToGrabIDIEnum.ToList();

                for (int i = 0; i < newVarTypeToGrabIDList.Count(); i++)
                {
                    newVariableList[i].VariableTypeID = newVarTypeToGrabIDList[i].ID;

                    returnVariableWithUnitList[i].ID = newVarTypeToGrabIDList[i].ID;
                }

                agent.Add(newVariableList).Wait();

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

                Variable newVariable = new Variable
                {
                    VariableTypeID = id,
                    UnitTypeID = entity.UnitTypeID,
                    Comments = "Default unit"
                };

                VariableType newVariableType = new VariableType
                {
                    ID = id,
                    Name = entity.Name,
                    Code = entity.Code,
                    Description = entity.Description
                };

                var newVarTypeToGrabID = await shared.Update(id, newVariableType);
                var varID = agent.GetVariable(id);
                await agent.Update(varID.ID, newVariable);

                VariableWithUnit returnVariableWithUnit = new VariableWithUnit
                {
                    ID = newVarTypeToGrabID.ID,
                    Name = newVariableType.Name,
                    Code = newVariableType.Code,
                    Description = newVariableType.Description,
                    UnitTypeID = newVariable.UnitTypeID
                };

                return Ok(returnVariableWithUnit);
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

                var returnVar = agent.DeleteVariable(id);
                if (returnVar != null)
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
