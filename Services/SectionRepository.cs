using AutoMapper;
using Menu.Data;
using Menu.Dtos.Section;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services
{
    public interface ISectionRepository
    {
        Task<List<SectionReadDto>> GetSectionsAsync();
        Task<SectionReadDto> GetSectionAsync(Guid id);
        Task<List<SectionReadDto>> GetSectionsByCategoryIdAsync(Guid categoryId);
        Task<SectionReadDto> CreateSectionAsync(Section sectionDto);
        Task<SectionReadDto> UpdateSectionAsync(Guid id, SectionReadDto sectionDto);
        Task<bool> DeleteSectionAsync(Guid id);
        Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId);
        Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId);
    }
    namespace Menu.Data.Repositories
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

        public async Task<SectionReadDto> GetSectionAsync(Guid id)
        {
            var section = await _context.Sections
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);

            return _mapper.Map<SectionReadDto>(section);
        }

        public async Task<List<SectionReadDto>> GetSectionsAsync()
        {
            var sections = await _context.Sections
                .Include(s => s.Items)
                .ToListAsync();
            
            return _mapper.Map<List<SectionReadDto>>(sections);
        }


        public async Task<List<SectionReadDto>> GetSectionsByCategoryIdAsync(Guid categoryId)
        {
            var sections = await _context.Sections
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();

            return _mapper.Map<List<SectionReadDto>>(sections);
        }

        public async Task<SectionReadDto> CreateSectionAsync(Section sectionDto)
        {
            var section = _mapper.Map<Section>(sectionDto);
            _context.Sections.Add(section);
            await _context.SaveChangesAsync();
            return _mapper.Map<SectionReadDto>(section);
        }

        public async Task<SectionReadDto> UpdateSectionAsync(Guid id, SectionReadDto sectionDto)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                throw new ArgumentException($"Section with ID {id} not found.");
            }

            section.Name = sectionDto.Name;

            await _context.SaveChangesAsync();
            return _mapper.Map<SectionReadDto>(section);
        }

        public async Task<bool> DeleteSectionAsync(Guid id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return false;
            }

            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSectionsByCategoryIdAsync(Guid categoryId)
        {
            var sections = await _context.Sections.Where(s => s.CategoryId == categoryId).ToListAsync();
            if (sections == null || sections.Count == 0)
            {
                return false;
            }

            _context.Sections.RemoveRange(sections);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteItemsBySectionIdAsync(Guid sectionId)
        {
            var items = await _context.Items.Where(i => i.SectionId == sectionId).ToListAsync();
            if (items == null || items.Count == 0)
            {
                return false;
            }

            _context.Items.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

}