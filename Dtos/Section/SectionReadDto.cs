using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionReadDto
{
    [Required] 
    public string Name { get; set; }
    public string Photo { get; set; }

}
