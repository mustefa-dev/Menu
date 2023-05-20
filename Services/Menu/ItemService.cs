using Auth.Data;
using AutoMapper;
using Menu.Dtos;
using Menu.Models;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Menu
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ItemService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ItemDto>> GetAllItems()
        {
            var items = await _context.Items.ToListAsync();
            return _mapper.Map<IEnumerable<ItemDto>>(items);
        }

        public async Task<ItemDto> GetItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);
            return _mapper.Map<ItemDto>(item);
        }

        public async Task<string> CreateItem(ItemDto itemDto)
        {
            if (await ItemExists(itemDto.Id))
            {
                return "Item with the same ID already exists.";
            }

            var item = _mapper.Map<Item>(itemDto);
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return "Item created successfully.";
        }

        public async Task UpdateItem(ItemDto itemDto)
        {
            var item = await _context.Items.FindAsync(itemDto.Id);
            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            _mapper.Map(itemDto, item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ItemExists(int id)
        {
            return await _context.Items.AnyAsync(item => item.Id == id);
        }
    }

}
