using AutoMapper;
using Menu.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Menu.Data.Repositories
{
    public interface IItemRepository
    {
        Task<List<ItemReadDto>> GetItemsAsync();
        Task<ItemReadDto> GetItemAsync(Guid id);
        Task<List<ItemReadDto>> GetItemsBySectionIdAsync(Guid sectionId);
        Task<ItemReadDto> CreateItemAsync(Item itemDto);
        Task<ItemReadDto> UpdateItemAsync(Guid id, ItemReadDto itemDto);
        Task<bool> DeleteItemsByCategoryIdAsync(Guid categoryId);
        Task<bool> DeleteItemAsync(Guid id);
    }
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ItemReadDto>> GetItemsAsync()
        {
            var items = await _context.Items.ToListAsync();
            return _mapper.Map<List<ItemReadDto>>(items);
        }

        public async Task<ItemReadDto> GetItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            return _mapper.Map<ItemReadDto>(item);
        }

        public async Task<List<ItemReadDto>> GetItemsBySectionIdAsync(Guid sectionId)
        {
            var items = await _context.Items.Where(i => i.SectionId == sectionId).ToListAsync();
            return _mapper.Map<List<ItemReadDto>>(items);
        }

        public async Task<ItemReadDto> CreateItemAsync(Item itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return _mapper.Map<ItemReadDto>(item);
        }

        public async Task<ItemReadDto> UpdateItemAsync(Guid id, ItemReadDto itemDto)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new ArgumentException($"Item with ID {id} not found.");
            }

            item.Name = itemDto.Name;
            item.Price = itemDto.Price;

            await _context.SaveChangesAsync();
            return _mapper.Map<ItemReadDto>(item);
        }

        public async Task<bool> DeleteItemsByCategoryIdAsync(Guid categoryId)
        {
            var sections = await _context.Sections.Where(s => s.CategoryId == categoryId).ToListAsync();
            var sectionIds = sections.Select(s => s.Id).ToList();

            var items = await _context.Items.Where(i => sectionIds.Contains(i.SectionId)).ToListAsync();
            _context.Items.RemoveRange(items);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}