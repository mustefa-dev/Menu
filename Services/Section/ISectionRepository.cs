using Menu.Dtos.Drink;
using Menu.Dtos.Section;

namespace Menu.Services.Section
{
    public interface ISectionRepository
    {
        Task<List<SectionReadDto>> GetSections();
        Task<SectionReadDto> GetSectionByName(string name);
        Task<(bool success, string message)> AddSection(Models.Section sectionCreateDto);
        Task<(bool success, string message)> AddSection(SectionCreateDto sectionCreateDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> UpdateSection(SectionUpdateDto sectionUpdateDto, IWebHostEnvironment webHostEnvironment);
        Task<(bool success, string message)> DeleteSection(string name);
    }
}

