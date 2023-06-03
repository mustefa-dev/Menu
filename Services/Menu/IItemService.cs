using Menu.Dtos;

namespace Menu.Services.Menu
{
    public interface IItemRepository
    {
        Task<ItemReadDto> AddItem(ItemCreateDto itemDto);
        Task<IEnumerable<ItemReadDto>> GetItems();
        Task<ItemReadDto> GetItem(int id);
        Task<bool> UpdateItem(int id, ItemUpdateDto itemDto); 
        Task<bool> DeleteItem(int id);
    }
}