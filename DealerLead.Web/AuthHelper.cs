using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DealerLead.Web
{
    public class AuthHelper
    {
        private readonly DealerLeadDBContext _context;

        public AuthHelper(DealerLeadDBContext context)
        {
            _context = context;
        }

        private string GetOid(ClaimsPrincipal user)
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

        public dynamic LoginDealerUser(ClaimsPrincipal user)
        {
            var result = new Dictionary<string, object>();
            var guid = GetOid(user);

            if (!String.IsNullOrWhiteSpace(guid))
            {
                result["Known"] = true;

                Guid oid = Guid.Parse(guid);
                var checkUser = _context.DealerLeadUser.FirstOrDefault(x => x.AzureId == oid);

                if (checkUser == null)
                    CreateDealerUser(user);
            }
            else
            {
                result["Known"] = false;
            }

            return result;
        }

        public void CreateDealerUser(ClaimsPrincipal user)
        {
            Guid oid = Guid.Parse(GetOid(user));

            _context.DealerLeadUser.Add(new DealerLeadUser
            {
                AzureId = oid,
                CreateDate = DateTime.Now
            });
            _context.SaveChanges();
        }
    }
}
