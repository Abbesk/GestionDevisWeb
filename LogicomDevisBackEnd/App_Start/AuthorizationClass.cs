using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LogicomDevisBackEnd.App_Start
{
    public class AuthorizationClass : IAuthorizationRequirement
{
    public string UserType { get; }

    public AuthorizationClass(string userType)
    {
        UserType = userType;
    }
}

public class UserTypeHandler : AuthorizationHandler<AuthorizationClass>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationClass requirement)
    {
        if (context.User.HasClaim(c => c.Type == "role" && c.Value == requirement.UserType))
        {
            context.Succeed(requirement);
        }

        return Task.FromResult<object>(null);
        }
}
}