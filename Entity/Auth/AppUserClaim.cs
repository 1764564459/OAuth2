using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    public class AppUserClaim : IdentityUserClaim<Guid>
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }
        public override string ClaimType { get => base.ClaimType; set => base.ClaimType = value; }
        public override string ClaimValue { get => base.ClaimValue; set => base.ClaimValue = value; }
    }
}
