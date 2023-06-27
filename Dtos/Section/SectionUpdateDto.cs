using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionUpdateDto{

    public int Id { get; set; } 

    [Required]
    public string Name { get; set; }
    public IFormFile Photo { get; set; }

}