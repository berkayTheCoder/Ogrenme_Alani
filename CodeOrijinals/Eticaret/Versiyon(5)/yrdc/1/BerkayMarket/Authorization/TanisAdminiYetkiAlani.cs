using System.Threading.Tasks;
using BerkayMarket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace BerkayMarket.Authorization
{
    public class TanisAdminiYetkiAlani
                    : AuthorizationHandler<OperationAuthorizationRequirement, Tanislik>
    {
        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement, 
                                     Tanislik resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.TanisAdminRolu))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
