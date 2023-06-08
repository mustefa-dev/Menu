using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionCreateDto{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}