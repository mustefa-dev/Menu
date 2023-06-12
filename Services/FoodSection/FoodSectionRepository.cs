using Menu.Data;
using Menu.Dtos.Food;
using Menu.Dtos.FoodSection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace Menu.Services.FoodSection
{
    public class FoodSectionRepository : IFoodSectionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FoodSectionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FoodSectionReadDto>> GetFoodSections()
        {
            var foodSections = await _context.FoodSections.ToListAsync();
            return _mapper.Map<List<FoodSectionReadDto>>(foodSections);
        }

        public async Task<FoodSectionReadDto> GetFoodSectionById(int id)
        {
            var foodSection = await _context.FoodSections.FindAsync(id);
            return _mapper.Map<FoodSectionReadDto>(foodSection);
        }

        public async Task<(bool success, string message)> AddFoodSection(FoodSectionCreateDto foodSectionCreateDto)
        {
            var foodSection = _mapper.Map<Models.FoodSection>(foodSectionCreateDto);
            await _context.FoodSections.AddAsync(foodSection);
            await _context.SaveChangesAsync();
            return (true, "Food section added successfully.");
        }

        public async Task<(bool success, string message)> UpdateFoodSection(FoodSectionReadDto foodSectionUpdateDto)
        {
            var foodSection = await _context.FoodSections.FindAsync(foodSectionUpdateDto);
            if (foodSection == null)
            {
                return (false, "Food section not found.");
            }

            _mapper.Map(foodSectionUpdateDto, foodSection);
            await _context.SaveChangesAsync();
            return (true, "Food section updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteFoodSection(int id)
        {
            var foodSection = await _context.FoodSections.FindAsync(id);
            if (foodSection == null)
            {
                return (false, "Food section not found.");
            }

            _context.FoodSections.Remove(foodSection);
            await _context.SaveChangesAsync();
            return (true, "Food section deleted successfully.");
        }

    }
}
