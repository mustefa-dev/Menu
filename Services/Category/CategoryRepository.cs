using AutoMapper;
using Item.Data;
using Menu.Data;
using Menu.Dtos.Category;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> AddCategory(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<global::Menu.Models.Category>(categoryDto);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> UpdateCategory(int id, CategoryUpdateDto categoryDto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return false;

            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
