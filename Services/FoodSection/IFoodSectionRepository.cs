using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos.Food;
using Menu.Dtos.FoodSection;

namespace Menu.Services.FoodSection
{
    public interface IFoodSectionRepository
    {
        Task<List<FoodSectionReadDto>> GetFoodSections();
        Task<FoodSectionReadDto> GetFoodSectionById(int id);
        Task<(bool success, string message)> AddFoodSection(FoodSectionCreateDto foodSectionCreateDto);
        Task<(bool success, string message)> UpdateFoodSection(FoodSectionReadDto foodSection);
        Task<(bool success, string message)> DeleteFoodSection(int id);
    }
}
