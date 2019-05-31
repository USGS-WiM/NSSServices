using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NSSDB.Resources;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WIM.Services.Controllers;
using WIM.Resources;
using NSSAgent;


namespace NSSServices.Controllers
{

    public abstract class NSSControllerBase: WIM.Services.Controllers.ControllerBase
    {
        protected INSSAgent agent;
        public bool IsAuthenticated
        {
            get { return User.Identity.IsAuthenticated; }
        }


        public NSSControllerBase(INSSAgent sa)
        {
            this.agent = sa;            
        }

        public bool IsAuthorizedToEdit(Object item)
        {
            if (User.IsInRole("Administrator")) return true;
            var username = LoggedInUser();

            Dictionary<Type, int> typeDict = new Dictionary<Type, int> {
                                                                            {typeof(Citation),0},
                                                                            {typeof(RegressionRegion),1},
                                                                            {typeof(Region),2}
                                                                        };

            switch (typeDict[item.GetType()])
            {
                case 0://citation
                    return agent.GetManagerCitations(username.ID).Select(w => w.ID).Contains((item as Citation).ID);
                case 1://regressionregion
                    return agent.GetManagerRegressionRegions(username.ID).Select(rr => rr.ID).Contains((item as RegressionRegion).ID);
                case 2://region
                    return agent.GetManagerRegions(username.ID).Select(rr => rr.ID).Contains((item as Region).ID);
                default:
                    break;
            }

            return false;
        }

         public Manager LoggedInUser() {
            return new Manager()
            {
                ID = Convert.ToInt32( User.Claims.Where(c => c.Type == ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault()),
                FirstName = User.Claims.Where(c => c.Type == ClaimTypes.Name)
                   .Select(c => c.Value).SingleOrDefault(),
                LastName = User.Claims.Where(c => c.Type == ClaimTypes.Surname)
                   .Select(c => c.Value).SingleOrDefault(),
                Username = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).SingleOrDefault(),
                 Role = User.Claims.Where(c => c.Type == ClaimTypes.Role)
                   .Select(c => c.Value).SingleOrDefault()
            };
        }

        protected override IActionResult HandleException(Exception ex)
        {
            if (ex is DbUpdateException)
            {
                string errorText;
                if (ex.InnerException is Npgsql.PostgresException && dbBadRequestErrors.TryGetValue(Convert.ToInt32(ex.InnerException.Data["Code"]), out errorText))
                {
                    return new BadRequestObjectResult(new Error(errorEnum.e_badRequest, errorText));
                }
                else
                {
                    sm($"Database error {ex.Message}");
                    return StatusCode(500, new Error(errorEnum.e_internalError, "A managed database error occured, See error message for more information"));
                }
            }

            return base.HandleException(ex);
        }

        private Dictionary<int, string> dbBadRequestErrors = new Dictionary<int, string>
        {
            //https://www.postgresql.org/docs/9.4/static/errcodes-appendix.html
            {23502, "One of the properties requires a value."},
            {23505, "One of the properties is marked as Unique index and there is already an entry with that value."},
            {23503, "One of the related features prevents you from performing this operation to the database." }
        };
    }
}
