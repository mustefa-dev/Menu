using Menu.Dtos.Food;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Menu.Services.Food
{
    public interface IFoodRepository
    {
        Task<IEnumerable<FoodReadDto>> GetFoods();
        Task<FoodReadDto> GetFoodById(int id);
        Task<(bool success, string message)> AddFood(FoodCreateDto foodDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> UpdateFood(int id, FoodUpdateDto foodDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> DeleteFood(int id);
        Task<IEnumerable<FoodReadDto>> GetFoodsBySection(string sectionName);
    }
}