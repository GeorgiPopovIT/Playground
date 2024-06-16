using Microsoft.AspNetCore.Authorization;

namespace JWT_Authentication;

public class AdminRequirementAuthorizationHandler : AuthorizationHandler<AdminRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
    {
        if (!context.User.HasClaim(x => x.Type == "Admin"))
        {
            return Task.CompletedTask;
        }

        var isUserAdmin = context.User?.Claims?
            .First(x => x.Type == "Admin").Value;

        bool isUserAdminClaim = bool.Parse(isUserAdmin);

        if (isUserAdminClaim)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
