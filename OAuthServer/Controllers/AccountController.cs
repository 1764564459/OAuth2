using EntityFrameWork.Server.Entity;
using EntityFrameWork.Server.Entity.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using OAuthServer.Models.AuthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OAuthServer.Controllers
{
    public class AccountController : Controller
    {
        private  UserManager<AppUser, Guid> _userManager;
        private  SignInManager<AppUser, Guid> _signInManager;

        public UserManager<AppUser, Guid> UserManager {
            get
            {
              return  _userManager=_userManager?? new UserManager<AppUser, Guid>(new UserStore<AppUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>(new AppDbContext()));
            } set {
                _userManager = value;
            }
        }

        public SignInManager<AppUser, Guid> SignInManager {
            get => _signInManager = _signInManager ??new SignInManager<AppUser, Guid> (UserManager,HttpContext.GetOwinContext().Authentication);
            set => _signInManager = value;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            var UserManage = new UserManager<AppUser,Guid>(new UserStore<AppUser,AppRole,Guid,AppUserLogin,AppUserRole,AppUserClaim>(new AppDbContext()));
            var users = await UserManage.FindAsync(model.UserName, model.Password);
            if (users == null)
            {
                ModelState.AddModelError("", "用户名或密码错误！");
                return View(model);
            }
            var sigin = new SignInManager<AppUser, Guid>(UserManage, HttpContext.GetOwinContext().Authentication);
            var Result = await sigin.PasswordSignInAsync(users.UserName, model.Password, false, false);
            return View(model);
        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", JsonConvert.SerializeObject(result.Errors));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}