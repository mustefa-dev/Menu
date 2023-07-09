using AutoMapper;
using Menu.Data.Repositories;
using Menu.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemReadDto>>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            return Ok(_mapper.Map<List<ItemReadDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemReadDto>> GetItem(Guid id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        [HttpGet("sections/{sectionId}")]
        public async Task<ActionResult<List<ItemReadDto>>> GetItemsBySectionId(Guid sectionId)
        {
            var items = await _itemRepository.GetItemsBySectionIdAsync(sectionId);
            return Ok(_mapper.Map<List<ItemReadDto>>(items));
        }

        [HttpPost]
        public async Task<ActionResult<ItemReadDto>> CreateItem(ItemCreateDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            var createdItem = await _itemRepository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItem), new { id = createdItem.Id }, _mapper.Map<ItemReadDto>(createdItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, ItemUpdateDto itemDto)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            _mapper.Map(itemDto, item);
            await _itemRepository.UpdateItemAsync(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var item = await _itemRepository.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await _itemRepository.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteItemsByCategoryId(Guid categoryId)
        {
            var result = await _itemRepository.DeleteItemsByCategoryIdAsync(categoryId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
