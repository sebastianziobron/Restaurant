using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Authorization
{
    public class MinimumAgeRequirment : IAuthorizationRequirement
    {
        public int MinimumAge { get; set; }

        public MinimumAgeRequirment(int minimumage)
        {
            MinimumAge = minimumage;
        }
    }
}
