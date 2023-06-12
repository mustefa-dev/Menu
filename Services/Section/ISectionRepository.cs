using Menu.Dtos.Drink;
using Menu.Dtos.Section;

namespace Menu.Services.Section;

public interface ISectionRepository
{
    Task<List<SectionReadDto>> GetSections();
    Task<SectionReadDto> GetSectionById(int id);
    Task<(bool success, string message)> AddSection(Models.Section sectionCreateDto);
    Task<(bool success, string message)> UpdateSection(SectionUpdateDto sectionUpdateDto);
    Task<(bool success, string message)> DeleteSection(int id);

}

