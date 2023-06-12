using Menu.Dtos.Drink;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos.Drink.Menu.Dtos.Drink;

namespace Menu.Services.Drink
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<DrinkReadDto>> GetDrinks();
        Task<DrinkReadDto> GetDrinkById(int id);
        Task<(bool success, string message)> AddDrink(DrinkCreateDto drinkDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> UpdateDrink(int id, DrinkUpdateDto drinkDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> DeleteDrink(int id);
        Task<IEnumerable<DrinkReadDto>> GetDrinksBySection(string sectionName);
    }
}