using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    public class AppUser : IdentityUser<Guid, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public override Guid Id { get => base.Id; set => base.Id = value; }

        public ICollection<AppUserRole> AppUserRole { get; set; }
    }
    
}
