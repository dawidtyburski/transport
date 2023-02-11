using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using transport.Models;

namespace transport.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Order>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Order order)
        {
            if(requirement.ResourceOperation == ResourceOperation.Read ||
               requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            /*if(order.UserId == userId)
            {
                context.Succeed(requirement);
            }*/

            return Task.CompletedTask;
        }
    }
}
