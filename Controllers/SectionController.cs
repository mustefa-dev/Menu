using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Data.Repositories;
using Menu.Dtos.Section;
using Menu.Services;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/sections")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectionReadDto>>> GetSections()
        {
            var sections = await _sectionRepository.GetSectionsAsync();
            return Ok(_mapper.Map<List<SectionReadDto>>(sections));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectionReadDto>> GetSection(Guid id)
        {
            var section = await _sectionRepository.GetSectionAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SectionReadDto>(section));
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<ActionResult<List<SectionReadDto>>> GetSectionsByCategoryId(Guid categoryId)
        {
            var sections = await _sectionRepository.GetSectionsByCategoryIdAsync(categoryId);
            return Ok(_mapper.Map<List<SectionReadDto>>(sections));
        }

        [HttpPost]
        public async Task<ActionResult<SectionReadDto>> CreateSection(SectionCreateDto sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            var createdSection = await _sectionRepository.CreateSectionAsync(section);
            return CreatedAtAction(nameof(GetSection), new { id = createdSection.Id }, _mapper.Map<SectionReadDto>(createdSection));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(Guid id, SectionUpdateDto sectionDto)
        {
            var section = await _sectionRepository.GetSectionAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            _mapper.Map(sectionDto, section);
            await _sectionRepository.UpdateSectionAsync(id, section);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(Guid id)
        {
            var section = await _sectionRepository.GetSectionAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            await _sectionRepository.DeleteSectionAsync(id);
            return NoContent();
        }

        [HttpDelete("categories/{categoryId}")]
        public async Task<IActionResult> DeleteSectionsByCategoryId(Guid categoryId)
        {
            var result = await _sectionRepository.DeleteSectionsByCategoryIdAsync(categoryId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("items/{sectionId}")]
        public async Task<IActionResult> DeleteItemsBySectionId(Guid sectionId)
        {
            var result = await _sectionRepository.DeleteItemsBySectionIdAsync(sectionId);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
