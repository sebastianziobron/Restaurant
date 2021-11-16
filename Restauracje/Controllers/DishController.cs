using kurs2.Entities;
using kurs2.Models;
using kurs2.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kurs2.Controllers
{
    [Route("api/restaurant/{restaurantid}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {

        private readonly IDishService _dish;

        public DishController(IDishService dish)
        {
            _dish = dish;
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute]int restaurantid)
        {
            var result = _dish.DeleteAllDishes(restaurantid);

            if (result) return Ok("Usunieto");

            return BadRequest("Bład usuwania");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById([FromRoute]int restaurantid, [FromRoute]int id)
        {
            var result = _dish.DeleteDishById(id, restaurantid);

            if (result) return Ok($"Usunięto Danie o ID: {id}");

            return BadRequest();
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantid, [FromBody] CreateDishDto dto)
        {
            var result = _dish.CreateDish(dto, restaurantid);

            if (result) return Ok("Dodano danie");

            return BadRequest("Nie znaleziono");
        }

        [HttpGet("{id}")]
        public ActionResult GetById([FromRoute] int id)
        {
            var dish = _dish.GetDishById(id);

            if (dish is null) return NotFound();

            return Ok(dish);
        }

        [HttpGet]
        public ActionResult<List<DishDTO>> GetAll([FromRoute]int restaurantid)
        {
            var dish = _dish.GetAllDishes(restaurantid);

            if (dish is null) return BadRequest();

            return Ok(dish);
        }

    }
}
