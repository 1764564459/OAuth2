using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameWork.Server.Entity.Auth
{
    /// <summary>
    /// 系统菜单   [key]数据注释  
    /// </summary>
    public class AppMenu
    {
        /// <summary>
        /// id
        /// </summary>
        /// 
        public Guid ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 控制器名字
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// Action名字
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 菜单链接
        /// </summary>
        public string MenuLink { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string MenuIcon { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }

        /// <summary>
        /// 角色菜单级联
        /// </summary>
        public ICollection<AppRoleMenu> RoleMenu { get; set; }

        //public virtual AppMenu FatherMenu { get; set; }

        /// <summary>
        /// 自连接-->子级菜单
        /// </summary>
        public ICollection<AppMenu> ChildrenMenu { get; set; }
    }
}
