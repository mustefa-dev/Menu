using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos.Category;

namespace Item.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryReadDto>> GetCategories();
        Task<CategoryReadDto> GetCategory(int id);
        Task<CategoryReadDto> AddCategory(CategoryCreateDto categoryDto);
        Task<bool> UpdateCategory(int id, CategoryUpdateDto categoryDto);
        Task<bool> DeleteCategory(int id);
    }
}