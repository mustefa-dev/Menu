using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos.Item
{
    public class ItemCreateDTO
    {
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }
    }
}