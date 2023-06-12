using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.FoodSection
{
    public class FoodSectionCreateDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}