using Menu.Dtos;

namespace Menu.Services.Menu
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDto>> GetAllItems();
        Task<ItemDto> GetItemById(int id);
        Task<string> CreateItem(ItemDto itemDto);
        Task UpdateItem(ItemDto itemDto);
        Task DeleteItem(int id);
        Task<bool> ItemExists(int id);

    }
}