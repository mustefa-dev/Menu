using Menu.Dtos.Drink;

namespace Menu.Services.Section;

public interface ISectionRepository
{
    Task<List<Models.Section>> GetSections();
    Task<Models.Section> GetSectionById(int id);
    Task<(bool success, string message)> AddSection(Models.Section section);
    Task<(bool success, string message)> UpdateSection(Models.Section section);
    Task<(bool success, string message)> DeleteSection(int id);

}

