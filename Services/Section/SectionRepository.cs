using AutoMapper;
using Menu.Data;
using Menu.Dtos.FoodSection;
using Menu.Dtos.Section;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Menu.Services.Section
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public SectionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SectionReadDto>> GetSections()
        {
            var sections = await _context.Sections.ToListAsync();
            return _mapper.Map<List<SectionReadDto>>(sections);
        }

        public async Task<SectionReadDto> GetSectionById(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return null;
            }

            return _mapper.Map<SectionReadDto>(section);
        }

        public async Task<(bool success, string message)> AddSection(Models.Section sectionCreateDto)
        {
            var section = _mapper.Map<Models.Section>(sectionCreateDto);

            bool sectionExists = await _context.Sections.AnyAsync(s => s.Name == sectionCreateDto.Name);
            if (sectionExists)
            {
                return (false, "Section name already exists.");
            }

            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return (true, "Section added successfully.");
        }

        public async Task<(bool success, string message)> UpdateSection(SectionUpdateDto sectionUpdateDto)
        {
            var section = await _context.Sections.FindAsync(sectionUpdateDto.Id);
            if (section == null)
            {
                return (false, "Section not found.");
            }

            bool sectionExists = await _context.Sections.AnyAsync(s => s.Name == sectionUpdateDto.Name && s.Id != sectionUpdateDto.Id);
            if (sectionExists)
            {
                return (false, "Section name already exists.");
            }

            _mapper.Map(sectionUpdateDto, section);

            await _context.SaveChangesAsync();
            return (true, "Section updated successfully.");
        }

        public async Task<(bool success, string message)> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return (false, "Section not found.");
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return (true, "Section deleted successfully.");
        }
    }
}
