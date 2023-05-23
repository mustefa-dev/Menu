namespace Menu.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
        public string? PhotoPath { get; set; } // Add the 'PhotoPath' property
    }

}