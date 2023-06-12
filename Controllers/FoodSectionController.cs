using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Data;
using Menu.Dtos.Food;
using Menu.Dtos.FoodSection;
using Menu.Models;
using Menu.Services.FoodSection;
using Microsoft.AspNetCore.Mvc;
using FoodSection = Menu.Models.FoodSection;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodSectionController : ControllerBase
    {
        private readonly IFoodSectionRepository _foodSectionRepository;
        private readonly IMapper _mapper;

        public FoodSectionController(IFoodSectionRepository foodSectionRepository, IMapper mapper)
        {
            _foodSectionRepository = foodSectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FoodSectionReadDto>>> GetFoodSections()
        {
            var foodSections = await _foodSectionRepository.GetFoodSections();
            var foodSectionDtos = _mapper.Map<List<FoodSectionReadDto>>(foodSections);
            return Ok(foodSectionDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodSectionReadDto>> GetFoodSection(int id)
        {
            var foodSection = await _foodSectionRepository.GetFoodSectionById(id);
            if (foodSection == null)
            {
                return NotFound();
            }

            var foodSectionDto = _mapper.Map<FoodSectionReadDto>(foodSection);
            return Ok(foodSectionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddFoodSection(FoodSectionCreateDto foodSectionCreateDto)
        {
            FoodSectionCreateDto foodSection = _mapper.Map<FoodSectionCreateDto>(foodSectionCreateDto);
            await _foodSectionRepository.AddFoodSection(foodSection);
            return Ok(foodSection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodSection(int id, FoodSectionUpdateDto foodSectionUpdateDto)
        {
            var foodSection = await _foodSectionRepository.GetFoodSectionById(id);
            if (foodSection == null)
            {
                return NotFound(foodSection);
            }

            _mapper.Map(foodSectionUpdateDto, foodSection);
            await _foodSectionRepository.UpdateFoodSection(foodSection);
            return Ok(foodSection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodSection(int id)
        {
            var foodSection = await _foodSectionRepository.GetFoodSectionById(id);
            if (foodSection == null)
            {
                return NotFound(foodSection);
            }
        
            await _foodSectionRepository.DeleteFoodSection(id);
            return Ok(foodSection);
        }
    }
}
