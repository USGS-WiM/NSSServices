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
using System.Threading.Tasks;
using System.Collections.Generic;
using WIM.Security;
using System.Text;
using System.Linq;
using WIM.Services.Attributes;
using WIM.Security.Authorization;
using WIM.Resources;
using WIM.Services.Security.Authentication.JWTBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using SharedDB.Resources;

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/summary.md")]
    public class ManagersController : JwtBearerAuthenticationBase
    {
        public new INSSAgent agent => (INSSAgent)base.agent;
        public ManagersController(INSSAgent sa, IOptions<JwtBearerSettings> jwtsettings) : base(sa, jwtsettings.Value.SecretKey)
        { }
        #region METHODS
        [HttpGet(Name = "Managers")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Get.md")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(agent.GetManagers().Select(m=>new Manager() {
                                                            ID = m.ID,
                                                            Email = m.Email,
                                                            FirstName=m.FirstName,
                                                            LastName = m.LastName,
                                                            OtherInfo = m.OtherInfo,
                                                            PrimaryPhone = m.PrimaryPhone,
                                                            Role = m.Role,
                                                            SecondaryPhone = m.SecondaryPhone,
                                                            Username = m.Username,
                                                            RegionManagers = m.RegionManagers
                                                        }));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}",Name ="Manager")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 0) return new BadRequestResult(); // This returns HTTP 404
                // managers cannot view other managers, just themselves
                if (!User.IsInRole("Administrator") && Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value) != id)
                    return new UnauthorizedResult();// return HTTP 401

                var x = agent.GetManager(id);
                //remove info not relevant
                x.Salt = null;
                x.Password = null;

                return Ok(x);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        } 
        
        [HttpPost(Name ="Add Manager")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Add.md")]
        public async Task<IActionResult> Post([FromBody]Manager entity)
        {
            try
            {
                // issue here with region managers when creating a region, still when editing as well
                if(string.IsNullOrEmpty(entity.FirstName)|| string.IsNullOrEmpty(entity.LastName) || 
                    string.IsNullOrEmpty(entity.Username)|| string.IsNullOrEmpty(entity.Email) ||
                    string.IsNullOrEmpty(entity.Role)) return new BadRequestObjectResult(new Error( errorEnum.e_badRequest, "You are missing one or more required parameter.")); // This returns HTTP 404

                if (string.IsNullOrEmpty(entity.Password))
                    entity.Password = generateDefaultPassword(entity);                

                entity.Salt = Cryptography.CreateSalt();
                entity.Password = Cryptography.GenerateSHA256Hash(entity.Password, entity.Salt);

                if (! isValid(entity)) return new BadRequestResult(); // This returns HTTP 404
                var x = await agent.Add(entity);
                //remove info not relevant
                x.Salt = null;
                x.Password = null;

                return Ok(x);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpPut("{id}",Name ="Edit Manager")][Authorize(Policy = Policy.Managed)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]Manager entity)
        {
            Manager ObjectToBeUpdated = null;
            try
            {
                if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) ||
                    string.IsNullOrEmpty(entity.Email)) return new BadRequestObjectResult(new Error(errorEnum.e_badRequest)); // This returns HTTP 404

                //fetch object, assuming it exists
                ObjectToBeUpdated = agent.GetManager(id);
                if (ObjectToBeUpdated == null) return new NotFoundObjectResult(entity);

                // managers cannot edit other managers, just themselves
                if (!User.IsInRole("Administrator") && Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.PrimarySid)?.Value) != id)
                    return new UnauthorizedResult();// return HTTP 401

                ObjectToBeUpdated.FirstName = entity.FirstName;
                ObjectToBeUpdated.LastName = entity.LastName;
                ObjectToBeUpdated.Username = entity.Username;
                ObjectToBeUpdated.OtherInfo = entity.OtherInfo?? entity.OtherInfo;
                ObjectToBeUpdated.PrimaryPhone = entity.PrimaryPhone?? entity.PrimaryPhone;
                ObjectToBeUpdated.SecondaryPhone = entity.SecondaryPhone?? entity.SecondaryPhone;
                ObjectToBeUpdated.Email = entity.Email;
                ObjectToBeUpdated.RegionManagers = entity.RegionManagers;

                //admin can only change role
                if(User.IsInRole(Role.Admin) && !string.IsNullOrEmpty(entity.Role))
                    ObjectToBeUpdated.Role = entity.Role;

                //change password if needed
                if (!string.IsNullOrEmpty(entity.Password) && !Cryptography
                            .VerifyPassword(entity.Password,ObjectToBeUpdated.Salt, ObjectToBeUpdated.Password))
                {
                    ObjectToBeUpdated.Salt = Cryptography.CreateSalt();
                    ObjectToBeUpdated.Password = Cryptography.GenerateSHA256Hash(entity.Password, ObjectToBeUpdated.Salt);
                }//end if

                var x = await agent.Update(id, ObjectToBeUpdated);

                //remove info not relevant
                x.Salt = null;
                x.Password = null;

                return Ok(x);
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }           
        }
               
        [HttpDelete("{id}",Name ="Delete Manager")][Authorize(Policy = Policy.AdminOnly)]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Delete.md")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return new BadRequestResult();
                await agent.DeleteManager(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }
        #endregion
        #region HELPER METHODS
        private string generateDefaultPassword(Manager entity)
        {
            //N55Defau1t+numbercharInlastname+first2letterFirstName
            string generatedPassword = "N55Defau1t" + entity.LastName.Length + entity.FirstName.Substring(0, 2);

            return generatedPassword;
        }
        #endregion
    }
}
