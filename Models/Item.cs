namespace Menu.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public Category Category { get; set; } // Add Category property
        public int CategoryId { get; set; } 
    }
}