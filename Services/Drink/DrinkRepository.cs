using AutoMapper;
using Menu.Data;
using Menu.Dtos.Drink;
using Menu.Services.Drink;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Section{
    public class DrinkRepository : IDrinkRepository{
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DrinkRepository(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DrinkDto>> GetDrinks() {
            var drinks = await _context.Drinks.Include(d => d.Section).ToListAsync();
            return _mapper.Map<IEnumerable<DrinkDto>>(drinks);
        }

        public async Task<DrinkDto> GetDrinkById(int id) {
            var drink = await _context.Drinks.Include(d => d.Section).FirstOrDefaultAsync(d => d.Id == id);
            return _mapper.Map<DrinkDto>(drink);
        }

        public async Task<(bool success, string message)> AddDrink(DrinkDto drinkDto) {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == drinkDto.SectionName);
            if (section == null) {
                return (false, "Invalid section Name.");
            }

            var drink = _mapper.Map<Models.Drink>(drinkDto);
            drink.Section = section;

            await _context.Drinks.AddAsync(drink);
            await _context.SaveChangesAsync();

            return (true, "Drink added successfully.");
        }

        public async Task<(bool success, string message)> UpdateDrink(int id, DrinkDto drinkDto) {
            var drink = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id);
            if (drink == null) {
                return (false, "Drink not found.");
            }

            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == drinkDto.SectionName);
            if (section == null) {
                return (false, "Invalid section ID.");
            }

            _mapper.Map(drinkDto, drink);
            drink.Section = section;

            await _context.SaveChangesAsync();

            return (true, "Drink updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteDrink(int id) {
            var drink = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id);
            if (drink == null) {
                return (false, "Drink not found.");
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return (true, "Drink deleted successfully.");
        }

        public async Task<IEnumerable<DrinkDto>> GetDrinksBySection(string sectionName) {
            var drinks = await _context.Drinks
                .Include(d => d.Section)
                .Where(d => d.Section.Name == sectionName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DrinkDto>>(drinks);
        }

    }
}