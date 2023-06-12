namespace Menu.Dtos.Food
{
    public class FoodReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}