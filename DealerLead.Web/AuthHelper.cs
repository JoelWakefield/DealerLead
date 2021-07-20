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
            var identity = (ClaimsIdentity)user.Identity;

            if (identity.Claims.Count() > 0)
                return identity.Claims
                    .FirstOrDefault(x => x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")
                    .Value;
            else
                return "";
        }

        public dynamic GetDealerUser(ClaimsPrincipal user)
        {
            var result = new Dictionary<string, object>();

            var guid = GetOid(user);

            if (!String.IsNullOrWhiteSpace(guid))
            {
                result["HasOid"] = true;

                Guid oid = Guid.Parse(guid);

                var checkUser = _context.DealerLeadUser.FirstOrDefault(x => x.AzureId == oid);
            
                if (checkUser == null)
                    result["KnownUser"] = false;
                else
                    result["KnownUser"] = true;
            }
            else
            {
                result["HasOid"] = false;
                result["KnownUser"] = false;
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
