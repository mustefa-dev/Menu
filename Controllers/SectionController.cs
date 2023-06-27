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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<List<SectionReadDto>>> GetSections()
        {
            var sections = await _sectionRepository.GetSections();
            var sectionDtos = _mapper.Map<List<SectionReadDto>>(sections);
            return Ok(sectionDtos);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SectionReadDto>> GetSection(string name)
        {
            var section = await _sectionRepository.GetSectionByName(name);
            if (section == null)
            {
                return NotFound();
            }

            var sectionDto = _mapper.Map<SectionReadDto>(section);
            return Ok(sectionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddSection([FromForm] SectionCreateDto sectionDto)
        {
            var (success, message) = await _sectionRepository.AddSection(sectionDto, _webHostEnvironment);
            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSection([FromForm] SectionUpdateDto sectionUpdateDto)
        {
            var (success, message) = await _sectionRepository.UpdateSection(sectionUpdateDto, _webHostEnvironment);
            if (!success)
            {
                return NotFound(message);
            }

            return Ok(message);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteSection(string name)
        {
            var section = await _sectionRepository.GetSectionByName(name);
            if (section == null)
            {
                return NotFound(section);
            }

            await _sectionRepository.DeleteSection(name);
            return Ok(section);
        }
    }
}
