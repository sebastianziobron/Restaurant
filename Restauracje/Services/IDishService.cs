using kurs2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Services
{
    public interface IDishService
    {
        bool CreateDish(CreateDishDto dish, int restaurantid);
        DishDTO GetDishById(int id);
        List<DishDTO> GetAllDishes(int restaurantid);
        bool DeleteAllDishes(int restaurantid);
        bool DeleteDishById(int id, int restaurantid);
    }
}
