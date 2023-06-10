using AutoMapper;
using Menu.Data;
using Menu.Dtos.Drink;
using Menu.Services.Drink;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Menu.Dtos.Drink.Menu.Dtos.Drink;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/drinks")]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DrinksController(IDrinkRepository drinkRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("section/{sectionName}")]
        public async Task<ActionResult<IEnumerable<DrinkDto>>> GetDrinksBySection(string sectionName)
        {
            var drinks = await _drinkRepository.GetDrinksBySection(sectionName);
            if (drinks == null)
            {
                return NoContent();
            }

            return Ok(drinks);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDto>>> GetDrinks()
        {
            var drinks = await _drinkRepository.GetDrinks();
            if (drinks == null)
            {
                return NoContent();
            }

            return Ok(drinks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkDto>> GetDrinkById(int id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        [HttpPost]
        public async Task<IActionResult> AddDrink([FromForm] DrinkCreateDto drinkDto)
        {
            var (success, message) = await _drinkRepository.AddDrink(drinkDto, _webHostEnvironment);
            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrink(int id, [FromForm] DrinkUpdateDto drinkDto)
        {
            var (success, message) = await _drinkRepository.UpdateDrink(id, drinkDto, _webHostEnvironment);
            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            var (success, message) = await _drinkRepository.DeleteDrink(id);
            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }
    }
}
