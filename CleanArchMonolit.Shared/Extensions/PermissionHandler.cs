using Microsoft.AspNetCore.Authorization;

namespace CleanArchMonolit.Shared.Extensions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var hasPermission = context.User.Claims.Any(c =>
                c.Type == "permissions" &&
                c.Value == requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
