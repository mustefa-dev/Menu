using Microsoft.AspNetCore.Http;

namespace Menu.Dtos.Food
{
    public class FoodUpdateDto
    {
        public string Name { get; set; }
        public string SectionName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}