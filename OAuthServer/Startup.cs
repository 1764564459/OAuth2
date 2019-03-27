using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using OAuthServer.Models.AuthProvider;
using Owin;

[assembly: OwinStartup(typeof(OAuthServer.Startup))]

namespace OAuthServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888

            //注入用户授权服务提供器
            var issuer = "http://localhost:60726/";
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions {
                AllowInsecureHttp = true,
                AuthorizeEndpointPath = new PathString("/Auth/authorize"),//授权地址
                TokenEndpointPath = new PathString("/Auth/token"),//token地址
                Provider = new AuthProvider(),
                AuthorizationCodeProvider = new AuthCodeProvider(),
                RefreshTokenProvider = new RefreshTokenProvider(),
                //AuthorizationCodeExpireTimeSpan
                AccessTokenExpireTimeSpan=TimeSpan.FromSeconds(60*60),//token 过去时间
                //AccessTokenExpireTimeSpan = TimeSpan.FromTicks(DateTime.UtcNow.AddSeconds(60 * 6).Ticks),
                AccessTokenFormat =new Models.AuthProvider.JwtFormat(issuer)//token  格式化成  Jwt
            });
            
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                AllowedAudiences = new[] { "id" },
               
                IssuerSecurityKeyProviders=new IIssuerSecurityKeyProvider[] {
                     new SymmetricKeyIssuerSecurityKeyProvider(issuer, "LHZ5bUlOR2dzW1Yzd1dkbHdFbXNQSVBHSEs9dTZQKTE=")
                }
            });

            //实现基于Access Token的身份验证
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
            });

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie

            //用户Cookie授权
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                    //    validateInterval: TimeSpan.FromMinutes(30),
                    //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
        }
    }
}
