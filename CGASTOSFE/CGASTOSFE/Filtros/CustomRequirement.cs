using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class CustomRequirement : IAuthorizationRequirement { }

public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var userToken = httpContext?.Session.GetString("UserToken");

        if (!string.IsNullOrEmpty(userToken))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
