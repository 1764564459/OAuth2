using EntityFrameWork.Server.Entity;
using EntityFrameWork.Server.UnitOfWork.Simple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OAuthServer.App_
{
    public class BaseController : Controller
    {
        public AppDbContext _context { get;private set; }//采用自带的

        public UnitOfWork unit { get; private set; }//采用封装的数据库交互
        public BaseController()
        {
            _context = new AppDbContext();
            unit = new UnitOfWork(new AppDbContext());
        }
    }
}