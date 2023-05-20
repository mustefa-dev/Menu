// ItemController.cs
using Menu.Dtos;
using Menu.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Menu.Services.Menu;
using Microsoft.AspNetCore.Authorization;

namespace Menu.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<string>> CreateItem(ItemDto itemDto)
        {
            var result = await _itemService.CreateItem(itemDto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemDto itemDto)
        {
            if (id != itemDto.Id)
                return BadRequest();

            if (!await _itemService.ItemExists(id))
                return NotFound();

            await _itemService.UpdateItem(itemDto);
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