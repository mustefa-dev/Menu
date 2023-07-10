using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Dtos.Category;
using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data.Repositories{
    public interface ICategoryRepository{
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(Guid id);
        Task<Category> CreateCategoryAsync(Category categoryDto);
        Task UpdateCategoryAsync(Guid id, Category categoryDto);
        Task DeleteCategoryAsync(Guid id);
        Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId);
        Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId);
    }

    public class CategoryRepository : ICategoryRepository{
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<Category>> GetCategoriesAsync() {
            return await _context.Categories.Include(c => c.Sections)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(Guid id) {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category categoryDto) {
            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateCategoryAsync(Guid id, Category categoryDto) {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) {
                throw new ArgumentException($"Category with ID {id} not found.");
            }

            category.Name = categoryDto.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id) {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) {
                throw new ArgumentException($"Category with ID {id} not found.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId) {
            var sections = await _context.Sections.Where(s => s.CategoryId == categoryId).ToListAsync();
            if (sections == null || sections.Count == 0) {
                return false;
            }

            _context.Sections.RemoveRange(sections);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId) {
            var items = await _context.Items.Where(i => i.SectionId == sectionId).ToListAsync();
            if (items == null || items.Count == 0) {
                return false;
            }

            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}