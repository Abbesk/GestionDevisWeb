using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace LogicomDevisBackEnd.Security
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
       

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization ==null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
               
            }

            else
            {
                string auth_string = actionContext.Request.Headers.Authorization.Parameter;
                string original_String = Encoding.UTF8.GetString(Convert.FromBase64String(auth_string));
                string username = original_String.Split(':')[0];
                string password = original_String.Split(':')[1];
                if (!webapi_security.ValidateUsers(username, password))
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
            base.OnAuthorization(actionContext); 
        }
    }
}