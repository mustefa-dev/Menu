using Menu.Dtos.Section;
using Menu.Models;

namespace Menu.Dtos.Category;
public class CategoryReadDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public List<SectionReadDto> Sections { get; set; }
}
public class CategoryCreateDto
{
    public string Name { get; set; }
}
public class CategoryUpdateDto
{
    public string Name { get; set; }
}
