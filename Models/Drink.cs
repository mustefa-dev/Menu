namespace Menu.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
    }
}