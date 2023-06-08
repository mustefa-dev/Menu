using AutoMapper;
using Menu.Dtos.Drink;
using Menu.Services.Drink;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/drinks")]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IMapper _mapper;

        public DrinksController(IDrinkRepository drinkRepository, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _mapper = mapper;
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
        public async Task<IActionResult> AddDrink(DrinkDto drinkDto)
        {
            var (success, message) = await _drinkRepository.AddDrink(drinkDto);
            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDrink(int id, DrinkDto drinkDto)
        {
            var (success, message) = await _drinkRepository.UpdateDrink(id, drinkDto);
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
    