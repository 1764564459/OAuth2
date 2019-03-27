using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OAuthServer.Models.AuthProvider
{
    /// <summary>
    /// 授权码提供器
    /// </summary>
    public class AuthCodeProvider: IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _authorizationCodes =
           new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _authorizationCodes[context.Token] = context.SerializeTicket();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return Task.Run(() =>
            {
                this.Create(context);
            });
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_authorizationCodes.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return Task.Run(() =>
            {
                this.Receive(context);
            });
        }
    }
}