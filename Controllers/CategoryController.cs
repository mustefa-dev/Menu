using AutoMapper;
using Menu.Data.Repositories;
using Menu.Dtos.Category;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return Ok(_mapper.Map<List<CategoryReadDto>>(categories));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryReadDto>(category));
        }

        [HttpPost]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory(CategoryCreateDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var createdCategory = await _categoryRepository.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, _mapper.Map<CategoryReadDto>(createdCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryDto)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            _mapper.Map(categoryDto, category);
            await _categoryRepository.UpdateCategoryAsync(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryRepository.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpDelete("{categoryId}/sections")]
        public async Task<IActionResult> DeleteSectionsByCategoryId(Guid categoryId)
        {
            var result = await _categoryRepository.DeleteSectionsByCategoryIdAsync(categoryId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("sections/{sectionId}/items")]
        public async Task<IActionResult> DeleteItemsBySectionId(Guid sectionId)
        {
            var result = await _categoryRepository.DeleteItemsBySectionIdAsync(sectionId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
