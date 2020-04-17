using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MantiScanServices.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId
        {
            get
            {
                return GetClaimValue("UserId");
            }
        }

        protected string UserEmail
        {
            get
            {
                return GetClaimValue("EmailId");
            }
        }

        protected int OrganizationId
        {
            get
            {
                return Convert.ToInt32(GetClaimValue("OrganizationId"));
            }
        }

        private string GetClaimValue(string claimType)
        {
            if (User.HasClaim(p => p.Type == claimType))
            {
                return User.Claims.Where(p => p.Type == claimType).Select(p => p.Value).SingleOrDefault();
            }

            return null;
        }
    }
}