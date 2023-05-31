using Auth.Dtos.Item;
using Item.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(IItemRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([FromForm] ItemCreateDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdItem = await _repository.AddItem(itemDto);

            return CreatedAtRoute(nameof(GetItem), new { id = createdItem.Id }, createdItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _repository.GetItems();
            return Ok(items);
        }

        [HttpGet("{id}", Name = nameof(GetItem))]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _repository.GetItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] ItemUpdateDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repository.UpdateItem(id, itemDto);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var result = await _repository.DeleteItem(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
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
