using System.Collections.Generic;
using System.Threading.Tasks;
using Auth.Dtos.Item;

namespace Item.Data
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
