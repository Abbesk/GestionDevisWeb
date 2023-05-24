using Microsoft.Owin.Security.OAuth;
using Raven.Database.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LogicomDevisBackEnd.App_Start
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using(DatabaseHelper repo = new DatabaseHelper())
            {
                var isUserValid = authenticateUserUsinADGroup(context.UserName);
                if(isUserValid== false)
                {
                    context.SetError("Unauthorized");
                    return; 
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, "Admi,"));
                context.Validated(identity); 

            }
        }
    }
}