using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos;
using Menu.Dtos.Menu.Dtos;

namespace Menu.Services.Menu
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItems();
        Task<ItemDto> GetItemById(int id);
        Task<string> CreateItem(ItemDto itemDto, IFormFile photo);
        Task UpdateItem(ItemDto itemDto, IFormFile photo);
        Task DeleteItem(int id);
        Task<bool> ItemExists(int id);
    }
}
