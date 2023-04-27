﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Web;
using Crud_Operation.Models;

namespace Crud_Operation
{
    public class JwtAuthentication
    {
        public static SymmetricSecurityKey _signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hgiefhhfduufdg778ygu"));
        public static string CreateJWTToken(Account userinfo)  
        {
            var credentials = new SigningCredentials(_signinkey, SecurityAlgorithms.HmacSha256);
            var issuer = "https://localhost:44384/";
            var audience = "https://localhost:44384/";
            var claims = new[] 
            {

                new Claim(ClaimTypes.Name,userinfo.Username),
            };

            var token = new JwtSecurityToken(
                    issuer,
                    audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                    );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public static ClaimsPrincipal ValidatejwtToken(string token)
        {
            var h = new JwtSecurityTokenHandler();
            h.ValidateToken(token, new TokenValidationParameters()
            {
           
                ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hgiefhhfduufdg778ygu")),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
                //ValidateLifetime = true,
                //ClockSkew = TimeSpan.FromMinutes(5)
            }, out var securityToken);
            var jwt = securityToken as JwtSecurityToken;
            var id = new ClaimsIdentity(jwt.Claims, "jwt", ClaimTypes.Name, ClaimTypes.Role);
            return new ClaimsPrincipal(id);
        }
        public static void AuthenticationRequest(string token)
        {
            
            var principal = ValidatejwtToken(token);
            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;
        }
    }
}
   