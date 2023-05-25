using Auth.Data;
using Auth.Dtos.Item;
using AutoMapper;
using Item.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Menu.Services.Menu
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemRepository(DataContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ItemReadDto> AddItem(ItemCreateDto itemDto)
        {
            var item = _mapper.Map<global::Menu.Models.Item>(itemDto);

            if (itemDto.Photo != null)
            {
                item.Photo = await SavePhoto(itemDto.Photo);
            }

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return _mapper.Map<ItemReadDto>(item);
        }
    


        public async Task<IEnumerable<ItemReadDto>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return _mapper.Map<IEnumerable<ItemReadDto>>(items);
        }

        public async Task<ItemReadDto> GetItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<ItemReadDto>(item);
        }

        public async Task<bool> UpdateItem(int id, ItemUpdateDto itemDto)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return false;

            _mapper.Map(itemDto, item);

            if (itemDto.Photo != null)
            {
                item.Photo = await SavePhoto(itemDto.Photo);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                return false;

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<string> SavePhoto(IFormFile photo)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = $"{Guid.NewGuid()}_{photo.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return Path.Combine("uploads", uniqueFileName);
        }
    }
}
