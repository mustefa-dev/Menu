using Auth.Data;
using AutoMapper;
using Menu.Dtos;
using Menu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Menu.Dtos.Menu.Dtos;

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

        public async Task<string> CreateItem(ItemDto itemDto, IFormFile photo)
        {
            if (await ItemExists(itemDto.Id))
            {
                return "Item with the same ID already exists.";
            }

            var item = _mapper.Map<Item>(itemDto);

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

            // Set the file path
            var filePath = Path.Combine("/home/mu/Pictures/", fileName);

            // Get the absolute path to the file
            var absoluteFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath);

            // Save the file to the specified path
            using (var stream = new FileStream(absoluteFilePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Set the PhotoPath property of the item
            item.PhotoPath = filePath;

            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            return "Item created successfully.";
        }

        public async Task UpdateItem(ItemDto itemDto, IFormFile photo)
        {
            var item = await _context.Items.FindAsync(itemDto.Id);
            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            _mapper.Map(itemDto, item);

            if (photo != null && photo.Length > 0)
            {
                // Process and save the new photo
                var photoPath = await SavePhoto(photo);
                item.PhotoPath = photoPath;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Item not found.");
            }

            // Delete the associated photo, if exists
            DeletePhoto(item.PhotoPath);

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ItemExists(int id)
        {
            return await _context.Items.AnyAsync(item => item.Id == id);
        }

        private async Task<string> SavePhoto(IFormFile photo)
        {
            // Generate a unique filename for the photo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            var filePath = Path.Combine("photo-uploads", fileName);

            // Save the photo to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            return filePath;
        }

        private void DeletePhoto(string photoPath)
        {
            if (!string.IsNullOrEmpty(photoPath))
            {
                // Delete the photo file from the file system
                File.Delete(photoPath);
            }
        }
    }
}
