using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LogicomDevisFrontEnd
{
    public class AuthenticationMiddleleware : OwinMiddleware
    {
        public AuthenticationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var sessionCookie = context.Request.Cookies["SessionID"];
            if (sessionCookie != null)
            {
                var sessionID = sessionCookie;
                var expirationTime = HttpContext.Current.Cache[sessionID] as DateTime?;
                if (expirationTime.HasValue && expirationTime.Value > DateTime.UtcNow)
                {
                    // The user is authenticated
                    await Next.Invoke(context);
                    return;
                }
            }

            // The user is not authenticated, so redirect to the login page
            var loginUrl = "";
            context.Response.Redirect(loginUrl);

        }
    }