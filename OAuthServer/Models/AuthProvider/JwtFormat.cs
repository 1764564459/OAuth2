using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using OAuthServer.Models.DataRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace OAuthServer.Models.AuthProvider
{
    public class JwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        //用于从AuthenticationTicket中获取Audience信息
        private const string AudiencePropertyKey = "aud";

        private readonly string _issuer = string.Empty;
        //Jwt的发布者和用于数字签名的密钥
        public JwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            //获取Audience名称及其信息
            string audienceId = data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ?
                data.Properties.Dictionary[AudiencePropertyKey] : null;
            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");
            var audience = ClientRepository.GetClient().Where(c => c.ID == audienceId).FirstOrDefault();
            if (audience == null) throw new InvalidOperationException("Audience invalid.");
            //根据密钥创建用于数字签名的SigningCredentials，该对象在JwtSecurityToken中使用
            var keyByteArray = TextEncodings.Base64Url.Decode(audience.Secret);
            //RSAParameters rsaSecurity = new RSAParameters();
            //rsaSecurity.
            var signingKey = new SymmetricSecurityKey(keyByteArray);//(keyByteArray);
            var signingCredentials = new SigningCredentials(signingKey,
                SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);
            //获取发布时间和过期时间
            var issued = data.Properties.IssuedUtc;
            //data.Properties.ExpiresUtc = data.Properties.ExpiresUtc.Value.AddSeconds(3*60);
            var expires = data.Properties.ExpiresUtc;
            //创建JwtToken对象
            var token = new JwtSecurityToken(_issuer,
                audienceId,
                data.Identity.Claims,
                issued.Value.UtcDateTime,
                expires.Value.UtcDateTime,
                signingCredentials);
            //使用JwtSecurityTokenHandler将Token对象序列化成字符串
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.WriteToken(token);
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}