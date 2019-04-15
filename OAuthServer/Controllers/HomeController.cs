using EntityFrameWork.Server.Entity.Auth;
using EntityFrameWork.Server.Helper;
using EntityFrameWork.Server.UnitOfWork.complex;
using Newtonsoft.Json;
using OAuthServer.App_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OAuthServer.Controllers
{
  
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            MongoDbHelper mongo = new MongoDbHelper();
            Random rand = new Random(10);
            int rand_num= rand.Next(0, 1000);
            //List< MyApp> user = new List<MyApp>()
            // {
            //    new MyApp (){
            //        Id = Guid.NewGuid(),
            //        Name = rand_num.ToString() ,
            //        Age = 12,
            //        Time = DateTime.UtcNow
            //    }
            // };
            // var result = await mongo.Insert(user[0]);
            //var result = mongo.GetList<MyApp>(1, 10, p => p.Time > DateTime.Parse("2019-4-13 15:00:00") && p.Time < DateTime.Parse("2019-4-13 15:27:00"), p => p.Time, "asc");
            //string json_data = JsonConvert.SerializeObject(result);
            //JsonConvert.PopulateObject(json_data, user);
            //user.ForEach(item =>
            //{
            //    item.Time =item.Time.ToString();
            //});
            //var result= mongo.QueryOne<MyApp>();
            //mongo.Delete<MyApp>(result);
            return View(); // Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult GetData()
        {
            return Json(new { data=new {User.Identity.Name,User.Identity.IsAuthenticated } },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自定义排序和、从仓库获取库数据（自带上下文交互、封装上下文交互）
        /// </summary>
        /// <returns></returns>
        public ActionResult order_database()
        {
            var user = unit._Repository.GetAll<AppUser>();
            var data = GetList(o => o.Age > 20, p => p.Id, "desc");
           var menu= _context.Menus;//直接获取实体对象
            return Json(new { data, user ,menu}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据条件筛选、排序
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderby"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<MyClass> GetList(Func<MyClass, bool> where, Func<MyClass, dynamic> orderby, string order)
        {

            //用户信息
            List<MyClass> list = new List<MyClass>()
            {
                new MyClass (){Id=1,Name="张三",Age=25},
                new MyClass (){Id=3,Name="李四",Age=38},
                new MyClass (){Id=8,Name="王五",Age=12},
                new MyClass (){Id=2,Name="赵六",Age=32},
            };
            var data = list.Where(where);
            if (order.Equals("asc"))
                data = data.OrderBy(orderby);
            else
                data = data.OrderByDescending(orderby);
            return data.ToList();
        }

        /// <summary>
        /// 用户类
        /// </summary>
        public class MyClass
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
        }
    }
}