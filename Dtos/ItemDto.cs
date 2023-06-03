using System.ComponentModel.DataAnnotations;
using Menu.Dtos.Category;

namespace Menu.Dtos
{
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile Photo { get; set; }
        public int CategoryId { get; set; } 
        
    }

    public class ItemReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public CategoryReadDto Category { get; set; }
    }
    public class ItemUpdateDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IFormFile Photo { get; set; }

        public int CategoryId { get; set; } // New property for category ID
    }
}
