using AutoMapper;
using Menu.Data;
using Menu.Dtos.Drink;
using Menu.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Menu.Dtos.Drink.Menu.Dtos.Drink;

namespace Menu.Services.Drink
{
    public class DrinkRepository : IDrinkRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DrinkRepository(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<DrinkDto>> GetDrinks()
        {
            var drinks = await _context.Drinks.Include(d => d.Section).ToListAsync();
            return _mapper.Map<IEnumerable<DrinkDto>>(drinks);
        }

        public async Task<DrinkDto> GetDrinkById(int id)
        {
            var drink = await _context.Drinks.Include(d => d.Section).FirstOrDefaultAsync(d => d.Id == id);
            return _mapper.Map<DrinkDto>(drink);
        }

        public async Task<(bool success, string message)> AddDrink(DrinkCreateDto drinkDto, IWebHostEnvironment webHostEnvironment)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Name == drinkDto.SectionName);
            if (section == null)
            {
                return (false, "Invalid section ID.");
            }

            // Check if drink name already exists
            bool drinkExists = await _context.Drinks.AnyAsync(d => d.Name == drinkDto.Name);
            if (drinkExists)
            {
                return (false, "Drink name already exists.");
            }

            var drink = _mapper.Map<Models.Drink>(drinkDto);
            drink.Section = section;

            if (drinkDto.Photo != null)
            {
                drink.Photo = await SavePhoto(drinkDto.Photo, webHostEnvironment);
            }

            await _context.Drinks.AddAsync(drink);
            await _context.SaveChangesAsync();

            return (true, "Drink added successfully.");
        }


        public async Task<(bool success, string message)> UpdateDrink(int id, DrinkUpdateDto drinkDto, IWebHostEnvironment webHostEnvironment)
        {
            var drink = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id);
            if (drink == null)
            {
                return (false, "Drink not found.");
            }

            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == drinkDto.SectionId);
            if (section == null)
            {
                return (false, "Invalid section ID.");
            }

            _mapper.Map(drinkDto, drink);
            drink.Section = section;

            if (drinkDto.Photo != null)
            {
                drink.Photo = await SavePhoto(drinkDto.Photo, webHostEnvironment);
            }

            await _context.SaveChangesAsync();

            return (true, "Drink updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteDrink(int id)
        {
            var drink = await _context.Drinks.FirstOrDefaultAsync(d => d.Id == id);
            if (drink == null)
            {
                return (false, "Drink not found.");
            }

            _context.Drinks.Remove(drink);
            await _context.SaveChangesAsync();

            return (true, "Drink deleted successfully.");
        }

        public async Task<IEnumerable<DrinkDto>> GetDrinksBySection(string sectionName)
        {
            var drinks = await _context.Drinks
                .Include(d => d.Section)
                .Where(d => d.Section.Name == sectionName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<DrinkDto>>(drinks);
        }

        private async Task<string> SavePhoto(IFormFile photo, IWebHostEnvironment webHostEnvironment)
        {
            var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return Path.Combine("uploads", uniqueFileName);
        }
    }
}
