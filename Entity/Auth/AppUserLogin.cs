using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    public class AppUserLogin : IdentityUserLogin<Guid>
    {
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }

        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }

        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }
    }
}
