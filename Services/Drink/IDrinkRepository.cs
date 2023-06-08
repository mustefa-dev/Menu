using Menu.Dtos.Drink;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Menu.Services.Drink
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<DrinkDto>> GetDrinks();
        Task<DrinkDto> GetDrinkById(int id);
        Task<(bool success, string message)> AddDrink(DrinkDto drinkDto);
        Task<(bool success, string message)> UpdateDrink(int id, DrinkDto drinkDto);
        Task<(bool success, string message)> DeleteDrink(int id);
        Task<IEnumerable<DrinkDto>> GetDrinksBySection(string sectionName);

    }
}