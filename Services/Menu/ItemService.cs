using Auth.Data;
using AutoMapper;
using Menu.Dtos;
using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MenuService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllMenuItems()
        {
            var menuItems = await _context.MenuItems.ToListAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto> GetMenuItemById(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<string> CreateMenuItem(MenuItemDto menuItemDto)
        {
            if (await MenuItemExists(menuItemDto.Id))
            {
                return "Menu item with the same ID already exists.";
            }

            var menuItem = _mapper.Map<MenuItem>(menuItemDto);
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return "Menu item created successfully.";
        }

        public async Task UpdateMenuItem(MenuItemDto menuItemDto)
        {
            var menuItem = await _context.MenuItems.FindAsync(menuItemDto.Id);
            if (menuItem == null)
            {
                throw new Exception("Menu item not found.");
            }

            _mapper.Map(menuItemDto, menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItem(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
            {
                throw new Exception("Menu item not found.");
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MenuItemExists(int id)
        {
            return await _context.MenuItems.AnyAsync(item => item.Id == id);
        }
    }
}
