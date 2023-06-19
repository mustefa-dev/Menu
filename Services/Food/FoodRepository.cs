using AutoMapper;
using Menu.Data;
using Menu.Dtos.Food;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Food
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodRepository(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<FoodReadDto>> GetFoods()
        {
            var foods = await _context.Foods.Include(f => f.Section).ToListAsync();
            return _mapper.Map<IEnumerable<FoodReadDto>>(foods);
        }

        public async Task<FoodReadDto> GetFoodById(int id)
        {
            var food = await _context.Foods.Include(f => f.Section).FirstOrDefaultAsync(f => f.Id == id);
            return _mapper.Map<FoodReadDto>(food);
        }

        public async Task<(bool success, string message)> AddFood(FoodCreateDto foodDto, IWebHostEnvironment webHostEnvironment)
        {
            var section = await _context.FoodSections.FirstOrDefaultAsync(s => s.Name == foodDto.SectionName);
            if (section == null)
            {
                return (false, "Invalid section ID.");
            }

            bool foodExists = await _context.Foods.AnyAsync(f => f.Name == foodDto.Name);
            if (foodExists)
            {
                return (false, "Food name already exists.");
            }

            var food = _mapper.Map<Models.Food>(foodDto);
            food.Section = section;

            if (foodDto.Photo != null)
            {
                food.Photo = await SavePhoto(foodDto.Photo, webHostEnvironment);
            }

            await _context.Foods.AddAsync(food);
            await _context.SaveChangesAsync();

            return (true, "Food added successfully.");
        }

        public async Task<(bool success, string message)> UpdateFood(int id, FoodUpdateDto foodDto, IWebHostEnvironment webHostEnvironment)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id);
            if (food == null)
            {
                return (false, "Food not found.");
            }

            var section = await _context.FoodSections.FirstOrDefaultAsync(s => s.Name == foodDto.SectionName);
            if (section == null)
            {
                return (false, "Invalid section ID.");
            }

            _mapper.Map(foodDto, food);
            food.Section = section;

            if (foodDto.Photo != null)
            {
                food.Photo = await SavePhoto(foodDto.Photo, webHostEnvironment);
            }

            await _context.SaveChangesAsync();

            return (true, "Food updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteFood(int id)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id == id);
            if (food == null)
            {
                return (false, "Food not found.");
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return (true, "Food deleted successfully.");
        }

        public async Task<IEnumerable<FoodReadDto>> GetFoodsBySection(string sectionName)
        {
            var foods = await _context.Foods
                .Include(f => f.Section)
                .Where(f => f.Section.Name == sectionName)
                .ToListAsync();

            return _mapper.Map<IEnumerable<FoodReadDto>>(foods);
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
