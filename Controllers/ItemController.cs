using Menu.Dtos;
using Menu.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Dtos.Menu.Dtos;
using Menu.Services.Menu;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItems()
        {
            var items = await _itemService.GetAllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            var item = await _itemService.GetItemById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateItem([FromForm] ItemDto itemDto)
        {
            var photo = itemDto.Photo; // Access the uploaded photo

            var result = await _itemService.CreateItem(itemDto, photo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] ItemDto itemDto)
        {
            if (id != itemDto.Id)
                return BadRequest();

            if (!await _itemService.ItemExists(id))
                return NotFound();

            var photo = itemDto.Photo; // Access the uploaded photo

            await _itemService.UpdateItem(itemDto, photo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (!await _itemService.ItemExists(id))
                return NotFound();

            await _itemService.DeleteItem(id);
            return NoContent();
        }
    }
}
