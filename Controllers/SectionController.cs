using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Menu.Data;
using Menu.Dtos.Section;
using Menu.Models;
using Menu.Services.Section;
using Microsoft.AspNetCore.Mvc;

namespace Menu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<SectionDto>>> GetSections()
        {
            var sections = await _sectionRepository.GetSections();
            var sectionDtos = _mapper.Map<List<SectionDto>>(sections);
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectionDto>> GetSection(int id)
        {
            var section = await _sectionRepository.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }

            var sectionDto = _mapper.Map<SectionDto>(section);
            return Ok(sectionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(Section sectionCreateDto)
        {
            var section = _mapper.Map<Section>(sectionCreateDto);
            await _sectionRepository.AddSection(section);
            return Ok(section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection(int id, Section sectionUpdateDto)
        {
            var section = await _sectionRepository.GetSectionById(id);
            if (section == null)
            {
                return NotFound(section);
            }

            _mapper.Map(sectionUpdateDto, section);
            await _sectionRepository.UpdateSection(section);
            return Ok(section);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _sectionRepository.GetSectionById(id);
            if (section == null)
            {
                return NotFound(section);
            }
        
            await _sectionRepository.DeleteSection(id);
            return Ok(section);
        }
    }
}
