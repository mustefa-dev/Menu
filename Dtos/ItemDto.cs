using System.ComponentModel.DataAnnotations;
using Menu.Dtos.Category;

namespace Menu.Dtos{
    public class ItemReadDto{
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class ItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid SectionId { get; set; }
    }
    public class ItemUpdateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}