using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.FoodSection
{
    public class FoodSectionUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}