using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos;

namespace Menu.Services
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuItemDto>> GetAllMenuItems();
        Task<MenuItemDto> GetMenuItemById(int id);
        Task<string> CreateMenuItem(MenuItemDto menuItemDto);
        Task UpdateMenuItem(MenuItemDto menuItemDto);
        Task DeleteMenuItem(int id);
        Task<bool> MenuItemExists(int id);
    }
}