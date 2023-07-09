using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionReadDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public List<ItemReadDto> Items { get; set; }
}

