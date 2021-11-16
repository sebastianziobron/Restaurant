using kurs2.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kurs2.Authorization
{
    public class CreatedMultipleRestaurantRequirmentHandler : AuthorizationHandler<CreatedMultipleRestaurantRequirment>
    {
        private readonly RestaurantDBContext _dbContext;
        public CreatedMultipleRestaurantRequirmentHandler(RestaurantDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirment requirement)
        {
            var userId = int.Parse(context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);


            var restaurantCount = _dbContext.Restaurants.Count(x => x.CreatedById == userId);

            if (restaurantCount >= requirement.MinimumRestaurantCreated) context.Succeed(requirement);


            return Task.CompletedTask;

        }
    }
}
