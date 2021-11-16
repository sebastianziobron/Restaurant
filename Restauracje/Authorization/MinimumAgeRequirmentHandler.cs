using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kurs2.Authorization
{
    public class MinimumAgeRequirmentHandler : AuthorizationHandler<MinimumAgeRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirment requirement)
        {
            var dateOfBrith = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBrith").Value);

            var useremail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

            if(dateOfBrith.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
