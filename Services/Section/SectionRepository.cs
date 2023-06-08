using Menu.Data;
using Microsoft.EntityFrameworkCore;

namespace Menu.Services.Section
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DataContext _context;

        public SectionRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Section>> GetSections()
        {
            return await _context.Sections.ToListAsync();
        }

        public async Task<Models.Section> GetSectionById(int id)
        {
            return await _context.Sections.FindAsync(id);
        }

        public async Task<(bool success, string message)> AddSection(Models.Section section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return (true, "Section added successfully.");
        }

        public async Task<(bool success, string message)> UpdateSection(Models.Section section)
        {
            var existingSection = await _context.Sections.FindAsync(section.Id);
            if (existingSection == null)
            {
                return (false, "Section not found.");
            }

            existingSection.Name = section.Name;
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