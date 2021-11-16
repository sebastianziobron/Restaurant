using AutoMapper;
using kurs2.Authorization;
using kurs2.Entities;
using kurs2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kurs2.Services
{
    public class RestaurantService : IRestaurantService
    {

        private readonly RestaurantDBContext _dbcontext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public RestaurantService(IMapper mapper, RestaurantDBContext dbcontext, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public bool DeleteRestaurant(int id)
        {
            var restaurant = _dbcontext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirment(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded) return false;

            _dbcontext.Restaurants.Remove(restaurant);
            _dbcontext.SaveChanges();
            return true;

        }

        public bool Update(UpdateRestaurantDto dto, int id)
        {
            var restaurant = _dbcontext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null) return false;

           var authorizationResult =  _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirment(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                return false;
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbcontext.Update(restaurant);
            _dbcontext.SaveChanges();

            return true;
        }

        public int Created(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            restaurant.CreatedById = _userContextService.GetUserId;

            _dbcontext.Restaurants.Add(restaurant);
            _dbcontext.SaveChanges();

            return restaurant.Id;
        }

        public PageResault<RestaurantDTO> GetAll(RestaurantQuery query)
        {

            var baseQuery = _dbcontext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .Where(r => query.Search == null || r.Name.ToLower().Contains(query.Search.ToLower()) || r.Description.ToLower().Contains(query.Search.ToLower()));

            if(!string.IsNullOrEmpty(query.SortBy))
            {

                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    { nameof(Restaurant.Name), r => r.Name },
                    { nameof(Restaurant.Description), r => r.Description},
                    { nameof(Restaurant.Category), r => r.Category}
                    
                };

                var selectedColumn = columnsSelector[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurant = baseQuery
                .Skip(query.PageSize * (query.PageNumber -1))
                .Take(query.PageSize)
                .ToList();

            var totalcount = baseQuery.Count();
            var restaurantdto = _mapper.Map<List<RestaurantDTO>>(restaurant);
            var resault = new PageResault<RestaurantDTO>(restaurantdto, totalcount, query.PageSize ,query.PageNumber);
            return resault;

        }

        public RestaurantDTO GetById(int id)
        {
            var restaurant = _dbcontext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            var restaurantdto = _mapper.Map<RestaurantDTO>(restaurant);

            return restaurantdto;
        }
    }
}
