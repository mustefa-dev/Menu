    using AutoMapper;
    using Menu.Data;
    using Menu.Dtos.Food;
    using Menu.Services.Food;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    namespace Menu.Controllers
    {
        [ApiController]
        [Route("api/foods")]
        public class FoodsController : ControllerBase
        {
            private readonly IFoodRepository _foodRepository;
            private readonly IMapper _mapper;
            private readonly IWebHostEnvironment _webHostEnvironment;

            public FoodsController(IFoodRepository foodRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
            {
                _foodRepository = foodRepository;
                _mapper = mapper;
                _webHostEnvironment = webHostEnvironment;
            }

            [HttpGet("section/{sectionName}")]
            [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 382")]
            public async Task<ActionResult<IEnumerable<FoodReadDto>>> GetFoodsBySection(string sectionName)
            {
                var foods = await _foodRepository.GetFoodsBySection(sectionName);
                if (foods == null)
                {
                    return NoContent();
                }

                return Ok(foods);
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<FoodReadDto>>> GetFoods()
            {
                var foods = await _foodRepository.GetFoods();
                if (foods == null)
                {
                    return NoContent();
                }

                return Ok(foods);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<FoodCreateDto>> GetFoodById(int id)
            {
                var food = await _foodRepository.GetFoodById(id);
                if (food == null)
                {
                    return NotFound();
                }

                return Ok(food);
            }

            [HttpPost]
            public async Task<IActionResult> AddFood([FromForm] FoodCreateDto foodDto)
            {
                var (success, message) = await _foodRepository.AddFood(foodDto, _webHostEnvironment);
                if (!success)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateFood(int id, [FromForm] FoodUpdateDto foodDto)
            {
                var (success, message) = await _foodRepository.UpdateFood(id, foodDto, _webHostEnvironment);
                if (!success)
                {
                    return NotFound(message);
                }

                return Ok(message);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteFood(int id)
            {
                var (success, message) = await _foodRepository.DeleteFood(id);
                if (!success)
                {
                    return NotFound(message);
                }

                return Ok(message);
            }
        }
    }
