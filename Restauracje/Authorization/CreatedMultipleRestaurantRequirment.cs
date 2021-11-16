using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Authorization
{
    public class CreatedMultipleRestaurantRequirment : IAuthorizationRequirement
    {

        public CreatedMultipleRestaurantRequirment(int minimumRestaurantCreated)
        {
            MinimumRestaurantCreated = minimumRestaurantCreated;
        }

        public int MinimumRestaurantCreated { get; set; }
    }
}
