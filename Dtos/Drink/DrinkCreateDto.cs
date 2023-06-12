namespace Menu.Dtos.Drink{
    public class DrinkCreateDto{
        public string Name { get; set; }
        public string SectionName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}