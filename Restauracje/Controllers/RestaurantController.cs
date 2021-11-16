using AutoMapper;
using kurs2.Entities;
using kurs2.Models;
using kurs2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kurs2.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute]int id)
        {
            var restaurant = _restaurantService.Update(dto, id);

            if (restaurant) return Ok("Aktualizacja udana");

            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute]int id)
        {
            var result = _restaurantService.DeleteRestaurant(id);

            if (result) return Ok("Usunięto");

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var restaurant = _restaurantService.Created(dto);

            return Created($"/api/restaurant/{restaurant}",null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll([FromQuery]RestaurantQuery query)
        {
            var restaurant = _restaurantService.GetAll(query);

            return Ok(restaurant);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<RestaurantDTO> GetById([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);

            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }
    }
}
