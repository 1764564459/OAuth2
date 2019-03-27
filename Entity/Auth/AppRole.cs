using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    /// <summary>
    /// 用户角色、继承与IdentityRole
    /// </summary>
    public class AppRole : IdentityRole<Guid, AppUserRole>
    {
        /// <summary>
        /// 相关用户角色
        /// </summary>
        public ICollection<AppUserRole> AppUserRole { get; set; }

        /// <summary>
        /// 相关角色菜单
        /// </summary>
        public ICollection<AppRoleMenu> AppRoleMenu { get; set; }
    }
}
