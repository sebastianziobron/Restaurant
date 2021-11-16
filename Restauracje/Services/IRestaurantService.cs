using kurs2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kurs2.Services
{
    public interface IRestaurantService
    {
        RestaurantDTO GetById(int id);
        PageResault<RestaurantDTO> GetAll(RestaurantQuery query);
        int Created(CreateRestaurantDto dto);
        bool DeleteRestaurant(int id);
        bool Update(UpdateRestaurantDto dto, int id);
    }
}
