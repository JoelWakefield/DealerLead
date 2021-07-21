using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DealerLead.Web
{
    public static class AuthHelper
    {
        public static string GetOid(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            var identity = (ClaimsIdentity)user.Identity;

            if (identity.Claims.Count() > 0)
                return identity.Claims
                    .FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")
                    .Value;
            else
                return null;
        }
    }
}
