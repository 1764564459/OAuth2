using EntityFrameWork.Server.Entity;
using EntityFrameWork.Server.Entity.Auth;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using OAuthServer.Models.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace OAuthServer.Models.AuthProvider
{
    /// <summary>
    /// 授权服务器对Client的验证
    /// </summary>
    public class AuthProvider: OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                return Task.FromResult<object>(null);
            }
            if (!string.IsNullOrEmpty(clientSecret))
            {
                context.OwinContext.Set("clientSecret", clientSecret);
            }
            var client = ClientRepository.GetClient().Where(c => c.ID == clientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated();
            }
            else
            {
                context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                return Task.FromResult<object>(null);
            }
            return Task.FromResult<object>(null);
        }
        //验证用户重定向
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            var client = ClientRepository.GetClient().Where(c => c.ID == context.ClientId).FirstOrDefault();
            if (client != null)
            {
                context.Validated(client.RedirectUrl);
            }
            return base.ValidateClientRedirectUri(context);
        }

        //实现客户端模式获取Access Token
        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            var secret = context.OwinContext.Get<string>("clientSecret");
            var client = ClientRepository.GetClient().Where(c => c.ID == context.ClientId && c.Secret == secret).FirstOrDefault();
            if (client != null)
            {
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.ClientId,
                    OAuthDefaults.AuthenticationType),
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x)));
                //支持jwt
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "aud", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            return Task.FromResult(0);
        }
        //实现通过用户密码模式获取Access Token
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //var userManager = context.OwinContext.Get<ApplicationUserManager>("AspNet.Identity.Owin:" + typeof(ApplicationUserManager).AssemblyQualifiedName);
            var userManager = new UserManager<AppUser, Guid>(new UserStore<AppUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>(new AppDbContext()));
            if (userManager != null)
            {
                var user = userManager.FindAsync(context.UserName, context.Password).Result;
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect");
                    //return;
                    return Task.FromResult<object>(null);
                }
                var identity = new ClaimsIdentity(
                    new GenericIdentity(context.UserName,
                    OAuthDefaults.AuthenticationType),
                    context.Scope.Select(x => new Claim("urn:oauth:scope", x)));
                //支持jwt
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "aud", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });
                var ticket = new AuthenticationTicket(identity, props);
                context.Validated(ticket);
            }
            return Task.FromResult(0);
        }


    }
}