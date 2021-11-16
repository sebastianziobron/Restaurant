using kurs2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDBContext _dbContext;
        public RestaurantSeeder(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.Restaurants.Any())
                {
                    var restaurant = GetRestaurant();
                    _dbContext.Restaurants.AddRange(restaurant);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var role = new List<Role>()
            {
                new Role()
                {
                    Name="User"
                },

                new Role()
                {
                    Name = "Manager"
                },

                new Role()
                {
                    Name = "Admin"
                }
            };

            return role;
        }

        private IEnumerable<Restaurant> GetRestaurant()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Category = "FastFood",
                    HasDelivery = true,
                    ContactEmail = "witam@gmail.com",
                    ContactNumber = "341678321",

                    Address = new Address()
                    {
                        City = "Dębica",
                        PostalCode = "39-200",
                        Street = "Towarnickiego 19"
                    },

                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Pierogi",
                            Price = 32.30M,
                        },

                         new Dish()
                        {
                            Name = "Bigos",
                            Price = 20.30M,
                        }
                    }

                }
            };

            return restaurants;
        }
    }
}
