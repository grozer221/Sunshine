using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Sunshine.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sunshine.Services
{
    public class AuthService
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyAuthClient"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public async Task Authenticate(HttpContext HttpContext, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        
        public string GetConfirmatioEmailToken(string email, string role)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString())
                };

            DateTime dateTimeNow = DateTime.Now;
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    notBefore: dateTimeNow,
                    claims: claims,
                    expires: dateTimeNow.Add(TimeSpan.FromMinutes(LIFETIME)),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt); 
        }
    }
}
