using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using LogicomDevisBackEnd.Models;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Owin.Security.Jwt;

namespace LogicomDevisBackEnd
{
    public partial class Startup
    {
       



     
        public void ConfigureAuth(IAppBuilder app)
        {
            var jwtOptions = new JwtBearerAuthenticationOptions
            {
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Your_Secret_Key_Here")),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            };
            app.UseJwtBearerAuthentication(jwtOptions);
        }
    }
}
