using System.Threading.Tasks;
using BerkayMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace BerkayMarket.Authorization
{
    public class TanisManageriYetkiAlani :
        AuthorizationHandler<OperationAuthorizationRequirement, Tanislik>
    {
        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Tanislik resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != Constants.OnaylaIslemAdi &&
                requirement.Name != Constants.RedIslemAdi)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.TanisManagerRole) || context.User.IsInRole(Constants.ManagerRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
