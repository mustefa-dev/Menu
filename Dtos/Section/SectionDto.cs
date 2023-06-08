using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Section;

public class SectionDto
{
    public int Id { get; set; }
    [Required] // Add this attribute to enforce the Name property to be provided
    public string Name { get; set; }
    public int SectionId { get; set; } // Add the SectionId property

}
