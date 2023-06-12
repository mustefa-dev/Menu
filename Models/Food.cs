namespace Menu.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SectionName { get; set; }
        public FoodSection Section { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}
