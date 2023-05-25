using System.ComponentModel.DataAnnotations;

namespace Menu.Dtos
{
    public class ItemReadDTO
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}