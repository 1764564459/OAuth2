using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    public class AppUserRole : IdentityUserRole<Guid>
    {
        public override Guid RoleId { get => base.RoleId; set => base.RoleId = value; }
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }
        public AppUser AppUser { get; set; }

        public AppRole AppRole { get; set; }

    }
}
