using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionCreateDto{

    [Required]
    public string Name { get; set; }
    public IFormFile Photo { get; set; }

}