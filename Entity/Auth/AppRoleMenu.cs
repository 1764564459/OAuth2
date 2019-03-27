using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    public class AppRoleMenu
    {
        public Guid RoleID { get; set; }
        public Guid MenuID { get; set; }

        public AppRole AppRole { get; set; }

        public AppMenu AppMenu { get; set; }
    }
}
