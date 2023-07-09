using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionCreateDto
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
}
