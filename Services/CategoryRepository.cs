using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Dtos.Category;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryReadDto>> GetCategoriesAsync();
        Task<CategoryReadDto> GetCategoryAsync(Guid id);
        Task<CategoryReadDto> CreateCategoryAsync(Category categoryDto);
        Task<CategoryReadDto> UpdateCategoryAsync(Guid id, CategoryReadDto categoryDto);
        Task<bool> DeleteCategoryAsync(Guid id);
        Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId);
        Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CategoryReadDto>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(Category categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(Guid id, CategoryReadDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ArgumentException($"Category with ID {id} not found.");
            }

            category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId)
        {
            var sections = await _context.Sections.Where(s => s.CategoryId == categoryId).ToListAsync();
            if (sections == null || sections.Count == 0)
            {
                return false;
            }

            _context.Sections.RemoveRange(sections);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId)
        {
            var items = await _context.Items.Where(i => i.SectionId == sectionId).ToListAsync();
            if (items == null || items.Count == 0)
            {
                return false;
            }

            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}