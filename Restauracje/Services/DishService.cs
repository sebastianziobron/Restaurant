using AutoMapper;
using kurs2.Entities;
using kurs2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDBContext _dbContext;
        private readonly IMapper _mapper;
        public DishService(RestaurantDBContext dBContext, IMapper mapper)
        {
            _dbContext = dBContext;
            _mapper = mapper;
        }

        public bool CreateDish(CreateDishDto dish, int restaurantid)
        {
            var restaurant = _dbContext.Restaurants.Include(r=>r.Dishes).FirstOrDefault(r => r.Id == restaurantid);

            if (restaurant is null)
                return false;

            dish.RestaurantId = restaurantid;

            var newDish = _mapper.Map<Dish>(dish);

            _dbContext.Dishes.Add(newDish);
            _dbContext.SaveChanges();

            return true;

        }

        public DishDTO GetDishById(int id)
        {
            var dish = _dbContext.Dishes.FirstOrDefault(r => r.Id == id);

            var showdish = _mapper.Map<DishDTO>(dish);

            return showdish;
        }

        public List<DishDTO> GetAllDishes(int restaurantid)
        {
            var restaurant = _dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantid);


            var dishes = _mapper.Map<List<DishDTO>>(restaurant.Dishes);

            return dishes;

        }

        public bool DeleteDishById(int id, int restaurantid)
        {
            var dish = _dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantid);

            if (dish is null) return false;

            var removeDish = dish.Dishes.FirstOrDefault(r => r.Id == id);

            if (removeDish is null) return false;

            _dbContext.Dishes.Remove(removeDish);
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteAllDishes(int restaurantid)
        {
            var restaurant = _dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefault(r => r.Id == restaurantid);


            if (restaurant is null) return false;

            _dbContext.Dishes.RemoveRange(restaurant.Dishes);
            _dbContext.SaveChanges();

            return true;
        
        }


    }
}
