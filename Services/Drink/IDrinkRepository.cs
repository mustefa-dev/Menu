using Menu.Dtos.Drink;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos.Drink.Menu.Dtos.Drink;

namespace Menu.Services.Drink
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<DrinkDto>> GetDrinks();
        Task<DrinkDto> GetDrinkById(int id);
        Task<(bool success, string message)> AddDrink(DrinkCreateDto drinkDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> UpdateDrink(int id, DrinkUpdateDto drinkDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> DeleteDrink(int id);
        Task<IEnumerable<DrinkDto>> GetDrinksBySection(string sectionName);
    }
}