using LogicomDevisBackEnd.Models;
using LogicomDevisBackEnd.Service;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LogicomDevisBackEnd.provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var userService = new UserService();
                utilisateur utilisateur = userService.GetUserByCredentials(username, password);
                if (utilisateur != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,utilisateur.codeuser),
                        new Claim("UserNom" , utilisateur.nom)
                    };
                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                }
                else
                {
                    context.SetError("invalide_grant", "error"); 
                }

            });
        }
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if(context.ClientId == null)
            {
                context.Validated(); 
            }
            return Task.FromResult<object>(null); 
        }
    }
}