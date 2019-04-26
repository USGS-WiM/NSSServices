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

namespace NSSServices.Controllers
{
    [Route("[controller]")]
    [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/summary.md")]
    public class ManagersController : NSSControllerBase
    {
        public ManagersController(INSSAgent sa) : base(sa)
        { }
        #region METHODS
        [HttpGet(Name = "Managers")][Authorize(Policy = "AdminOnly")]
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
                                                             RoleID = m.RoleID,
                                                             SecondaryPhone = m.SecondaryPhone,
                                                             Username=m.Username
                                                            }));
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("/Login", Name ="Login")]
        [Authorize(Policy = "Restricted")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Login.md")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            try
            {
                return Ok(LoggedInUser());
            }
            catch (Exception ex)
            {
                return await HandleExceptionAsync(ex);
            }
        }

        [HttpGet("{id}",Name ="Manager")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/GetDistinct.md")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id < 0) return new BadRequestResult(); // This returns HTTP 404

                var x = await agent.GetManager(id);
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
        
        [HttpPost(Name ="Add Manager")][Authorize(Policy = "AdminOnly")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Add.md")]
        public async Task<IActionResult> Post([FromBody]Manager entity)
        {
            try
            {
                entity.ID = 0;
                if(string.IsNullOrEmpty(entity.FirstName)|| string.IsNullOrEmpty(entity.LastName) || 
                    string.IsNullOrEmpty(entity.Username)|| string.IsNullOrEmpty(entity.Email) ||
                    entity.RoleID <1) return new BadRequestObjectResult(new Error( errorEnum.e_badRequest, "You are missing one or more required parameter.")); // This returns HTTP 404

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

        [HttpPut("{id}",Name ="Edit Manager")][Authorize(Policy = "CanModify")]
        [APIDescription(type = DescriptionType.e_link, Description = "/Docs/Managers/Edit.md")]
        public async Task<IActionResult> Put(int id, [FromBody]Manager entity)
        {
            Manager ObjectToBeUpdated = null;
            try
            {
                if (string.IsNullOrEmpty(entity.FirstName) || string.IsNullOrEmpty(entity.LastName) ||
                    string.IsNullOrEmpty(entity.Email)) return new BadRequestObjectResult(new Error(errorEnum.e_badRequest)); // This returns HTTP 404

                //fetch object, assuming it exists
                ObjectToBeUpdated = await agent.GetManager(id);
                if (ObjectToBeUpdated == null) return new NotFoundObjectResult(entity);

                if (!User.IsInRole("Administrator")|| LoggedInUser().ID !=id)
                    return new UnauthorizedResult();// return HTTP 401

                ObjectToBeUpdated.FirstName = entity.FirstName;
                ObjectToBeUpdated.LastName = entity.LastName;
                ObjectToBeUpdated.OtherInfo = entity.OtherInfo?? entity.OtherInfo;
                ObjectToBeUpdated.PrimaryPhone = entity.PrimaryPhone?? entity.PrimaryPhone;
                ObjectToBeUpdated.SecondaryPhone = entity.SecondaryPhone?? entity.SecondaryPhone;
                ObjectToBeUpdated.Email = entity.Email;

                //admin can only change role
                if(User.IsInRole("Administrator") && entity.RoleID > 0)
                    ObjectToBeUpdated.RoleID = entity.RoleID;

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
               
        [HttpDelete("{id}",Name ="Delete Manager")][Authorize(Policy = "AdminOnly")]
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
